// vsichko ba4ka, ne butaj!
namespace Minesweeper
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private const int MaxRows = 5;
        private const int MaxColumns = 10;
        private const int MaxMines = 15;
        private const int MaxTopPlayers = 5;

        private static Board board;
        private static List<Player> topPlayers;

        public static void Main(string[] args)
        {
            Menu();
        }

        private static void InitializeGameBoard()
        {
            board = new Board(MaxRows, MaxColumns, MaxMines);
        }

        private static void InitializeTopPlayers()
        {
            topPlayers = new List<Player>();
            topPlayers.Capacity = MaxTopPlayers;
        }

        private static bool CheckHighScores(int score)
        {
            if (topPlayers.Capacity > topPlayers.Count)
            {
                return true;
            }

            foreach (Player currentPlayer in topPlayers)
            {
                if (currentPlayer.Score < score)
                {
                    return true;
                }
            }

            return false;
        }

        private static void Topadd(ref Player player)
        {
            if (topPlayers.Capacity > topPlayers.Count)
            {
                topPlayers.Add(player);
                topPlayers.Sort();
            }
            else
            {
                topPlayers.RemoveAt(topPlayers.Capacity - 1);
                topPlayers.Add(player);
                topPlayers.Sort();
            }
        }

        private static void Top()
        {
            Console.WriteLine("Scoreboard");
            for (int i = 0; i < topPlayers.Count; i++)
            {
                Console.WriteLine((int)(i + 1) + ". " + topPlayers[i]);
            }
        }

        /// <summary>
        /// The Main Menu of the Game.
        /// </summary>
        private static void Menu()
        {
            InitializeTopPlayers();

            int chosenRow = 0;
            int chosenColumn = 0;
            string gameState = "restart";

            while (gameState != "exit")
            {
                if (gameState == "restart")
                {
                    InitializeGameBoard();

                    Console.WriteLine("Welcome to the game “Minesweeper”. " +
                        "Try to reveal all cells without mines. " +
                        "Use 'top' to view the scoreboard, 'restart' to start a new game" +
                        "and 'exit' to quit the game.");

                    board.PrintGameBoard();
                }
                else if (gameState == "exit")
                {
                    Console.WriteLine("Good bye!");
                    Console.Read();
                }
                else if (gameState == "top")
                {
                    Top();
                }
                else if (gameState == "coordinates")
                {
                    try
                    {
                        BoardStatus boardStatus = board.OpenField(chosenRow, chosenColumn);

                        if (boardStatus == BoardStatus.SteppedOnAMine)
                        {
                            board.PrintAllFields();

                            int playerScore = board.CountOpenedFields();
                            Console.WriteLine("Booooom! You were killed by a mine. You revealed " +
                                playerScore +
                                " cells without mines.");

                            if (CheckHighScores(playerScore))
                            {
                                Console.WriteLine("Please enter your name for the top players' scoreboard: ");
                                string playerName = Console.ReadLine();
                                Player player = new Player(playerName, playerScore);

                                Topadd(ref player);
                                Top();
                            }

                            gameState = "restart";
                            continue;
                        }
                        else if (boardStatus == BoardStatus.AlreadyOpened)
                        {
                            Console.WriteLine("The field is already opened!");
                        }
                        else if (boardStatus == BoardStatus.AllFieldsAreOpened)
                        {
                            board.PrintAllFields();
                            Console.WriteLine("Congratulations! You win!!");

                            int playerScore = board.CountOpenedFields();

                            if (CheckHighScores(playerScore))
                            {
                                Console.WriteLine("Please enter your name for the top players' scoreboard: ");
                                string playerName = Console.ReadLine();
                                Player player = new Player(playerName, playerScore);

                                Topadd(ref player);
                                Top();
                            }

                            gameState = "restart";
                            continue;
                        }
                        else
                        {
                            board.PrintGameBoard();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Wrong field's coordinates!");
                    }
                }

                Console.Write(System.Environment.NewLine + "Enter row and column: ");
                gameState = Console.ReadLine();

                try
                {
                    chosenRow = int.Parse(gameState);
                    gameState = "coordinates";
                }
                catch
                {
                    continue;
                }

                gameState = Console.ReadLine();

                try
                {
                    chosenColumn = int.Parse(gameState);
                    gameState = "coordinates";
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Check the current status of the Game and print a result.        
        /// </summary>
        /// <param name="chosenRow">Current field's row.</param>
        /// <param name="chosenColumn">Current field's column.</param>
        private static void CheckBoardStatus(int chosenRow, int chosenColumn)
        {
            try
            {
                BoardStatus boardStatus = board.OpenField(chosenRow, chosenColumn);

                switch (boardStatus)
                {
                    case BoardStatus.SteppedOnAMine:
                        {
                            board.PrintAllFields();

                            int playerScore = board.CountOpenedFields();
                            Console.WriteLine("Booooom! You were killed by a mine. You revealed " +
                                playerScore + " cells without mines.");

                            if (CheckHighScores(playerScore))
                            {
                                Console.WriteLine("Please enter your name for the top players' scoreboard: ");
                                string playerName = Console.ReadLine();
                                Player player = new Player(playerName, playerScore);

                                Topadd(ref player);
                                Top();
                            }
                        }

                        break;

                    case BoardStatus.AlreadyOpened:
                        {
                            Console.WriteLine("The field is already opened!");
                        }

                        break;

                    case BoardStatus.AllFieldsAreOpened:
                        {
                            board.PrintAllFields();
                            Console.WriteLine("Congratulations! You win!!");

                            int playerScore = board.CountOpenedFields();
                            if (CheckHighScores(playerScore))
                            {
                                Console.WriteLine("Please enter your name for the top players' scoreboard: ");
                                string playerName = Console.ReadLine();
                                Player player = new Player(playerName, playerScore);

                                Topadd(ref player);
                                Top();
                            }
                        }

                        break;

                    default:
                        {
                            board.PrintGameBoard();
                        }

                        break;
                }
            }
            catch
            {
                Console.WriteLine("Wrong field's coordinates!");
            }
        }
    }
}