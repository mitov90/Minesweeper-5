// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game.cs" company="">
//   
// </copyright>
// <summary>
//   The game.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Minesweeper
{
    using System;

    using Minesweeper.Data;
    using Minesweeper.Enums;
    using Minesweeper.Interfaces;
    using Minesweeper.Logic;

    /// <summary>
    /// The game.
    /// </summary>
    public sealed class Game
    {
        #region Static Fields

        /// <summary>
        /// The the game.
        /// </summary>
        private static Game theGame;

        #endregion

        #region Fields

        /// <summary>
        /// The highscore.
        /// </summary>
        internal readonly IHighscore Highscore;

        /// <summary>
        /// The renderer.
        /// </summary>
        internal readonly IRenderer Renderer;

        /// <summary>
        /// The board.
        /// </summary>
        private readonly Board board;

        /// <summary>
        /// The board manager.
        /// </summary>
        private readonly IBoardManager boardManager;

        /// <summary>
        /// The board scanner.
        /// </summary>
        private readonly IBoardScanner boardScanner;

        /// <summary>
        /// The user input handler.
        /// </summary>
        private readonly UserInput userInputHandler;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Game"/> class from being created.
        /// </summary>
        private Game()
        {
            this.Renderer = new Renderer();
            this.Highscore = new Highscore();
            this.board = new Board(GameData.MaxRows, GameData.MaxColumns, GameData.MaxMines);
            this.boardScanner = new BoardScanner(this.board);
            this.boardManager = new BoardManager(this.board, this.boardScanner);
            this.userInputHandler = new UserInput(this);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static Game Instance
        {
            // 'Lazy initialization'
            get
            {
                return theGame ?? (theGame = new Game());
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The run.
        /// </summary>
        public void Run()
        {
            bool inGame = true;

            while (inGame)
            {
                this.Renderer.PrintMainMenu();
                inGame = this.userInputHandler.Handle();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The Engine of the Game.
        /// </summary>
        internal void Engine()
        {
            this.board.Accept(new MineSetterVisitor());
            this.Renderer.PrintGameBoard(this.board);

            while (true)
            {
                this.Renderer.Write(
                    "\nChoose and press Enter:\n" + "'" + PlayerCommand.ReturnKey + "'"
                    + " to return to the menu or\nEnter row and column separated by a space: \n");

                // getting player input as object
                var command = new PlayerCommand(Console.ReadLine());

                if (command.IsBadInput)
                {
                    this.Renderer.Write(command.Message);
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
        /// If the current player is with top score, add it to Top list players and show the scoreboard.
        /// </summary>
        /// <param name="playerScore">
        /// The score of the current player.
        /// </param>
        private void AddIfTopPlayer(int playerScore)
        {
            if (this.Highscore.IsHighScore(playerScore))
            {
                this.Renderer.Write("Please enter your name for the top players' scoreboard: ");

                string playerName = Console.ReadLine();

                if (string.IsNullOrEmpty(playerName))
                {
                    playerName = "no name";
                }

                var player = new Player(playerName, playerScore);

                this.Highscore.AddTopPlayer(player);
                this.Renderer.PrintTopPlayers(this.Highscore.TopPlayers);
            }
        }

        /// <summary>
        /// Check the current status of the Game and print a result.
        /// </summary>
        /// <param name="chosenRow">
        /// Current field's row.
        /// </param>
        /// <param name="chosenColumn">
        /// Current field's column.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsGameOver(int chosenRow, int chosenColumn)
        {
            bool gameOver = false;
            try
            {
                BoardStatus boardStatus = this.boardManager.OpenField(chosenRow, chosenColumn);

                switch (boardStatus)
                {
                    case BoardStatus.SteppedOnAMine:
                        {
                            this.Renderer.PrintAllFields(this.board, this.boardScanner);

                            int playerScore = this.boardManager.CountOpenedFields();
                            this.Renderer.Write(
                                "Booooom! You were killed by a mine. You revealed " + playerScore
                                + " cells without mines.");

                            this.AddIfTopPlayer(playerScore);
                            this.Renderer.Write("Press Enter: to return to the menu");
                            Console.ReadLine();
                            gameOver = true;
                        }

                        break;

                    case BoardStatus.AlreadyOpened:
                        {
                            this.Renderer.Write("The field is already opened!");
                        }

                        break;

                    case BoardStatus.AllFieldsAreOpened:
                        {
                            this.Renderer.PrintAllFields(this.board, this.boardScanner);
                            this.Renderer.Write("Congratulations! You win!!!");

                            int playerScore = this.boardManager.CountOpenedFields();

                            this.AddIfTopPlayer(playerScore);
                            this.Renderer.Write("Press Enter: to return to the menu");
                            Console.ReadLine();
                            gameOver = true;
                        }

                        break;

                    default:
                        {
                            this.Renderer.PrintGameBoard(this.board);
                        }

                        break;
                }
            }
            catch
            {
                this.Renderer.Write("Wrong field's coordinates!");
            }

            return gameOver;
        }

        #endregion
    }
}