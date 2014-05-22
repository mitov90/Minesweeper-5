//vsichko ba4ka, ne butaj!
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    internal class Game
    {
        private const int MaxRows = 5;
        private const int MaxColumns = 10;
        private const int MaxMines = 15;
        private const int MaxTopPlayers = 5;

        private static Board _board;
        private static List<Player> _topPlayers;

        private static void InitializeGameBoard()
        {
            _board = new Board(MaxRows, MaxColumns, MaxMines);
        }

        private static void InitializeTopPlayers()
        {
            _topPlayers = new List<Player> {Capacity = MaxTopPlayers};
        }

        private static bool CheckHighScores(int score)
        {
            return _topPlayers.Capacity > _topPlayers.Count ||
                   _topPlayers.Any(currentPlayer => currentPlayer.Score < score);
        }

        private static void topadd(ref Player player)
        {
            if (_topPlayers.Capacity > _topPlayers.Count)
            {
                _topPlayers.Add(player);
                _topPlayers.Sort();
            }
            else
            {
                _topPlayers.RemoveAt(_topPlayers.Capacity - 1);
                _topPlayers.Add(player);
                _topPlayers.Sort();
            }
        }

        private static void top()
        {
            Console.WriteLine("Scoreboard");
            for (var i = 0; i < _topPlayers.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + _topPlayers[i]);
            }
        }

        private static void Menu()
        {
            InitializeTopPlayers();
            var str = "restart";
            var choosenRow = 0;
            var chosenColumn = 0;

            while (str != "exit")
            {
                switch (str)
                {
                    case "restart":
                        InitializeGameBoard();
                        Console.WriteLine("Welcome to the game “Minesweeper”. " +
                                          "Try to reveal all cells without mines. " +
                                          "Use 'top' to view the scoreboard, 'restart' to start a new game" +
                                          "and 'exit' to quit the game.");
                        _board.PrintGameBoard();
                        break;
                    case "exit":
                        Console.WriteLine("Good bye!");
                        Console.Read();
                        break;
                    case "top":
                        top();
                        break;
                    case "coordinates":
                        try
                        {
                            var status = _board.OpenField(choosenRow, chosenColumn);
                            if (status == Board.Status.SteppedOnAMine)
                            {
                                _board.PrintAllFields();
                                var score = _board.CountOpenedFields();
                                Console.WriteLine("Booooom! You were killed by a mine. You revealed " +
                                                  score +
                                                  " cells without mines.");

                                if (CheckHighScores(score))
                                {
                                    Console.WriteLine("Please enter your name for the top scoreboard: ");
                                    string name = Console.ReadLine();
                                    var player = new Player(name, score);
                                    topadd(ref player);
                                    top();
                                }
                                str = "restart";
                                continue;
                            }


                            switch (status)
                            {
                                case Board.Status.AlreadyOpened:
                                    Console.WriteLine("Illegal move!");
                                    break;
                                case Board.Status.AllFieldsAreOpened:
                                {
                                    _board.PrintAllFields();
                                    var score = _board.CountOpenedFields();
                                    Console.WriteLine("Congratulations! You win!!");
                                    if (CheckHighScores(score))
                                    {
                                        Console.WriteLine("Please enter your name for the top scoreboard: ");
                                        var name = Console.ReadLine();
                                        var player = new Player(name, score);
                                        topadd(ref player);
                                        // pokazvame klasiraneto
                                        top();
                                    }
                                    str = "restart";
                                    continue;
                                }
                                default:
                                    _board.PrintGameBoard();
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Illegal move");
                        }
                        break;
                }

                Console.Write(Environment.NewLine + "Enter row and column: ");

                str = Console.ReadLine();
                try
                {
                    choosenRow = int.Parse(str);
                    str = "coordinates";
                }
                catch
                {
                    // niama smisal tuka
                    continue;
                }

                str = Console.ReadLine();
                try
                {
                    chosenColumn = int.Parse(str);
                    str = "coordinates";
                }
                catch (Exception)
                {
                }
            }
        }

        private static void Main()
        {
            Menu();
        }
    }
}