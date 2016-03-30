using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;


namespace CAD_Translate_Sys
{
    public class Translator
    {
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {return "";}
        }
        public static string TranslateText(string input, string languagepair = "en|pt")
        {

            string result = String.Empty;
            string textFile = "";
            Boolean long_text = false;
            if (input.Contains(".txt"))
            {
                try { textFile = File.ReadAllText(input); input = textFile; }
                catch(Exception e) { Console.WriteLine(e.Message); }
            }
            Encoding encoding = System.Text.Encoding.UTF8;
            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagepair);

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Encoding = encoding;
                    result = webClient.DownloadString(url);
                }
            }

            catch (Exception e){Console.WriteLine("Error " + e.Message);}

            /*Code for html scraping */ 
           /*string text = getBetween(result, "<span id=result_box class=\"short_text\">", "</span>");

            if(text.Length==0)
            {
                long_text = true;
                text = getBetween(result, "<span id=result_box class=\"long_text\">", "</span>");
            }
            Console.WriteLine("result - " + result);
            */
            string final = getBetween(result, "TRANSLATED_TEXT=", ";");

            //return text;
            return final;
        }


    }
}
