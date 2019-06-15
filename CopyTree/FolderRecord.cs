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
/// Edit folder record
/// </summary>
public partial class FolderRecord : Form
	{
	/// <summary>
	/// New or modified record
	/// </summary>
	public Folder NewFolder;

	/// <summary>
	/// Record index
	/// </summary>
	public int FolderIndex;

	private ListBox.ObjectCollection Items;
	private int SelectedIndex;

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="Items">FolderListBox items</param>
	/// <param name="SelectedIndex">Selected index or -1 for add</param>
	public FolderRecord
			(
			ListBox.ObjectCollection Items,
			int SelectedIndex
			)
		{
		this.Items = Items;
		this.SelectedIndex = SelectedIndex;
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
		if(SelectedIndex >= 0)
			{
			NewFolder = (Folder) Items[SelectedIndex];
			BackupNameTextBox.Text = NewFolder.BackupName;
			SourceFolderTextBox.Text = NewFolder.SourceFolder;
			}
		return;
		}

	/// <summary>
	/// Browse for backup folder
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnBrowse(object sender, EventArgs e)
		{
		FolderBrowserDialog Dialog = new FolderBrowserDialog();
		Dialog.Description = "Browse for Source Folder";
		Dialog.ShowNewFolderButton = false;
		if(Dialog.ShowDialog(this) == DialogResult.OK)
			{
			SourceFolderTextBox.Text = Dialog.SelectedPath;
			if(string.IsNullOrWhiteSpace(BackupNameTextBox.Text))
				{
				try
					{
					BackupNameTextBox.Text = (new DirectoryInfo(Dialog.SelectedPath)).Name;
					}
				catch(Exception){}
				}
			}
		Dialog.Dispose();
		return;
		}

	/// <summary>
	/// Accept changes
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnOK(object sender, EventArgs e)
		{
		// backup folder name
		string BackupName = BackupNameTextBox.Text.Trim();
		if(string.IsNullOrWhiteSpace(BackupName))
			{
			MessageBox.Show("Backup name is empty");
			return;
			}

		// source folder
		string SourceFolder = SourceFolderTextBox.Text.Trim();
		if(SourceFolder.Length < 4 || !char.IsLetter(SourceFolder[0]) || SourceFolder[1] != ':' || SourceFolder[2] != '\\')
			{
			MessageBox.Show("Invalid source folder");
			return;
			}

		// test for duplication
		int Index;
		for(Index = 0; Index < Items.Count; Index++)
			{
			if(((Folder) Items[Index]).BackupName == BackupName && Index != SelectedIndex) break;
			if(((Folder) Items[Index]).SourceFolder == SourceFolder && Index != SelectedIndex) break;
			}
		if(Index < Items.Count)
			{
			MessageBox.Show("Duplicate is not allowed");
			return;
			}

		// find index position
		for(FolderIndex = 0; FolderIndex < Items.Count; FolderIndex++)
			{
			if(string.Compare(((Folder) Items[FolderIndex]).BackupName, BackupName, true) >= 0) break;
			}

		// return new folder record
		NewFolder = new Folder(BackupName, SourceFolder);
		DialogResult = DialogResult.OK;
		return;
		}

	/// <summary>
	/// Cancel editing
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnCancel(object sender, EventArgs e)
		{
		DialogResult = DialogResult.Cancel;
		return;
		}
	}
}
