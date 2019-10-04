using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRProcSimulator
{
    public class Assembler
    {
        private bool containsOrg;
        private int index;
        private List<string> errors = new List<string>(); //TODO: add error checking
        private LinkedList<tableItem> table = new LinkedList<tableItem>();

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
        public List<Instruction> AssemblerPass1(List<string> code)
        {
            List<Instruction> instructions = new List<Instruction>();
            foreach (string line in code)
            {
                string[] parsedLine;
                if (Regex.Match(line, "[\torg ][0-9]+").Success && !containsOrg) //Check for ORG
                {
                    containsOrg = true;
                    index = int.Parse(line.Split(' ')[1]);
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
                    if(parsedLine.Length == 4)
                    {
                        instructions.Add(new Instruction(parsedLine[0], getOpcode(parsedLine[0]), parsedLine[1].Trim(' ', ','), parsedLine[2].Trim(' ', ','), parsedLine[3].Trim(' ', ',')));
                    } else if(parsedLine.Length == 3)
                    {
                        instructions.Add(new Instruction(parsedLine[0], getOpcode(parsedLine[0]), parsedLine[1].Trim(' ', ','), parsedLine[2].Trim(' ', ','), ""));
                    }
                    index++;
                }
                else
                {
                    errors.Add("Line " + line + " does not match any grammar rule.");
                }
            }
            return instructions;
        }

        public void AssemblerPass2(List<Instruction> instructions, string output)
        {
            string outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + output + ".obj";
            TextWriter docwriter = new StreamWriter(outputPath);
            if (!File.Exists(outputPath))
                File.Create(outputPath);

            foreach (Instruction instruction in instructions)
            {
                string line = "";
                line += instruction.opcode;
                int index = 0;
                switch (instruction.format)
                {
                    case "F1":
                        line += getRegisterBinary(instruction.registers[0]);
                        line += getRegisterBinary(instruction.registers[1]);
                        if (!String.IsNullOrWhiteSpace(instruction.registers[2]))
                        {
                            line += getRegisterBinary(instruction.registers[2]);
                            line += "00";
                        }
                        else
                        {
                            line += "00000";
                        }
                        
                        break;
                    case "F2":
                        line += getRegisterBinary(instruction.registers[0]);
                        index = searchInRefTable(instruction.const_address);
                        if(index != -1)
                        {
                            int address = table.ElementAt(index).address;
                            string bin = Convert.ToString(address, 2);
                            int length = 8 - bin.Length;
                            if(length > 0)
                            {
                                for(int i = 0; i < length; i++)
                                {
                                    line += "0";
                                }
                                line += bin;
                            }
                            else
                            {
                                line += bin.Substring(0, 8);
                            }
                        }
                        else
                        {
                            if(Regex.Match(instruction.const_address, "[#][A-Z0-9]{2}").Success)
                            {
                                char[] digits = instruction.const_address.ToCharArray();
                                if(digits.Length > 1)
                                {
                                    line += getBinary(digits[0]);
                                    line += getBinary(digits[1]);
                                }
                                else
                                {
                                    line += "0000" + getBinary(digits[0]);
                                }
                            }
                        }
                        break;
                    case "F3":
                        index = searchInRefTable(instruction.const_address);
                        if (index != -1)
                        {
                            int address = table.ElementAt(index).address;
                            string bin = Convert.ToString(address, 2);
                            int length = 11 - bin.Length;
                            if (length > 0)
                            {
                                for (int i = 0; i < length; i++)
                                {
                                    line += "0";
                                }
                                line += bin;
                            }
                            else
                            {
                                line += bin.Substring(0, 11);
                            }
                        }
                        else
                        {
                            if (Regex.Match(instruction.const_address, "[#][A-Z0-9]{2}").Success)
                            {
                                line += "000";
                                char[] digits = instruction.const_address.ToCharArray();
                                if (digits.Length > 1)
                                {
                                    line += getBinary(digits[0]);
                                    line += getBinary(digits[1]);
                                }
                                else
                                {
                                    line += "0000" + getBinary(digits[0]);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
                docwriter.WriteLine(line);
            }
            docwriter.Close();
        }
      
            
        internal class tableItem
        {
            internal string name;
            internal string type;
            internal int address;
            internal string value;
            

            public tableItem(string name, string type, int address, string value)
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

        private string getBinary(char hex)
        {
            switch (hex)
            {
                case '0':
                    return "0000";
                case '1':
                    return "0001";
                case '2':
                    return "0010";
                case '3':
                    return "0011";
                case '4':
                    return "0100";
                case '5':
                    return "0101";
                case '6':
                    return "0110";
                case '7':
                    return "0111";
                case '8':
                    return "1000";
                case '9':
                    return "1001";
                case 'A':
                    return "1010";
                case 'B':
                    return "1011";
                case 'C':
                    return "1100";
                case 'D':
                    return "1101";
                case 'E':
                    return "1110";
                case 'F':
                    return "1111";
                default:
                    return "";
            }
        }

        private string getRegisterBinary(string register)
        {
            switch (register)
            {
                case "R0":
                    return "000";
                case "R1":
                    return "001";
                case "R2":
                    return "010";
                case "R3":
                    return "011";
                case "R4":
                    return "100";
                case "R5":
                    return "101";
                case "R6":
                    return "110";
                case "R7":
                    return "111";
                default:
                    return "";

            }
        }
    }
}
 //format 1 intructions LOADRIND, STORERIND, ADD, SUV, AND, OR, XOR, NOT, NEG, 
            //SHIFTR, SHIFTL, ROTAR, ROTAL, JUMPIND, GRT, GRTEQ, EQ, NEQ, NOP
      //format 2  LOAD, LOADIM, POP, STORE, PUSH, ADDIM, SUBIM, LOOP, 
        //format 3 JMPADDR, JCONDRIN, JCONDADDR