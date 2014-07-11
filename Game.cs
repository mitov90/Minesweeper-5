namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class Game
    {
        private const int MAX_ROWS = 5;
        private const int MAX_COLUMNS = 10;
        private const int MAX_MINES = 15;
        private const int MAX_TOP_PLAYERS = 5;

        private Board board;
        private List<IPlayer> topPlayers;

        public Game()
        {
            this.board = new Board(MAX_ROWS, MAX_COLUMNS, MAX_MINES);
            this.topPlayers = new List<IPlayer> { Capacity = MAX_TOP_PLAYERS };
        }        

        private bool IsHighScore(int currentPlayerScore)
        {
            if (this.topPlayers.Capacity > this.topPlayers.Count)
            {
                return true;
            }

            foreach (var player in this.topPlayers)
            {
                var topPlayer = (Player)player;
                if (topPlayer.Score < currentPlayerScore)
                {
                    return true;
                }
            }

            return false;
        }

        private void AddTopPlayer(ref Player currentPlayer)
        {
            if (currentPlayer == null)
            {
                throw new ArgumentNullException("currentPlayer");
            }

            if (this.topPlayers.Capacity > this.topPlayers.Count)
            {
                this.topPlayers.Add(currentPlayer);
                this.topPlayers.Sort();
            }
            else
            {
                var lastTopPlayerIndex = this.topPlayers.Capacity - 1;

                this.topPlayers.RemoveAt(lastTopPlayerIndex);
                this.topPlayers.Add(currentPlayer);
                this.topPlayers.Sort();
            }
        }

        private void ShowTopPlayers()
        {
            if (this.topPlayers.Count > 0)
            {
                Console.WriteLine("Scoreboard");
                for (var i = 0; i < this.topPlayers.Count; i++)
                {
                    var playerRank = i + 1;
                    Console.WriteLine(playerRank + ". " + this.topPlayers[i]);
                }
            }
            else
            {
                Console.WriteLine("There is still no TOP players!---");
            }
        }

        /// <summary>
        /// The Main Menu of the Game.
        /// </summary>
        public void Menu()
        {

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
                        // Start a new Game
                    case ConsoleKey.N:
                        {
                            Engine();
                            inGame = false;
                        }

                        break;

                    // Exit the Game
                    case ConsoleKey.Q:
                        {
                            inGame = false;
                            Console.WriteLine("Good bye!");
                            Environment.Exit(1);
                        }

                        break;

                    // Show Top Scores
                    case ConsoleKey.T:
                        {
                            if (this.topPlayers.Count > 0)
                            {
                                this.ShowTopPlayers();
                            }
                            else
                            {
                                Console.WriteLine("There is still no TOP players!");
                            }
                        }

                        break;

                        // Ask for a choice again
                    default:
                        {
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// The Engine of the Game.
        /// </summary>
        private void Engine()
        {
            this.board.PrintGameBoard();

            while (true)
            {
                Console.WriteLine(Environment.NewLine + "Choose and press Enter:\n" + "'" + ConsoleKey.X.ToString() + "'" +
                    " to return to the menu or\nEnter row and column separated by a space: ");
                Console.WriteLine();
                var command = Console.ReadLine();

                if (command != null && command.Trim().ToUpper() == ConsoleKey.X.ToString())
                {
                    this.Menu();
                }
                else
                {
                    try
                    {
                        if (command != null)
                        {
                            var coordinates = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            var chosenRow = int.Parse(coordinates[0]);
                            var chosenColumn = int.Parse(coordinates[1]);
                            CheckBoardStatus(chosenRow, chosenColumn);
                        }
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
        private void CheckBoardStatus(int chosenRow, int chosenColumn)
        {
            try
            {
                var boardStatus = this.board.OpenField(chosenRow, chosenColumn);

                switch (boardStatus)
                {
                    case BoardStatus.SteppedOnAMine:
                        {
                            this.board.PrintAllFields();

                            var playerScore = this.board.CountOpenedFields();
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
                            this.board.PrintAllFields();
                            Console.WriteLine("Congratulations! You win!!");

                            var playerScore = this.board.CountOpenedFields();
                            AddIfTopPlayer(playerScore);
                        }

                        break;

                    default:
                        {
                            this.board.PrintGameBoard();
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
        private void AddIfTopPlayer(int playerScore)
        {
            if (this.IsHighScore(playerScore))
            {
                Console.WriteLine("Please enter your name for the top players' scoreboard: ");
                var playerName = Console.ReadLine();
                var player = new Player(playerName, playerScore);

                this.AddTopPlayer(ref player);
                this.ShowTopPlayers();
            }
        }
    }
}