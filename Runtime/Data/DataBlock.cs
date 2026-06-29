using System;
using UnityEngine;

namespace tcDahn
{
    [Serializable]
    public class DataBlock<T> where T : DataBlock<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = DataFileHandler.Load<T>(typeof(T).Name) ?? (T)Activator.CreateInstance(typeof(T));
                    DataAutoSave.Register(Save);
                }
                return _instance;
            }
        }

        public static void Save()
        {
            DataFileHandler.Save(_instance, typeof(T).Name);
        }
        public static void Delete()
        {
            _instance = null;
            DataFileHandler.Delete(typeof(T).Name);
        }
    }
}
