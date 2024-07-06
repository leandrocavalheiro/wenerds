namespace WeNerds.Commons;

public static class WeConstants
{
    public const string EV_NAME_JWT_SECRETS = "WN_JWT_SECRETS";
    public const string EV_NAME_JWT_TOKEN_EXPIRATION = "WN_JWT_TOKEN_EXPIRATION_IN_HOURS";
    public const string EV_NAME_JWT_REFRESH_TOKEN_EXPIRATION = "WN_JWT_REFRESH_TOKEN_EXPIRATION_IN_HOURS";
    public const string EV_NAME_JWT_ISSUER= "WN_JWT_ISSUER";
    public const string EV_NAME_JWT_AUDIANCE = "WN_JWT_AUDIANCE";

    public const string EV_NAME_CRYPT_SALT = "WN_CRYPT_SALT";
    public const string EV_NAME_CRYPT_KEY = "WN_CRYPT_KEY";
    public const string EV_NAME_CRYPT_INTERATIONS = "WN_CRYPT_INTERATIONS";

    public const string EV_NAME_EXCEPTION_HANDLER_SHOW_LOG_PRODUCTION = "WN_EXCEPTION_HANDLER_SHOW_LOG_PRODUCTION";
    
    public const string EV_NAME_BABEL_RESOURCES_PATH = "WN_BABEL_RESOURCES_PATH";
    public const string EV_NAME_BABEL_RESOURCE_NAME = "WN_BABEL_RESOURCE_NAME";
    public const string EV_NAME_BABEL_LIST_LANGUAGES = "WN_BABEL_LIST_LANGUAGES";
    public const string EV_NAME_BABEL_DEFAULT_LANGUAGE = "WN_BABEL_DEFAULT_LANGUAGE";
    public const string EV_NAME_BABEL_DEFAULT_CONTEXT = "WN_BABEL_DEFAULT_CONTEXT";

    public const string DEFAULT_BABEL_RESOURCES_PATH = "Resources";
    public const string DEFAULT_BABEL_RESOURCE_NAME = "AppMessages";
    public const string DEFAULT_BABEL_LIST_LANGUAGES = "pt-BR, en";
    public const string DEFAULT_BABEL_DEFAULT_LANGUAGE = "en";
    


    public const string DEFAULT_CRYPT_SALT = "b7d72229-5d7b-4889-893d-635ebadd05cc";
    public const string DEFAULT_CRYPT_KEY = "ce3b2eea-6677-4446-90cc-798c0443216d";
    public const int DEFAULT_CRYPT_INTERATIONS = 1000;
}
