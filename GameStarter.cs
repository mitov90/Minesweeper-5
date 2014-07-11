namespace Minesweeper
{
    using System;
    public abstract class GameStarter
    {
        public static void Main()
        {
            var game = new Game();
            game.Menu();
        }
        
    }
}