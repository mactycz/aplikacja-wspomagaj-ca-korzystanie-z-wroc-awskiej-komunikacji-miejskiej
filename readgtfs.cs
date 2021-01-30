using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Reflection;

namespace praca_inż
{
    class readgtfs
    {

     

        public String input;
        public int lineCount;
        public string[,] result;
        public void readfile(string name, int columns)
        {
            string dir = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            var path = dir+ "\\rozklad\\" +  name+ ".txt";
            input = File.ReadAllText(path);
            lineCount = File.ReadLines(path).Count();
            result = new string[lineCount+1, columns];
            int i = 0, j = 0;
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(','))
                {
                    result[i, j] = col.Trim();
                    j++;
                }
                i++;
            }
        }
       


    }
}
    
      
    

