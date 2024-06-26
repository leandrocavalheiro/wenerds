﻿namespace WeNerds.Commons;

public static class WeConstants
{
    public const string EV_NAME_JWT_SECRETS = "WN_JWT_SECRETS";
    public const string EV_NAME_JWT_TOKEN_EXPIRATION = "WN_JWT_TOKEN_EXPIRATION_IN_HOURS";
    public const string EV_NAME_JWT_REFRESH_TOKEN_EXPIRATION = "WN_JWT_REFRESH_TOKEN_EXPIRATION_IN_HOURS";
    public const string EV_NAME_JWT_ISSUER= "WN_JWT_ISSUER";
    public const string EV_NAME_JWT_AUDIANCE = "WN_JWT_AUDIANCE";

    public const string EV_NAME_CRYPT_SALT = "WN_CRYPT_SALT";
    public const string EV_NAME_CRYPT_KEY = "WN_CRYPT_SALT";
    public const string EV_NAME_CRYPT_INTERATIONS = "WN_CRYPT_SALT";


    public const string DEFAULT_CRYPT_SALT = "b7d72229-5d7b-4889-893d-635ebadd05cc";
    public const string DEFAULT_CRYPT_KEY = "ce3b2eea-6677-4446-90cc-798c0443216d";
    public const int DEFAULT_CRYPT_INTERATIONS = 1000;
}
