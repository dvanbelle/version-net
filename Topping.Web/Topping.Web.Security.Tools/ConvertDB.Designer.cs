namespace Topping.Web.Security.Tools
{
    partial class ConvertDB
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
            this.txtPathSqlLite = new System.Windows.Forms.TextBox();
            this.OFDPathDB = new System.Windows.Forms.OpenFileDialog();
            this.btnPath = new System.Windows.Forms.Button();
            this.lblPathDB = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPathSqlLite
            // 
            this.txtPathSqlLite.Location = new System.Drawing.Point(138, 15);
            this.txtPathSqlLite.Name = "txtPathSqlLite";
            this.txtPathSqlLite.Size = new System.Drawing.Size(462, 22);
            this.txtPathSqlLite.TabIndex = 0;
            // 
            // OFDPathDB
            // 
            this.OFDPathDB.Filter = "\"SqlLite (*.db)|*.db||\"";
            this.OFDPathDB.FilterIndex = 0;
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(636, 12);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(75, 25);
            this.btnPath.TabIndex = 1;
            this.btnPath.Text = "...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // lblPathDB
            // 
            this.lblPathDB.AutoSize = true;
            this.lblPathDB.Location = new System.Drawing.Point(22, 15);
            this.lblPathDB.Name = "lblPathDB";
            this.lblPathDB.Size = new System.Drawing.Size(97, 17);
            this.lblPathDB.TabIndex = 2;
            this.lblPathDB.Text = "Path db sqllite";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(138, 64);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(178, 33);
            this.btnConvert.TabIndex = 3;
            this.btnConvert.Text = "Convert File";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // ConvertDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 255);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.lblPathDB);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.txtPathSqlLite);
            this.Name = "ConvertDB";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPathSqlLite;
        private System.Windows.Forms.OpenFileDialog OFDPathDB;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.Label lblPathDB;
        private System.Windows.Forms.Button btnConvert;
    }
}

