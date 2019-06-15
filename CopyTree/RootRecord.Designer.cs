namespace CopyTree
{
	partial class RootRecord
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
			this.Cancel_Button = new System.Windows.Forms.Button();
			this.OK_Button = new System.Windows.Forms.Button();
			this.RootNameTextBox = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.LastBackupTextBox = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Cancel_Button
			// 
			this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel_Button.Location = new System.Drawing.Point(233, 132);
			this.Cancel_Button.Name = "Cancel_Button";
			this.Cancel_Button.Size = new System.Drawing.Size(95, 36);
			this.Cancel_Button.TabIndex = 6;
			this.Cancel_Button.Text = "Cancel";
			this.Cancel_Button.UseVisualStyleBackColor = true;
			this.Cancel_Button.Click += new System.EventHandler(this.OnCancel);
			// 
			// OK_Button
			// 
			this.OK_Button.Location = new System.Drawing.Point(117, 132);
			this.OK_Button.Name = "OK_Button";
			this.OK_Button.Size = new System.Drawing.Size(95, 36);
			this.OK_Button.TabIndex = 5;
			this.OK_Button.Text = "OK";
			this.OK_Button.UseVisualStyleBackColor = true;
			this.OK_Button.Click += new System.EventHandler(this.OnOK);
			// 
			// RootNameTextBox
			// 
			this.RootNameTextBox.Location = new System.Drawing.Point(13, 86);
			this.RootNameTextBox.MaxLength = 250;
			this.RootNameTextBox.Name = "RootNameTextBox";
			this.RootNameTextBox.Size = new System.Drawing.Size(313, 22);
			this.RootNameTextBox.TabIndex = 3;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(13, 67);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(123, 16);
			this.label14.TabIndex = 2;
			this.label14.Text = "Backup Root Folder";
			// 
			// LastBackupTextBox
			// 
			this.LastBackupTextBox.Location = new System.Drawing.Point(13, 32);
			this.LastBackupTextBox.MaxLength = 20;
			this.LastBackupTextBox.Name = "LastBackupTextBox";
			this.LastBackupTextBox.Size = new System.Drawing.Size(169, 22);
			this.LastBackupTextBox.TabIndex = 1;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(13, 13);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(169, 16);
			this.label13.TabIndex = 0;
			this.label13.Text = "Last Backup Date and Time";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(336, 79);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(95, 36);
			this.button1.TabIndex = 4;
			this.button1.Text = "Browse";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OnBrowse);
			// 
			// RootRecord
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(444, 184);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.Cancel_Button);
			this.Controls.Add(this.OK_Button);
			this.Controls.Add(this.RootNameTextBox);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.LastBackupTextBox);
			this.Controls.Add(this.label13);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RootRecord";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Backup Root Folder";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button Cancel_Button;
		private System.Windows.Forms.Button OK_Button;
		private System.Windows.Forms.TextBox RootNameTextBox;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox LastBackupTextBox;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Button button1;
	}
}