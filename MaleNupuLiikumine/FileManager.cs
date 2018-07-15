using System;
using System.Linq;
using System.Text;
using System.IO;

namespace MaleNupuLiikumine
{
    static class FileManager
    {
        public static string[] importFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("Viga: Sisendfaili ei eksisteeri!");
            }

            string[] inputLines = File.ReadAllLines(path);

            if (inputLines.Count() != 3)
            {
                throw new Exception("Viga: Sisendfaili sisu formaat on vale!");
            }

            return inputLines;
        }

        public static void exportToFile(string file, string content)
        {

            if (File.Exists(file))
            {
                string response = "";

                while (response != "Y" && response != "N")
                {
                    Console.WriteLine("Teade: Antud nimega väljundfail juba eksisteerib, kas soovid ülekirjutada? Y/N");
                    response = Console.ReadLine().ToUpper();
                } 

                if (response == "N")
                {
                    Console.WriteLine("Sisesta uus väljundfaili nimi:");
                    exportToFile(Console.ReadLine(), content);
                }
                else
                {
                    File.Delete(file);
                }             
            }

            using (FileStream fs = File.Create(file))
            {
                Byte[] info = Encoding.Default.GetBytes(content);

                fs.Write(info, 0, info.Length);
            }
        }
    }
}
