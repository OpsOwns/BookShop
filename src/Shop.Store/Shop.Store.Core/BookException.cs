using Shop.Shared.Shared;
using System;

namespace Shop.Store.Core
{
    public class BookException : BaseException
    {
        public BookException()
        {
        }

        public BookException(string code) : base(code)
        {
        }

        public BookException(string message, params object[] args) : base(string.Empty, message, args)
        {
        }

        public BookException(string code, string message, params object[] args) : base(null, code, message, args)
        {
        }

        public BookException(Exception innerException, string message, params object[] args)
            : base(innerException, string.Empty, message, args)
        {
        }

        public BookException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }

        protected BookException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}