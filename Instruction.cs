using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRProcSimulator
{
    public class Instruction
    {
        public string keyword;
        public string format;
        public string opcode;
        public string[] registers;
        public string const_address;

        /// <summary>
        /// Create Format 1 Instruction
        /// </summary>
        /// <param name="kw">Keyword</param>
        /// <param name="op">Opcode</param>
        /// <param name="r1">Register A</param>
        /// <param name="r2">Register B</param>
        /// <param name="r3">Register C</param>
        public Instruction(string kw, string op, string r1, string r2, string r3)
        {
            keyword = kw.ToUpper();
            format = "F1";
            opcode = op.ToUpper();
            registers = new string[3];
            registers[0] = r1.ToUpper();
            registers[1] = r2.ToUpper();
            registers[2] = r3.ToUpper();
            const_address = null;
        }

        /// <summary>
        /// Create Format 2 Instruction
        /// </summary>
        /// <param name="kw">Keyword</param>
        /// <param name="op">Opcode</param>
        /// <param name="r1">Register A</param>
        /// <param name="addr">Constant or Address</param>
        public Instruction(string kw, string op, string r1, string addr)
        {
            keyword = kw.ToUpper();
            format = "F2";
            opcode = op.ToUpper();
            registers = new string[1];
            registers[0] = r1.ToUpper();
            const_address = addr;
        }

      

        /// <summary>
        /// Create Format 3 Instruction
        /// </summary>
        /// <param name="kw">Keyword</param>
        /// <param name="op">Opcode</param>
        /// <param name="addr">Constant or Address</param>
        public Instruction(string kw, string op, string addr)
        {
            keyword = kw.ToUpper();
            format = "F3";
            opcode = op.ToUpper();
            registers = null;
            const_address = addr;
        }
        public void assemble() 
        { 

        }

        public override string ToString()
        {
            if (format.Equals("F1"))
            {
                return opcode + registers[0] + registers[1] + registers[2] + "00";
            }
            else if (format.Equals("F2"))
            {
                return opcode + registers[0] + const_address;
            }
            else
            {
                return opcode + const_address;
            }
        }
        
    }
}
