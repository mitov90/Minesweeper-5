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
        private IBoardScanner boardScanner;
        private List<IPlayer> topPlayers;
        private IRenderer renderer;
        private IBoardManager boardManager;                                

        /// <summary>
        /// The Main Menu of the Game.
        /// </summary>
        public void Run()
        {
            bool inGame = true;

            while (inGame)
            {
                Renderer.PrintMainMenu();
                
                ConsoleKeyInfo keyPressed = Console.ReadKey();

                switch (keyPressed.Key)
                {
                        // Start a new Game
                    case ConsoleKey.N:
                        {
                            this.Engine();
                            inGame = false;
                        }

                        break;

                    // Exit the Game
                    case ConsoleKey.Q:
                        {
                            inGame = false;
                            renderer.Write("Good bye!");
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
                                renderer.Write("There is still no TOP players!");
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

        private void InitializeGameBoard()
        {
            this.board = new Board(MAX_ROWS, MAX_COLUMNS, MAX_MINES);
            this.boardScanner = new BoardScanner(this.board);
            this.renderer = new Renderer(this.board, this.boardScanner);
            this.boardManager = new BoardManager(this.board, this.boardScanner);
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

        /// <summary>
        /// The Engine of the Game.
        /// </summary>
        private void Engine()
        {
            this.InitializeGameBoard();
            this.board.Accept(new MineSetterVisitor());
            this.renderer.PrintGameBoard();

            while (true)
            {
                renderer.Write("\nChoose and press Enter:\n" + "'" + ConsoleKey.X.ToString() + "'" +
                    " to return to the menu or\nEnter row and column separated by a space: \n");

                var command = Console.ReadLine();

                if (command != null && command.Trim().ToUpper() == ConsoleKey.X.ToString())
                {
                    this.Run();
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
                            if (this.IsGameOver(chosenRow, chosenColumn))
                            {
                                this.Run();
                            }                          
                        }
                    }
                    catch
                    {
                        renderer.Write("\nWrong field's coordinates!");
                    }
                }
            }
        }

        /// <summary>
        /// Check the current status of the Game and print a result.        
        /// </summary>
        /// <param name="chosenRow">Current field's row.</param>
        /// <param name="chosenColumn">Current field's column.</param>
        private bool IsGameOver(int chosenRow, int chosenColumn)
        {
            bool gameOver = false;
            try
            {
                var boardStatus = this.boardManager.OpenField(chosenRow, chosenColumn);

                switch (boardStatus)
                {
                    case BoardStatus.SteppedOnAMine:
                        {
                            this.renderer.PrintAllFields();

                            var playerScore = this.boardManager.CountOpenedFields();
                            renderer.Write("Booooom! You were killed by a mine. You revealed " +
                                playerScore + " cells without mines.");

                            this.AddIfTopPlayer(playerScore);
                            renderer.Write("Press Enter: to return to the menu");
                            Console.ReadLine();
                            gameOver = true;
                        }

                        break;

                    case BoardStatus.AlreadyOpened:
                        {
                            renderer.Write("The field is already opened!");
                        }

                        break;

                    case BoardStatus.AllFieldsAreOpened:
                        {
                            this.renderer.PrintAllFields();
                            renderer.Write("Congratulations! You win!!!");

                            var playerScore = this.boardManager.CountOpenedFields();

                            this.AddIfTopPlayer(playerScore);
                            renderer.Write("Press Enter: to return to the menu");
                            Console.ReadLine();
                            gameOver = true;
                        }

                        break;

                    default:
                        {
                            this.renderer.PrintGameBoard();
                        }

                        break;
                }
            }
            catch
            {
                renderer.Write("Wrong field's coordinates!");
            }

            return gameOver;
        }

        /// <summary>
        /// If the current player is with top score, add it to Top list players and show the scoreboard.
        /// </summary>
        /// <param name="playerScore">The score of the current player.</param>
        private void AddIfTopPlayer(int playerScore)
        {
            if (this.IsHighScore(playerScore))
            {
                renderer.Write("Please enter your name for the top players' scoreboard: ");

                var playerName = Console.ReadLine();
                var player = new Player(playerName, playerScore);

                this.AddTopPlayer(ref player);
                this.ShowTopPlayers();
            }
        }
    }
}