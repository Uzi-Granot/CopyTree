/////////////////////////////////////////////////////////////////////
//
//	Copy Tree.
//	Backup rotation scheme.
//	Windows Forms Application.
//	Source code developed with visual C#.
//
//	Author: Uzi Granot
//	Version: 1.0
//	Date: January 1, 2018
//	Copyright (C) 2018 Uzi Granot. All Rights Reserved
//
//	The Copy Tree application is a free software.
//	It is distributed under the Code Project Open License (CPOL 1.02)
//	agreement. The full text of the CPOL is given in:
//	https://www.codeproject.com/info/cpol10.aspx
//	
//	The main points of CPOL 1.02 subject to the terms of the License are:
//
//	Source Code and Executable Files can be used in commercial applications;
//	Source Code and Executable Files can be redistributed; and
//	Source Code can be modified to create derivative works.
//	No claim of suitability, guarantee, or any warranty whatsoever is
//	provided. The software is provided "as-is".
//	The Article accompanying the Work may not be distributed or republished
//	without the Author's consent
//
//	Version History:
//
//	Version 1.0 2018/01/01
//		Original revision
//	Complete versions information look at CopyTree.cs source
/////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Windows.Forms;

namespace CopyTree
{
/// <summary>
/// Edit backup schema
/// </summary>
public partial class EditSchema : Form
	{
	/// <summary>
	/// Schema
	/// </summary>
	public Schema Schema;

	/// <summary>
	/// Schema was modified
	/// </summary>
	private bool Modified;

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="Schema">Schema</param>
	public EditSchema
			(
			Schema Schema
			)
		{
		this.Schema = Schema;
		InitializeComponent();
		return;
		}

	/// <summary>
	/// Initialization
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnLoad(object sender, EventArgs e)
		{
		// schema is defined
		if(Schema != null)
			{
			// load backup roots list box
			if(Schema.Roots != null && Schema.Roots.Length > 0)
				{
				foreach(Root Root in Schema.Roots) RootsListBox.Items.Add(Root);
				}

			// load source folders list box
			if(Schema.Folders != null && Schema.Folders.Length > 0)
				{
				foreach(Folder Folder in Schema.Folders) FoldersListBox.Items.Add(Folder);
				}
			}

		// disable roots modify and delete buttons
		if(RootsListBox.Items.Count < 1) RootModifyButton.Enabled = false;
		if(RootsListBox.Items.Count < 2) RootDeleteButton.Enabled = false;

		// disable folders modify and delete buttons
		if(FoldersListBox.Items.Count < 1) FolderModifyButton.Enabled = false;
		if(FoldersListBox.Items.Count < 2) FolderDeleteButton.Enabled = false;
		return;
		}

	/// <summary>
	/// Add a new backup root folder
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnRootAdd(object sender, EventArgs e)
		{
		RootRecord Dialog = new RootRecord(RootsListBox.Items, -1);
		if(Dialog.ShowDialog(this) != DialogResult.OK) return;

		// insert based on sort order
		RootsListBox.Items.Insert(Dialog.RootIndex, Dialog.NewRoot);
		RootsListBox.SelectedIndex = Dialog.RootIndex;
		Modified = true;
		RootModifyButton.Enabled = true;
		if(RootsListBox.Items.Count > 1) RootDeleteButton.Enabled = true;
		return;
		}

	/// <summary>
	/// Modify root folder
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnRootModify(object sender, EventArgs e)
		{
		int Index = RootsListBox.SelectedIndex;
		if(Index < 0) return;
		RootRecord Dialog = new RootRecord(RootsListBox.Items, Index);
		if(Dialog.ShowDialog(this) != DialogResult.OK) return;

		// Root index
		int NewIndex = Dialog.RootIndex;

		// modify text only
		if(NewIndex == Index)
			{
			RootsListBox.Items[Index] = Dialog.NewRoot;
			Modified = true;
			return;
			}

		// delete
		RootsListBox.Items.RemoveAt(Index);

		// adjust new index
		if(Index < NewIndex) NewIndex--;
		
		// insert based on sort order
		RootsListBox.Items.Insert(NewIndex, Dialog.NewRoot);
		RootsListBox.SelectedIndex = NewIndex;
		Modified = true;
		return;
		}

	/// <summary>
	/// Delete root folder
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnRootDelete(object sender, EventArgs e)
		{
		int Index = RootsListBox.SelectedIndex;
		if(Index < 0) return;
		Root DelRoot = (Root) RootsListBox.Items[Index];
		if(MessageBox.Show("Do you want to delete: " + DelRoot.ToString(),
			"Delete Root Record", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
		RootsListBox.Items.RemoveAt(Index);
		if(RootsListBox.Items.Count > 0)
			{
			if(Index == RootsListBox.Items.Count) Index--;
			RootsListBox.SelectedIndex = Index;
			}
		Modified = true;
		if(RootsListBox.Items.Count < 2) RootDeleteButton.Enabled = false;
		return;
		}

	/// <summary>
	/// Add a new source folder
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnFolderAdd(object sender, EventArgs e)
		{
		FolderRecord Dialog = new FolderRecord(FoldersListBox.Items, -1);
		if(Dialog.ShowDialog(this) != DialogResult.OK) return;
		// insert based on sort order
		FoldersListBox.Items.Insert(Dialog.FolderIndex, Dialog.NewFolder);
		FoldersListBox.SelectedIndex = Dialog.FolderIndex;
		Modified = true;
		FolderModifyButton.Enabled = true;
		if(FoldersListBox.Items.Count > 1) FolderDeleteButton.Enabled = true;
		return;
		}

	/// <summary>
	/// Modify source folder
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnFolderModify(object sender, EventArgs e)
		{
		int Index = FoldersListBox.SelectedIndex;
		if(Index < 0) return;
		FolderRecord Dialog = new FolderRecord(FoldersListBox.Items, Index);
		if(Dialog.ShowDialog(this) != DialogResult.OK) return;

		// folder index
		int NewIndex = Dialog.FolderIndex;

		// modify text only
		if(NewIndex == Index)
			{
			FoldersListBox.Items[Index] = Dialog.NewFolder;
			Modified = true;
			return;
			}

		// delete
		FoldersListBox.Items.RemoveAt(Index);

		// adjust new index
		if(Index < NewIndex) NewIndex--;
		
		// insert based on sort order
		FoldersListBox.Items.Insert(NewIndex, Dialog.NewFolder);
		FoldersListBox.SelectedIndex = NewIndex;
		Modified = true;
		return;
		}

	/// <summary>
	/// Delete source folder
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnFolderDelete(object sender, EventArgs e)
		{
		int Index = FoldersListBox.SelectedIndex;
		if(Index < 0) return;
		Folder DelFolder = (Folder) FoldersListBox.Items[Index];
		if(MessageBox.Show("Do you want to delete: " + DelFolder.ToString(),
			"Delete Source Folder Record", MessageBoxButtons.OKCancel) != DialogResult.OK) return;
		FoldersListBox.Items.RemoveAt(Index);
		if(FoldersListBox.Items.Count > 0)
			{
			if(Index == FoldersListBox.Items.Count) Index--;
			FoldersListBox.SelectedIndex = Index;
			}
		Modified = true;
		if(FoldersListBox.Items.Count < 2) FolderDeleteButton.Enabled = false;
		return;
		}

	/// <summary>
	/// Save schema
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnSchemaSave(object sender, EventArgs e)
		{
		// make sure we have roots and folders
		if(RootsListBox.Items.Count == 0 || FoldersListBox.Items.Count == 0)
			{
			MessageBox.Show("You must have at least one backup root folder and one source folder path");
			return;
			}

		// not modified
		if(!Modified)
			{
			// ignore
			DialogResult = DialogResult.Cancel;
			return;
			}

		// extract backup roots from list box
		Root[] Roots = new Root[RootsListBox.Items.Count];
		for(int Index = 0; Index < Roots.Length; Index++) Roots[Index] = (Root) RootsListBox.Items[Index];

		// extract source folders from lis box
		Folder[] Folders = new Folder[FoldersListBox.Items.Count];
		for(int Index = 0; Index < Folders.Length; Index++) Folders[Index] = (Folder) FoldersListBox.Items[Index];

		// create new schema		
		Schema = new Schema(Roots, Folders);

		// test folders
		while(!TestBackupFolders(Schema))
			{
			DialogResult Result = MessageBox.Show("Backup folder structure is not complete.\r\n" +
				"Press Yes to create it.\r\n" +
				"Press No to create it manually later.\r\n" +
				"Press Cancel to edit the schema.\r\n",
				"Create Backup Root Folders", MessageBoxButtons.YesNoCancel);

			if(Result == DialogResult.Cancel) return;
			if(Result == DialogResult.No) break;

			// create base folders
			CreateRootFolders(Schema);
			}

		// save schema to disk
		Schema.Save();
		DialogResult = DialogResult.OK;
		return;
		}

	/// <summary>
	/// Test backup folders for required root folders
	/// </summary>
	/// <param name="Schema">Schema</param>
	/// <returns>Result</returns>
	private bool TestBackupFolders
			(
			Schema Schema
			)
		{
		// make sure backup roots folders exist
		foreach(Root Root in Schema.Roots)
			{
			if(!Directory.Exists(Root.RootName)) return false;
			foreach(Folder Folder in Schema.Folders)
				{
				if(!Directory.Exists(Path.Combine(Root.RootName, Folder.BackupName))) return false;
				}
			}
		return true;
		}

	/// <summary>
	/// Create all required backup folders
	/// </summary>
	/// <param name="Schema">Schema</param>
	private void CreateRootFolders
			(
			Schema Schema
			)
		{
		// make sure roots folders exist
		foreach(Root Root in Schema.Roots)
			{
			// backup root
			if(!Directory.Exists(Root.RootName))
				{
				try
					{
					Directory.CreateDirectory(Root.RootName);
					}
				catch (Exception Ex)
					{
					MessageBox.Show("Warning\r\nCreate folder: " + Root.RootName + " failed.\r\n" + Ex.Message);
					continue;
					}
				}

			// make sure roots folders exist
			foreach(Folder Folder in Schema.Folders)
				{
				string BackupFolder = Path.Combine(Root.RootName, Folder.BackupName);
				if(Directory.Exists(BackupFolder)) continue;
				try
					{
					Directory.CreateDirectory(BackupFolder);
					}
				catch (Exception Ex)
					{
					MessageBox.Show("Warning\r\nCreate folder: " + BackupFolder + " failed.\r\n" + Ex.Message);
					}
				}
			}
		return;
		}

	/// <summary>
	/// Cancel
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnSchemaCancel(object sender, EventArgs e)
		{
		DialogResult = DialogResult.Cancel;
		return;
		}
	}
}
