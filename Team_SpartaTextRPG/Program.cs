namespace Team_SpartaTextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Maingame.instance.StartGame();

            while(true)
            {
                Maingame.instance.Update();
            }
        }
    }
}
