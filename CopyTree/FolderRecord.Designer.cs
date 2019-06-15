namespace CopyTree
{
	partial class FolderRecord
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
			this.button1 = new System.Windows.Forms.Button();
			this.Cancel_Button = new System.Windows.Forms.Button();
			this.OK_Button = new System.Windows.Forms.Button();
			this.SourceFolderTextBox = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.BackupNameTextBox = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(432, 37);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(95, 36);
			this.button1.TabIndex = 4;
			this.button1.Text = "Browse";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OnBrowse);
			// 
			// Cancel_Button
			// 
			this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel_Button.Location = new System.Drawing.Point(280, 131);
			this.Cancel_Button.Name = "Cancel_Button";
			this.Cancel_Button.Size = new System.Drawing.Size(95, 36);
			this.Cancel_Button.TabIndex = 6;
			this.Cancel_Button.Text = "Cancel";
			this.Cancel_Button.UseVisualStyleBackColor = true;
			this.Cancel_Button.Click += new System.EventHandler(this.OnCancel);
			// 
			// OK_Button
			// 
			this.OK_Button.Location = new System.Drawing.Point(164, 131);
			this.OK_Button.Name = "OK_Button";
			this.OK_Button.Size = new System.Drawing.Size(95, 36);
			this.OK_Button.TabIndex = 5;
			this.OK_Button.Text = "OK";
			this.OK_Button.UseVisualStyleBackColor = true;
			this.OK_Button.Click += new System.EventHandler(this.OnOK);
			// 
			// SourceFolderTextBox
			// 
			this.SourceFolderTextBox.Location = new System.Drawing.Point(10, 85);
			this.SourceFolderTextBox.MaxLength = 240;
			this.SourceFolderTextBox.Name = "SourceFolderTextBox";
			this.SourceFolderTextBox.Size = new System.Drawing.Size(517, 22);
			this.SourceFolderTextBox.TabIndex = 3;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(10, 66);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(120, 16);
			this.label14.TabIndex = 2;
			this.label14.Text = "Source Folder Path";
			// 
			// BackupNameTextBox
			// 
			this.BackupNameTextBox.Location = new System.Drawing.Point(10, 31);
			this.BackupNameTextBox.MaxLength = 20;
			this.BackupNameTextBox.Name = "BackupNameTextBox";
			this.BackupNameTextBox.Size = new System.Drawing.Size(169, 22);
			this.BackupNameTextBox.TabIndex = 1;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(10, 12);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(148, 16);
			this.label13.TabIndex = 0;
			this.label13.Text = "Backup Subfolder Name";
			// 
			// FolderRecord
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(539, 182);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.Cancel_Button);
			this.Controls.Add(this.OK_Button);
			this.Controls.Add(this.SourceFolderTextBox);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.BackupNameTextBox);
			this.Controls.Add(this.label13);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FolderRecord";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Source Folder Path and Backup Subfolder Name";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button Cancel_Button;
		private System.Windows.Forms.Button OK_Button;
		private System.Windows.Forms.TextBox SourceFolderTextBox;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox BackupNameTextBox;
		private System.Windows.Forms.Label label13;
	}
}