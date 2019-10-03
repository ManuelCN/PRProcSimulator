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

        public Instruction()
        {
            
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
