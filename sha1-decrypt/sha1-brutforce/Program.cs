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
            int workerCount = 12;

            var possibleChars = getPossibleChars();
            var hash = getSearchedHash();
            var charCountPerWorker = 5;
            

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Console.WriteLine("Processing...");
            Task[] workers = getWorkers(workerCount, charCountPerWorker, hash, possibleChars);
            Task.WaitAll(workers);
            watch.Stop();
            double timeSpent = watch.ElapsedMilliseconds / 1000;
            Console.WriteLine("Done! Benötigte Zeit {0} Sekunden.", timeSpent);
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
                '0', '1', '2', '3', '4',
                '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e',
                'f', 'g', 'h', 'i', 'j',
                'k', 'l', 'm', 'n', 'o',
                'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y',
                'z', 'A', 'B', 'C', 'D',
                'E', 'F', 'G', 'H', 'I',
                'J', 'K', 'L', 'M', 'N',
                'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z'
            };
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

            return hash1;
        }
        private static Task[] getWorkers (int workerCount, int charCountPerWorker, string searchedHash, List<Char> possibleChars)
        {
            Task[] workers = new Task[workerCount];
            Task task0 = Task.Factory.StartNew(() => doStuff(0, 4, searchedHash, possibleChars));
            workers[0] = task0;
            Task task1 = Task.Factory.StartNew(() => doStuff(5, 9, searchedHash, possibleChars));
            workers[1] = task1;
            Task task2 = Task.Factory.StartNew(() => doStuff(10, 14, searchedHash, possibleChars));
            workers[2] = task2;
            Task task3 = Task.Factory.StartNew(() => doStuff(15, 19, searchedHash, possibleChars));
            workers[3] = task3;
            Task task4 = Task.Factory.StartNew(() => doStuff(20, 24, searchedHash, possibleChars));
            workers[4] = task4;
            Task task5 = Task.Factory.StartNew(() => doStuff(25, 29, searchedHash, possibleChars));
            workers[5] = task5;
            Task task6 = Task.Factory.StartNew(() => doStuff(30, 34, searchedHash, possibleChars));
            workers[6] = task6;
            Task task7 = Task.Factory.StartNew(() => doStuff(35, 39, searchedHash, possibleChars));
            workers[7] = task7;
            Task task8 = Task.Factory.StartNew(() => doStuff(40, 44, searchedHash, possibleChars));
            workers[8] = task8;
            Task task9 = Task.Factory.StartNew(() => doStuff(45, 49, searchedHash, possibleChars));
            workers[9] = task9;
            Task task10 = Task.Factory.StartNew(() => doStuff(50, 55, searchedHash, possibleChars));
            workers[10] = task10;
            Task task11 = Task.Factory.StartNew(() => doStuff(56, 61, searchedHash, possibleChars));
            workers[11] = task11;
            return workers;
        }
        private static void printResultToFile(long msElapsed)
        {
            string text = "Password found: " + password + "\n\r Elapsed Milliseconds: " + msElapsed;
            File.WriteAllText("result.txt", text);
        }
        private static void doStuff(int startIndex, int endIndex, string shahash, List<char> possibleChars)
        {
            brut5(startIndex, endIndex, shahash, possibleChars);
        }
        private static void brut5(int startIndex, int endIndex, string shahash, List<char> possibleChars)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (found)
                        break;
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