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
        private List<string> errors;

        public Assembler()
        {
            //Define Grammar Rules
            grammar = new Dictionary<string, string>();
            grammar.Add("[\torg ][0-9]+", "ORG"); //Check for ORG
            grammar.Add("[a-zA-Z]*[0-9]*[:]", "LABELS"); //Check for LABELS
            grammar.Add("[a-zA-Z0-9]*[ ]+[db][ ]+[0-9]+||([ ]*[[[ ]*||,]0-9]*)", "LABELSDEF"); //Check for LABELS DEFINITION
            grammar.Add("[const][ ]+[a-zA-Z0-9]+[ ]+[0-9]+", "CONSTANT"); //Check for CONSTANTS

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
            foreach(string line in code)
            {
                foreach(string rule in grammar.Keys)
                {
                    if (!Regex.Match(line, rule).Success)
                    {
                        string err = "";
                        grammar.TryGetValue(rule, out err);
                        err += " grammar error for line: " + line;
                        errors.Add(err);
                    }

                    
                }
            }
        }
    }
}
