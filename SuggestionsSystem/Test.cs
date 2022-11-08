using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuggestionsSystem
{
    class Test
    {
        static void Main(string[] args)
        {
            string line = "Username:test#Password:123456#Email:test@test.com#CatID:7#";
            string username = "Password";
            Regex r = new Regex(username + @":(.+?)#");
            MatchCollection mc = r.Matches(line);
            foreach (Match match in mc)
            {
                Console.WriteLine("full match: " + match.Groups[0]);
                Console.WriteLine("value: " + match.Groups[1]);
                Console.ReadLine();
            }
        }
    }
}
