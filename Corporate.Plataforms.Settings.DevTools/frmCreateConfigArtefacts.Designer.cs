namespace Corporate.Plataforms.Settings.DevTools
{
    partial class frmCreateConfigArtefacts
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
            this.lblPathConfigFiles = new System.Windows.Forms.Label();
            this.txtPathConfigFile = new System.Windows.Forms.TextBox();
            this.btnReadFiles = new System.Windows.Forms.Button();
            this.cbExtension = new System.Windows.Forms.ComboBox();
            this.lblExtension = new System.Windows.Forms.Label();
            this.txtApplicationName = new System.Windows.Forms.TextBox();
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.dgvSettings = new System.Windows.Forms.DataGridView();
            this.btnClasses = new System.Windows.Forms.Button();
            this.btnInserts = new System.Windows.Forms.Button();
            this.Instance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Reference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPathConfigFiles
            // 
            this.lblPathConfigFiles.AutoSize = true;
            this.lblPathConfigFiles.Location = new System.Drawing.Point(13, 48);
            this.lblPathConfigFiles.Name = "lblPathConfigFiles";
            this.lblPathConfigFiles.Size = new System.Drawing.Size(118, 17);
            this.lblPathConfigFiles.TabIndex = 0;
            this.lblPathConfigFiles.Text = "Path Config Files:";
            // 
            // txtPathConfigFile
            // 
            this.txtPathConfigFile.Location = new System.Drawing.Point(137, 45);
            this.txtPathConfigFile.Name = "txtPathConfigFile";
            this.txtPathConfigFile.Size = new System.Drawing.Size(353, 22);
            this.txtPathConfigFile.TabIndex = 1;
            this.txtPathConfigFile.Text = "C:\\Lixo\\FilesConfig\\Nd.Notification";
            // 
            // btnReadFiles
            // 
            this.btnReadFiles.Location = new System.Drawing.Point(16, 133);
            this.btnReadFiles.Name = "btnReadFiles";
            this.btnReadFiles.Size = new System.Drawing.Size(115, 26);
            this.btnReadFiles.TabIndex = 3;
            this.btnReadFiles.Text = "Read Files";
            this.btnReadFiles.UseVisualStyleBackColor = true;
            this.btnReadFiles.Click += new System.EventHandler(this.btnReadFiles_Click);
            // 
            // cbExtension
            // 
            this.cbExtension.DisplayMember = "*.config";
            this.cbExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExtension.FormattingEnabled = true;
            this.cbExtension.Items.AddRange(new object[] {
            "*.config",
            "*.ini",
            "*.xml",
            "*.cfg"});
            this.cbExtension.Location = new System.Drawing.Point(137, 76);
            this.cbExtension.Name = "cbExtension";
            this.cbExtension.Size = new System.Drawing.Size(140, 24);
            this.cbExtension.TabIndex = 4;
            this.cbExtension.SelectedIndex = 0;
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point(13, 76);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(73, 17);
            this.lblExtension.TabIndex = 5;
            this.lblExtension.Text = "Extension:";
            // 
            // txtApplicationName
            // 
            this.txtApplicationName.Location = new System.Drawing.Point(137, 14);
            this.txtApplicationName.Name = "txtApplicationName";
            this.txtApplicationName.Size = new System.Drawing.Size(353, 22);
            this.txtApplicationName.TabIndex = 7;
            this.txtApplicationName.Text = "Nd.Notification";
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.AutoSize = true;
            this.lblApplicationName.Location = new System.Drawing.Point(13, 17);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(118, 17);
            this.lblApplicationName.TabIndex = 6;
            this.lblApplicationName.Text = "ApplicationName:";
            // 
            // dgvSettings
            // 
            this.dgvSettings.AllowUserToAddRows = false;
            this.dgvSettings.AllowUserToDeleteRows = false;
            this.dgvSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Instance,
            this.ReferenceType,
            this.Reference,
            this.Property,
            this.Type,
            this.Value});
            this.dgvSettings.Location = new System.Drawing.Point(12, 176);
            this.dgvSettings.Name = "dgvSettings";
            this.dgvSettings.RowHeadersWidth = 51;
            this.dgvSettings.RowTemplate.Height = 24;
            this.dgvSettings.Size = new System.Drawing.Size(820, 276);
            this.dgvSettings.TabIndex = 8;
            this.dgvSettings.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSettings_CellEndEdit);
            this.dgvSettings.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvSettings_CurrentCellDirtyStateChanged);
            // 
            // btnClasses
            // 
            this.btnClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClasses.Location = new System.Drawing.Point(13, 465);
            this.btnClasses.Name = "btnClasses";
            this.btnClasses.Size = new System.Drawing.Size(122, 35);
            this.btnClasses.TabIndex = 9;
            this.btnClasses.Text = "Create Classes";
            this.btnClasses.UseVisualStyleBackColor = true;
            this.btnClasses.Click += new System.EventHandler(this.btnClasses_Click);
            // 
            // btnInserts
            // 
            this.btnInserts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInserts.Location = new System.Drawing.Point(147, 466);
            this.btnInserts.Name = "btnInserts";
            this.btnInserts.Size = new System.Drawing.Size(122, 35);
            this.btnInserts.TabIndex = 10;
            this.btnInserts.Text = "Create Inserts";
            this.btnInserts.UseVisualStyleBackColor = true;
            this.btnInserts.Click += new System.EventHandler(this.btnInserts_Click);
            // 
            // Instance
            // 
            this.Instance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Instance.HeaderText = "Instance";
            this.Instance.MinimumWidth = 6;
            this.Instance.Name = "Instance";
            this.Instance.ReadOnly = true;
            this.Instance.Width = 90;
            // 
            // ReferenceType
            // 
            this.ReferenceType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ReferenceType.HeaderText = "ReferenceType";
            this.ReferenceType.MinimumWidth = 6;
            this.ReferenceType.Name = "ReferenceType";
            this.ReferenceType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReferenceType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ReferenceType.Width = 135;
            // 
            // Reference
            // 
            this.Reference.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Reference.HeaderText = "Reference";
            this.Reference.MinimumWidth = 6;
            this.Reference.Name = "Reference";
            this.Reference.ReadOnly = true;
            this.Reference.Width = 103;
            // 
            // Property
            // 
            this.Property.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Property.HeaderText = "Property";
            this.Property.MinimumWidth = 6;
            this.Property.Name = "Property";
            this.Property.ReadOnly = true;
            this.Property.Width = 91;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 6;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 69;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "Value";
            this.Value.MinimumWidth = 6;
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // frmCreateConfigArtefacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 516);
            this.Controls.Add(this.btnInserts);
            this.Controls.Add(this.btnClasses);
            this.Controls.Add(this.dgvSettings);
            this.Controls.Add(this.txtApplicationName);
            this.Controls.Add(this.lblApplicationName);
            this.Controls.Add(this.lblExtension);
            this.Controls.Add(this.cbExtension);
            this.Controls.Add(this.btnReadFiles);
            this.Controls.Add(this.txtPathConfigFile);
            this.Controls.Add(this.lblPathConfigFiles);
            this.Name = "frmCreateConfigArtefacts";
            this.Text = "Create Config Artefacts";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPathConfigFiles;
        private System.Windows.Forms.TextBox txtPathConfigFile;
        private System.Windows.Forms.Button btnReadFiles;
        private System.Windows.Forms.ComboBox cbExtension;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.TextBox txtApplicationName;
        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.DataGridView dgvSettings;
        private System.Windows.Forms.Button btnClasses;
        private System.Windows.Forms.Button btnInserts;
        private System.Windows.Forms.DataGridViewTextBoxColumn Instance;
        private System.Windows.Forms.DataGridViewComboBoxColumn ReferenceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reference;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}