namespace Minesweeper
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private const int MAX_ROWS = 5;
        private const int MAX_COLUMNS = 10;
        private const int MAX_MINES = 15;
        private const int MAX_TOP_PLAYERS = 5;

        private static Board board;
        private static List<Player> topPlayers;

        public static void Main(string[] args)
        {
            Menu();
        }

        private static void InitializeGameBoard()
        {
            board = new Board(MAX_ROWS, MAX_COLUMNS, MAX_MINES);
        }

        private static void InitializeTopPlayers()
        {
            topPlayers = new List<Player>();
            topPlayers.Capacity = MAX_TOP_PLAYERS;
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

            bool inGame = true;

            while (inGame)
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to the game “Minesweeper”!\nTry to reveal all cells without mines.\nPlease press:\n\n" +
                                "'" + ConsoleKey.T.ToString() + "' to view the scoreboard\n" +
                                "'" + ConsoleKey.N.ToString() + "' to start a new game\n" +
                                "'" + ConsoleKey.Q.ToString() + "' to quit the game!\n\n");
                Console.WriteLine();
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                Console.WriteLine();

                switch (keyPressed.Key)
                {
                    ///Start a new Game
                    case ConsoleKey.N:
                        {
                            Engine();
                            inGame = false;
                        }

                        break;

                    ///Exit the Game
                    case ConsoleKey.Q:
                        {
                            inGame = false;
                            Console.WriteLine("Good bye!");
                            Environment.Exit(1);
                        }

                        break;

                    ///Show Top Scores
                    case ConsoleKey.T:
                        {
                            inGame = true;
                            if (topPlayers.Count > 0)
                            {
                                Top();
                            }
                            else
                            {
                                Console.WriteLine("There is still no TOP players!");
                            }
                        }

                        break;
                    ///Ask for a choice again
                    default:
                        {
                            inGame = true;
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// The Engine of the Game.
        /// </summary>
        private static void Engine()
        {
            InitializeGameBoard();
            board.PrintGameBoard();

            int chosenRow = 0;
            int chosenColumn = 0;

            while (true)
            {
                Console.WriteLine(System.Environment.NewLine + "Choose and press Enter:\n" + "'" + ConsoleKey.X.ToString() + "'" +
                    " to return to the menu or\nEnter row and column separated by a space: ");
                Console.WriteLine();
                string command = Console.ReadLine();

                if (command.Trim().ToUpper() == ConsoleKey.X.ToString())
                {
                    Menu();
                }
                else
                {
                    try
                    {
                        string[] coordinates = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        chosenRow = int.Parse(coordinates[0]);
                        chosenColumn = int.Parse(coordinates[1]);
                        CheckBoardStatus(chosenRow, chosenColumn);
                    }
                    catch
                    {
                        Console.WriteLine("Wrong field's coordinates!");
                    }
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

                            AddIfTopPlayer(playerScore);
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
                            AddIfTopPlayer(playerScore);
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

        /// <summary>
        /// If the current player is with top score, add it to Top list players and show the scoreboard.
        /// </summary>
        /// <param name="playerScore">The score of the current player.</param>
        private static void AddIfTopPlayer(int playerScore)
        {
            if (CheckHighScores(playerScore))
            {
                Console.WriteLine("Please enter your name for the top players' scoreboard: ");
                string playerName = Console.ReadLine();
                Player player = new Player(playerName, playerScore);

                Topadd(ref player);
                Top();
            }
        }
    }
}