// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: auth.proto

// This CPP symbol can be defined to use imports that match up to the framework
// imports needed when using CocoaPods.
#if !defined(GPB_USE_PROTOBUF_FRAMEWORK_IMPORTS)
 #define GPB_USE_PROTOBUF_FRAMEWORK_IMPORTS 0
#endif

#if GPB_USE_PROTOBUF_FRAMEWORK_IMPORTS
 #import <Protobuf/GPBProtocolBuffers_RuntimeSupport.h>
#else
 #import "GPBProtocolBuffers_RuntimeSupport.h"
#endif

#if GPB_USE_PROTOBUF_FRAMEWORK_IMPORTS
 #import <Protobuf/Timestamp.pbobjc.h>
#else
 #import "google/protobuf/Timestamp.pbobjc.h"
#endif

 #import "Auth.pbobjc.h"
// @@protoc_insertion_point(imports)

#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wdeprecated-declarations"

#pragma mark - ETGAuthRoot

@implementation ETGAuthRoot

// No extensions in the file and none of the imports (direct or indirect)
// defined extensions, so no need to generate +extensionRegistry.

@end

#pragma mark - ETGAuthRoot_FileDescriptor

static GPBFileDescriptor *ETGAuthRoot_FileDescriptor(void) {
  // This is called by +initialize so there is no need to worry
  // about thread safety of the singleton.
  static GPBFileDescriptor *descriptor = NULL;
  if (!descriptor) {
    GPB_DEBUG_CHECK_RUNTIME_VERSIONS();
    descriptor = [[GPBFileDescriptor alloc] initWithPackage:@"etg.auth"
                                                 objcPrefix:@"ETG"
                                                     syntax:GPBFileSyntaxProto3];
  }
  return descriptor;
}

#pragma mark - Enum ETGLoginType

GPBEnumDescriptor *ETGLoginType_EnumDescriptor(void) {
  static GPBEnumDescriptor *descriptor = NULL;
  if (!descriptor) {
    static const char *valueNames =
        "Email\000CompanyId\000AccessToken\000";
    static const int32_t values[] = {
        ETGLoginType_Email,
        ETGLoginType_CompanyId,
        ETGLoginType_AccessToken,
    };
    GPBEnumDescriptor *worker =
        [GPBEnumDescriptor allocDescriptorForName:GPBNSStringifySymbol(ETGLoginType)
                                       valueNames:valueNames
                                           values:values
                                            count:(uint32_t)(sizeof(values) / sizeof(int32_t))
                                     enumVerifier:ETGLoginType_IsValidValue];
    if (!OSAtomicCompareAndSwapPtrBarrier(nil, worker, (void * volatile *)&descriptor)) {
      [worker release];
    }
  }
  return descriptor;
}

BOOL ETGLoginType_IsValidValue(int32_t value__) {
  switch (value__) {
    case ETGLoginType_Email:
    case ETGLoginType_CompanyId:
    case ETGLoginType_AccessToken:
      return YES;
    default:
      return NO;
  }
}

#pragma mark - ETGLoginRequest

@implementation ETGLoginRequest

@dynamic loginType;
@dynamic email;
@dynamic hashedPasswd;
@dynamic companyId;
@dynamic token;
@dynamic accessToken;
@dynamic captchaId;
@dynamic captcha;

typedef struct ETGLoginRequest__storage_ {
  uint32_t _has_storage_[1];
  ETGLoginType loginType;
  NSString *email;
  NSString *hashedPasswd;
  NSString *companyId;
  NSString *token;
  NSString *accessToken;
  NSString *captchaId;
  NSString *captcha;
} ETGLoginRequest__storage_;

// This method is threadsafe because it is initially called
// in +initialize for each subclass.
+ (GPBDescriptor *)descriptor {
  static GPBDescriptor *descriptor = nil;
  if (!descriptor) {
    static GPBMessageFieldDescription fields[] = {
      {
        .name = "loginType",
        .dataTypeSpecific.enumDescFunc = ETGLoginType_EnumDescriptor,
        .number = ETGLoginRequest_FieldNumber_LoginType,
        .hasIndex = 0,
        .offset = (uint32_t)offsetof(ETGLoginRequest__storage_, loginType),
        .flags = (GPBFieldFlags)(GPBFieldOptional | GPBFieldHasEnumDescriptor),
        .dataType = GPBDataTypeEnum,
      },
      {
        .name = "email",
        .dataTypeSpecific.className = NULL,
        .number = ETGLoginRequest_FieldNumber_Email,
        .hasIndex = 1,
        .offset = (uint32_t)offsetof(ETGLoginRequest__storage_, email),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeString,
      },
      {
        .name = "hashedPasswd",
        .dataTypeSpecific.className = NULL,
        .number = ETGLoginRequest_FieldNumber_HashedPasswd,
        .hasIndex = 2,
        .offset = (uint32_t)offsetof(ETGLoginRequest__storage_, hashedPasswd),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeString,
      },
      {
        .name = "companyId",
        .dataTypeSpecific.className = NULL,
        .number = ETGLoginRequest_FieldNumber_CompanyId,
        .hasIndex = 3,
        .offset = (uint32_t)offsetof(ETGLoginRequest__storage_, companyId),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeString,
      },
      {
        .name = "token",
        .dataTypeSpecific.className = NULL,
        .number = ETGLoginRequest_FieldNumber_Token,
        .hasIndex = 4,
        .offset = (uint32_t)offsetof(ETGLoginRequest__storage_, token),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeString,
      },
      {
        .name = "accessToken",
        .dataTypeSpecific.className = NULL,
        .number = ETGLoginRequest_FieldNumber_AccessToken,
        .hasIndex = 5,
        .offset = (uint32_t)offsetof(ETGLoginRequest__storage_, accessToken),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeString,
      },
      {
        .name = "captchaId",
        .dataTypeSpecific.className = NULL,
        .number = ETGLoginRequest_FieldNumber_CaptchaId,
        .hasIndex = 6,
        .offset = (uint32_t)offsetof(ETGLoginRequest__storage_, captchaId),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeString,
      },
      {
        .name = "captcha",
        .dataTypeSpecific.className = NULL,
        .number = ETGLoginRequest_FieldNumber_Captcha,
        .hasIndex = 7,
        .offset = (uint32_t)offsetof(ETGLoginRequest__storage_, captcha),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeString,
      },
    };
    GPBDescriptor *localDescriptor =
        [GPBDescriptor allocDescriptorForClass:[ETGLoginRequest class]
                                     rootClass:[ETGAuthRoot class]
                                          file:ETGAuthRoot_FileDescriptor()
                                        fields:fields
                                    fieldCount:(uint32_t)(sizeof(fields) / sizeof(GPBMessageFieldDescription))
                                   storageSize:sizeof(ETGLoginRequest__storage_)
                                         flags:GPBDescriptorInitializationFlag_None];
    NSAssert(descriptor == nil, @"Startup recursed!");
    descriptor = localDescriptor;
  }
  return descriptor;
}

@end

int32_t ETGLoginRequest_LoginType_RawValue(ETGLoginRequest *message) {
  GPBDescriptor *descriptor = [ETGLoginRequest descriptor];
  GPBFieldDescriptor *field = [descriptor fieldWithNumber:ETGLoginRequest_FieldNumber_LoginType];
  return GPBGetMessageInt32Field(message, field);
}

void SetETGLoginRequest_LoginType_RawValue(ETGLoginRequest *message, int32_t value) {
  GPBDescriptor *descriptor = [ETGLoginRequest descriptor];
  GPBFieldDescriptor *field = [descriptor fieldWithNumber:ETGLoginRequest_FieldNumber_LoginType];
  GPBSetInt32IvarWithFieldInternal(message, field, value, descriptor.file.syntax);
}

#pragma mark - ETGLoginReply

@implementation ETGLoginReply

@dynamic isSuccessful;
@dynamic message;
@dynamic accessToken;
@dynamic hasTimestampExpireAt, timestampExpireAt;
@dynamic hasUser, user;
@dynamic hasCompany, company;

typedef struct ETGLoginReply__storage_ {
  uint32_t _has_storage_[1];
  NSString *message;
  NSString *accessToken;
  GPBTimestamp *timestampExpireAt;
  UserProfile *user;
  CompanyProfile *company;
} ETGLoginReply__storage_;

// This method is threadsafe because it is initially called
// in +initialize for each subclass.
+ (GPBDescriptor *)descriptor {
  static GPBDescriptor *descriptor = nil;
  if (!descriptor) {
    static GPBMessageFieldDescription fields[] = {
      {
        .name = "isSuccessful",
        .dataTypeSpecific.className = NULL,
        .number = ETGLoginReply_FieldNumber_IsSuccessful,
        .hasIndex = 0,
        .offset = 1,  // Stored in _has_storage_ to save space.
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeBool,
      },
      {
        .name = "message",
        .dataTypeSpecific.className = NULL,
        .number = ETGLoginReply_FieldNumber_Message,
        .hasIndex = 2,
        .offset = (uint32_t)offsetof(ETGLoginReply__storage_, message),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeString,
      },
      {
        .name = "accessToken",
        .dataTypeSpecific.className = NULL,
        .number = ETGLoginReply_FieldNumber_AccessToken,
        .hasIndex = 3,
        .offset = (uint32_t)offsetof(ETGLoginReply__storage_, accessToken),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeString,
      },
      {
        .name = "timestampExpireAt",
        .dataTypeSpecific.className = GPBStringifySymbol(GPBTimestamp),
        .number = ETGLoginReply_FieldNumber_TimestampExpireAt,
        .hasIndex = 4,
        .offset = (uint32_t)offsetof(ETGLoginReply__storage_, timestampExpireAt),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeMessage,
      },
      {
        .name = "user",
        .dataTypeSpecific.className = GPBStringifySymbol(UserProfile),
        .number = ETGLoginReply_FieldNumber_User,
        .hasIndex = 5,
        .offset = (uint32_t)offsetof(ETGLoginReply__storage_, user),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeMessage,
      },
      {
        .name = "company",
        .dataTypeSpecific.className = GPBStringifySymbol(CompanyProfile),
        .number = ETGLoginReply_FieldNumber_Company,
        .hasIndex = 6,
        .offset = (uint32_t)offsetof(ETGLoginReply__storage_, company),
        .flags = GPBFieldOptional,
        .dataType = GPBDataTypeMessage,
      },
    };
    GPBDescriptor *localDescriptor =
        [GPBDescriptor allocDescriptorForClass:[ETGLoginReply class]
                                     rootClass:[ETGAuthRoot class]
                                          file:ETGAuthRoot_FileDescriptor()
                                        fields:fields
                                    fieldCount:(uint32_t)(sizeof(fields) / sizeof(GPBMessageFieldDescription))
                                   storageSize:sizeof(ETGLoginReply__storage_)
                                         flags:GPBDescriptorInitializationFlag_None];
    NSAssert(descriptor == nil, @"Startup recursed!");
    descriptor = localDescriptor;
  }
  return descriptor;
}

@end


#pragma clang diagnostic pop

// @@protoc_insertion_point(global_scope)
