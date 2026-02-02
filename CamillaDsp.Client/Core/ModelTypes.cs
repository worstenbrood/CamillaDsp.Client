using System;

namespace CamillaDsp.Client.Core
{
    internal class ModelTypes<T>
    {
        /// <summary>
        /// TypeCode of T
        /// </summary>
        public static readonly TypeCode TypeCode = Type.GetTypeCode(typeof(T));
    }
}
