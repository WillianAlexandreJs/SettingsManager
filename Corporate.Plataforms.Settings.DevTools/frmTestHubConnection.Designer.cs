namespace Corporate.Plataforms.Settings.DevTools
{
    partial class frmTestHubConnection
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
            this.lblInstancesNumber = new System.Windows.Forms.Label();
            this.txtInstancesNumber = new System.Windows.Forms.NumericUpDown();
            this.lblUrlHubConnection = new System.Windows.Forms.Label();
            this.txtUrlHubConnection = new System.Windows.Forms.TextBox();
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.txtApplicationName = new System.Windows.Forms.TextBox();
            this.btnStartHub = new System.Windows.Forms.Button();
            this.btnStopHub = new System.Windows.Forms.Button();
            this.dgvUpdates = new System.Windows.Forms.DataGridView();
            this.txtHubProxyName = new System.Windows.Forms.TextBox();
            this.lblHubProxyName = new System.Windows.Forms.Label();
            this.ClientId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MessageReceived = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstancesNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdates)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInstancesNumber
            // 
            this.lblInstancesNumber.AutoSize = true;
            this.lblInstancesNumber.Location = new System.Drawing.Point(11, 130);
            this.lblInstancesNumber.Name = "lblInstancesNumber";
            this.lblInstancesNumber.Size = new System.Drawing.Size(126, 17);
            this.lblInstancesNumber.TabIndex = 0;
            this.lblInstancesNumber.Text = "Instances Number:";
            // 
            // txtInstancesNumber
            // 
            this.txtInstancesNumber.Location = new System.Drawing.Point(145, 128);
            this.txtInstancesNumber.Name = "txtInstancesNumber";
            this.txtInstancesNumber.Size = new System.Drawing.Size(120, 22);
            this.txtInstancesNumber.TabIndex = 1;
            this.txtInstancesNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblUrlHubConnection
            // 
            this.lblUrlHubConnection.AutoSize = true;
            this.lblUrlHubConnection.Location = new System.Drawing.Point(11, 34);
            this.lblUrlHubConnection.Name = "lblUrlHubConnection";
            this.lblUrlHubConnection.Size = new System.Drawing.Size(131, 17);
            this.lblUrlHubConnection.TabIndex = 2;
            this.lblUrlHubConnection.Text = "Url HubConnection:";
            // 
            // txtUrlHubConnection
            // 
            this.txtUrlHubConnection.Location = new System.Drawing.Point(145, 33);
            this.txtUrlHubConnection.Name = "txtUrlHubConnection";
            this.txtUrlHubConnection.Size = new System.Drawing.Size(331, 22);
            this.txtUrlHubConnection.TabIndex = 3;
            this.txtUrlHubConnection.Text = "http://localhost:38102";
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.AutoSize = true;
            this.lblApplicationName.Location = new System.Drawing.Point(12, 94);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(122, 17);
            this.lblApplicationName.TabIndex = 4;
            this.lblApplicationName.Text = "Application Name:";
            // 
            // txtApplicationName
            // 
            this.txtApplicationName.Location = new System.Drawing.Point(145, 94);
            this.txtApplicationName.Name = "txtApplicationName";
            this.txtApplicationName.Size = new System.Drawing.Size(331, 22);
            this.txtApplicationName.TabIndex = 5;
            this.txtApplicationName.Text = "XpApplication";
            // 
            // btnStartHub
            // 
            this.btnStartHub.Location = new System.Drawing.Point(15, 168);
            this.btnStartHub.Name = "btnStartHub";
            this.btnStartHub.Size = new System.Drawing.Size(122, 31);
            this.btnStartHub.TabIndex = 6;
            this.btnStartHub.Text = "StartHub";
            this.btnStartHub.UseVisualStyleBackColor = true;
            this.btnStartHub.Click += new System.EventHandler(this.btnStartHub_Click);
            // 
            // btnStopHub
            // 
            this.btnStopHub.Location = new System.Drawing.Point(145, 168);
            this.btnStopHub.Name = "btnStopHub";
            this.btnStopHub.Size = new System.Drawing.Size(122, 31);
            this.btnStopHub.TabIndex = 7;
            this.btnStopHub.Text = "StopHub";
            this.btnStopHub.UseVisualStyleBackColor = true;
            // 
            // dgvUpdates
            // 
            this.dgvUpdates.AllowUserToAddRows = false;
            this.dgvUpdates.AllowUserToDeleteRows = false;
            this.dgvUpdates.AllowUserToOrderColumns = true;
            this.dgvUpdates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpdates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClientId,
            this.Date,
            this.MessageReceived});
            this.dgvUpdates.Location = new System.Drawing.Point(15, 224);
            this.dgvUpdates.Name = "dgvUpdates";
            this.dgvUpdates.ReadOnly = true;
            this.dgvUpdates.RowHeadersWidth = 51;
            this.dgvUpdates.RowTemplate.Height = 24;
            this.dgvUpdates.Size = new System.Drawing.Size(761, 233);
            this.dgvUpdates.TabIndex = 8;
            // 
            // txtHubProxyName
            // 
            this.txtHubProxyName.Location = new System.Drawing.Point(145, 61);
            this.txtHubProxyName.Name = "txtHubProxyName";
            this.txtHubProxyName.Size = new System.Drawing.Size(331, 22);
            this.txtHubProxyName.TabIndex = 10;
            this.txtHubProxyName.Text = "SettingsHub";
            // 
            // lblHubProxyName
            // 
            this.lblHubProxyName.AutoSize = true;
            this.lblHubProxyName.Location = new System.Drawing.Point(11, 62);
            this.lblHubProxyName.Name = "lblHubProxyName";
            this.lblHubProxyName.Size = new System.Drawing.Size(106, 17);
            this.lblHubProxyName.TabIndex = 9;
            this.lblHubProxyName.Text = "HubProxyName";
            // 
            // ClientId
            // 
            this.ClientId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ClientId.HeaderText = "InstanceId";
            this.ClientId.MinimumWidth = 6;
            this.ClientId.Name = "ClientId";
            this.ClientId.ReadOnly = true;
            this.ClientId.Width = 101;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.MinimumWidth = 6;
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 125;
            // 
            // MessageReceived
            // 
            this.MessageReceived.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MessageReceived.HeaderText = "MessageReceived";
            this.MessageReceived.MinimumWidth = 6;
            this.MessageReceived.Name = "MessageReceived";
            this.MessageReceived.ReadOnly = true;
            // 
            // frmTestHubConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 475);
            this.Controls.Add(this.txtHubProxyName);
            this.Controls.Add(this.lblHubProxyName);
            this.Controls.Add(this.dgvUpdates);
            this.Controls.Add(this.btnStopHub);
            this.Controls.Add(this.btnStartHub);
            this.Controls.Add(this.txtApplicationName);
            this.Controls.Add(this.lblApplicationName);
            this.Controls.Add(this.txtUrlHubConnection);
            this.Controls.Add(this.lblUrlHubConnection);
            this.Controls.Add(this.txtInstancesNumber);
            this.Controls.Add(this.lblInstancesNumber);
            this.Name = "frmTestHubConnection";
            this.Text = "frmTestHubConnection";
            ((System.ComponentModel.ISupportInitialize)(this.txtInstancesNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInstancesNumber;
        private System.Windows.Forms.NumericUpDown txtInstancesNumber;
        private System.Windows.Forms.Label lblUrlHubConnection;
        private System.Windows.Forms.TextBox txtUrlHubConnection;
        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.TextBox txtApplicationName;
        private System.Windows.Forms.Button btnStartHub;
        private System.Windows.Forms.Button btnStopHub;
        private System.Windows.Forms.DataGridView dgvUpdates;
        private System.Windows.Forms.TextBox txtHubProxyName;
        private System.Windows.Forms.Label lblHubProxyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessageReceived;
    }
}