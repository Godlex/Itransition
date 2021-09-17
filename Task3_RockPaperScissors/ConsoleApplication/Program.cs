namespace ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    static class Program
    {
        static int tableWidth = 126;

        static void Main(string[] args)
        {
            if (!IsCorrectInput(args)) return;

            bool isEnd = false;

            do
            {
                var computerMoveIndex = GetComputerMoveIndex(args);
                var key = new byte[16];
                RandomNumberGenerator.Create().GetNonZeroBytes(key);
                var hmac = new HMACSHA256(key);
                var computerMove = args[computerMoveIndex];
                var argsLength = args.Length;

                var value = GetHmacKeyAndGenerateHmac(hmac, computerMove);

                PrintMenu(args, argsLength);

                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "0":
                        isEnd = true;
                        break;
                    case "?":
                        PrintTable(args, argsLength);
                        break;
                    //выводит таблицу с подсказкой кто кого побеждает
                    default:
                        PrintPlayerMoveResult(args, argsLength, answer, computerMove, computerMoveIndex);
                        Console.WriteLine("HMAC key {0}", value); //ключ проверки и ход
                        break;
                }

                Console.WriteLine("\n\n\n\n");
            } while (!isEnd);
            //var b = StringToByteArray(a); //проверка 
        }

        private static void PrintMenu(string[] args, int argsLength)
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < argsLength; i++)
            {
                Console.WriteLine("{0} - {1}", i + 1, args[i]);
            }

            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
            Console.Write("Enter your move:");
        }

        private static string GetHmacKeyAndGenerateHmac(HMACSHA256 hmac, string computerMove)
        {
            Console.Write("HMAC:");
            var result = hmac.ComputeHash(Encoding.UTF8.GetBytes(computerMove));
            var value = BitConverter.ToString(hmac.Key).Replace("-", "");
            Console.WriteLine(BitConverter.ToString(result).Replace("-", "")); //ход компа HMAC
            return value;
        }

        private static void PrintPlayerMoveResult(string[] args, int argsLength, string answer, string computerMove,
            int computerMoveIndex)
        {
            bool isError = true;
            for (int i = 0; i < argsLength; i++)
            {
                if ((i + 1).ToString() == answer)
                {
                    isError = false;
                    var playerMoveIndex = Convert.ToInt32(answer) - 1;
                    Console.WriteLine("Your move: {0}", args[playerMoveIndex]);
                    Console.WriteLine("Computer move: {0}", computerMove);
                    Console.WriteLine(GameResult(playerMoveIndex, computerMoveIndex, argsLength));
                }
            }

            if (isError) Console.WriteLine("error input!\nTry again");
        }

        private static void PrintTable(string[] args, int argsLength)
        {
            PrintLine();
            PrintRow(args);
            PrintLine();
            string[] winRow = new string[argsLength];
            string[] loseRow = new string[argsLength];
            string[] drawRow = new string[argsLength];
            for (int i = 0; i < argsLength; i++)
            {
                winRow[i] += "Win:";
                loseRow[i] += "Lose:";
                drawRow[i] += "Draw:";
                for (int j = 0; j < argsLength; j++)
                {
                    switch (GameResult(i, j, argsLength))
                    {
                        case "Draw":
                            drawRow[i] += " " + args[j];
                            break;
                        case "Lose":
                            loseRow[i] += " " + args[j];
                            break;
                        case "Win":
                            winRow[i] += " " + args[j];
                            break;
                    }
                }
            }

            PrintRow(winRow);
            PrintLine();
            PrintRow(loseRow);
            PrintLine();
            PrintRow(drawRow);
            PrintLine();
        }

        private static string GameResult(int playerMoveIndex, int computerMoveIndex, int argsLength)
        {
            if (playerMoveIndex == computerMoveIndex)
            {
                return "Draw";
            }
            else
            {
                if (playerMoveIndex > computerMoveIndex)
                {
                    return playerMoveIndex - computerMoveIndex >
                           Math.Truncate(argsLength / 2.0)
                        ? "Win"
                        : "Lose";
                }
                else
                {
                    return computerMoveIndex - playerMoveIndex >
                           Math.Truncate(argsLength / 2.0)
                        ? "Lose"
                        : "Win";
                }
            }
        }

        private static int GetComputerMoveIndex(string[] args)
        {
            return new Random().Next(0, args.Length - 1);
        }

        private static bool IsCorrectInput(string[] args)
        {
            if (args.Length >= 3 && (args.Length % 2)==1 && IsAllArgumentsUnique(args)) return true;
            Console.WriteLine(
                "input error \nexample of correct input : paper rock scissors (3 and more odd UNIQUE number arguments)");
            return false;
        }

        private static bool IsAllArgumentsUnique(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                for (int j = 0; j < args.Length; j++)
                {
                    if (args[i] == args[j] && i != j)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static IEnumerable<byte> StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        private static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        private static void PrintRow(string[] columns)
        {
            var width = (tableWidth - columns.Length) / columns.Length;
            var row = columns.Aggregate("|", (current, column) => current + (AlignCentre(column, width) + "|"));

            Console.WriteLine(row);
        }

        private static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            return string.IsNullOrEmpty(text)
                ? new string(' ', width)
                : text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }
}