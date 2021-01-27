using System;
using Shop.Shared.Shared;

namespace Shop.Auth.Application.Security.Jwt
{
    public class AuthException : BaseException
    {
        public AuthException() { }
        public AuthException(string code) : base(code) { }
        public AuthException(string message, params object[] args) : base(string.Empty, message, args) { }
        public AuthException(string code, string message, params object[] args) : base(null, code, message, args) { }
        public AuthException(Exception innerException, string message, params object[] args)
            : base(innerException, string.Empty, message, args) { }
        public AuthException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException) { }
        protected AuthException(string message, Exception innerException) : base(message, innerException) { }
    }
}
