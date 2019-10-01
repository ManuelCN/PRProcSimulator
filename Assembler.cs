using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRProcSimulator
{
    public class Assembler
    {
        private bool containsOrg;
        private bool matches;
        private Dictionary<string, string> grammar;
        private List<string> labels;
        private List<string> errors;

        public Assembler()
        {
            //Define Grammar Rules
            grammar = new Dictionary<string, string>();
            grammar.Add("[const][ ]+[a-zA-Z0-9]+[ ]+[0-9]+", "CONSTANT"); //Check for CONSTANTS

            labels = new List<string>();
            errors = new List<string>();
        }

        public List<string> FilterComments(string input)
        {
            List<string> result;
            //Find and remove comments
            result = Regex.Replace(input, "//*[a-zA-Z0-9 ]*", " ").Split('\n').ToList();
            string[] copy = new string[result.Count];
            result.CopyTo(copy);
            //Remove empty code lines
            foreach(string line in copy)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    result.Remove(line);
                }
            }
            //Return filtered code
            return result;
        }
        public void PrepareInput(ref List<string> code)
        {
            Console.WriteLine(Regex.Match(code.ElementAt(1), "[a-zA-Z0-9]*[ ]+[d][b][ ]+[0-9]+|([ ]*[[[ ]*|,]0-9]*)").Success);

            foreach (string line in code)
            {
                if(Regex.Match(line, "[\torg ][0-9]+").Success && !containsOrg) //Check for ORG
                {
                    containsOrg = true;
                } else if(Regex.Match(line, "[a-zA-Z]*[0-9]*[:]").Success && containsOrg) //Check for LABELS
                {
                    int endIndex = line.IndexOf(':');
                    labels.Add(line.Substring(0, endIndex));
                } else if(Regex.Match(line, "[a-zA-Z0-9]*[ ]+[db][ ]+[0-9]+|([ ]*[[[ ]*|,]0-9]*)").Success && containsOrg) //Check for LABELSDEF
                {
                    labels.Add(line.Split(' ')[0]);
                } else if(Regex.Match(line, "[const][ ]+[a-zA-Z0-9]+[ ]+[0-9]+").Success && containsOrg) //Check for CONSTANTS
                {
                    labels.Add(line.Split(' ')[1]);
                }
                else
                {
                    errors.Add("Line " + line + " does not match any grammar rule.");
                }
            }
        }
    }
}
