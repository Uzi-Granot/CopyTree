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
//	Version 1.1 2018/01/08
//		Change DateTime.TryParse out parameter to work with earlier
//		C# compilers.
//	Version 1.2 2018/01/09
//		Fix for first time Go button enable function
/////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CopyTree
{
/// <summary>
/// CopyTree class
/// </summary>
public partial class CopyTree : Form
	{
	private string LogFileName;
	private StreamWriter LogFile;
	private string ErrorFileName;
	private StreamWriter ErrorFile;
	private Schema Schema;
	private Backup Backup;
	private Timer CounterTimer;
	private List<string> ErrorQueue;
	private int StartTime;

	/// <summary>
	/// CopyTree constructor
	/// </summary>
	public CopyTree()
		{
		InitializeComponent();
		Text="CopyTree (Rev 1.3.0 Date 2019-06-14) --- Copyright © 2018-2019 Uzi Granot. All rights reserved.";
		return;
		}

	/// <summary>
	/// On load event
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnLoad(object sender, EventArgs e)
		{
		#if DEBUG
		// current directory
		string CurDir = Environment.CurrentDirectory;
		string WorkDir = CurDir.Replace("bin\\Debug", "Work");
		if(WorkDir != CurDir && Directory.Exists(WorkDir)) Environment.CurrentDirectory = WorkDir;
		#endif

		// backup log
		LogFileName = Path.GetFullPath("CopyTreeLogFile.txt");
		if(!File.Exists(LogFileName)) ViewLogButton.Enabled = false;

		// error log
		ErrorFileName = Path.GetFullPath("CopyTreeErrorFile.txt");
		if(!File.Exists(ErrorFileName)) ViewErrorButton.Enabled = false;

		// load backup schema
		Schema = Schema.Load();
		if(Schema == null)
			{
			// schema not found
			GoButton.Enabled = false;
			}
		else
			{
			// load last backup date and time to combo box
			foreach(Root Root in Schema.Roots) BackupFolderComboBox.Items.Add(Root);

			// select oldest backup
			BackupFolderComboBox.SelectedIndex = 0;
			}

		// disable cancel button
		CancelBackupButton.Enabled = false;

		// create empty error messages queue
		ErrorQueue = new List<string>();

		// create backup class
		Backup = new Backup();
		Backup.WriteToLogFile += WriteToLogFile;
		Backup.BackupCompletedEvent += OnBackupCompleted;

		// create display progress timer
		CounterTimer = new Timer();
		CounterTimer.Interval = 250;
		CounterTimer.Tick += CounterTimer_Tick;
		return;
		}

	/// <summary>
	/// Display backup progress
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void CounterTimer_Tick(object sender, EventArgs e)
		{
		SourceRootLabel.Text = Backup.SourceFolderFullName;
		BackupRootLabel.Text = Backup.BackupFolderFullName;
		FileUpdateLabel.Text = Backup.FileOverrideCounter.ToString("#,###");
		FileCopyLabel.Text = Backup.FileCopyCounter.ToString("#,###");
		FileDeleteLabel.Text = Backup.FileDeleteCounter.ToString("#,###");
		FolderCreateLabel.Text = Backup.FolderCreateCounter.ToString("#,###");
		FolderDeleteLabel.Text = Backup.FolderDeleteCounter.ToString("#,###");

		int ElapseTime = (Environment.TickCount - StartTime) / 1000;
		TimerLabel.Text = string.Format("{0}:{1}", ElapseTime / 60, ElapseTime % 60);

		if(ErrorQueue.Count != 0)
			{
			lock(ErrorQueue)
				{
				foreach(string ErrMsg in ErrorQueue) ErrorLogListBox.Items.Add(ErrMsg);
				ErrorQueue.Clear();
				}
			ErrorLogListBox.SelectedIndex = ErrorLogListBox.Items.Count - 1;
			}
		return;
		}

	/// <summary>
	/// Start backup
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void GoClick(object sender, EventArgs e)
		{
		// test for second backup today
		DateTime TodayDate = DateTime.Now.Date;
		bool BackupToday = false;
		foreach(Root Root in Schema.Roots) if(Root.LastBackup.Date == TodayDate) BackupToday = true;
		if(BackupToday && MessageBox.Show("Do you want to perform a second backup today?",
			"Backup warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

		// disable buttons except cancel
		BackupFolderComboBox.Enabled = false;
		EditSchemaButton.Enabled = false;
		GoButton.Enabled = false;
		CancelBackupButton.Enabled = true;
		ViewLogButton.Enabled = false;
		ViewErrorButton.Enabled = false;
		ErrorLogListBox.Items.Clear();
		TimerLabel.Text = "0";

		// create log file
		LogFile = new StreamWriter(LogFileName);
		ErrorFile = new StreamWriter(ErrorFileName);

		// write date and time
		string LogMsg = DateTime.Now.ToString(CustomCultureInfo.CustomDateTime);
		LogFile.WriteLine(LogMsg);
		ErrorFile.WriteLine(LogMsg);
		LogMsg = ((Root) BackupFolderComboBox.SelectedItem).ToString();
		LogFile.WriteLine(LogMsg);
		ErrorFile.WriteLine(LogMsg);

		Schema.RootIndex = BackupFolderComboBox.SelectedIndex;
		Backup.BackupFolders(Schema);

		StartTime = Environment.TickCount;
		CounterTimer_Tick(null, null);
		CounterTimer.Start();
		return;
		}

	/// <summary>
	/// Backup process is completed
	/// </summary>
	private void OnBackupCompleted
			(
			bool BackupDone
			)
		{
		CounterTimer.Stop();
		CounterTimer_Tick(null, null);
		CancelBackupButton.Enabled = false;
		
		if(BackupDone)
			{
			MessageBox.Show("Backup successfuly done.");
			}
		else if(MessageBox.Show("Backup was cancelled.\r\n" +
				"Press Yes to save current date in Backup Folder.\r\n" +
				"Press No to keep backup folder date as is.",
				"Backup Cancelled", MessageBoxButtons.YesNo) == DialogResult.Yes) BackupDone = true;

		if(BackupDone)
			{
			Root SelectedRoot = Schema.Roots[Schema.RootIndex];
			SelectedRoot.LastBackup = DateTime.Now;
			Schema.SortRoots();
			Schema.Save();

			BackupFolderComboBox.Items.Clear();
			foreach(Root Root in Schema.Roots) BackupFolderComboBox.Items.Add(Root);
			BackupFolderComboBox.SelectedIndex = 0;
			}

		// close the file
		LogFile.Close();
		LogFile = null;

		// close the file
		ErrorFile.Close();
		ErrorFile = null;

		BackupFolderComboBox.Enabled = true;
		EditSchemaButton.Enabled = true;
		GoButton.Enabled = true;
		ViewLogButton.Enabled = true;
		ViewErrorButton.Enabled = true;
		return;
		}

	/// <summary>
	/// Write to log file
	/// </summary>
	/// <param name="Control">Log or Error</param>
	/// <param name="LogText">Message</param>
	private void WriteToLogFile(LogControl Control, string LogText)
		{
		// log file
		LogFile.WriteLine(LogText);

		// error log file
		if(Control == LogControl.Error)
			{
			ErrorFile.WriteLine(LogText);
			lock(ErrorQueue) ErrorQueue.Add(LogText); 
			}
		return;
		}

	/// <summary>
	/// User pressed cancel
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void OnCancelClick(object sender, EventArgs e)
		{
		Backup.CancelBackup();
		return;
		}

	/// <summary>
	/// Display latest log file
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void ViewResultClick(object sender, EventArgs e)
		{
		// start text editor
		Process.Start(LogFileName);
		return;
		}

	/// <summary>
	/// Display latest error file
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void ViewErrorsLog(object sender, EventArgs e)
		{
		// start text editor
		Process.Start(ErrorFileName);
		return;
		}

	/// <summary>
	/// Edit schema
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void EditSchemaClick(object sender, EventArgs e)
		{
		EditSchema Dialog = new EditSchema(Schema);
		if(Dialog.ShowDialog(this) == DialogResult.OK)
			{
			Schema = Dialog.Schema;
			BackupFolderComboBox.Items.Clear();
			foreach(Root Root in Schema.Roots) BackupFolderComboBox.Items.Add(Root);
			BackupFolderComboBox.SelectedIndex = 0;
			GoButton.Enabled = true;
			}
		return;
		}
	}
}
