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
        private long index;
        private Dictionary<string, string> grammar;
        private List<string> labels;
        private List<string> errors;
        private LinkedList<tableItem> table = new LinkedList<tableItem>();
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
            foreach (string line in copy)
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
                if (Regex.Match(line, "[\torg ][0-9]+").Success && !containsOrg) //Check for ORG
                {
                    containsOrg = true;

                    index = long.Parse(Regex.Match(line, @"\d+").Value); //find the starting address from origin

                } else if (Regex.Match(line, "[a-zA-Z]+[a-zA-Z0-9]*[:]").Success && containsOrg) //Check for LABELS
                {
                    index += 1;
                    int endIndex = line.IndexOf(':');
                    labels.Add(line.Substring(0, endIndex));
                    //after finding a label puts a new item with param (name,type,address,value)into the table of items
                    
           
                    tableItem item = new tableItem(Regex.Match(line, @"[a-zA-Z]+[a-zA-Z0-9]").Value, "label", index,null);
                } else if (Regex.Match(line, "([a-zA-Z0-9]*[ ]+[d][b][ ]+[)|([0-9]+([ ]*,[ ]*[0-9]+)*)").Success && containsOrg) //Check for LABELSDEF
                {
                    index += 1;
                    labels.Add(line.Split(' ')[0]);
                } else if (Regex.Match(line, "[const][ ]+[a-zA-Z0-9]+[ ]+[0-9]+").Success && containsOrg) //Check for CONSTANTS
                {
                    index += 1;
                    labels.Add(line.Split(' ')[1]);

                    table.AddLast(new tableItem(Regex.Match(line, @"[ ]+[a-zA-Z0-9]+[ ]+").Value, "const", index, Regex.Match(line, @"[ ]+[0-9]+").Value));
                   
                }
                else if (Regex.Match(line,"((LOADRIND)|(STORERIND)|(ADD)|(SUV)|(AND)|(OR)|(XOR)|(NOT)|(NEG)|(SHIFTR)|(SHIFTL)|(ROTAR)|(ROTAL)|(JUMPIND)|(GRT)|(GRTEQ)|(EQ)|(NEQ)|(NOP))([ ]*[a-zA-Z0-9]+([ ]*[,][ ]*[a-zA-Z0-9]+)*)").Success && containsOrg)
                {  //the following instrucctionsare in F1 format 
                    index += 1;
                    table.AddLast(new tableItem(Regex.Match(line, @"\t[a-zA-Z]*").Value, "instruction", index, null));
                   
                }
                else if (Regex.Match(line, "((LOAD)|(LOADIM)|(POP)|(STORE)|(PUSH)|(ADDIM)|(SUBIM)|(LOOP))([ ]*[a-zA-Z0-9]+([ ]*[,][ ]*[a-zA-Z0-9]+)*)").Success && containsOrg)
                {  //the following instrucctionsare in F2 format 
                    index += 1;
                    table.AddLast(new tableItem(Regex.Match(line, @"\t[a-zA-Z]*").Value, "instruction", index, null));
                }
                else if (Regex.Match(line, "((JMPADDR)|(JCONDRIN)|(JCONDADDR))([ ]*[a-zA-Z0-9]+([ ]*[,][ ]*[a-zA-Z0-9]+)*)").Success && containsOrg)
                {  //the following instrucctionsare in F3 format 
                    index += 1;
                    table.AddLast(new tableItem(Regex.Match(line, @"\t[a-zA-Z]*").Value, "instruction", index, null));
                }
                else
                {
                    errors.Add("Line " + line + " does not match any grammar rule.");
                }
            }
        }
      
            
        internal class tableItem
        {
            internal string name;
            internal string type;
            internal long address;
            internal string value;
            

            public tableItem(string name, string type, long address, string value)
            {
                this.name = name;
                this.type = type;
                this.address = address;
                this.value = value;
            }

        }

        private void assembleTable(LinkedList<tableItem> table)
        {

        }

    }
}
 //format 1 intructions LOADRIND, STORERIND, ADD, SUV, AND, OR, XOR, NOT, NEG, 
            //SHIFTR, SHIFTL, ROTAR, ROTAL, JUMPIND, GRT, GRTEQ, EQ, NEQ, NOP
      //format 2  LOAD, LOADIM, POP, STORE, PUSH, ADDIM, SUBIM, LOOP, 
        //format 3 JMPADDR, JCONDRIN, JCONDADDR