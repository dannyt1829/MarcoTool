namespace MarcoCreatorTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            recordButton = new Button();
            play = new Button();
            saveButton = new Button();
            loadButton = new Button();
            SuspendLayout();
            // 
            // recordButton
            // 
            recordButton.Location = new Point(12, 12);
            recordButton.Name = "recordButton";
            recordButton.Size = new Size(75, 75);
            recordButton.TabIndex = 0;
            recordButton.Text = "Record";
            recordButton.UseVisualStyleBackColor = true;
            recordButton.Click += recordButton_Click;
            // 
            // play
            // 
            play.Location = new Point(93, 12);
            play.Name = "play";
            play.Size = new Size(75, 75);
            play.TabIndex = 1;
            play.Text = "Play";
            play.UseVisualStyleBackColor = true;
            play.Click += play_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(174, 12);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 75);
            saveButton.TabIndex = 2;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // loadButton
            // 
            loadButton.Location = new Point(255, 12);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(75, 75);
            loadButton.TabIndex = 3;
            loadButton.Text = "Load";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += loadButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(354, 99);
            Controls.Add(loadButton);
            Controls.Add(saveButton);
            Controls.Add(play);
            Controls.Add(recordButton);
            Name = "Form1";
            Text = "Macro Recorder V0";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button recordButton;
        private Button play;
        private Button button1;
        private Button saveButton;
        private Button loadButton;
    }
}
