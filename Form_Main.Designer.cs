namespace PRProcSimulator
{
    partial class Form_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Editor = new System.Windows.Forms.Panel();
            this.richTextBox_editor = new System.Windows.Forms.RichTextBox();
            this.button_Assemble = new System.Windows.Forms.Button();
            this.menuStrip_main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.importDialog = new System.Windows.Forms.OpenFileDialog();
            this.label_outputName = new System.Windows.Forms.Label();
            this.textBox_output = new System.Windows.Forms.TextBox();
            this.panel_Editor.SuspendLayout();
            this.menuStrip_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Editor
            // 
            this.panel_Editor.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Editor.Controls.Add(this.textBox_output);
            this.panel_Editor.Controls.Add(this.label_outputName);
            this.panel_Editor.Controls.Add(this.richTextBox_editor);
            this.panel_Editor.Controls.Add(this.button_Assemble);
            this.panel_Editor.Location = new System.Drawing.Point(12, 36);
            this.panel_Editor.Name = "panel_Editor";
            this.panel_Editor.Size = new System.Drawing.Size(660, 546);
            this.panel_Editor.TabIndex = 0;
            // 
            // richTextBox_editor
            // 
            this.richTextBox_editor.AcceptsTab = true;
            this.richTextBox_editor.Location = new System.Drawing.Point(3, 3);
            this.richTextBox_editor.Name = "richTextBox_editor";
            this.richTextBox_editor.Size = new System.Drawing.Size(651, 494);
            this.richTextBox_editor.TabIndex = 0;
            this.richTextBox_editor.Text = "";
            // 
            // button_Assemble
            // 
            this.button_Assemble.Location = new System.Drawing.Point(550, 503);
            this.button_Assemble.Name = "button_Assemble";
            this.button_Assemble.Size = new System.Drawing.Size(104, 40);
            this.button_Assemble.TabIndex = 1;
            this.button_Assemble.Text = "Assemble";
            this.button_Assemble.UseVisualStyleBackColor = true;
            this.button_Assemble.Click += new System.EventHandler(this.Button_Assemble_Click);
            // 
            // menuStrip_main
            // 
            this.menuStrip_main.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip_main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip_main.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip_main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_main.Name = "menuStrip_main";
            this.menuStrip_main.Size = new System.Drawing.Size(1002, 33);
            this.menuStrip_main.TabIndex = 1;
            this.menuStrip_main.Text = "Menu Options";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFilesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importFilesToolStripMenuItem
            // 
            this.importFilesToolStripMenuItem.Name = "importFilesToolStripMenuItem";
            this.importFilesToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.importFilesToolStripMenuItem.Text = "Import files...";
            this.importFilesToolStripMenuItem.Click += new System.EventHandler(this.ImportFilesToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(678, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 546);
            this.panel1.TabIndex = 2;
            // 
            // importDialog
            // 
            this.importDialog.FileName = "importDialog";
            this.importDialog.Filter = "Text files (*.txt)|*.txt";
            this.importDialog.Title = "Choose file to import";
            // 
            // label_outputName
            // 
            this.label_outputName.AutoSize = true;
            this.label_outputName.Location = new System.Drawing.Point(3, 513);
            this.label_outputName.Name = "label_outputName";
            this.label_outputName.Size = new System.Drawing.Size(106, 20);
            this.label_outputName.TabIndex = 3;
            this.label_outputName.Text = "Output name:";
            // 
            // textBox_output
            // 
            this.textBox_output.Location = new System.Drawing.Point(115, 510);
            this.textBox_output.Name = "textBox_output";
            this.textBox_output.Size = new System.Drawing.Size(177, 26);
            this.textBox_output.TabIndex = 4;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1002, 594);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_Editor);
            this.Controls.Add(this.menuStrip_main);
            this.MainMenuStrip = this.menuStrip_main;
            this.Name = "Form_Main";
            this.Text = "PrProc Microprocessor Simulator";
            this.panel_Editor.ResumeLayout(false);
            this.panel_Editor.PerformLayout();
            this.menuStrip_main.ResumeLayout(false);
            this.menuStrip_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_Editor;
        private System.Windows.Forms.Button button_Assemble;
        private System.Windows.Forms.RichTextBox richTextBox_editor;
        private System.Windows.Forms.MenuStrip menuStrip_main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFilesToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog importDialog;
        private System.Windows.Forms.TextBox textBox_output;
        private System.Windows.Forms.Label label_outputName;
    }
}

