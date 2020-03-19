using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class WrongCredentialsException : Exception
    {
        public override string Message { get; }
        public WrongCredentialsException() : base()
        {
            Message = "User entered wrong credentials";
        }
        public WrongCredentialsException(string str) : base(str) { }
    }
}
