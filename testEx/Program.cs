using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace testEx
{
    class MainClass
    {
        static object locker = new object();

        public static void Main(string[] args)
        {
            Console.Write("Vvedite text =>  ");
            string text = Console.ReadLine();

            Console.Clear();

            Console.WriteLine("\t\tMenu\t\t");

            

            Console.WriteLine("Vyberite neobhodimuyu operaciyu dlya raboty s vawim tekstom:\n");
            Console.WriteLine("1.Kol-vo simvolov\n2.Kol-vo slov\n3.Kol-vo predlojeniy\n4.Kol-vo voprositel'nih predlojeniy\n5.Kol-vo vosklicatel'nih predlojeniy\n" +
                "6.Summa vseh chisel v tekste\n7.Vse dannye\n8.Exit");
            int input = Convert.ToInt32(Console.ReadLine());

            Console.Clear();


            Thread thread;
          

            while (true)
            {
                
                    if (input == 1)
                    {
                        thread = new Thread(new ParameterizedThreadStart(CheckSymbols));
                        thread.Start(text);
                        break;

                    }
                    else if(input == 2)
                    {
                        thread = new Thread(new ParameterizedThreadStart(CheckWords));
                        thread.Start(text);
                        break;
                    }
                    else if(input == 3)
                    {
                        thread = new Thread(new ParameterizedThreadStart(CheckSentence));
                        thread.Start(text);
                        break;
                    }
                    else if (input == 4)
                    {
                        thread = new Thread(new ParameterizedThreadStart(CheckInterrogative));
                        thread.Start(text);
                        break;
                    }
                    //v fail zapis i avtomatichski otkyvaetsya file
                    //
                    else if (input == 5)
                    {
                        thread = new Thread(new ParameterizedThreadStart(CheckExclamatory));
                        thread.Start(text);
                        break;
                    }
                    else if (input == 6)
                    {
                        thread = new Thread(new ParameterizedThreadStart(CheckDigits));
                        thread.Start(text);
                        break;
                    }
                    else if (input == 7)
                    {
                        thread = new Thread(new ParameterizedThreadStart(WriteAll));
                        thread.Start(text);
                        break;
                    }
                    else if(input == 8)
                    {
                        Console.WriteLine("Have a good day!Bye!");
                        break;
                    }
                    
                

            }




        }


        private static void CheckSymbols(object txt)
        {
            string text = (string)txt;
            int checkSymbols = Regex.Matches(text, @"[@#$%^&*]").Count;

            Console.WriteLine("\t\tVyvod\t\t");
            Console.WriteLine("1.Konsol'\n2.V fail");
            int checkInput = Convert.ToInt32(Console.ReadLine());



            Console.Clear();

            if (checkInput == 1)
            {
                Console.WriteLine("Kol-vo simvolov => " + checkSymbols);
            }
            else if(checkInput == 2)
            {

                Console.WriteLine("Nazovite fail(bez .txt)");
                string file = Console.ReadLine();

                Console.WriteLine("Done!");
          

                using (FileStream fstream = new FileStream($"{file}.txt", FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes("Kol-vo simvolov => " + checkSymbols);
                    fstream.Write(array, 0, array.Length);
               
                }

                System.Diagnostics.Process.Start($"/Users/seda/Projects/testEx/testEx/bin/Debug/{file}.txt");

            }
            else
            {
                Console.WriteLine("Nekorrektniy vvod");
            }

        }

        private static void CheckWords(object txt)
        {
            string text = (string)txt;
            int checkWords = Regex.Matches(text, @"[\w]+").Count;

            Console.WriteLine("\t\tVyvod\t\t");
            Console.WriteLine("1.Konsol'\n2.V fail");
            int checkInput = Convert.ToInt32(Console.ReadLine());



            Console.Clear();

            if (checkInput == 1)
            {
                Console.WriteLine("Kol-vo slov v predlojenii => " + checkWords);
            }
            else if (checkInput == 2)
            {
                Console.WriteLine("Nazovite fail(bez .txt)");
                string file = Console.ReadLine();

                Console.WriteLine("Done!");
             

                using (FileStream fstream = new FileStream($"{file}.txt", FileMode.OpenOrCreate))
                {         
                    byte[] array = System.Text.Encoding.Default.GetBytes("Kol-vo slov v predlojenii => " + checkWords);
                    fstream.Write(array, 0, array.Length);

                }

                System.Diagnostics.Process.Start($"/Users/seda/Projects/testEx/testEx/bin/Debug/{file}.txt");
            }
            else
            {
                Console.WriteLine("Nekorrektniy vvod");
            }

        }

       

        public static  void CheckSentence(object txt)
        {
            string text = (string)txt;
            int checkSentence = Regex.Matches(text, @"[!?.]").Count;

            Console.WriteLine("\t\tVyvod\t\t");
            Console.WriteLine("1.Konsol'\n2.V fail");
            int checkInput = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            if (checkInput == 1)
            {
                Console.WriteLine("Kol-vo predlojeniy => " + checkSentence);
            }
            else if (checkInput == 2)
            {

                Console.WriteLine("Done!");
                Console.WriteLine("Nazovite fail(bez .txt)");
                string file = Console.ReadLine();

                
                using (FileStream fstream = new FileStream($"{file}.txt", FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes("Kol-vo predlojeniy => " + checkSentence);
                    fstream.Write(array, 0, array.Length);

                }

                System.Diagnostics.Process.Start($"/Users/seda/Projects/testEx/testEx/bin/Debug/{file}.txt");
            }
            else
            {
                Console.WriteLine("Nekorrektniy vvod");
            }

         
        }

        public static void CheckInterrogative(object txt)
        {
            string text = (string)txt;
            int checkInterrogative = Regex.Matches(text, @"[?]").Count;

            Console.WriteLine("\t\tVyvod\t\t");
            Console.WriteLine("1.Konsol'\n2.V fail");
            int checkInput = Convert.ToInt32(Console.ReadLine());



            Console.Clear();

            if (checkInput == 1)
            {
                Console.WriteLine("Kol-vo voprositel'nih predlojeniy => " + checkInterrogative);
            }
            else if (checkInput == 2)
            {
                Console.WriteLine("Nazovite fail(bez .txt)");
                string file = Console.ReadLine();

                Console.WriteLine("Done!");


                using (FileStream fstream = new FileStream($"{file}.txt", FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes("Kol-vo voprositel'nih predlojeniy => " + checkInterrogative);
                    fstream.Write(array, 0, array.Length);

                }

                System.Diagnostics.Process.Start($"/Users/seda/Projects/testEx/testEx/bin/Debug/{file}.txt");
            }
            else
            {
                Console.WriteLine("Nekorrektniy vvod");
            }

          
        }


        public static void CheckExclamatory(object txt)
        {
            string text = (string)txt;
            int checkExclamatory = Regex.Matches(text, @"[!]").Count;

            Console.WriteLine("\t\tVyvod\t\t");
            Console.WriteLine("1.Konsol'\n2.V fail");
            int checkInput = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            if (checkInput == 1)
            {
                Console.WriteLine("Kol-vo vosklicatel'niy predlojeniy => " + checkExclamatory);
            }
            else if (checkInput == 2)
            {
                Console.WriteLine("Nazovite fail(bez .txt)");
                string file = Console.ReadLine();

                Console.WriteLine("Done!");

                using (FileStream fstream = new FileStream($"{file}.txt", FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes("Kol-vo vosklicatel'niy predlojeniy => " + checkExclamatory);
                    fstream.Write(array, 0, array.Length);

                }

                System.Diagnostics.Process.Start($"/Users/seda/Projects/testEx/testEx/bin/Debug/{file}.txt");
            }
            else
            {
                Console.WriteLine("Nekorrektniy vvod");
            }
        }

        public static void CheckDigits(object txt)
        {
            string text = (string)txt;
            int checkDigits =  Regex.Matches(text, @"[\d]").Cast<Match>()
                  .Select(x => int.Parse(x.Value))
                  .Sum();

        
            Console.WriteLine("\t\tVyvod\t\t");
            Console.WriteLine("1.Konsol'\n2.V fail");
            int checkInput = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            if (checkInput == 1)
            {
                Console.WriteLine("Kol-vo cifr v predlojenii => " + checkDigits);
            }
            else if (checkInput == 2)
            {

                Console.WriteLine("Nazovite fail(bez .txt)");
                string file = Console.ReadLine();


                Console.WriteLine("Done!");

                using (FileStream fstream = new FileStream($"{file}.txt", FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes("Kol-vo cifr v predlojenii => " + checkDigits);
                    fstream.Write(array, 0, array.Length);

                }

                System.Diagnostics.Process.Start($"/Users/seda/Projects/testEx/testEx/bin/Debug/{file}.txt");

            }
            else
            {
                Console.WriteLine("Nekorrektniy vvod");
            }
        }


        public static void WriteAll(object txt)
        {
            string text = (string)txt;

            int checkSymbols = Regex.Matches(text, @"[@#$%^&*]").Count;
            int checkWords = Regex.Matches(text, @"[\w]+").Count;
            int checkSentence = Regex.Matches(text, @"[!?.]").Count;
            int checkInterrogative = Regex.Matches(text, @"[?]").Count;
            int checkExclamatory = Regex.Matches(text, @"[!]").Count;
            int checkDigits = Regex.Matches(text, @"[\d]").Cast<Match>()
                 .Select(x => int.Parse(x.Value))
                 .Sum();

            Console.WriteLine("\t\tVyvod\t\t");
            Console.WriteLine("1.Konsol'\n2.V fail");
            int checkInput = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            if (checkInput == 1)
            {
                Console.WriteLine("Kol-vo simvolov => " + checkSymbols);
                Console.WriteLine("Kol-vo slov v predlojenii => " + checkWords);
                Console.WriteLine("Kol-vo predlojeniy => " + checkSentence);
                Console.WriteLine("Kol-vo voprositel'nih predlojeniy => " + checkInterrogative);
                Console.WriteLine("Kol-vo vosklicatel'niy predlojeniy => " + checkExclamatory);
                Console.WriteLine("Kol-vo cifr v predlojenii => " + checkDigits);

            }
            
            else if (checkInput == 2)
            {

                Console.WriteLine("Nazovite fail(bez .txt)");
                string file = Console.ReadLine();


                Console.WriteLine("Done!");


                using (FileStream fstream = new FileStream($"{file}.txt", FileMode.OpenOrCreate))
                {
                    int[] arr = new int[6] {checkInterrogative,checkSentence,checkSymbols,checkWords,checkDigits,checkExclamatory };
                    byte[] array = System.Text.Encoding.Default.GetBytes("All" + arr);
                    fstream.Write(array,0,array.Length);

                }

                System.Diagnostics.Process.Start($"/Users/seda/Projects/testEx/testEx/bin/Debug/{file}.txt");

            }
            else
            {
                Console.WriteLine("Nekorrektniy vvod");
            }
        }
    }
}
