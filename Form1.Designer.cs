namespace MiniLTGRebuilderForm
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
            this.LTGRebuild = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LTGRebuild
            // 
            this.LTGRebuild.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LTGRebuild.Location = new System.Drawing.Point(12, 12);
            this.LTGRebuild.Name = "LTGRebuild";
            this.LTGRebuild.Size = new System.Drawing.Size(277, 171);
            this.LTGRebuild.TabIndex = 0;
            this.LTGRebuild.Text = "Rebuild LTG";
            this.LTGRebuild.UseVisualStyleBackColor = true;
            this.LTGRebuild.Click += new System.EventHandler(this.LTGRebuild_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 195);
            this.Controls.Add(this.LTGRebuild);
            this.Name = "Form1";
            this.Text = "LTG Rebuilder";
            this.ResumeLayout(false);

        }

        #endregion

        private Button LTGRebuild;
    }
}