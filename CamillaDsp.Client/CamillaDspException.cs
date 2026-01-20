using System;

namespace CamillaDsp.Client
{
    public class CamillaDspException : Exception
    {
        public CamillaDspException(string? methodName, string? message)
            : base($"{methodName}: {message}")
        {
        }
        public CamillaDspException(string? methodName, string? message, Exception inner)
            : base($"{methodName}: {message}", inner)
        {
        }
    }
}
