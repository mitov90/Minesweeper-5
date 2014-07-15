namespace Minesweeper
{
    using System;

    using Interfaces;

    using Minesweeper.Data;
    using Minesweeper.Enums;
    using Minesweeper.Logic;

    public sealed class Game
    {
        internal readonly IHighscore Highscore;
        internal readonly IRenderer Renderer;

        private static readonly Game TheGame = new Game();

        private readonly UserInput userInputHandler;
        private readonly IBoardScanner boardScanner;
        private readonly IBoardManager boardManager;
        private readonly Board board;

        private Game()
        {
            this.Renderer = new Renderer();
            this.Highscore = new Highscore();
            this.board = new Board(GameData.MaxRows, GameData.MaxColumns, GameData.MaxMines);
            this.boardScanner = new BoardScanner(this.board);
            this.boardManager = new BoardManager(this.board, this.boardScanner);
            this.userInputHandler = new UserInput(this);
        }
      
        public static Game Instance
        {
            get
            {
                return TheGame;
            }
        }

        public void Run()
        {
            var inGame = true;

            while (inGame)
            {
                this.Renderer.PrintMainMenu();
                inGame = this.userInputHandler.Handle();
            }
        }

        /// <summary>
        /// The Engine of the Game.
        /// </summary>
        internal void Engine()
        {
            this.board.Accept(new MineSetterVisitor());
            this.Renderer.PrintGameBoard(this.board);

            while (true)
            {
                this.Renderer.Write("\nChoose and press Enter:\n" + "'" + PlayerCommand.ReturnKey + "'" +
                    " to return to the menu or\nEnter row and column separated by a space: \n");

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
                            this.Renderer.PrintAllFields(this.board, this.boardScanner);

                            var playerScore = this.boardManager.CountOpenedFields();
                            this.Renderer.Write("Booooom! You were killed by a mine. You revealed " +
                                playerScore + " cells without mines.");

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

                            var playerScore = this.boardManager.CountOpenedFields();

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

        /// <summary>
        /// If the current player is with top score, add it to Top list players and show the scoreboard.
        /// </summary>
        /// <param name="playerScore">The score of the current player.</param>
        private void AddIfTopPlayer(int playerScore)
        {
            if (this.Highscore.IsHighScore(playerScore))
            {
                this.Renderer.Write("Please enter your name for the top players' scoreboard: ");

                var playerName = Console.ReadLine();

                if (string.IsNullOrEmpty(playerName))
                {
                    playerName = "no name";
                }

                var player = new Player(playerName, playerScore);

                this.Highscore.AddTopPlayer(player);
                this.Renderer.PrintTopPlayers(this.Highscore.TopPlayers);
            }
        }
    }
}
