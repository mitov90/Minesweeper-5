namespace Minesweeper
{
    using System;
    using Minesweeper.Data;
    using Minesweeper.Enums;
    using Minesweeper.Interfaces;
    using Minesweeper.Logic;

    public sealed class Game
    {
        #region Fields
        internal readonly GameData gameData;
        private static Game theGame;
        private readonly UserInput userInputHandler;
        private Board board;
        private IBoardManager boardManager;
        private IBoardScanner boardScanner;
        #endregion

        #region Constructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="Game" /> class from being created.
        /// </summary>
        private Game()
        {
            this.gameData = new GameData(new Renderer(), new Highscore());
            this.userInputHandler = new UserInput(this);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the instance.
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

        public void Run()
        {
            var inGame = true;

            while (inGame)
            {
                this.gameData.Renderer.PrintMainMenu();
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
            this.board = new Board(GameData.MAX_ROWS, GameData.MAX_COLUMNS, GameData.MAX_MINES);
            this.boardScanner = new BoardScanner(this.board);
            this.boardManager = new BoardManager(this.board, this.boardScanner);
            this.board.Accept(new MineSetterVisitor(new RandomGenerator()));
            this.gameData.Renderer.PrintGameBoard(this.board);

            while (true)
            {
                this.gameData.Renderer.Write(
                    "\nChoose and press Enter:\n" + "'" + PlayerCommand.ReturnKey + "'"
                    + " to return to the menu or\nEnter row and column separated by a space: \n");

                // getting player input as object
                var command = new PlayerCommand(Console.ReadLine());

                if (command.IsBadInput)
                {
                    this.gameData.Renderer.Write(command.Message);
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
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsGameOver(int chosenRow, int chosenColumn)
        {
            var gameOver = false;
            try
            {
                var boardStatus = this.boardManager.OpenField(chosenRow, chosenColumn);

                switch (boardStatus)
                {
                    case BoardStatus.SteppedOnAMine:
                        {
                            this.gameData.Renderer.PrintAllFields(this.board, this.boardScanner);

                            var playerScore = this.boardManager.CountOpenedFields();
                            this.gameData.Renderer.Write(
                                "Booooom! You were killed by a mine. You revealed " + playerScore
                                + " cells without mines.");

                            gameOver = this.GetPlayerName(playerScore);
                        }

                        break;

                    case BoardStatus.AlreadyOpened:
                        {
                            this.gameData.Renderer.Write("The field is already opened!");
                        }

                        break;

                    case BoardStatus.AllFieldsAreOpened:
                        {
                            this.gameData.Renderer.PrintAllFields(this.board, this.boardScanner);
                            this.gameData.Renderer.Write("Congratulations! You win!!!");

                            var playerScore = this.boardManager.CountOpenedFields();

                            gameOver = this.GetPlayerName(playerScore);
                        }

                        break;

                    default:
                        {
                            this.gameData.Renderer.PrintGameBoard(this.board);
                        }

                        break;
                }
            }
            catch
            {
                this.gameData.Renderer.Write("Wrong field's coordinates!");
            }

            return gameOver;
        }

        private bool GetPlayerName(int playerScore)
        {
            if (this.gameData.Highscore.TopPlayers.Count == 0)
            {
                foreach (var player in Scoreboard.Load())
                {
                    this.gameData.Highscore.AddTopPlayer((Player)player);
                }
            }

            this.gameData.Highscore.AddIfTopPlayer(playerScore, this.gameData.Renderer);
            Scoreboard.Save(this.gameData.Highscore.TopPlayers);
            this.gameData.Renderer.Write("Press Enter: to return to the menu");
            Console.ReadLine();
            return true;
        }

        #endregion
    }
}