namespace cs
{
    partial class Help
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
            this._edHelp = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _edHelp
            // 
            this._edHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this._edHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._edHelp.ForeColor = System.Drawing.Color.Navy;
            this._edHelp.Location = new System.Drawing.Point(1, 5);
            this._edHelp.Multiline = true;
            this._edHelp.Name = "_edHelp";
            this._edHelp.ReadOnly = true;
            this._edHelp.Size = new System.Drawing.Size(676, 267);
            this._edHelp.TabIndex = 0;
            this._edHelp.WordWrap = false;
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 273);
            this.Controls.Add(this._edHelp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Help";
            this.Text = "TWSLink C# Sample Help";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _edHelp;
    }
}