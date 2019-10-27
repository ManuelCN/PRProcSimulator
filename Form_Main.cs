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
using System.Timers;


namespace PRProcSimulator
{
    

    public partial class Form_Main : Form
    {
        List<string> reserved = new List<string>(){"ORG","DB","CONST","JMPADDR","JCONDRIN","JCONDADDR","LOAD","LOADIM","POP","STORE"
                            ,"PUSH","ADDIM","SUBIM","LOOP","LOADRIND","STORERIND","ADD","SUV","AND","OR","XOR"
                            ,"NOT","NEG","SHIFTR","SHIFTL","ROTAR","ROTAL","JUMPIND","GRT","GRTEQ","EQ","NEQ","NOP"};
    
       
        System.Windows.Forms.Timer timer;

        public Form_Main()
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 10000;
            timer.Tick += new EventHandler(HighlightKeyWords);
            timer.Enabled = true;
            

        }

        private void Button_Assemble_Click(object sender, EventArgs e)
        {
            Assembler assembly = new Assembler();
            List<string> code;
            List<Instruction> instructions;
            code = assembly.FilterComments(richTextBox_editor.Text);
            instructions = assembly.AssemblerPass1(code);
            assembly.AssemblerPass2(instructions, this.textBox_output.Text);

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

        private void HighlightKeyWords(object sender, EventArgs e)
        {
            
            int index;
            int pointer;
            
     
            foreach(string word in reserved)
            {
                index = 0;
                pointer = 0;
                while((index = richTextBox_editor.Find(word, pointer, RichTextBoxFinds.MatchCase)) != -1 )
                {
            
                    richTextBox_editor.SelectionColor = Color.MediumPurple;
                    pointer += word.Length;
                }
            }

        }
    }
}
