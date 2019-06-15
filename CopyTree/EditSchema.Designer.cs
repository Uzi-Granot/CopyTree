namespace CopyTree
{
	partial class EditSchema
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
			this.label1 = new System.Windows.Forms.Label();
			this.RootsListBox = new System.Windows.Forms.ListBox();
			this.RootAddButton = new System.Windows.Forms.Button();
			this.RootModifyButton = new System.Windows.Forms.Button();
			this.RootDeleteButton = new System.Windows.Forms.Button();
			this.FolderDeleteButton = new System.Windows.Forms.Button();
			this.FolderModifyButton = new System.Windows.Forms.Button();
			this.FolderAddButton = new System.Windows.Forms.Button();
			this.FoldersListBox = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.CancelSchemaButton = new System.Windows.Forms.Button();
			this.SaveSchemaButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.Control;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(6, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(162, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "Backup Root Folder";
			// 
			// RootsListBox
			// 
			this.RootsListBox.FormattingEnabled = true;
			this.RootsListBox.ItemHeight = 16;
			this.RootsListBox.Location = new System.Drawing.Point(8, 50);
			this.RootsListBox.Name = "RootsListBox";
			this.RootsListBox.Size = new System.Drawing.Size(470, 132);
			this.RootsListBox.TabIndex = 1;
			// 
			// RootAddButton
			// 
			this.RootAddButton.Location = new System.Drawing.Point(494, 51);
			this.RootAddButton.Name = "RootAddButton";
			this.RootAddButton.Size = new System.Drawing.Size(93, 35);
			this.RootAddButton.TabIndex = 2;
			this.RootAddButton.Text = "Add";
			this.RootAddButton.UseVisualStyleBackColor = true;
			this.RootAddButton.Click += new System.EventHandler(this.OnRootAdd);
			// 
			// RootModifyButton
			// 
			this.RootModifyButton.Location = new System.Drawing.Point(494, 99);
			this.RootModifyButton.Name = "RootModifyButton";
			this.RootModifyButton.Size = new System.Drawing.Size(93, 35);
			this.RootModifyButton.TabIndex = 3;
			this.RootModifyButton.Text = "Modify";
			this.RootModifyButton.UseVisualStyleBackColor = true;
			this.RootModifyButton.Click += new System.EventHandler(this.OnRootModify);
			// 
			// RootDeleteButton
			// 
			this.RootDeleteButton.Location = new System.Drawing.Point(494, 147);
			this.RootDeleteButton.Name = "RootDeleteButton";
			this.RootDeleteButton.Size = new System.Drawing.Size(93, 35);
			this.RootDeleteButton.TabIndex = 4;
			this.RootDeleteButton.Text = "Delete";
			this.RootDeleteButton.UseVisualStyleBackColor = true;
			this.RootDeleteButton.Click += new System.EventHandler(this.OnRootDelete);
			// 
			// FolderDeleteButton
			// 
			this.FolderDeleteButton.Location = new System.Drawing.Point(229, 518);
			this.FolderDeleteButton.Name = "FolderDeleteButton";
			this.FolderDeleteButton.Size = new System.Drawing.Size(93, 35);
			this.FolderDeleteButton.TabIndex = 9;
			this.FolderDeleteButton.Text = "Delete";
			this.FolderDeleteButton.UseVisualStyleBackColor = true;
			this.FolderDeleteButton.Click += new System.EventHandler(this.OnFolderDelete);
			// 
			// FolderModifyButton
			// 
			this.FolderModifyButton.Location = new System.Drawing.Point(119, 518);
			this.FolderModifyButton.Name = "FolderModifyButton";
			this.FolderModifyButton.Size = new System.Drawing.Size(93, 35);
			this.FolderModifyButton.TabIndex = 8;
			this.FolderModifyButton.Text = "Modify";
			this.FolderModifyButton.UseVisualStyleBackColor = true;
			this.FolderModifyButton.Click += new System.EventHandler(this.OnFolderModify);
			// 
			// FolderAddButton
			// 
			this.FolderAddButton.Location = new System.Drawing.Point(8, 518);
			this.FolderAddButton.Name = "FolderAddButton";
			this.FolderAddButton.Size = new System.Drawing.Size(93, 35);
			this.FolderAddButton.TabIndex = 7;
			this.FolderAddButton.Text = "Add";
			this.FolderAddButton.UseVisualStyleBackColor = true;
			this.FolderAddButton.Click += new System.EventHandler(this.OnFolderAdd);
			// 
			// FoldersListBox
			// 
			this.FoldersListBox.FormattingEnabled = true;
			this.FoldersListBox.ItemHeight = 16;
			this.FoldersListBox.Location = new System.Drawing.Point(10, 229);
			this.FoldersListBox.Name = "FoldersListBox";
			this.FoldersListBox.Size = new System.Drawing.Size(664, 276);
			this.FoldersListBox.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.SystemColors.Control;
			this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(6, 204);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(379, 19);
			this.label2.TabIndex = 5;
			this.label2.Text = "Backup Subfolder Name and Source Folder Path";
			// 
			// CancelSchemaButton
			// 
			this.CancelSchemaButton.Location = new System.Drawing.Point(580, 518);
			this.CancelSchemaButton.Name = "CancelSchemaButton";
			this.CancelSchemaButton.Size = new System.Drawing.Size(93, 35);
			this.CancelSchemaButton.TabIndex = 11;
			this.CancelSchemaButton.Text = "Cancel";
			this.CancelSchemaButton.UseVisualStyleBackColor = true;
			this.CancelSchemaButton.Click += new System.EventHandler(this.OnSchemaCancel);
			// 
			// SaveSchemaButton
			// 
			this.SaveSchemaButton.Location = new System.Drawing.Point(470, 518);
			this.SaveSchemaButton.Name = "SaveSchemaButton";
			this.SaveSchemaButton.Size = new System.Drawing.Size(93, 35);
			this.SaveSchemaButton.TabIndex = 10;
			this.SaveSchemaButton.Text = "Save";
			this.SaveSchemaButton.UseVisualStyleBackColor = true;
			this.SaveSchemaButton.Click += new System.EventHandler(this.OnSchemaSave);
			// 
			// EditSchema
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 561);
			this.Controls.Add(this.CancelSchemaButton);
			this.Controls.Add(this.SaveSchemaButton);
			this.Controls.Add(this.FolderDeleteButton);
			this.Controls.Add(this.FolderModifyButton);
			this.Controls.Add(this.FolderAddButton);
			this.Controls.Add(this.FoldersListBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.RootDeleteButton);
			this.Controls.Add(this.RootModifyButton);
			this.Controls.Add(this.RootAddButton);
			this.Controls.Add(this.RootsListBox);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "EditSchema";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Backup Schema";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox RootsListBox;
		private System.Windows.Forms.Button RootAddButton;
		private System.Windows.Forms.Button RootModifyButton;
		private System.Windows.Forms.Button RootDeleteButton;
		private System.Windows.Forms.Button FolderDeleteButton;
		private System.Windows.Forms.Button FolderModifyButton;
		private System.Windows.Forms.Button FolderAddButton;
		private System.Windows.Forms.ListBox FoldersListBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button CancelSchemaButton;
		private System.Windows.Forms.Button SaveSchemaButton;
	}
}