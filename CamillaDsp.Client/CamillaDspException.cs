using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamillaDsp.Client
{
    public class CamillaDspException : Exception
    {
        public CamillaDspException(Enum method, string message)
            : base($"{method}: {message}")
        {
        }
        public CamillaDspException(Enum method, string message, Exception inner)
            : base($"{method}: {message}", inner)
        {
        }
    }
}
