using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Tic_Tac_Toe_v2
{
    class Program
    {
        static char[] pos = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static int moveCount;
        static void DrawBoard()
        {
            Console.WriteLine("");
            Console.WriteLine($"   {pos[1]}  ║  {pos[2]}  ║  {pos[3]}   ");
            Console.WriteLine(" ═════╬═════╬════");
            Console.WriteLine($"   {pos[4]}  ║  {pos[5]}  ║  {pos[6]}   ");
            Console.WriteLine(" ═════╬═════╬════");
            Console.WriteLine($"   {pos[7]}  ║  {pos[8]}  ║  {pos[9]}   ");
            Console.WriteLine("");
        }
        static void DrawTitle()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" +
                "████████╗██╗ ██████╗    ████████╗ █████╗  ██████╗    ████████╗ ██████╗ ███████╗\n" +
                "╚══██╔══╝██║██╔════╝    ╚══██╔══╝██╔══██╗██╔════╝    ╚══██╔══╝██╔═══██╗██╔════╝\n" +
                "   ██║   ██║██║            ██║   ███████║██║            ██║   ██║   ██║█████╗\n" +
                "   ██║   ██║██║            ██║   ██╔══██║██║            ██║   ██║   ██║██╔══╝\n" +
                "   ██║   ██║╚██████╗       ██║   ██║  ██║╚██████╗       ██║   ╚██████╔╝███████╗\n" +
                "   ╚═╝   ╚═╝ ╚═════╝       ╚═╝   ╚═╝  ╚═╝ ╚═════╝       ╚═╝    ╚═════╝ ╚══════╝");
            Console.ResetColor();
        }
        private static string[] EnterPlayers()
        {
            Console.WriteLine("Hello! This is Tic Tac Toe.");
            Console.WriteLine("What is the name of player 1?");
            var player1 = Console.ReadLine();
            Console.WriteLine("Very good. What is the name of player 2?");
            var player2 = Console.ReadLine();
            Console.WriteLine($"Okay good. {player1} is X and {player2} is O.");
            Console.WriteLine($"{player1} goes first. Get ready!");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            return new[] { player1, player2 };
        }
        static void Main(string[] args)
        {

            Console.Title = ("You're playing Tic Tac Toe.");

            string[] players = EnterPlayers();

            char[] ticTac = { 'X', 'O' };
            moveCount = 0;

            int[] scores = { 0, 0 };
            bool playing = true;
            while (playing)
            {
                PlayGame(players, scores, ticTac);
                playing = AskToPlayAgain();
            }
        }
        private static void PlayGame(string[] players, int[] scores, char[] ticTac)
        {
            bool gameWon = false;
            bool gameOver = false;
            int playerIndex = 0;
            while (gameOver == false)
            {
                var nextPlayerIndex = 1 - playerIndex;
                var player = players[playerIndex];
                var piece = ticTac[playerIndex];
                var opponentPiece = ticTac[nextPlayerIndex];
                PlayerMakesAMove(players, scores, player, piece, opponentPiece);
                gameWon = CheckWin();
                gameOver = gameWon || CheckDraw();
                if (gameOver == false)
                {
                    playerIndex = nextPlayerIndex;
                }
            }
            Console.Clear();
            DrawTitle();
            DrawBoard();
            ResetBoard();
            if (gameWon) // Someone won --
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PlayerScore(scores, playerIndex);
                Console.WriteLine($"{players[playerIndex]} wins!");
                Console.ResetColor();
            }
            else // No one won -----
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("It's a draw!");
                Console.ResetColor();
            }
            ScoreBoard(players, scores);
        }
        private static void PlayerMakesAMove(string[] players, int[] scores, string player, char piece, char opponentPiece)
        {
            do
            {
                Console.Clear();
                DrawTitle();
                Console.WriteLine("");
                DrawBoard();
                Console.WriteLine("");
                ScoreBoard(players, scores);
            } while (!TryToPlaceAPiece(player, piece, opponentPiece));
        }
        public static bool CheckDraw()
        {

            if (moveCount >= 9)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static void PlayerScore(int[] scores, int turn)
        {
            scores[turn] = scores[turn] + 1;
        }
        private static void ScoreBoard(string[] players, int[] scores)
        {

            Console.WriteLine($"Score: {players[0]} - {scores[0]}     {players[1]} - {scores[1]}");
        }
        private static bool TryToPlaceAPiece(string player, char playerPiece, char opponentsPiece)
        {

            Console.WriteLine($"{player}'s ({playerPiece}) turn");

            var move = AskTheUser("Which position would you like to take?", 1, 9);
            if (!IsMoveTaken(playerPiece, opponentsPiece, move))
            {
                pos[move] = playerPiece;
                moveCount++;
                return true;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sorry, that position is taken.\n" +
                                "Try again.");
            Console.ResetColor();
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            return false;
        }
        private static bool IsMoveTaken(char playerPiece, char opponentsPiece, int move)
        {
            return pos[move] == opponentsPiece || pos[move] == playerPiece;
        }
        private static bool AskToPlayAgain()
        {
            moveCount = 0;
            Console.WriteLine("");
            Console.WriteLine("What would you like to do now?");
            Console.WriteLine("1. Play again");
            Console.WriteLine("2. Leave");
            var choice = AskTheUser("Enter your option: ", 1, 2);
            Console.Clear();
            if (choice == 1) return true;
            Console.WriteLine("Thanks for playing!");
            Console.ReadLine();
            return false;
        }
        private static int AskTheUser(string prompt, int min, int max)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                int choice = int.Parse(Console.ReadLine());
                if (choice >= min && choice <= max)
                {
                    return choice;
                }
            }
        }
        private static void ResetBoard()
        {
            for (int i = 1; i < pos.Length; i++)
            {
                pos[i] = i.ToString()[0];
            }

        }


        static bool CheckWin()
        {
            if (pos[1] == 'O' && pos[2] == 'O' && pos[3] == 'O') // Horizontal ------------
            {
                return true;
            }
            else if (pos[4] == 'O' && pos[5] == 'O' && pos[6] == 'O')
            {
                return true;
            }
            else if (pos[7] == 'O' && pos[8] == 'O' && pos[9] == 'O')
            {
                return true;
            }
            else if (pos[1] == 'O' && pos[5] == 'O' && pos[9] == 'O') // Diagonal ---
            {
                return true;
            }
            else if (pos[7] == 'O' && pos[5] == 'O' && pos[3] == 'O')
            {
                return true;
            }
            else if (pos[1] == 'O' && pos[4] == 'O' && pos[7] == 'O')// Columns ---
            {
                return true;
            }
            else if (pos[2] == 'O' && pos[5] == 'O' && pos[8] == 'O')
            {
                return true;
            }
            else if (pos[3] == 'O' && pos[6] == 'O' && pos[9] == 'O')
            {
                return true;
            }
            if (pos[1] == 'X' && pos[2] == 'X' && pos[3] == 'X') // Horizontal -----
            {
                return true;
            }
            else if (pos[4] == 'X' && pos[5] == 'X' && pos[6] == 'X')
            {
                return true;
            }
            else if (pos[7] == 'X' && pos[8] == 'X' && pos[9] == 'X')
            {
                return true;
            }
            else if (pos[1] == 'X' && pos[5] == 'X' && pos[9] == 'X') // Diagonal ----
            {
                return true;
            }
            else if (pos[7] == 'X' && pos[5] == 'X' && pos[3] == 'X')
            {
                return true;
            }
            else if (pos[1] == 'X' && pos[4] == 'X' && pos[7] == 'X') // Columns -------
            {
                return true;
            }
            else if (pos[2] == 'X' && pos[5] == 'X' && pos[8] == 'X')
            {
                return true;
            }
            else if (pos[3] == 'X' && pos[6] == 'X' && pos[9] == 'X')
            {
                return true;
            }
            else // No winner ----------
            {
                return false;
            }
        }
    }
}
