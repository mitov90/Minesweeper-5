namespace Minesweeper
{
    using System;

    using Interfaces;

    public sealed class Game
    {
        private const int MAX_ROWS = 5;
        private const int MAX_COLUMNS = 10;
        private const int MAX_MINES = 15;

        private const ConsoleKey ExitGameKey = ConsoleKey.Q;
        private const ConsoleKey NewGameKey = ConsoleKey.N;
        private const ConsoleKey TopPlayersKey = ConsoleKey.T;

        private static readonly Game TheGame = new Game();
        private readonly IHighscore highscore;
        private readonly IRenderer renderer;
        private IBoardScanner boardScanner;
        private IBoardManager boardManager;
        private Board board;

        private Game()
        {
            this.renderer = new Renderer();
            this.highscore = new Highscore();
        }

        public static Game Instance
        {
            get
            {
                return TheGame;
            }
        }

        /// <summary>
        /// The Main Menu of the Game.
        /// </summary>
        public void Run()
        {
            bool inGame = true;

            while (inGame)
            {
                this.renderer.PrintMainMenu();

                ConsoleKeyInfo keyPressed = Console.ReadKey();

                switch (keyPressed.Key)
                {
                    // Start a new Game
                    case NewGameKey:
                        {
                            this.Engine();
                            inGame = false;
                        }

                        break;

                    // Exit the Game
                    case ExitGameKey:
                        {
                            inGame = false;
                            this.renderer.Write("Good bye!");
                            Environment.Exit(1);
                        }

                        break;

                    // Show Top Scores
                    case TopPlayersKey:
                        {
                            if (this.highscore.TopPlayers.Count > 0)
                            {
                                this.renderer.PrintTopPlayers(this.highscore.TopPlayers);
                            }
                            else
                            {
                                this.renderer.Write("There is still no TOP players!");
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

        private void InitializeGameBoard()
        {
            this.board = new Board(MAX_ROWS, MAX_COLUMNS, MAX_MINES);
            this.boardScanner = new BoardScanner(this.board);
            this.boardManager = new BoardManager(this.board, this.boardScanner);
        }

        /// <summary>
        /// The Engine of the Game.
        /// </summary>
        private void Engine()
        {
            this.InitializeGameBoard();
            this.board.Accept(new MineSetterVisitor());
            this.renderer.PrintGameBoard(this.board);

            while (true)
            {
                this.renderer.Write("\nChoose and press Enter:\n" + "'" + PlayerCommand.ReturnKey + "'" +
                    " to return to the menu or\nEnter row and column separated by a space: \n");

                // getting player input as object
                var command = new PlayerCommand(Console.ReadLine());

                if (command.IsBadInput)
                {
                    this.renderer.Write(command.Message);
                }
                else
                {
                    if (command.IsReturnKey || this.IsGameOver(command.Row, command.Col))
                    {
                        this.Run();
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
                            this.renderer.PrintAllFields(this.board, this.boardScanner);

                            var playerScore = this.boardManager.CountOpenedFields();
                            this.renderer.Write("Booooom! You were killed by a mine. You revealed " +
                                playerScore + " cells without mines.");

                            this.AddIfTopPlayer(playerScore);
                            this.renderer.Write("Press Enter: to return to the menu");
                            Console.ReadLine();
                            gameOver = true;
                        }

                        break;

                    case BoardStatus.AlreadyOpened:
                        {
                            this.renderer.Write("The field is already opened!");
                        }

                        break;

                    case BoardStatus.AllFieldsAreOpened:
                        {
                            this.renderer.PrintAllFields(this.board, this.boardScanner);
                            this.renderer.Write("Congratulations! You win!!!");

                            var playerScore = this.boardManager.CountOpenedFields();

                            this.AddIfTopPlayer(playerScore);
                            this.renderer.Write("Press Enter: to return to the menu");
                            Console.ReadLine();
                            gameOver = true;
                        }

                        break;

                    default:
                        {
                            this.renderer.PrintGameBoard(this.board);
                        }

                        break;
                }
            }
            catch
            {
                this.renderer.Write("Wrong field's coordinates!");
            }

            return gameOver;
        }

        /// <summary>
        /// If the current player is with top score, add it to Top list players and show the scoreboard.
        /// </summary>
        /// <param name="playerScore">The score of the current player.</param>
        private void AddIfTopPlayer(int playerScore)
        {
            if (this.highscore.IsHighScore(playerScore))
            {
                this.renderer.Write("Please enter your name for the top players' scoreboard: ");

                var playerName = Console.ReadLine();

                if (string.IsNullOrEmpty(playerName))
                {
                    playerName = "no name";
                }

                var player = new Player(playerName, playerScore);

                this.highscore.AddTopPlayer(player);
                this.renderer.PrintTopPlayers(this.highscore.TopPlayers);
            }
        }
    }
}
