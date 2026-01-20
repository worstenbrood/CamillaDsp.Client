using System;

namespace CamillaDsp.Client.Base
{
    internal class ModelTypes<T>
    {
        /// <summary>
        /// Type of T
        /// </summary>
        public static readonly Type Type = typeof(T);

        /// <summary>
        /// TypeCode of T
        /// </summary>
        public static readonly TypeCode TypeCode = Type.GetTypeCode(Type);

    }
}
