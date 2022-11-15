namespace sha1_brutforce
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<char> possibleChars = new List<char>() {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
                'a', 'b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                'A', 'B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
            };

            string hash1 = "5aea476328379d3bff2204501bb57aa8b4268fac";
            string hash2 = "d31d62ed0af022248e28fc0dc4a9580217987e55";
            string hash3 = "66ceeafde8453dda201978b2b497b9c85d4b6da5";

            brutforce5(hash1, possibleChars);
            Console.ReadLine();
        }
        private static void brutforce5(string sha1Hash, List<char> possibleChars)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Console.WriteLine("Processing...");
            Int64 passwordCount = (Int64)Math.Pow(possibleChars.Count, 2);
            Int64 passwordCountPerWorker = passwordCount / 4;
            Int64 worker1Start = 0;
            Int64 worker1End = passwordCountPerWorker-1;
            Int64 worker2Start = passwordCountPerWorker;
            Int64 worker2End = 2* passwordCountPerWorker - 1;
            Int64 worker3Start = 2* passwordCountPerWorker;
            Int64 worker3End = 3 * passwordCountPerWorker - 1;
            Int64 worker4Start = 3 * passwordCountPerWorker;
            Int64 worker4End = 4 * passwordCountPerWorker;
            watch.Stop();
            Console.WriteLine("Done! Benötigte Zeit {0}ms.", watch.ElapsedMilliseconds);
        }
    }
}