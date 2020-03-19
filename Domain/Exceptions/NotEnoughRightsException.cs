using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class NotEnoughRightsException : Exception
    {
        public override string Message { get; }
        public NotEnoughRightsException() : base()
        {
            Message = "This user doens't have enouth rigths to perform this action";
        }
        public NotEnoughRightsException(string str) : base(str) { }
    }
}
