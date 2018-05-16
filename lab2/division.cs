using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_2c
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Int16 dividend, divisor;
            Console.WriteLine("Введіть дільник:");
            dividend = Int16.Parse(Console.ReadLine());
            Console.WriteLine("Введіть ділене:");
            divisor = Int16.Parse(Console.ReadLine());
            Division(dividend, divisor);
            Console.ReadKey();
        }

        static void Division(Int16 dividend, Int16 divisor)
        {
            Int64 register = 0 | dividend;
                int remainder = Convert.ToInt32("11111111111111110000000000000000", 2),
                quotient = Convert.ToInt32("1111111111111111", 2),
                shiftedDivisor = divisor << 16,
                shiftedNegativeDivisor = -divisor << 16;

            const int remainderBitsAmount = 17,
                quotientCount = 16,
                registerCount = 33;

            Console.WriteLine("\tРегістр:\n\t\t   {0}", Stringify(register, registerCount));
            for (int i = 0; i < 16; ++i)
            {
                register <<= 1;
                Console.WriteLine("\tЗсув регістру вліво:\n\t\t   {0}", Stringify(register, registerCount));

                if ((register >> 32 & 1) == 0)
                {
                    Console.WriteLine("Віднімання дільника: {0}", Stringify(shiftedNegativeDivisor, remainderBitsAmount, true));
                    register += shiftedNegativeDivisor;
                    Console.WriteLine("\tРегістр:\n\t\t   {0}", Stringify(register, registerCount));
                }
                else
                {
                    Console.WriteLine("      Додавання здвинутого регістру: {0}", Stringify(shiftedDivisor, remainderBitsAmount, true));
                    register += shiftedDivisor;
                    Console.WriteLine("\tРегістр:\n\t\t   {0}", Stringify(register, registerCount));
                }

                if ((register >> 32 & 1) == 0)
                {
                    register |= 1;
                    Console.WriteLine("\tВстановлення останнього біту частки на 1:\n\t\t   {0}", Stringify(register, registerCount));
                }
                else
                    Console.WriteLine("\tВстановлення останнього біту частки на 0:\n\t\t   {0}", Stringify(register, registerCount));
            }

            if ((register >> 32 & 1) == 1)
            {
                Console.WriteLine("      Додавання дільника: {0}", Stringify(shiftedDivisor, remainderBitsAmount, true));
                register += shiftedDivisor;
                Console.WriteLine("\tРегістр:\n\t\t   {0}", Stringify(register, registerCount));
            }

            Console.WriteLine("\tВідповідь:");
           
            Console.WriteLine("\t\tЧастка:\t      {0} (Десяткове: {1})",
                Stringify(register & quotient, quotientCount),
                register & quotient);
            Console.WriteLine("\t\tОстача:\t    {0} (Десяткове: {1})",
               Stringify(register & remainder, remainderBitsAmount, true),
               (register & remainder) >> 16);
        }

        static string Stringify(Int64 register, byte count, bool isDivisor = false)
        {
            string result = string.Empty;

            int last_index = isDivisor ? 15 : -1;
            for (int i = count - 1 + (isDivisor ? 16 : 0); i > last_index; --i)
                result += (register >> i & 1) + (i % 4 == 0 && i != 0 ? " " : "");

            return result;
        }

    }
}