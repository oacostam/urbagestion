using System;

namespace Urbagestion.Model.Common
{
    public class BussinesException : ApplicationException
    {
        public BussinesException(string message) : base(message)
        {
        }

        public BussinesException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}