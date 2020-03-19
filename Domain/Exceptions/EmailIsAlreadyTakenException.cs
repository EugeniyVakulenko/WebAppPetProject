using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class EmailIsAlreadyTakenException : Exception
    {
        public override string Message { get; }
        public EmailIsAlreadyTakenException() : base()
        {
            Message = "User tried to enter email that was already taken";
        }
        public EmailIsAlreadyTakenException(string str) : base(str) { }
    }
}
