using System.Collections;
using System.Collections.Generic;

namespace IkLibrary
{

    public class SingletonClass<T> where T : class, new()
    {

        static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }

        public static bool Exists
        {
            get
            {
                return instance != null;
            }
        }
    }
}