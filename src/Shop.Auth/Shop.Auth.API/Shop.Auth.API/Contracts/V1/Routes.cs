namespace Shop.Auth.API.Contracts.V1
{
    public static class Routes
    {
        public const string Auth = "v{version:apiVersion}/auth";
        public const string Account = "v{version:apiVersion}/account";
        public const string Register = "register";
        public const string Login = "login";
        public const string Refresh = "refresh";
    }
}
