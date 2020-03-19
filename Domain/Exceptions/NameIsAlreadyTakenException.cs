using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class NameIsAlreadyTakenException : Exception
    {
        public override string Message { get; }
        public NameIsAlreadyTakenException() : base()
        {
            Message = "User tried to enter name that was already taken";
        }
        public NameIsAlreadyTakenException(string str) : base(str) { }
    }
}
