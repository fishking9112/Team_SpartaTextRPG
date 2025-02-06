namespace Team_SpartaTextRPG.Helper
{
    public class Singleton<T> where T : class, new()
    {
        private static T? m_instance;

        // Singleton 인스턴스에 접근하는 속성
        public static T instance
        {
            get
            {
                // Double-Check Locking
                if (m_instance == null)
                {
                    m_instance = new T();
                }
                return m_instance;
            }
        }
    }
}