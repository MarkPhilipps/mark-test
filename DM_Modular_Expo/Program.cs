using System.Numerics;

namespace DM_Modular_Expo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C# Funktion:");
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
           

            BigInteger calc1 = ModCalc(5, 99, 11);
            BigInteger calc2 = ModCalc(50, 529, 13);
            BigInteger calc3 = ModCalc(50, 999, 17);
            BigInteger calc4 = ModCalc(1819, 13, 2537);

            watch.Stop();
            Console.WriteLine("Ergenis in {0}ms.\nErgebnis 1: {1}\nErgebnis 2: {2}\nErgebnis 3: {3}\nErgebnis 4: {4} ", watch.ElapsedMilliseconds, calc1, calc2, calc3, calc4);

            Console.WriteLine("Eigene Funktion:");
            watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            calc1 = ModCalcOwn(5, 99, 11);
            calc2 = ModCalcOwn(50, 529, 13);
            calc3 = ModCalcOwn(50, 999, 17);
            calc4 = ModCalcOwn(1819, 13, 2537);
            watch.Stop();
            Console.WriteLine("Ergenis in {0}ms.\nErgebnis 1: {1}\nErgebnis 2: {2}\nErgebnis 3: {3}\nErgebnis 4: {4} ", watch.ElapsedMilliseconds, calc1, calc2, calc3, calc4);
            Console.ReadKey();
        }

        static BigInteger ModCalc(int x, int power, int modInt)
        {
            BigInteger calc = BigInteger.ModPow(x, power, modInt);
            return calc;
        }

        static BigInteger ModCalcOwn(BigInteger base1, BigInteger exponent, BigInteger modulus)
        {
            BigInteger result = 1;

            base1 %= modulus;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                    result = (result * base1) % modulus;

                var test = exponent.ToByteArray();
                exponent >>= 1;
                var test2 = exponent.ToByteArray();
                base1 = (base1 * base1) % modulus;
            }

            return result;
        }
    }
}