namespace TopMachine.Training.FrontEnd
{
    partial class ImportForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbExtension = new System.Windows.Forms.ComboBox();
            this.bindingConfig = new System.Windows.Forms.BindingSource(this.components);
            this.lblType = new System.Windows.Forms.Label();
            this.groupDestination = new System.Windows.Forms.GroupBox();
            this.txtGameName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.cbList = new System.Windows.Forms.ComboBox();
            this.bindingParams = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.groupImportSource = new System.Windows.Forms.GroupBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.chkLstCategorie = new System.Windows.Forms.CheckedListBox();
            this.criteriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileImport = new System.Windows.Forms.OpenFileDialog();
            this.btnClose = new TopMachine.Desktop.Controls.ImageButton();
            this.btnSave = new TopMachine.Desktop.Controls.ImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).BeginInit();
            this.groupDestination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingParams)).BeginInit();
            this.groupImportSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cbExtension
            // 
            this.cbExtension.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingConfig, "Dico", true));
            this.cbExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExtension.Location = new System.Drawing.Point(11, 56);
            this.cbExtension.Margin = new System.Windows.Forms.Padding(4);
            this.cbExtension.Name = "cbExtension";
            this.cbExtension.Size = new System.Drawing.Size(207, 24);
            this.cbExtension.TabIndex = 53;
            this.cbExtension.SelectedIndexChanged += new System.EventHandler(this.cbExtension_SelectedIndexChanged);
            // 
            // bindingConfig
            // 
            this.bindingConfig.DataSource = typeof(TopMachine.Training.DAL.fdbo.ListConfig);
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(8, 32);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(122, 20);
            this.lblType.TabIndex = 52;
            this.lblType.Text = "Type de fichier :";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupDestination
            // 
            this.groupDestination.BackColor = System.Drawing.Color.DarkGray;
            this.groupDestination.Controls.Add(this.txtGameName);
            this.groupDestination.Controls.Add(this.lblName);
            this.groupDestination.Controls.Add(this.cbList);
            this.groupDestination.Controls.Add(this.label8);
            this.groupDestination.Location = new System.Drawing.Point(521, 12);
            this.groupDestination.Margin = new System.Windows.Forms.Padding(4);
            this.groupDestination.Name = "groupDestination";
            this.groupDestination.Padding = new System.Windows.Forms.Padding(4);
            this.groupDestination.Size = new System.Drawing.Size(372, 357);
            this.groupDestination.TabIndex = 51;
            this.groupDestination.TabStop = false;
            this.groupDestination.Text = "Destination";
            // 
            // txtGameName
            // 
            this.txtGameName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Name", true));
            this.txtGameName.Location = new System.Drawing.Point(99, 91);
            this.txtGameName.Margin = new System.Windows.Forms.Padding(4);
            this.txtGameName.Name = "txtGameName";
            this.txtGameName.Size = new System.Drawing.Size(190, 22);
            this.txtGameName.TabIndex = 43;
            this.txtGameName.Text = "Nouvelle Liste";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(8, 91);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(83, 20);
            this.lblName.TabIndex = 42;
            this.lblName.Text = "Nom :";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbList
            // 
            this.cbList.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingParams, "TypeOfPlay", true));
            this.cbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbList.Location = new System.Drawing.Point(8, 43);
            this.cbList.Margin = new System.Windows.Forms.Padding(4);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(333, 24);
            this.cbList.TabIndex = 94;
            this.cbList.SelectedIndexChanged += new System.EventHandler(this.cbList_SelectedIndexChanged);
            // 
            // bindingParams
            // 
            this.bindingParams.DataSource = typeof(TopMachine.Training.DAL.fdbo.GameConfig);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 19);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(164, 20);
            this.label8.TabIndex = 95;
            this.label8.Text = "Nom de la liste :";
            // 
            // groupImportSource
            // 
            this.groupImportSource.BackColor = System.Drawing.Color.DarkGray;
            this.groupImportSource.Controls.Add(this.txtFileName);
            this.groupImportSource.Controls.Add(this.btnFile);
            this.groupImportSource.Controls.Add(this.lblCategory);
            this.groupImportSource.Controls.Add(this.chkLstCategorie);
            this.groupImportSource.Controls.Add(this.cbExtension);
            this.groupImportSource.Controls.Add(this.lblType);
            this.groupImportSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupImportSource.Location = new System.Drawing.Point(12, 10);
            this.groupImportSource.Margin = new System.Windows.Forms.Padding(4);
            this.groupImportSource.Name = "groupImportSource";
            this.groupImportSource.Padding = new System.Windows.Forms.Padding(4);
            this.groupImportSource.Size = new System.Drawing.Size(349, 359);
            this.groupImportSource.TabIndex = 50;
            this.groupImportSource.TabStop = false;
            this.groupImportSource.Text = "Source à importer";
            // 
            // txtFileName
            // 
            this.txtFileName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Name", true));
            this.txtFileName.Location = new System.Drawing.Point(69, 88);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(190, 22);
            this.txtFileName.TabIndex = 57;
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(11, 92);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(51, 22);
            this.btnFile.TabIndex = 56;
            this.btnFile.Text = "...";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // lblCategory
            // 
            this.lblCategory.Location = new System.Drawing.Point(4, 181);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(122, 20);
            this.lblCategory.TabIndex = 55;
            this.lblCategory.Text = "Catégories : ";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkLstCategorie
            // 
            this.chkLstCategorie.CheckOnClick = true;
            this.chkLstCategorie.FormattingEnabled = true;
            this.chkLstCategorie.Location = new System.Drawing.Point(7, 204);
            this.chkLstCategorie.Name = "chkLstCategorie";
            this.chkLstCategorie.Size = new System.Drawing.Size(183, 89);
            this.chkLstCategorie.TabIndex = 54;
            // 
            // criteriaBindingSource
            // 
            this.criteriaBindingSource.DataMember = "Criteria";
            this.criteriaBindingSource.DataSource = this.bindingConfig;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(200)))), ((int)(((byte)(128)))), ((int)(((byte)(68)))));
            this.btnClose.CenterColor = System.Drawing.Color.White;
            this.btnClose.FocusDrawn = false;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(656, 377);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.RecessDepth = 0;
            this.btnClose.Round = 10;
            this.btnClose.Size = new System.Drawing.Size(237, 36);
            this.btnClose.TabIndex = 53;
            this.btnClose.Text = "Fermez";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(68)))));
            this.btnSave.CenterColor = System.Drawing.Color.White;
            this.btnSave.FocusDrawn = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(12, 377);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.RecessDepth = 0;
            this.btnSave.Round = 10;
            this.btnSave.Size = new System.Drawing.Size(237, 36);
            this.btnSave.TabIndex = 52;
            this.btnSave.Text = "Importer";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupDestination);
            this.Controls.Add(this.groupImportSource);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImportForm";
            this.Size = new System.Drawing.Size(907, 460);
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).EndInit();
            this.groupDestination.ResumeLayout(false);
            this.groupDestination.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingParams)).EndInit();
            this.groupImportSource.ResumeLayout(false);
            this.groupImportSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbExtension;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.GroupBox groupDestination;
        private System.Windows.Forms.ComboBox cbList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupImportSource;
        private System.Windows.Forms.TextBox txtGameName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.BindingSource bindingConfig;
        private Desktop.Controls.ImageButton btnSave;
        private System.Windows.Forms.BindingSource bindingParams;
        private System.Windows.Forms.BindingSource criteriaBindingSource;
        private Desktop.Controls.ImageButton btnClose;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.CheckedListBox chkLstCategorie;
        private System.Windows.Forms.OpenFileDialog openFileImport;
    }
}
