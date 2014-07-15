namespace Minesweeper.Data
{
    using System;

    /// <summary>
    /// Represent private data for game instance
    /// Structural patterns -> Private Class Data
    /// More info at: http://sourcemaking.com/design_patterns/private_class_data
    /// </summary>
    internal class GameData
    {
        public const int MaxRows = 5;
        public const int MaxColumns = 10;
        public const int MaxMines = 15;

        public const ConsoleKey ExitGameKey = ConsoleKey.Q;
        public const ConsoleKey NewGameKey = ConsoleKey.N;
        public const ConsoleKey TopPlayersKey = ConsoleKey.T;



    }
}
