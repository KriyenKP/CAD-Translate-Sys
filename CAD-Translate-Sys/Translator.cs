using System;
using System.Text;
using System.Net;               //web interface
using System.IO;                //file handling


namespace CAD_Translate_Sys
{
    public class Translator
    {
        // receives input string, finds location of parameter bounds and collects information between
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;                                                                             //init variables
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))                             //check if bounds exist
            {       
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;                           //gets integer index of start characters
                End = strSource.IndexOf(strEnd, Start);                                             //gets integer index of end characters
                return strSource.Substring(Start, End - Start);                                     //return content inbetween start and end
            }
            else{return "Error bounds do not exist";}                                                 //error handling 
        }
        //does translation - accepts in text or textfile - language pair = (from-language|to-language)
        public static string TranslateText(string input, string languagepair = "en|pt")
        {
            string result = "",textFileContent = "";                        //init var
            if (input.Contains(".txt"))                                     //checks if parameter is string containing txt file ext
            {
                try {textFileContent = File.ReadAllText(input); input = textFileContent;}    //reads content from text file & saves as string
                catch(Exception e) {Console.WriteLine("Error reading from text file. Error :" + e.Message);} //error handling
            }
            Encoding encoding = System.Text.Encoding.UTF8;      // web encoding
            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagepair);
            //google translate url with input and languagepair as parameters
            try
            {
                using (WebClient webClient = new WebClient()) //create C# web interface
                {
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    //"spoof" browser identity
                    webClient.Encoding = encoding;  //set web encoding
                    result = webClient.DownloadString(url);   //download all web content
                }
            }
            catch (Exception e){Console.WriteLine("Error :" + e.Message);}       //error handling
            string final = getBetween(result, "TRANSLATED_TEXT=", ";");         //locate content between Translated_text variable and ;
            return final; //return translated text
        }
    }
}
