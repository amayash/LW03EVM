using System.Collections;

namespace ConsoleApp1
{
    internal class Program
    {
        static string alphabet = "ABCDEF";
        static string alphabet2pol = "GHIJKLMNOPQRSTUVWXYZ";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Исходная система счисления? (2-16)");
                int start;
                while (!int.TryParse(Console.ReadLine(), out start))
                {
                    Console.WriteLine("Ошибка ввода. Исходная система счисления? (2-16)");
                }
                if (start < 2 || start > 16)
                {
                    Console.WriteLine("Ошибка ввода. Исходная система счисления принята за 10.");
                    start = 10;
                }

                Console.WriteLine("Конечная система счисления? (2-16)");
                int end;
                while (!int.TryParse(Console.ReadLine(), out end))
                {
                    Console.WriteLine("Ошибка ввода. Конечная система счисления? (2-16)");
                }
                if (end < 2 || end > 16)
                {
                    Console.WriteLine("Ошибка ввода. Конечная система счисления принята за 10.");
                    end = 10;
                }

                Console.WriteLine("Введите нужное число для перевода.");
                string number = Console.ReadLine();
                bool checkNumber = true;
                for (int i = 0; i < number.Length; i++)
                {
                    string s = number[i].ToString();
                    if (s == "-")
                    {
                        continue;
                    }
                    if (alphabet.Contains(s))
                    {
                        if (s == "A")
                        {
                            s = "10";
                        }
                        if (s == "B")
                        {
                            s = "11";
                        }
                        if (s == "C")
                        {
                            s = "12";
                        }
                        if (s == "D")
                        {
                            s = "13";
                        }
                        if (s == "E")
                        {
                            s = "14";
                        }
                        if (s == "F")
                        {
                            s = "15";
                        }
                    }
                    else if (alphabet2pol.Contains(s))
                    {
                        s = "16";
                    }
                    int temp = int.Parse(s);
                    if (temp >= start)
                    {
                        checkNumber = false;
                        break;
                    }
                }

                if (checkNumber)
                {
                    if (start == 10 && end != 10)
                    {
                        if (end == 2 && number[0] == '-')
                        {
                            Console.WriteLine(DesVEnd(number, end));
                            Console.WriteLine(PryamoyKod(DesVEnd(number, end)));
                        }
                        else 
                        { Console.WriteLine(DesVEnd(number, end)); }
                    }
                    if (start != 10 && end == 10)
                    {
                        Console.WriteLine(FromNtoDes(number, start));
                    }
                    if (start != 10 && end != 10)
                    {
                        if (end == 2 && number[0] == '-')
                        {
                            Console.WriteLine(FromTo(number, start, end));
                            Console.WriteLine(PryamoyKod(FromTo(number, start, end)));
                        }
                        else { Console.WriteLine(FromTo(number, start, end)); }
                    }
                }
                if (!checkNumber)
                {
                    Console.WriteLine("Ошибка ввода. Неподходящее число в исходной системе счисления.");
                }
                Console.ReadKey();
            }
        }

        public static string DesVEnd(string number, int end)
        {
            string newNum = "";
            int num = Convert.ToInt32(number);
            int ostat = Convert.ToInt32(number);
            if (Convert.ToInt32(number) < 0)
            {
                newNum += "-";
                num = num * (-1);
                ostat = ostat * (-1);
            }
            ArrayList numTemp = new ArrayList();
            while (ostat > 0)
            {
                ostat = ostat / end;
                numTemp.Add(num - ostat * end);
                num = ostat;
            }
            for (int temp = numTemp.Count - 1; temp >= 0; temp--)
                newNum += Character(numTemp[temp].ToString(), "to");
            return newNum;
        }

        public static string PryamoyKod(string s)
        {
            string result = "";
            bool otr = false;
            if (s.StartsWith("-"))
            {
                otr = true;
            }
            s = s.Replace("-", string.Empty);

            if (otr) 
                result += "1";
            for (int i = 0; i < 8 - s.Length - 1; i++)
            {
                result += "0";
            }
            result += s;
            return result;
        }
        static string Character(string tempString, string otk)
        {
            string s = "";
            if (otk == "to")
            {
                if (Convert.ToInt32(tempString) > 10)
                    s += alphabet.Substring(Convert.ToInt32(tempString) - 10, 1);
                else
                    s += tempString;
            }
            else if (otk == "from")
            {
                if (alphabet.IndexOf(tempString) == -1)
                    s += tempString;
                else
                    s += (alphabet.IndexOf(tempString) + 10).ToString();
            }
            return s;
        }
        public static string FromNtoDes(string number, int ss)
        {
            int newNum = 0;
            string temp;
            string numCopy;
            int calc;

            if (number[0].ToString() == "-")
            {
                numCopy = number.Replace("-", string.Empty);
            }
            else
            {
                numCopy = number;
            }
            for (int i = 0; i < numCopy.Length; i++)
            {
                temp = "";
                temp += Character(numCopy.Substring(i, 1), "from");
                calc = (int)Math.Pow(Convert.ToDouble(ss), Convert.ToDouble(numCopy.Length - (i + 1)));
                newNum += Convert.ToInt32(temp) * calc;
            }
            if (number[0].ToString() == "-")
            {
                newNum = -newNum;
            }
            return newNum.ToString();
        }
        public static string FromTo(string number, int ssN, int ssK)
        {
            string temp = FromNtoDes(number, ssN);
            temp = DesVEnd(temp, ssK);
            return temp;
        }

    }
}

