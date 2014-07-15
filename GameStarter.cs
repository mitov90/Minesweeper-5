namespace Minesweeper
{
    public abstract class GameStarter
    {
        public static void Main()
        {
            var game = new Game();
            game.Run();
        }
    }
}