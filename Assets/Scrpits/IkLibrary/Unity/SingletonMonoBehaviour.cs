using UnityEngine;

namespace IkLibrary.Unity
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        Debug.Log("instance is null");
                    }
                }
                return instance;
            }
        }

        public virtual void OnDestroy()
        {
            if (instance == this) instance = null;
        }

        protected bool CheckInstance()
        {
            if (this != Instance)
            {
                Destroy(this);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Exists
        {
            get { return instance != null; }
        }
    }
}