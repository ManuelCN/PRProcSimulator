namespace PRProcSimulator
{
    partial class Form1
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
            this.panel_Editor.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Editor
            // 
            this.panel_Editor.Controls.Add(this.richTextBox_editor);
            this.panel_Editor.Controls.Add(this.button_Assemble);
            this.panel_Editor.Location = new System.Drawing.Point(12, 12);
            this.panel_Editor.Name = "panel_Editor";
            this.panel_Editor.Size = new System.Drawing.Size(563, 570);
            this.panel_Editor.TabIndex = 0;
            // 
            // richTextBox_editor
            // 
            this.richTextBox_editor.AcceptsTab = true;
            this.richTextBox_editor.Location = new System.Drawing.Point(3, 3);
            this.richTextBox_editor.Name = "richTextBox_editor";
            this.richTextBox_editor.Size = new System.Drawing.Size(552, 518);
            this.richTextBox_editor.TabIndex = 0;
            this.richTextBox_editor.Text = "";
            // 
            // button_Assemble
            // 
            this.button_Assemble.Location = new System.Drawing.Point(451, 527);
            this.button_Assemble.Name = "button_Assemble";
            this.button_Assemble.Size = new System.Drawing.Size(104, 40);
            this.button_Assemble.TabIndex = 1;
            this.button_Assemble.Text = "Assemble";
            this.button_Assemble.UseVisualStyleBackColor = true;
            this.button_Assemble.Click += new System.EventHandler(this.Button_Assemble_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 594);
            this.Controls.Add(this.panel_Editor);
            this.Name = "Form1";
            this.Text = "PrProc Microprocessor Simulator";
            this.panel_Editor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Editor;
        private System.Windows.Forms.Button button_Assemble;
        private System.Windows.Forms.RichTextBox richTextBox_editor;
    }
}

