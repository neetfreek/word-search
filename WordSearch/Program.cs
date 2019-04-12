using System;

namespace WordSearch
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new WordSearch())
                game.Run();
        }
    }
}