using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace sha1_brutforce
{
    internal class Program
    {
        static bool found = false;
        static string password = "";
        static void Main(string[] args)
        {
            int workerCount = 16;

            var possibleChars = getPossibleChars();
            var hash = getSearchedHash();
            var charCountPerWorker = getCharCountPerWorker(workerCount);
            

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Console.WriteLine("Processing...");
            Task[] workers = getWorkers(workerCount, charCountPerWorker, hash, possibleChars);
            Task.WaitAll(workers);
            watch.Stop();
            Console.WriteLine("Done! Benötigte Zeit {0}ms.", watch.ElapsedMilliseconds);
            if (found)
            {
                printResultToFile(watch.ElapsedMilliseconds);
                Console.WriteLine("Passwort wurde gefunden und gespeichert.");
            }
            Console.ReadLine();
        }
        private static List<Char> getPossibleChars()
        {
            List<char> possibleChars = new List<char>() {
                'a', 'q', 'G', 'W', 
                'b', 'r', 'H', 'X', 
                'c', 's', 'I', 'Y',
                'd', 't', 'J', 'Z',
                'e', 'u', 'K', '0',
                'f', 'v', 'L', '1',
                'g', 'w', 'M', '2',
                'h', 'x', 'N', '3',
                'i', 'y', 'O', '4',
                'j', 'z', 'P', '5',
                'k', 'A', 'Q', '6',
                'l', 'B', 'R', '7',
                'm', 'C', 'S', '8',
                'n', 'D', 'T', '9',
                'o', 'E', 'U',
                'p', 'F', 'V'
            };
            /*List<char> possibleChars = new List<char>() {
                '0', '1', '2', '3',
                '4', '5', '6', '7',
                '8', '9', 'a', 'b',
                'c', 'd', 'e', 'f',
                'g', 'h', 'i', 'j',
                'k', 'l', 'm', 'n',
                'o', 'p', 'q', 'r',
                's', 't', 'u', 'v',
                'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D',
                'E', 'F', 'G', 'H',
                'I', 'J', 'K', 'L',
                'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X',
                'Y', 'Z'
            };*/
            return possibleChars;
        }
        private static String getSearchedHash()
        {
            string hash1 = "5aea476328379d3bff2204501bb57aa8b4268fac";
            string hash2 = "d31d62ed0af022248e28fc0dc4a9580217987e55";
            string hash3 = "66ceeafde8453dda201978b2b497b9c85d4b6da5";

            string hashZ0123 = "b3f665e01b7ef7a1d0d34bd46ea531d3114457c9";
            string hash0012Z = "618d3cf2978ad5f4683851df2196e43c619fc795";

            string hashaaaaaaaaaa = "3495ff69d34671d1e15b33a63c1379fdedd3a32a";

            return hash2;
        }
        private static int getCharCountPerWorker(int workerCount)
        {
            return 4;
        }
        private static Task[] getWorkers (int workerCount, int charCountPerWorker, string searchedHash, List<Char> possibleChars)
        {
            Task[] workers = new Task[workerCount];
            Task task0 = Task.Factory.StartNew(() => doStuff(0, 3, searchedHash, possibleChars));
            workers[0] = task0;
            Task task1 = Task.Factory.StartNew(() => doStuff(4, 7, searchedHash, possibleChars));
            workers[1] = task1;
            Task task2 = Task.Factory.StartNew(() => doStuff(8, 11, searchedHash, possibleChars));
            workers[2] = task2;
            Task task3 = Task.Factory.StartNew(() => doStuff(12, 15, searchedHash, possibleChars));
            workers[3] = task3;
            Task task4 = Task.Factory.StartNew(() => doStuff(16, 19, searchedHash, possibleChars));
            workers[4] = task4;
            Task task5 = Task.Factory.StartNew(() => doStuff(20, 23, searchedHash, possibleChars));
            workers[5] = task5;
            Task task6 = Task.Factory.StartNew(() => doStuff(24, 27, searchedHash, possibleChars));
            workers[6] = task6;
            Task task7 = Task.Factory.StartNew(() => doStuff(28, 31, searchedHash, possibleChars));
            workers[7] = task7;
            Task task8 = Task.Factory.StartNew(() => doStuff(32, 35, searchedHash, possibleChars));
            workers[8] = task8;
            Task task9 = Task.Factory.StartNew(() => doStuff(36, 39, searchedHash, possibleChars));
            workers[9] = task9;
            Task task10 = Task.Factory.StartNew(() => doStuff(40, 43, searchedHash, possibleChars));
            workers[10] = task10;
            Task task11 = Task.Factory.StartNew(() => doStuff(44, 47, searchedHash, possibleChars));
            workers[11] = task11;
            Task task12 = Task.Factory.StartNew(() => doStuff(48, 51, searchedHash, possibleChars));
            workers[12] = task12;
            Task task13 = Task.Factory.StartNew(() => doStuff(52, 55, searchedHash, possibleChars));
            workers[13] = task13;
            Task task14 = Task.Factory.StartNew(() => doStuff(56, 58, searchedHash, possibleChars));
            workers[14] = task14;
            Task task15 = Task.Factory.StartNew(() => doStuff(59, 61, searchedHash, possibleChars));
            workers[15] = task15;
            return workers;
        }
        private static void printResultToFile(long msElapsed)
        {
            string text = "Password found: " + password + "\n\r Elapsed Milliseconds: " + msElapsed;
            File.WriteAllText("result.txt", text);
        }
        private static void doStuff(int startIndex, int endIndex, string shahash, List<char> possibleChars)
        {
            brut10(startIndex, endIndex, shahash, possibleChars);
        }
        private static void brut5(int startIndex, int endIndex, string shahash, List<char> possibleChars)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    Console.WriteLine("Worker started to work with passwords with letter {0}",possibleChars[i]);
                    
                    if (found)
                        break;
                    for (int i2 = 0; i2 < possibleChars.Count; i2++)
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
                                    + possibleChars[i4].ToString()
                                    + possibleChars[i5].ToString()).ToString();
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
        private static void brut10(int startIndex, int endIndex, string shahash, List<char> possibleChars)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                for (int i1 = startIndex; i1 <= endIndex; i1++)
                {
                    Console.WriteLine("Worker started to work with passwords with letter {0}", possibleChars[i1]);

                    if (found)
                        break;
                    for (int i2 = 0; i2 < possibleChars.Count; i2++)
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
                                    for (int i6 = 0; i6 < possibleChars.Count; i6++)
                                    {
                                        if (found)
                                            break;
                                        for (int i7 = 0; i7 < possibleChars.Count; i7++)
                                        {
                                            if (found)
                                                break;
                                            for (int i8 = 0; i8 < possibleChars.Count; i8++)
                                            {
                                                if (found)
                                                    break;
                                                for (int i9 = 0; i9 < possibleChars.Count; i9++)
                                                {
                                                    if (found)
                                                        break;
                                                    for (int i10 = 0; i10 < possibleChars.Count; i10++)
                                                    {
                                                        if (found)
                                                            break;
                                                        string possiblePassword = (
                                                        possibleChars[i1].ToString()
                                                        + possibleChars[i2].ToString()
                                                        + possibleChars[i3].ToString()
                                                        + possibleChars[i4].ToString()
                                                        + possibleChars[i5].ToString()
                                                        + possibleChars[i6].ToString()
                                                        + possibleChars[i7].ToString()
                                                        + possibleChars[i8].ToString()
                                                        + possibleChars[i9].ToString()
                                                        + possibleChars[i10].ToString()).ToString();
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
                }
            }
        }
    }
}