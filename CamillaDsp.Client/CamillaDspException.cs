using System;

namespace CamillaDsp.Client
{
    public class CamillaDspException : Exception
    {
        public CamillaDspException(string? method, string? message)
            : base($"{method}: {message}")
        {
        }
        public CamillaDspException(string? method, string? message, Exception inner)
            : base($"{method}: {message}", inner)
        {
        }
    }
}
