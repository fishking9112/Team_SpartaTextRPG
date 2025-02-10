
namespace Team_SpartaTextRPG
{
    class CursorManager : Helper.Singleton<CursorManager>
    {

        private static readonly object _lock = new object();

        // 커서 이동 시 다른 쓰레드에서 서로 커서 가져가서 Console.Write()하려고 해서 오류가 남
        // 해당 오류는 락을 걸어서 만약 다른 쓰레드가 커서 가져가서 Console.Write()하려고 하면 또 다른 쓰레드는 기다린 뒤 쓰게 함
        public void CurserPointUse(Action _action)
        {
            lock (_lock)
            {
                try
                {
                    _action();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CurserManager] 예외 발생: {ex.Message}");
                    throw; // 예외를 다시 던져 호출자가 처리할 수 있도록 함
                }
            }
        }
    }
}
