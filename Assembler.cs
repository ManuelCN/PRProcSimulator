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
        private long index;
        private List<string> errors; //TODO: add error checking
        private List<Instruction> instructions;
        private LinkedList<tableItem> table = new LinkedList<tableItem>();
        public Assembler()
        {
            errors = new List<string>();
            instructions = new List<Instruction>();
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
            foreach (string line in code)
            {
                string[] parsedLine;
                if (Regex.Match(line, "[\torg ][0-9]+").Success && !containsOrg) //Check for ORG
                {
                    containsOrg = true;
                    index = long.Parse(line.Split(' ')[1]);
                }
                else if (Regex.Match(line, "[a-zA-Z]+[a-zA-Z0-9]*[:]").Success && containsOrg) //Check for LABELS
                {
                    parsedLine = line.Split(' ', ':');
                    //After finding a label, search in table for the label
                    //If label is found and does not have an address, update the entry's address
                    //If label is found and has an address, do nothing
                    bool error = false;
                    int tableIndex = searchInRefTable(parsedLine[0]);
                    if(tableIndex != -1)
                    {
                        if (table.ElementAt(tableIndex).address == -1)
                            table.ElementAt(tableIndex).address = index;
                        else
                            error = true;
                    } else
                        table.AddLast(new tableItem(parsedLine[0], "label", index, null));
                    
                    //If error == true, report error
                    index++;
                }
                else if (Regex.Match(line, "([a-zA-Z0-9]*[ ]+[d][b][ ]+[)|([0-9]+([ ]*,[ ]*[0-9]+)*)").Success && containsOrg) //Check for LABELSDEF
                {
                    parsedLine = line.Split(' ');
                    table.AddLast(new tableItem(parsedLine[0], "var", index, null));
                    index++;
                }
                else if (Regex.Match(line, "[const][ ]+[a-zA-Z0-9]+[ ]+[0-9]+").Success && containsOrg) //Check for CONSTANTS
                {
                    parsedLine = line.Split(' ');
                    table.AddLast(new tableItem(parsedLine[1], "const", index, parsedLine[2]));
                    index++;
                }
                else if (Regex.Match(line, "((JMPADDR)|(JCONDRIN)|(JCONDADDR))([ ]*[a-zA-Z0-9]+([ ]*[,][ ]*[a-zA-Z0-9]+)*)").Success && containsOrg)
                {
                    //The following instrucctionsare in F3 format 
                    if (int.Parse(index.ToString()) % 2 != 0)
                    {
                        index++;
                    }
                    parsedLine = line.Trim().Split(' ');
                    instructions.Add(new Instruction(parsedLine[0], getOpcode(parsedLine[0]), parsedLine[1]));
                    int tableIndex = searchInRefTable(parsedLine[1]);
                    if (tableIndex == -1)
                        table.AddLast(new tableItem(parsedLine[1], "label", -1, null));
                    index++;
                }
                else if (Regex.Match(line, "((LOAD)|(LOADIM)|(POP)|(STORE)|(PUSH)|(ADDIM)|(SUBIM)|(LOOP))([ ]*[a-zA-Z0-9]+([ ]*[,][ ]*[a-zA-Z0-9]+)*)").Success && containsOrg)
                {
                    //The following instrucctionsare in F2 format 
                    if (int.Parse(index.ToString()) % 2 != 0)
                    {
                        index++;
                    }
                    parsedLine = line.Trim().Split(' ');
                    instructions.Add(new Instruction(parsedLine[0], getOpcode(parsedLine[0]), parsedLine[1].Trim(' ', ','), parsedLine[2].Trim(' ', ',')));
                    int tableIndex = searchInRefTable(parsedLine[2]);
                    if(tableIndex == -1)
                        table.AddLast(new tableItem(parsedLine[2], "label", -1, null));
                    index++;
                }
                else if (Regex.Match(line,"((LOADRIND)|(STORERIND)|(ADD)|(SUV)|(AND)|(OR)|(XOR)|(NOT)|(NEG)|(SHIFTR)|(SHIFTL)|(ROTAR)|(ROTAL)|(JUMPIND)|(GRT)|(GRTEQ)|(EQ)|(NEQ)|(NOP))([ ]*[a-zA-Z0-9]+([ ]*[,][ ]*[a-zA-Z0-9]+)*)").Success && containsOrg)
                {
                    //The following instrucctionsare in F1 format 
                    if (int.Parse(index.ToString()) % 2 != 0)
                    {
                        index++;
                    }
                    parsedLine = line.Trim().Split(' ');
                    instructions.Add(new Instruction(parsedLine[0], getOpcode(parsedLine[0]), parsedLine[1].Trim(' ', ','), parsedLine[2].Trim(' ', ','), parsedLine[3].Trim(' ', ',')));
                    index++;
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
        
        /// <summary>
        /// Returns the opcode of the given instruction name
        /// </summary>
        /// <param name="instr">The instruction name</param>
        /// <returns></returns>
        private string getOpcode(string instr)
        {
            switch (instr.ToUpper())
            {
                case "LOAD":
                    return "00000";
                case "LOADIM":
                    return "00001";
                case "POP":
                    return "00010";
                case "STORE":
                    return "00011";
                case "PUSH":
                    return "00100";
                case "LOADRIND":
                    return "00101";
                case "STORERIND":
                    return "00110";
                case "ADD":
                    return "00111";
                case "SUB":
                    return "01000";
                case "ADDIM":
                    return "01001";
                case "SUBIM":
                    return "01010";
                case "AND":
                    return "01011";
                case "OR":
                    return "01100";
                case "XOR":
                    return "01101";
                case "NOT":
                    return "01110";
                case "NEG":
                    return "01111";
                case "SHIFTR":
                    return "10000";
                case "SHIFTL":
                    return "10001";
                case "ROTAR":
                    return "10010";
                case "ROTAL":
                    return "10011";
                case "JMPRIND":
                    return "10100";
                case "JMPADDR":
                    return "10101";
                case "JCONDRIN":
                    return "10110";
                case "JCONDADDR":
                    return "10111";
                case "LOOP":
                    return "11000";
                case "GRT":
                    return "11001";
                case "GRTEQ":
                    return "11010";
                case "EQ":
                    return "11011";
                case "NEQ":
                    return "11100";
                case "NOP":
                    return "11101";
                case "CALL":
                    return "11110";
                case "RETURN":
                    return "10101";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Searches in reference table for label, variable or constant name. If found, returns index in LinkedList
        /// </summary>
        /// <param name="name">The name of the label, variable or constant to search for</param>
        /// <returns></returns>
        private int searchInRefTable(string name)
        {
            if(table.Count > 0)
            {
                for(int i = 0; i < table.Count; i++)
                {
                    if (table.ElementAt(i).name.Equals(name))
                        return i;
                }
            }
            return -1;
        }
    }
}
 //format 1 intructions LOADRIND, STORERIND, ADD, SUV, AND, OR, XOR, NOT, NEG, 
            //SHIFTR, SHIFTL, ROTAR, ROTAL, JUMPIND, GRT, GRTEQ, EQ, NEQ, NOP
      //format 2  LOAD, LOADIM, POP, STORE, PUSH, ADDIM, SUBIM, LOOP, 
        //format 3 JMPADDR, JCONDRIN, JCONDADDR