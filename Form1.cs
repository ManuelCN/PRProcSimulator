using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PRProcSimulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button_Assemble_Click(object sender, EventArgs e)
        {
            Assembler assembly = new Assembler();
            List<string> code;
            code = assembly.FilterComments(richTextBox_editor.Text);
            assembly.PrepareInput(ref code);

        }
    }
}
