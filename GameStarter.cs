namespace Minesweeper
{
    using System;

    public abstract class GameStarter
    {
        public static void Main()
        {
            var game = Game.Instance;
            game.Run();
        }
    }
}