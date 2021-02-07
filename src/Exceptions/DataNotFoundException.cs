using System;

namespace Epsic.Gestion_artistes.Rpg.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message) : base(message)
        {
        }
    }
}