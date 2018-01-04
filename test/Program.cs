using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    /*Вычисли многочлен -2+x^1-3*x^2+100*x^3-2*x^4*/
    class Program
    {
        static void Main(string[] args)
        {
            string str = "-2+x^1-3*x^2+100*x^3-2*x^4";
            if (str.StartsWith("-"))
            {
                str = str.Insert(0, "0");
            }
            int x = 3;

            #region избавляемся от ^ и x
            while (true)
            {
                if (str.IndexOf('^') == -1)
                {
                    break;
                }
                int position = str.IndexOf('^');

                string str2 = str.Substring(position - 1, 3);

                int pow = int.Parse(str2.Substring(2, 1));
                int result = (int)Math.Pow(x, pow);

                str = str.Remove(position - 1, 3);
                str = str.Insert(position - 1, result + "");
            }
            
            str = str.Replace("x", x + "");
            #endregion

            //2+2-3*4+100*8-2*2

            #region формируем коллекции operands operators
            string[] tmp = str.Split('+','-','*');

            List<int> operands = new List<int>();
            List<char> operators = new List<char>();

            operators.Add('s');
            
            for (int i = 0; i < tmp.Length; i++)
                operands.Add(int.Parse(tmp[i]));

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '+' || str[i] == '-' || str[i] == '*')
                    operators.Add(str[i]);
            }
            #endregion

            #region проход в поисках + - *
            //проход в поисках *
            //2 2 3 4 100 8 2 2
            //s + - *   + * - *

            //2 2 12 800 4 
            //s +  -   + - 
            while(true)
            {
                int position = operators.IndexOf('*');
                if (position == -1)
                {
                    break;
                }

                operands[position - 1] = operands[position - 1] * operands[position];
                operands.RemoveAt(position);
                operators.RemoveAt(position);
            }

            //проход в поисках + -
            //788 
            //s   
            int firstOperatorIndex = 1;
            while(true)
            {
                if (operands.Count == 1)
                {
                    break;
                }

                if (operators[firstOperatorIndex] == '+')
                {
                    operands[firstOperatorIndex - 1] = operands[firstOperatorIndex - 1] + operands[firstOperatorIndex];
                    operands.RemoveAt(firstOperatorIndex);
                    operators.RemoveAt(firstOperatorIndex);
                }

                if (operators[1] == '-')
                {
                    operands[firstOperatorIndex - 1] = operands[firstOperatorIndex - 1] - operands[firstOperatorIndex];
                    operands.RemoveAt(1);
                    operators.RemoveAt(1);
                }
            }
            #endregion

            int result2 = operands[firstOperatorIndex - 1];
            Console.WriteLine(result2);
            
            Console.ReadLine();
        }
    }
}