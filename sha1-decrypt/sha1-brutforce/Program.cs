using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace sha1_brutforce
{
    internal class Program
    {
        static bool found = false;
        static string password = "";
        static void Main(string[] args)
        {
            List<char> possibleChars = new List<char>() {
                'X', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
                'a', 'b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                'A', 'B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','0','Y','Z'
            };

            string hash1 = "5aea476328379d3bff2204501bb57aa8b4268fac";
            string hash2 = "d31d62ed0af022248e28fc0dc4a9580217987e55";
            string hash3 = "66ceeafde8453dda201978b2b497b9c85d4b6da5";
            string hashz = "395df8f7c51f007019cb30201c49e884b46b92fa";
            string hashG = "a36a6718f54524d846894fb04b5b885b4e43e63b";
            string hashG321 = "27e1c9179581370b9117a0c5cafbdd474364448a";

            brutforce(5, hash1, possibleChars);
            Console.ReadLine();
        }
        private static void brutforce(int charCount, string sha1Hash, List<char> possibleChars)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Console.WriteLine("Processing...");
            int charRangePerWorker = possibleChars.Count / 4;

            Task task1 = Task.Factory.StartNew(() => doStuff(0, charRangePerWorker,sha1Hash,possibleChars));
            Task task2 = Task.Factory.StartNew(() => doStuff(charRangePerWorker, 2 * charRangePerWorker, sha1Hash, possibleChars));
            Task task3 = Task.Factory.StartNew(() => doStuff(2 * charRangePerWorker, 3 * charRangePerWorker, sha1Hash, possibleChars));
            Task task4 = Task.Factory.StartNew(() => doStuff(3 * charRangePerWorker, 4 * charRangePerWorker, sha1Hash, possibleChars));
            Task.WaitAll(task1, task2, task3, task4);
            watch.Stop();
            Console.WriteLine("Done! Benötigte Zeit {0}ms.", watch.ElapsedMilliseconds);
            if (found)
            {
                printResultToFileOnDesktop(watch.ElapsedMilliseconds);
            }
        }

        private static void printResultToFileOnDesktop(long msElapsed)
        {
            string text = "Password found: " + password + "\n\r Elapsed Milliseconds: " + msElapsed;
            File.WriteAllText("result.txt", text);
        }
        static void doStuff(int startIndex, int endIndex, string shahash, List<char> possibleChars)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    Console.WriteLine(possibleChars[i]);
                    if (found)
                        break;
                    for(int i2 = 0; i2 < possibleChars.Count; i2++)
                    {
                        if (found)
                            break;
                        for (int i3 = 0; i3 < possibleChars.Count; i3++)
                        {
                            if (found)
                                break;
                            for (int i4 = 0; i4 < possibleChars.Count; i4++)
                            {
                                if (found)
                                    break;
                                for (int i5 = 0; i5 < possibleChars.Count; i5++)
                                {
                                    if (found)
                                        break;
                                    string possiblePassword = (
                                    possibleChars[i].ToString()
                                    + possibleChars[i2].ToString()
                                    + possibleChars[i3].ToString()
                                    + possibleChars[i4].ToString()).ToString();
                                    var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(possiblePassword));
                                    StringBuilder sb = null;
                                    sb = new StringBuilder(hash.Length * 2);
                                    foreach (byte b in hash)
                                    {
                                        sb.Append(b.ToString("X2"));
                                    }
                                    if (sb.Equals(shahash.ToUpper()))
                                    {
                                        Console.WriteLine("Treffer!!!");
                                        found = true;
                                        password = possiblePassword;
                                    }
                                    Thread.Yield();
                                }
                            }
                        } 
                    }
                }
            }
        }
    }
}