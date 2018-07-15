using System;
using System.Linq;

namespace MaleNupuLiikumine
{   

    class Program
    {
              
        static void Main(string[] args)
        {          
           try
           {    
                // Check args
                if ( args.Count() < 2)
                {
                    throw new Exception("Viga: Liiga vähe argumente. Kasutada myAppName.exe \"inputFile\" \"outputFile\"");
                }
              
                string inputFile = args[0];
                string outputFile = args[1];

                // load inputFile
                string[] inputLines = FileManager.importFromFile(inputFile);

    
                // initialize chessboard. Default grid size 8x8. For custom grid size use "new Chessboard(int rows, int columns)";
                // Maximum amount of columns currently is 26 due A-Z in ascii table
                Chessboard game = new Chessboard();

                // Game settings
                int[] startPoint = game.textToCord(inputLines[0].Trim());
                int[] endPoint = game.textToCord(inputLines[1].Trim());

                // Set designated squares to be filled
                game.fillBlocks(inputLines[2].Trim().Split(','));


                // Get movement info to end point - Call getFormattedMovementInfo(int startPointX, int StartPointY, optional string "piece")
                // Pieces available: Ratsu, Oda, Kahur, Ettur
                // Default piece: Ratsu
                string content = game.getMovementInfo(startPoint, endPoint, "Ratsu");


                // Print content to file
                FileManager.exportToFile(outputFile, content);
                Console.WriteLine("Väljundfaili sisu:\r\n{0}", content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Programmi sulgemiseks vajutage 'Enter'");
                Console.ReadLine();
            }
            
        }
    }
}
