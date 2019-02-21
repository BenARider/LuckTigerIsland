using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LTI
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;
        public static T Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<T>();

                return instance;
            }
        }
    }
   
}