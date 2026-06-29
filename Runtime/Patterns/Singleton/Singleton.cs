namespace tcDahn
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                return _instance ??= new T();
            }
        }

        public static void SetInstance(T instance)
        {
            _instance = instance;
        }
    }
}
