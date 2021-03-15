using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkService.CustomExceptions
{
    public class NoFreeSpacesException : Exception
    {
        public NoFreeSpacesException()
        {
        }

        public NoFreeSpacesException(string message)
            : base(message)
        {
        }
    }
}
