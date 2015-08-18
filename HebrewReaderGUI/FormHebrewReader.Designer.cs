namespace HebrewReaderGUI
{
    partial class FormHebrewReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHebrewReader));
            this.textBoxHebrew = new System.Windows.Forms.TextBox();
            this.buttonSpeakHebrew = new System.Windows.Forms.Button();
            this.textBoxEnglish = new System.Windows.Forms.TextBox();
            this.buttonSpeakEnglish = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelVoices = new System.Windows.Forms.Label();
            this.buttonStopEnglish = new System.Windows.Forms.Button();
            this.buttonQueue = new System.Windows.Forms.Button();
            this.buttonStopHebrew = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxHebrew
            // 
            this.textBoxHebrew.Location = new System.Drawing.Point(21, 72);
            this.textBoxHebrew.Name = "textBoxHebrew";
            this.textBoxHebrew.Size = new System.Drawing.Size(243, 20);
            this.textBoxHebrew.TabIndex = 0;
            // 
            // buttonSpeakHebrew
            // 
            this.buttonSpeakHebrew.Location = new System.Drawing.Point(270, 70);
            this.buttonSpeakHebrew.Name = "buttonSpeakHebrew";
            this.buttonSpeakHebrew.Size = new System.Drawing.Size(121, 23);
            this.buttonSpeakHebrew.TabIndex = 1;
            this.buttonSpeakHebrew.Text = "Speak Hebrew";
            this.buttonSpeakHebrew.UseVisualStyleBackColor = true;
            this.buttonSpeakHebrew.Click += new System.EventHandler(this.buttonSpeakHebrew_Click);
            // 
            // textBoxEnglish
            // 
            this.textBoxEnglish.Location = new System.Drawing.Point(21, 107);
            this.textBoxEnglish.Name = "textBoxEnglish";
            this.textBoxEnglish.Size = new System.Drawing.Size(243, 20);
            this.textBoxEnglish.TabIndex = 2;
            // 
            // buttonSpeakEnglish
            // 
            this.buttonSpeakEnglish.Location = new System.Drawing.Point(270, 105);
            this.buttonSpeakEnglish.Name = "buttonSpeakEnglish";
            this.buttonSpeakEnglish.Size = new System.Drawing.Size(121, 23);
            this.buttonSpeakEnglish.TabIndex = 3;
            this.buttonSpeakEnglish.Text = "Speak English";
            this.buttonSpeakEnglish.UseVisualStyleBackColor = true;
            this.buttonSpeakEnglish.Click += new System.EventHandler(this.buttonSpeakEnglish_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(66, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(198, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // labelVoices
            // 
            this.labelVoices.AutoSize = true;
            this.labelVoices.Location = new System.Drawing.Point(18, 31);
            this.labelVoices.Name = "labelVoices";
            this.labelVoices.Size = new System.Drawing.Size(42, 13);
            this.labelVoices.TabIndex = 5;
            this.labelVoices.Text = "Voices:";
            // 
            // buttonStopEnglish
            // 
            this.buttonStopEnglish.Enabled = false;
            this.buttonStopEnglish.Location = new System.Drawing.Point(397, 104);
            this.buttonStopEnglish.Name = "buttonStopEnglish";
            this.buttonStopEnglish.Size = new System.Drawing.Size(49, 23);
            this.buttonStopEnglish.TabIndex = 6;
            this.buttonStopEnglish.Text = "Stop";
            this.buttonStopEnglish.UseVisualStyleBackColor = true;
            this.buttonStopEnglish.Click += new System.EventHandler(this.buttonStopEnglish_Click);
            // 
            // buttonQueue
            // 
            this.buttonQueue.Location = new System.Drawing.Point(174, 182);
            this.buttonQueue.Name = "buttonQueue";
            this.buttonQueue.Size = new System.Drawing.Size(90, 23);
            this.buttonQueue.TabIndex = 7;
            this.buttonQueue.Text = "Queue Fun";
            this.buttonQueue.UseVisualStyleBackColor = true;
            this.buttonQueue.Click += new System.EventHandler(this.buttonQueue_Click);
            // 
            // buttonStopHebrew
            // 
            this.buttonStopHebrew.Enabled = false;
            this.buttonStopHebrew.Location = new System.Drawing.Point(397, 70);
            this.buttonStopHebrew.Name = "buttonStopHebrew";
            this.buttonStopHebrew.Size = new System.Drawing.Size(49, 23);
            this.buttonStopHebrew.TabIndex = 8;
            this.buttonStopHebrew.Text = "Stop";
            this.buttonStopHebrew.UseVisualStyleBackColor = true;
            this.buttonStopHebrew.Click += new System.EventHandler(this.buttonStopHebrew_Click);
            // 
            // FormHebrewReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 223);
            this.Controls.Add(this.buttonStopHebrew);
            this.Controls.Add(this.buttonQueue);
            this.Controls.Add(this.buttonStopEnglish);
            this.Controls.Add(this.labelVoices);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonSpeakEnglish);
            this.Controls.Add(this.textBoxEnglish);
            this.Controls.Add(this.buttonSpeakHebrew);
            this.Controls.Add(this.textBoxHebrew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHebrewReader";
            this.Text = "Hebrew Reader";
            this.Load += new System.EventHandler(this.FormHebrewReader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxHebrew;
        private System.Windows.Forms.Button buttonSpeakHebrew;
        private System.Windows.Forms.TextBox textBoxEnglish;
        private System.Windows.Forms.Button buttonSpeakEnglish;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label labelVoices;
        private System.Windows.Forms.Button buttonStopEnglish;
        private System.Windows.Forms.Button buttonQueue;
        private System.Windows.Forms.Button buttonStopHebrew;
    }
}

