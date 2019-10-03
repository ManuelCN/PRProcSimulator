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
using System.IO;

namespace PRProcSimulator
{
    public partial class Form_Main : Form
    {
        public Form_Main()
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

        private void ImportFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = importDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.richTextBox_editor.Text = File.ReadAllText(importDialog.FileName);
                this.textBox_output.Text = importDialog.FileName.Split('\\').Last().Replace(".txt", "");
            }
        }
    }
}
