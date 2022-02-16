namespace Client.Static;

public static class APIEndpoints
{
    #if DEBUG
        internal const string ServerBaseUrl = "https://localhost:7266";
    #else
        internal const string ServerBaseUrl = "https://lukebugapi.azurewebsites.net";
    #endif

    internal readonly static string s_createproject = $"{ServerBaseUrl}/api/createproject";
    internal readonly static string s_getallprojects = $"{ServerBaseUrl}/api/getallprojects";
    internal readonly static string s_getallusers = $"{ServerBaseUrl}/api/getallusers";
    internal readonly static string s_createticket = $"{ServerBaseUrl}/api/createticket";
    internal readonly static string s_getalltickets = $"{ServerBaseUrl}/api/getalltickets";

    internal readonly static string s_login = $"{ServerBaseUrl}/api/login";

}