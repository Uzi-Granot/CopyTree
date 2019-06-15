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
using System.Windows.Forms;

namespace CopyTree
{
/// <summary>
/// Edit backup root record
/// </summary>
public partial class RootRecord : Form
	{
	/// <summary>
	/// New root record
	/// </summary>
	public Root NewRoot;

	/// <summary>
	/// New root index position
	/// </summary>
	public int RootIndex;

	private ListBox.ObjectCollection Items;
	private int SelectedIndex;

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="Items">RootListBox items</param>
	/// <param name="SelectedIndex">Selected index position or -1 for add</param>
	public RootRecord
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
	/// initialization
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnLoad(object sender, EventArgs e)
		{
		if(SelectedIndex >= 0)
			{
			NewRoot = (Root) Items[SelectedIndex];
			LastBackupTextBox.Text = NewRoot.LastBackup.ToString(CustomCultureInfo.CustomDateTime);
			RootNameTextBox.Text = NewRoot.RootName;
			}
		else
			{
			// initialize with date time 24 hours ago
			LastBackupTextBox.Text = DateTime.Now.AddDays(-1).ToString(CustomCultureInfo.CustomDateTime);
			}
		return;
		}

	/// <summary>
	/// Browse for folder
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnBrowse(object sender, EventArgs e)
		{
		FolderBrowserDialog Dialog = new FolderBrowserDialog();
		Dialog.Description = "Browse for Backup Root Folder";
		Dialog.ShowNewFolderButton = true;
		if(Dialog.ShowDialog(this) == DialogResult.OK) RootNameTextBox.Text = Dialog.SelectedPath;
		Dialog.Dispose();
		return;
		}

	/// <summary>
	/// Accept editing
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnOK(object sender, EventArgs e)
		{
		// test last backup date and time
		DateTime LastBackup = Root.ParseDateTime(LastBackupTextBox.Text);
		if(LastBackup == DateTime.MinValue)
			{
			MessageBox.Show("Last backup format is in error");
			return;
			}

		// test root name
		string RootName = RootNameTextBox.Text.Trim();
		if(RootName.Length < 4 || !char.IsLetter(RootName[0]) || RootName[1] != ':' || RootName[2] != '\\')
			{
			MessageBox.Show("Invalid root name");
			return;
			}

		// test duplication
		int Index;
		for(Index = 0; Index < Items.Count; Index++)
			{
			if(((Root) Items[Index]).RootName == RootName && Index != SelectedIndex) break;
			}
		if(Index < Items.Count)
			{
			MessageBox.Show("Duplicate root name is not allowed");
			return;
			}

		// find index position
		for(RootIndex = 0; RootIndex < Items.Count; RootIndex++)
			{
			if(((Root) Items[RootIndex]).LastBackup >= LastBackup) break;
			}

		// new root record
		NewRoot = new Root(LastBackup, RootName);
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
