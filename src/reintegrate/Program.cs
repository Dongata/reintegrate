using System;

namespace reintegrate
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new ReintegrateGame();
            game.Run();
        }
    }
}
