using System;

using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeWork7
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.Write("Vvedite tekst: ");
            string text = Console.ReadLine().ToLower();

            Task task = new Task(() => WorkWithText(text));
            task.Start();


            Console.ReadKey();
        }

        static void WorkWithText(string text)
        {

            int checkVowels = Regex.Matches(text, @"[aeoiu]").Count;
            int checkConsonants = Regex.Matches(text, @"[qwrtypsdfghjklzxcvbnm]").Count;
            int checkPunctuationMark = Regex.Matches(text, @"[!?""();:.,-]").Count;

            Console.WriteLine("Kol-vo glasnyh: {0}\nKol-vo soglasnyh: {1}\nKol-vo znakov prepinaniya: {2}", checkVowels, checkConsonants, checkPunctuationMark);
            
        }
    }
}
