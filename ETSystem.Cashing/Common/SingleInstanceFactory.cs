using System;

namespace ETSystem.Cashing.Common
{
    public class SingleInstanceFactory
    {
        public static T GetInstance<T>() where T : class
        {
            try
            {
                return (T)Instance<T>.obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static class Instance<T> where T : class
        {
            public static object obj = Activator.CreateInstance(typeof(T));
        }
    }
}
