﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Etg.Data.Entry;
using System.ServiceProcess;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Etg.Service
{
    public class Program : ServiceBase
    {
        private Grpc.Core.Server server;
        private int[] securePorts;
        private int[] insecurePorts;
        private readonly IServiceProvider serviceProvider;

        public Program(int[] securePorts, int[] insecurePorts, IServiceProvider provider)
        {
            this.securePorts = securePorts;
            this.insecurePorts = insecurePorts;
            this.serviceProvider = provider;
        }

        public static void Main(string[] args)
        {
            CommandLineApplication app = new CommandLineApplication();
            var runAsServiceOption = app.Option("-s|--service", "Run as Windows Service", CommandOptionType.NoValue);
            var portOption = app.Option("-p|--port|--secure-port", "Port to listen to", CommandOptionType.MultipleValue);
            var insecurePortOption = app.Option("-i|--insecure-port", "Insecure channel port to listen to", CommandOptionType.MultipleValue);
            app.HelpOption("-?|-h|--help");
            app.OnExecute(() =>
            {
                List<int> securePorts = new List<int>();
                List<int> insecurePorts = new List<int>();
                if(portOption.HasValue())
                {
                    int port;
                    foreach(var v in portOption.Values)
                    {
                        if(Int32.TryParse(v,out port))
                        {
                            securePorts.Add(port);
                        }
                    }
                }
                if (insecurePortOption.HasValue())
                {
                    int port;
                    foreach (var v in insecurePortOption.Values)
                    {
                        if (Int32.TryParse(v, out port))
                        {
                            insecurePorts.Add(port);
                        }
                    }
                }
                if(securePorts.Count == 0 && insecurePorts.Count == 0)
                {
                    securePorts.Add(8443);
                }

                ServiceCollection services = new ServiceCollection();
                ConfigureService(services);



                var program = new Program(securePorts.ToArray(), insecurePorts.ToArray(), services.BuildServiceProvider());
                if (runAsServiceOption.HasValue())
                {
                    ServiceBase.Run(program);
                }
                else
                {
                    program.StartServer();
                    Console.ReadKey(true);
                    program.StopServer();
                }
                return 0;
            });


            app.Execute(args);
        }


        private static void ConfigureService(ServiceCollection services)
        {
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug()
                .AddEventLog();

            services.AddLogging();
            services.AddSingleton<EntryDataServiceImpl>();
        } 


        private void StartServer()
        {
            var serverCredential = new SslServerCredentials(new List<KeyCertificatePair>(){new KeyCertificatePair(
                @"-----BEGIN CERTIFICATE-----
MIIDlDCCAnwCAQMwDQYJKoZIhvcNAQEFBQAwgckxCzAJBgNVBAYTAkNOMRIwEAYD
VQQIEwlHdWFuZ2RvbmcxEjAQBgNVBAcTCUd1YW5nemhvdTEmMCQGA1UEChMdR3Vh
bmd6aG91IEN1c3RvbXMgRGF0YSBDZW50ZXIxIjAgBgNVBAsTGUlUIERldmVsb3Bt
ZW50IERlcGFydG1lbnQxHzAdBgNVBAMTFmVwb3J0Lmd6Y3VzdG9tcy5nb3YuY24x
JTAjBgkqhkiG9w0BCQEWFmFkbWluQGd6Y3VzdG9tcy5nb3YuY24wHhcNMTYxMTA5
MDkyNjA0WhcNMTkxMTA5MDkyNjA0WjBWMQswCQYDVQQGEwJDTjESMBAGA1UECAwJ
R3Vhbmdkb25nMRIwEAYDVQQHDAlHdWFuZ3pob3UxCzAJBgNVBAsMAklUMRIwEAYD
VQQDDAlsb2NhbGhvc3QwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDK
6PvMXHH5fTuoYOa2QYqEfIAuy9hR5VLHuBzieZqhL6zVFu0vL9xhTIjAVgwkhKlm
BRIG88O589+0BBJXhZGRgGkjeuiZgBxw9Q/jOq2RXxXPfTOWF20NnOCE4P6l68dD
piX9TReZECUNXacXC6aacrsnu66E+5mylJUCqdqiY+Y+uCv5Uji9HBBqnCXWm0sV
NIO8rdWSSFX27Plb8hgohBjZ33ty9M64PR0bXBgiRfsuEKvgv9zGW/b9hu+iY3hT
APcea+XZ+4vXeabKVT9KhJJgftCeJbfGE4sgB/B9FNgbtDPsEwv3YIotg6Dm5dgY
selKcj9XU8pljt+R9RfdAgMBAAEwDQYJKoZIhvcNAQEFBQADggEBAKj1cwrFMiUJ
bgXRczNc5UpNsoHs5nDU91DYZM6vSVFQeP74kOAOnXwLjonQzGXWtlaF+zLVjvll
PinA6abiYU8iHVfrS1NjS5nxYSIibkBgGtQAvmU5TXb4c1EvNAcdq2+v7PQ/acw8
ppb5S1ZBLewwULFS1pRPu/DNnUQovdLNEB+KKp1plUTpGMeYj5vD0xFa4pD5O5/K
CGFIZNVqzGvac4NEVL5F541gV0F2OlFgs6+xklKSM/eUEmxkjKDWzdk3UHybd8up
dTFwYANrcu66JrRCuHEBkwP3v0C+a2UIazYMfb1sJVzltTGRcxq/nfeQPMUmfzPi
avJfH0jzhbU=
-----END CERTIFICATE-----", @"-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEAyuj7zFxx+X07qGDmtkGKhHyALsvYUeVSx7gc4nmaoS+s1Rbt
Ly/cYUyIwFYMJISpZgUSBvPDufPftAQSV4WRkYBpI3romYAccPUP4zqtkV8Vz30z
lhdtDZzghOD+pevHQ6Yl/U0XmRAlDV2nFwummnK7J7uuhPuZspSVAqnaomPmPrgr
+VI4vRwQapwl1ptLFTSDvK3VkkhV9uz5W/IYKIQY2d97cvTOuD0dG1wYIkX7LhCr
4L/cxlv2/YbvomN4UwD3Hmvl2fuL13mmylU/SoSSYH7QniW3xhOLIAfwfRTYG7Qz
7BML92CKLYOg5uXYGLHpSnI/V1PKZY7fkfUX3QIDAQABAoIBAFxBQ8KtwXBCvS4Y
KK1y7SzBgnJEYi0SC+ocTp211lU03OrhiqNqqlNevcpdFRZBbtegtIqOqE3SkMJD
G6fJZd72uFbWWgz4j3XYJgoVMrcmuT7mWN8D9aQ70GT5+y2rHqUmVJ1vQKxqB76k
9wRmWrBcO7WcAoQZ9M6Z+YoFeg9cdxSn1pUE3O19BSBJ+FaaUpRN+h22hIGpdE5d
dRyfGRZNFc+iauc2lPOSrO16Kz0vGg5xAn8qnUs+1JPaPk6mLNzvhsEJJ74PbyTK
YCSMRpFWn/+gXmdK4FolvRrh/Uw6uh6WzsM9OXt876ni+1Z3ERGVtkW2ytZ6pBRL
XNaSQI0CgYEA6QvWm7PxYf90605tyGcyJn9gEWFXV6/5e2dHFoYd5DwaADYY8Jyt
kxDpq6mVkm2WBFRra6D/goPRLW55iuqXGsMrxLbEXJZqhhUoEyf+gSAJMO4y2Ln0
Lu2MYlw85kzpcUeIsifHosHXRqenoI4hBRwLwgToKTLz1FBULUTozysCgYEA3uVG
arFyUHhn93PN/2e9z24JujW8dBIuFg8AWaMuLwx3vRDRByQ93e2OSlflqzaPyYIh
2oQUEFt5OCRvCLrjGBccWHOhuL9D046i8nNDONaX28UHiXoLIjzZimj9NC5vEKmH
9xvQ4/MGqd2xaVkMg/CWp9bdERaFcxtGHQiJ8RcCgYEArMMh7XuQTl3ahzY1HIOk
IfX7eeb3oQHLqTf+8yuprTEA9XclNfpwkr3O/HtTbqHevIb4u2k3AcJGp69mWx1d
t3FIWSREnX7EqXG1q73SZlcheSycdR4lb0Sa9a/7VZ9ez6OAKtJipL2eobpYAiZb
RDZuYP7SPPiQ2axTOtwC2tECgYAlBGbBaV7WxmhdzDm15QC85kVvS2VU0YAd4bfp
KxSMc8GfAJ/2U6qCpOUwq5BU8ubGTHpa0/yRCuAC1uopxP/aDFyExA9jo0Acbl/Z
bBMJ6Xmm4f3ycvZOZVSri+whMmT3m3AdNd1nPgEpTMwd9tABSX97uE9WeysGhs0K
HVTrWQKBgCanFywp6OfplulgNQmqx+rfaGwhurkmvn+T6MAmye1U09wCTAU0ltVF
D6De2rVAzFJMMtG1U4SLBXjZ9qaVr2qYxLnelHD0FsGSKYB4wDIiaeNzMOoV68oI
2So3h2Gjz5WTMSlnLhUwsD8zq8qkzlCCsKEQ/PXNMGONPq8mRrdO
-----END RSA PRIVATE KEY-----")}, @"-----BEGIN CERTIFICATE-----
MIIFSzCCBDOgAwIBAgIJAK1eE69OSQpDMA0GCSqGSIb3DQEBBQUAMIHJMQswCQYD
VQQGEwJDTjESMBAGA1UECBMJR3Vhbmdkb25nMRIwEAYDVQQHEwlHdWFuZ3pob3Ux
JjAkBgNVBAoTHUd1YW5nemhvdSBDdXN0b21zIERhdGEgQ2VudGVyMSIwIAYDVQQL
ExlJVCBEZXZlbG9wbWVudCBEZXBhcnRtZW50MR8wHQYDVQQDExZlcG9ydC5nemN1
c3RvbXMuZ292LmNuMSUwIwYJKoZIhvcNAQkBFhZhZG1pbkBnemN1c3RvbXMuZ292
LmNuMB4XDTE2MTEwOTA4NDAzMVoXDTE5MTEwOTA4NDAzMlowgckxCzAJBgNVBAYT
AkNOMRIwEAYDVQQIEwlHdWFuZ2RvbmcxEjAQBgNVBAcTCUd1YW5nemhvdTEmMCQG
A1UEChMdR3Vhbmd6aG91IEN1c3RvbXMgRGF0YSBDZW50ZXIxIjAgBgNVBAsTGUlU
IERldmVsb3BtZW50IERlcGFydG1lbnQxHzAdBgNVBAMTFmVwb3J0Lmd6Y3VzdG9t
cy5nb3YuY24xJTAjBgkqhkiG9w0BCQEWFmFkbWluQGd6Y3VzdG9tcy5nb3YuY24w
ggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDTIJTVIP7FkoiGBgmftHPO
MGZb1wdY5WVFwK8gwtmwPweSwMOoLCddey5eIMk7L2KZFciUxLR9oD1vIDetZcKl
2OWccOenb/e7A06qFTQb0yfXZQfAXGVb/k+ansHsOVHT64nGFM+Fkr1eECJ4qkpf
ilaBn3BRoOsv2igEs9eumAjWDXsibClXk635rLlBkz0N0GryoRt8rhCJZ6CFsHmW
PvZLKHgqvTwPxIu5LjKUsjqCW87voxCp2EMGI/XnQnZSsmDINit7Q0iANxLtWNt0
sHMxFRT0AalOXsbXdGkrS+9z+Hfo8jDfGrlqwNF+Gl5HKpm6vYx3pFdz0u2wiWqd
AgMBAAGjggEyMIIBLjAdBgNVHQ4EFgQUirkI0sbw8+YcockLVrXS+SEL7Pcwgf4G
A1UdIwSB9jCB84AUirkI0sbw8+YcockLVrXS+SEL7Pehgc+kgcwwgckxCzAJBgNV
BAYTAkNOMRIwEAYDVQQIEwlHdWFuZ2RvbmcxEjAQBgNVBAcTCUd1YW5nemhvdTEm
MCQGA1UEChMdR3Vhbmd6aG91IEN1c3RvbXMgRGF0YSBDZW50ZXIxIjAgBgNVBAsT
GUlUIERldmVsb3BtZW50IERlcGFydG1lbnQxHzAdBgNVBAMTFmVwb3J0Lmd6Y3Vz
dG9tcy5nb3YuY24xJTAjBgkqhkiG9w0BCQEWFmFkbWluQGd6Y3VzdG9tcy5nb3Yu
Y26CCQCtXhOvTkkKQzAMBgNVHRMEBTADAQH/MA0GCSqGSIb3DQEBBQUAA4IBAQCW
wA2LMsIyuK02FiisLFlckQ/yg9wkIVi37z83LR+m24LqHsB4j54HCkPaUCU/sWbq
fxcmPgJslEJKBiHT9JQS8AIgrP3/zov3RyA5As+dyEVBh6HTHXAjuCFdyEmhuNRG
yR0b9/MPsIncjD+DZrnkV5a2UvROZKGMS/bF6NgsV9eNrAOKZvOX55IvKytYFoTW
qZqCqEvNABMzEOGNqmbjnCAETWyTRRMyVzQTpC7KrinaWnYVzRPOOI+MCc4lNIbP
dO+ygg2k6HRG1KIzaI4/YKrc7+qAnYo0Ot0ezivfMx4Qzbbq7TXysWH1po5zRSGz
EQ7X3D8WAC54mSRvmVCh
-----END CERTIFICATE-----", true);
            this.server = new Server()
            {
                Services = { EntryDataService.BindService(serviceProvider.GetRequiredService<EntryDataServiceImpl>()) },
            };

            foreach(int port in this.securePorts)
            {
                this.server.Ports.Add(new ServerPort("0.0.0.0", port, serverCredential));
            }
            foreach(int port in this.insecurePorts)
            {
                this.server.Ports.Add(new ServerPort("0.0.0.0", port, ServerCredentials.Insecure));
            }

            server.Start();
            System.Console.WriteLine("EtgService server listening on secure port {0}, insecure port {1}",String.Join(",",securePorts), String.Join(",",insecurePorts));
            System.Console.WriteLine("Press any key to stop the server");
        }

        private void StopServer()
        {
            this.server.ShutdownAsync().Wait();
        }

        protected override void OnStart(string[] args)
        {

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            this.StopServer();
            base.OnStop();
        }


    }
}
