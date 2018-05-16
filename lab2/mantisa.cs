using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace heh
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Введіть a = ");
            float a = float.Parse(Console.ReadLine());
            Console.Write("Введіть b = ");
            float b = float.Parse(Console.ReadLine());

            string aString = Dec_BinString(a), bString = Dec_BinString(b);
            int aSign = int.Parse(aString[0].ToString()), bSign = int.Parse(bString[0].ToString());
            int aEx = Convert.ToInt32(aString.Substring(1, 8), 2), bEx = Convert.ToInt32(bString.Substring(1, 8), 2);
            int aMantisa = Convert.ToInt32(aString.Substring(9), 2), bMantisa = Convert.ToInt32(bString.Substring(9), 2);
            int resultSign = 0, resultEx = aEx, resultMantisa = 0;

            aMantisa = aMantisa + (1 << 23);
            bMantisa = bMantisa + (1 << 23);

            //get large exp and shift mantisa
            if (aEx > bEx)
            {
                resultEx = aEx;
                bMantisa = bMantisa >> (aEx - bEx);
            }
            if (bEx > aEx)
            {
                resultEx = bEx;
                aMantisa = aMantisa >> (bEx - aEx);
            }
            //Align binary points

            Console.WriteLine("Результуюча експонента: " + new string('0', 8 - Convert.ToString(resultEx, 2).Length) + Convert.ToString(resultEx, 2));
            Console.WriteLine("Здвинута мантіса а: " + new string('0', 32 - Convert.ToString(aMantisa, 2).Length) + Convert.ToString(aMantisa, 2));
            Console.WriteLine("Здвинута мантіса b: " + new string('0', 32 - Convert.ToString(bMantisa, 2).Length) + Convert.ToString(bMantisa, 2));

            //get sign and +/- mantisa
            if (aSign == bSign)
            {
                resultSign = aSign;
                resultMantisa = aMantisa + bMantisa;
            }
            else if (aMantisa > bMantisa && aSign == 0 && bSign == 1)
            {
                resultSign = 0;
                resultMantisa = aMantisa - bMantisa;
            }
            else if (bMantisa > aMantisa && aSign == 0 && bSign == 1)
            {
                resultSign = 1;
                resultMantisa = bMantisa - aMantisa;
            }
            else if (aMantisa > bMantisa && aSign == 1 && bSign == 0)
            {
                resultSign = 1;
                resultMantisa = aMantisa - bMantisa;
            }
            else if (bMantisa > aMantisa && aSign == 1 && bSign == 0)
            {
                resultSign = 0;
                resultMantisa = bMantisa - aMantisa;
            }


            //significants
            Console.WriteLine("додавання значень: " +
                new string('0', 32 - Convert.ToString(resultMantisa, 2).Length) + Convert.ToString(resultMantisa, 2));


            while ((resultMantisa >> 24) > 0)
            {
                resultMantisa = resultMantisa >> 1;
                resultEx++;
            }
            while ((resultMantisa & (1 << 23)) != (1 << 23))
            {
                resultMantisa = resultMantisa << 1;
                resultEx--;

            }

            resultMantisa = resultMantisa & ((1 << 23) - 1);
            string resultExString = new string('0', 8 - Convert.ToString(resultEx, 2).Length) + Convert.ToString(resultEx, 2);
            string resultMantisaString = new string('0', 23 - Convert.ToString(resultMantisa, 2).Length) + Convert.ToString(resultMantisa, 2);
            string resultString = resultSign.ToString() + resultExString + resultMantisaString;



            Console.WriteLine("Результуюча експонента = " + Convert.ToString(resultEx, 2));
            Console.WriteLine("Результуюча мантіса = " + Convert.ToString(resultMantisa, 2));

            Console.WriteLine("Результат   :" + resultString + "          (десяткове: {0})", Bin_Dec(resultString));

            Console.ReadLine();
        }

        public static string Dec_BinString(float f)
        {
            byte[] b = BitConverter.GetBytes(f);
            int i = BitConverter.ToInt32(b, 0);
            string result = Convert.ToString(i, 2);
            return new String('0', 32 - result.Length) + result;
        }

        public static float Bin_Dec(string s)
        {
            int i = Convert.ToInt32(s, 2);
            byte[] b = BitConverter.GetBytes(i);
            return BitConverter.ToSingle(b, 0);

        }

        public static int bMantisa { get; set; }
    }
}