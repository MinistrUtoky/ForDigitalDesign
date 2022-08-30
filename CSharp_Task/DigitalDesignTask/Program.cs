using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

String line;

try
{
    Dictionary<string, int> counter = new Dictionary<string, int>();
    StreamReader sr = new StreamReader("tolstoj_lew.txt");
    char[] d = " ,.!?:\"();\\/".ToCharArray();
    string[] parts;
    string pattern = @"\<(.*?)\>";
    Regex regex = new Regex(pattern);

    while (!sr.EndOfStream)
    {
        line = sr.ReadLine();
        line = regex.Replace(line, "");
        parts = line.Split(d);
        
        foreach (string part in parts)
        {
            if (part != "" && part != "--")
                if (counter.ContainsKey(part))
                    counter[part]++;
                else counter[part] = 1;
        }
    }
    counter = counter.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
    using (StreamWriter sw = new StreamWriter("tolstoj_dict.txt"))
    {
        foreach (string s in counter.Keys)
        {
            sw.WriteLine("{0} - {1}", s, counter[s]);
        }
    }
    sr.Close();
    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine(e.Message); 
}
finally
{
    Console.WriteLine("Executing block.");
}
