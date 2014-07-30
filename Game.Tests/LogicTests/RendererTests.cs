namespace Game.Tests.LogicTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper.Data;
    using Minesweeper.Interfaces;
    using Minesweeper.Logic;

    [TestClass]
    public class RendererTests
    {
        [TestInitialize]
        public void InitializeTest()
        {
            StreamWriter standardOut =
                new StreamWriter(Console.OpenStandardOutput());
            standardOut.AutoFlush = true;
            Console.SetOut(standardOut);
        }

        [TestMethod]
        public void TestWrite()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("Pesho" + Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Renderer ab = new Renderer();
                    ab.Write("Pesho");

                    string expected = string.Format("Pesho" + Environment.NewLine);
                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }

        [TestMethod]
        public void TestPrintTopPlayers()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("Scoreboard" + Environment.NewLine + "1. Ivaylo --> 10" + Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Renderer ab = new Renderer();
                    Player ivo = new Player("Ivaylo", 10);
                    List<IPlayer> players = new List<IPlayer>();
                    players.Add(ivo);
                    ab.PrintTopPlayers(players);

                    string expected = string.Format("Scoreboard" + Environment.NewLine + "1. Ivaylo --> 10" + Environment.NewLine);
                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }

        [TestMethod]
        public void TestPrintIfNotTopPlayers()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("There is still no TOP players!" + Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Renderer ab = new Renderer();
                    List<IPlayer> players = new List<IPlayer>();
                    ab.PrintTopPlayers(players);

                    string expected = string.Format("There is still no TOP players!" + Environment.NewLine);
                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }

        [TestMethod]
        public void TestPrintGameMatrix()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("    0 " + Environment.NewLine +
                                                                        "  ╔═══╗" + Environment.NewLine +
                                                                        "0 ║ 0 ║" + Environment.NewLine +
                                                                        "  ╚═══╝" + Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    Renderer ab = new Renderer();
                    IBoard gameMatrix = new Board(1, 1, 0);

                    ab.PrintGameMatrix(gameMatrix, true);

                    string expected = string.Format("    0 " + Environment.NewLine +
                                                    "  ╔═══╗" + Environment.NewLine +
                                                    "0 ║ 0 ║" + Environment.NewLine +
                                                    "  ╚═══╝" + Environment.NewLine);

                    Assert.AreEqual<string>(expected, sw.ToString());
                }
            }
        }
    }
}
