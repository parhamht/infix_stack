using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infix
{
    public class Stack
    {
        public int Top;
        public int Max_Size;
        public string[] stack;
        public Stack(int maxsize)
        {
            Max_Size = maxsize;
            stack = new string[Max_Size];
            Top = -1;
        }
        public bool IsFull()
        {
            if (Top == Max_Size - 1)
                return true;
            else
                return false;
        }
        public bool IsEmpty()
        {
            if (Top == -1)
                return true;
            else
                return false;
        }
        public void Add(string x)
        {
            if (IsFull() == true)
                Console.WriteLine("Stack is full.");
            else
                stack[++Top] = x;
        }
        public string Delete()
        {
            if (IsEmpty() == true)
            {
                Console.WriteLine("Stack is empty.");
                return "0";
            }
            else
                return stack[Top--];
        }
        public int Operator(char x) // in stack so "(" is the lowest.
        {
            if (x == '(')
                return 0;
            if (x == '+' || x == '-')
                return 1;
            if (x == '*' || x == '/')
                return 2;
            else
                return 3;
        }
        public int menu()
        {
            Console.WriteLine("which operation you want to use?");
            Console.WriteLine("1. infix to postfix.");
            Console.WriteLine("2. infix to prefix.");
            Console.WriteLine("3. postfix to infix.");
            Console.WriteLine("4. prefix to infix.");
            Console.WriteLine("5. postfix to prefix.");
            Console.WriteLine("6. prefix to postfix.");
            Console.WriteLine("7. finish.");
            int a = Convert.ToInt32(Console.ReadLine());
            return a;
        }
        public void Display()
        {
            for (Top = 0; Top != Max_Size; Top++)
            {
                Console.Write(stack[Top]);
            }
        }
        public string Reverse(string x)
        {
            string a = "";
            for (int i = 0; i < x.Length; i++)
            {
                char c = x[i];
                a = c + a;
            }
            return a;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Stack s = new Stack(50);
                int k = s.menu();
                while (k != 7)
                {
                    if (k == 1)
                    {
                        string a;
                        Console.WriteLine("Enter your infix expression: ");
                        string x = Console.ReadLine();
                        Console.WriteLine("your postfix expression is: ");
                        for (int i = 0; i < x.Length; i++)
                        {
                            if ((x[i] >= 'a' && x[i] <= 'z') || x[i] >= 'A' && x[i] <= 'Z' || x[i] >= '1' && x[i] <= '9')
                                Console.Write(x[i]);
                            else if (x[i] == ' ')
                            {
                                i++;
                                continue;
                            }
                            else if (x[i] == '(')
                                s.Add(Convert.ToString((x[i])));
                            else if (x[i] == ')')
                            {
                                while ((a = s.Delete()) != "(") // stack is LIFO
                                    Console.Write(a);
                            }
                            else
                            {
                                if (s.Top != -1 && s.Operator(Convert.ToChar(s.stack[s.Top])) >= s.Operator(x[i]))
                                    Console.Write(s.Delete());
                                s.Add(Convert.ToString((x[i])));
                            }
                        }
                        while (s.Top != -1)
                            Console.Write(s.Delete());
                        Console.WriteLine();
                        k = s.menu();
                    }
                    else if (k == 2)
                    {
                        string o = "";
                        string a;
                        Console.WriteLine("Enter your infix expression: ");
                        string x = Console.ReadLine();
                        Console.WriteLine("your prefix expression is: ");
                        string y = s.Reverse(x);
                        for (int i = 0; i < y.Length; i++)
                        {
                            if (y[i] == ')')
                            {
                                y = y.Remove(i, 1);
                                y = y.Insert(i, "(");
                            }
                            else if (y[i] == '(')
                            {
                                y = y.Remove(i, 1);
                                y = y.Insert(i, ")");
                            }
                        }
                        for (int i = 0; i < y.Length; i++)
                        {
                            if ((y[i] >= 'a' && y[i] <= 'z') || y[i] >= 'A' && y[i] <= 'Z' || y[i] >= '1' && y[i] <= '9')
                                o += y[i];
                            else if (y[i] == ' ')
                            {
                                i++;
                                continue;
                            }
                            else if (y[i] == '(')
                                s.Add(Convert.ToString((y[i])));
                            else if (y[i] == ')')
                            {
                                while ((a = s.Delete()) != "(") // stack is LIFO
                                    o += a;
                            }
                            else
                            {
                                if (s.Top != -1 && s.Operator(Convert.ToChar(s.stack[s.Top])) >= s.Operator(y[i]))
                                    o += s.Delete();
                                s.Add(Convert.ToString(y[i]));
                            }
                        }
                        while (s.Top != -1)
                            o += s.Delete();
                        o = s.Reverse(o);
                        Console.WriteLine(o);
                        k = s.menu();
                    }
                    else if (k == 3)
                    {
                        string a;
                        string b;
                        Console.WriteLine("Enter your postfix expression: ");
                        string x = Console.ReadLine();
                        Console.WriteLine("your infix expression is: ");
                        for (int i = 0; i < x.Length; i++)
                        {
                            if ((x[i] >= 'a' && x[i] <= 'z') || x[i] >= 'A' && x[i] <= 'Z' || x[i] >= '1' && x[i] <= '9')
                                s.Add(Convert.ToString((x[i])));
                            else
                            {
                                a = s.Delete(); //first one.
                                b = s.Delete(); //seconde one.
                                string c = ($"({b}{x[i]}{a})");
                                s.Add(c);
                            }
                        }
                        while (s.Top != -1)
                            Console.WriteLine(s.Delete());
                        k = s.menu();
                    }
                    else if (k == 4)
                    {
                        string a;
                        string b;
                        Console.WriteLine("Enter your prefix expression: ");
                        string x = Console.ReadLine();
                        Console.WriteLine("your infix expression is: ");
                        for (int i = x.Length - 1; i >= 0; i--)
                        {
                            if ((x[i] >= 'a' && x[i] <= 'z') || x[i] >= 'A' && x[i] <= 'Z' || x[i] >= '1' && x[i] <= '9')
                                s.Add(Convert.ToString((x[i])));
                            else
                            {
                                a = s.Delete(); //first one.
                                b = s.Delete(); //seconde one.
                                string c = ($"({a}{x[i]}{b})");
                                s.Add(c);
                            }
                        }
                        while (s.Top != -1)
                            Console.WriteLine(s.Delete());
                        k = s.menu();
                    }
                    else if (k == 5) //first convert postfix to infix then convert infix to prefix.
                    {
                        string a;
                        string b;
                        Console.WriteLine("Enter your postfix expression: ");
                        string x = Console.ReadLine();
                        Console.WriteLine("your prefix expression is: ");
                        for (int i = 0; i < x.Length; i++)
                        {
                            if ((x[i] >= 'a' && x[i] <= 'z') || x[i] >= 'A' && x[i] <= 'Z' || x[i] >= '1' && x[i] <= '9')
                                s.Add(Convert.ToString((x[i])));
                            else
                            {
                                a = s.Delete();
                                b = s.Delete(); 
                                string c = ($"({b}{x[i]}{a})");
                                s.Add(c);
                            }
                        }
                        string w = s.Delete();
                        string o = "";
                        string y = s.Reverse(w);
                        for (int i = 0; i < y.Length; i++)
                        {
                            if (y[i] == ')')
                            {
                                y = y.Remove(i, 1);
                                y = y.Insert(i, "(");
                            }
                            else if (y[i] == '(')
                            {
                                y = y.Remove(i, 1);
                                y = y.Insert(i, ")");
                            }
                        }
                        for (int i = 0; i < y.Length; i++)
                        {
                            if ((y[i] >= 'a' && y[i] <= 'z') || y[i] >= 'A' && y[i] <= 'Z' || y[i] >= '1' && y[i] <= '9')
                                o += y[i];
                            else if (y[i] == ' ')
                            {
                                i++;
                                continue;
                            }
                            else if (y[i] == '(')
                                s.Add(Convert.ToString((y[i])));
                            else if (y[i] == ')')
                            {
                                while ((a = s.Delete()) != "(") // stack is LIFO
                                    o += a;
                            }
                            else
                            {
                                if (s.Top != -1 && s.Operator(Convert.ToChar(s.stack[s.Top])) >= s.Operator(y[i]))
                                    o += s.Delete();
                                s.Add(Convert.ToString(y[i]));
                            }
                        }
                        while (s.Top != -1)
                            o += s.Delete();
                        o = s.Reverse(o);
                        Console.WriteLine(o);
                        k = s.menu();
                    }
                    else if (k == 6) //first convert prefix to infix then convert infix to postfix.
                    {
                        string a;
                        string b;
                        Console.WriteLine("Enter your prefix expression: ");
                        string x = Console.ReadLine();
                        Console.WriteLine("your postfix expression is: ");
                        for (int i = x.Length - 1; i >= 0; i--)
                        {
                            if ((x[i] >= 'a' && x[i] <= 'z') || x[i] >= 'A' && x[i] <= 'Z' || x[i] >= '1' && x[i] <= '9')
                                s.Add(Convert.ToString((x[i])));
                            else
                            {
                                a = s.Delete();
                                b = s.Delete();
                                string c = ($"({a}{x[i]}{b})");
                                s.Add(c);
                            }
                        }
                        string w = s.Delete();
                        for (int i = 0; i < w.Length; i++)
                        {
                            if ((w[i] >= 'a' && w[i] <= 'z') || w[i] >= 'A' && w[i] <= 'Z' || w[i] >= '1' && w[i] <= '9')
                                Console.Write(w[i]);
                            else if (w[i] == ' ')
                            {
                                i++;
                                continue;
                            }
                            else if (w[i] == '(')
                                s.Add(Convert.ToString((w[i])));
                            else if (w[i] == ')')
                            {
                                while ((a = s.Delete()) != "(") // stack is LIFO
                                    Console.Write(a);
                            }
                            else
                            {
                                if (s.Top != -1 && s.Operator(Convert.ToChar(s.stack[s.Top])) >= s.Operator(w[i]))
                                    Console.Write(s.Delete());
                                s.Add(Convert.ToString((w[i])));
                            }
                        }
                        while (s.Top != -1)
                            Console.Write(s.Delete());
                        Console.WriteLine();
                        k = s.menu();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}