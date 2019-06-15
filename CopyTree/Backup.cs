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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace CopyTree
{
/// <summary>
/// Log control
/// </summary>
public enum LogControl
	{
	Log,
	Error,
	}

/// <summary>
/// Perform backup using worker thread
/// </summary>
public class Backup : IComparer<FileInfo>, IComparer<DirectoryInfo>
	{
    public delegate void LogEventHandler(LogControl Control, string LogText);
    public event LogEventHandler WriteToLogFile;
	public delegate void BackupCompleted(bool BackupDone);
	public event BackupCompleted BackupCompletedEvent;

	BackgroundWorker BackupWorker;

	public string SourceFolderFullName;
	public string BackupFolderFullName;
	public int FileOverrideCounter;
	public int FileCopyCounter;
	public int FileDeleteCounter;
	public int FolderCreateCounter;
	public int FolderDeleteCounter;

	/// <summary>
	/// Backup constructor
	/// </summary>
	public Backup()
		{
		// define background thread
		BackupWorker = new BackgroundWorker();
		BackupWorker.DoWork += BackupWorker_DoWork;
		BackupWorker.RunWorkerCompleted += BackupWorker_RunWorkerCompleted;
		BackupWorker.WorkerSupportsCancellation = true;
		return;
		}

	/// <summary>
	/// Cancel backup
	/// </summary>
	public void CancelBackup()
		{
		BackupWorker.CancelAsync();
		return;
		}

	/// <summary>
	/// Backup process is completed
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void BackupWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
		BackupCompletedEvent?.Invoke((bool) e.Result);
		return;
		}

	/// <summary>
	/// Start background process
	/// </summary>
	/// <param name="Schema">Backup schema</param>
	public void BackupFolders
			(
			Schema Schema
			)
		{
		BackupWorker.RunWorkerAsync(Schema);
		return;
		}

	/// <summary>
	/// Background backup process
	/// </summary>
	/// <param name="sender">Sender (not used)</param>
	/// <param name="e">Event arguments (not used)</param>
	private void BackupWorker_DoWork
			(
			object sender,
			DoWorkEventArgs e
			)
		{
		// reset counters
		FileOverrideCounter = 0;
		FileCopyCounter = 0;
		FileDeleteCounter = 0;
		FolderCreateCounter = 0;
		FolderDeleteCounter = 0;

		// loop for folders backup
		try
			{
			Schema Schema = (Schema) e.Argument;
			foreach(Folder Folder in Schema.Folders)
				{
				FolderBackup(Folder.SourceFolder, Path.Combine(Schema.Roots[Schema.RootIndex].RootName, Folder.BackupName));
				}
			e.Result = true;
			return;
			}

		// backup was cancelled
		catch (CanceBackupException)
			{
			e.Result = false;
			return;
			}
		}

	/// <summary>
	/// Backup one folder
	/// </summary>
	/// <param name="SourceFolderName">Source folder name</param>
	/// <param name="BackupFolderName">Backup folder name</param>
	private void FolderBackup
			(
			string SourceFolderName,
			string BackupFolderName
			)
		{
		// get source folder information
		DirectoryInfo SourceFolderInfo;
		try
			{
			SourceFolderInfo = new DirectoryInfo(SourceFolderName);
			}
		catch(Exception Ex)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Source folder exception " + Ex.Message);
			return;
			}

		// save full name
		SourceFolderFullName = SourceFolderInfo.FullName;

		// make sure folder exist
		if(!SourceFolderInfo.Exists)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Source folder does not exist " + SourceFolderFullName);
			return;
			}

		// make sure folder is not a root
		if(SourceFolderFullName == SourceFolderInfo.Root.FullName)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Source folder cannot be a root folder " + SourceFolderFullName);
			return;
			}

		// get backup folder information
		DirectoryInfo BackupFolderInfo;
		try
			{
			BackupFolderInfo = new DirectoryInfo(BackupFolderName);
			}
		catch(Exception Ex)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Backup folder exception " + Ex.Message);
			return;
			}

		// save full name
		BackupFolderFullName = BackupFolderInfo.FullName;

		// make sure folder exist
		if(!BackupFolderInfo.Exists)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Backup folder does not exist " + BackupFolderFullName);
			return;
			}

		// make sure folder is not a root
		if(BackupFolderFullName == BackupFolderInfo.Root.FullName)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Backup folder cannot be a root folder " + BackupFolderFullName);
			return;
			}

		// make sure one is not child of the other
		if(SourceFolderFullName.Length >= BackupFolderFullName.Length)
			{
			if(SourceFolderFullName.StartsWith(BackupFolderFullName))
				{
				WriteToLogFile?.Invoke(LogControl.Error, "Source folder " + SourceFolderFullName +
					"\r\ncannot be a child of backup folder " + BackupFolderFullName);
				return;
				}
			}
		else
			{
			if(BackupFolderFullName.StartsWith(SourceFolderFullName))
				{
				WriteToLogFile?.Invoke(LogControl.Error, "Backup folder " + BackupFolderFullName +
					"\r\ncannot be a child of source folder " + SourceFolderFullName);
				return;
				}
			}

		// perform recursive backup
		PerformFolderBackup(SourceFolderInfo, BackupFolderInfo);
		return;
		}

	/// <summary>
	/// recursive folder backup
	/// </summary>
	/// <param name="SourceFolderName">Source folder name</param>
	/// <param name="BackupFolderName">Backup folder name</param>
	private void PerformFolderBackup
			(
			DirectoryInfo SourceFolderInfo,
			DirectoryInfo BackupFolderInfo
			)
		{
		// cancel backup
		if(BackupWorker.CancellationPending) throw new CanceBackupException();

		// backup folder full name shortcut
		string BackupFolderFullName = BackupFolderInfo.FullName;

		// get source child files
		FileInfo[] SourceFiles;
		try
			{
			SourceFiles = SourceFolderInfo.GetFiles();
			}
		catch (Exception Ex)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Source folder get files exception: " + Ex.Message);
			SourceFiles = new FileInfo[0];
			}

		// get backup child files
		FileInfo[] BackupFiles = BackupFolderInfo.GetFiles();

		// source has files
		if(SourceFiles.Length != 0)
			{
			// backup has files
			if(BackupFiles.Length != 0)
				{
				// backup has files
				EqualizeFiles(SourceFiles, BackupFiles, BackupFolderFullName);
				}

			// backup has no files
			else
				{
				// copy all source files to backup folder
				CopyFiles(SourceFiles, BackupFolderFullName);
				}
			}

		// source has no files but backup has files
		else if(BackupFiles.Length != 0)
			{
			// delete all files of backup folder
			DeleteFiles(BackupFiles);
			}
	
		// get source child folders
		DirectoryInfo[] SourceFolders;
		try
			{
			SourceFolders = SourceFolderInfo.GetDirectories();
			}
		catch (Exception Ex)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Source folder get folders exception: " + Ex.Message);
			SourceFolders = new DirectoryInfo[0];
			}

		// get backup child folders
		DirectoryInfo[] BackupFolders = BackupFolderInfo.GetDirectories();

		// source has folders
		if(SourceFolders.Length != 0)
			{
			// backup has folders
			if(BackupFolders.Length != 0)
				{
				// backup has folders
				EqualizeFolders(SourceFolders, BackupFolders, BackupFolderFullName);
				}

			// backup has no folders
			else
				{
				// copy all source child folders to backup folder
				CreateFolders(SourceFolders, BackupFolderFullName);
				}
			}

		// source has no folders but backup has folders
		else if(BackupFolders.Length != 0)
			{
			// delete all files of backup folder
			DeleteFolders(BackupFolders);
			}
	
		// done
		return;
		}

	/// <summary>
	/// Make backup files equal to source files
	/// </summary>
	/// <param name="SourceFiles">Source files array</param>
	/// <param name="BackupFiles">Backup files array</param>
	/// <param name="BackupFullName">Backup directory full name</param>
	private void EqualizeFiles
			(
			FileInfo[] SourceFiles,
			FileInfo[] BackupFiles,
			string BackupFullName
			)
		{
		// sort both array by file name
		Array.Sort<FileInfo>(SourceFiles, this);
		Array.Sort<FileInfo>(BackupFiles, this);

		// pointers to source and backup folders arrays
		int SourcePtr = 0;
		int BackupPtr = 0;

		// merge the two arrays
		for(;;)
			{
			// cancel backup
			if(BackupWorker.CancellationPending) throw new CanceBackupException();

			// test source pointer
			if(SourcePtr < SourceFiles.Length)
				{
				// test backup pointer
				if(BackupPtr < BackupFiles.Length)
					{
					// get both file infos
					FileInfo SourceFile = SourceFiles[SourcePtr];
					FileInfo BackupFile = BackupFiles[BackupPtr];

					// compare names
					int Cmp = Compare(SourceFile, BackupFile);

					// the two files have the same name
					if(Cmp == 0)
						{
						// file was modified since last backup
						if(SourceFile.LastWriteTimeUtc != BackupFile.LastWriteTimeUtc) OverrideFile(SourceFile, BackupFile);
						SourcePtr++;
						BackupPtr++;
						continue;
						}

					// source is less than backup
					if(Cmp < 0)
						{
						// copy the file
						CopyFile(SourceFile, BackupFullName);
						SourcePtr++;
						continue;
						}

					// source is more than backup
					// delete file
					DeleteFile(BackupFile);
					BackupPtr++;
					continue;
					}

				// copy the file
				CopyFile(SourceFiles[SourcePtr++], BackupFullName);
				continue;
				}

			// source pointer is at end of array
			else
				{
				// test backup pointer
				if(BackupPtr < BackupFiles.Length)
					{
					// delete file
					DeleteFile(BackupFiles[BackupPtr++]);
					continue;
					}

				// backup pointer is at end of array
				break;
				}
			}

		return;
		}

	/// <summary>
	/// Copy one file from source to backup (no override)
	/// </summary>
	/// <param name="SourceFile">Source file</param>
	/// <param name="BackupFullName">Backup directory full name</param>
	private void CopyFile
			(
			FileInfo SourceFile,
			string BackupFullName
			)
		{
		// log
		WriteToLogFile?.Invoke(LogControl.Log, "Copy: " + SourceFile.FullName);
		FormatAttributes(SourceFile.Attributes);

		try
			{
			// copy file to backup
			string BackupFileName = Path.Combine(BackupFullName, SourceFile.Name);
			SourceFile.CopyTo(BackupFileName);

			// make sure creation time and last access are the same
			FileInfo BackupFile = new FileInfo(BackupFileName);
			if(BackupFile.IsReadOnly)
				{
				BackupFile.IsReadOnly = false;
				BackupFile.CreationTimeUtc = SourceFile.CreationTimeUtc;
				BackupFile.LastAccessTimeUtc = SourceFile.LastAccessTimeUtc;
				BackupFile.IsReadOnly = true;
				}
			else
				{
				BackupFile.CreationTimeUtc = SourceFile.CreationTimeUtc;
				BackupFile.LastAccessTimeUtc = SourceFile.LastAccessTimeUtc;
				}

			// update counter
			FileCopyCounter++;
			}
		catch (Exception Ex)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Copy exception: " + Ex.Message);
			}
		return;
		}

	/// <summary>
	/// Copy one file from source overriding existing file in backup
	/// </summary>
	/// <param name="SourceFile">Source file</param>
	/// <param name="BackupFullName">Backup directory full name</param>
	private void OverrideFile
			(
			FileInfo SourceFile,
			FileInfo BackupFile
			)
		{
		// log
		WriteToLogFile?.Invoke(LogControl.Log, "Update: " + SourceFile.FullName);
		FormatAttributes(SourceFile.Attributes);

		try
			{
			// if backup is read only, remove this flag
			if(BackupFile.IsReadOnly) BackupFile.IsReadOnly = false;

			// override backup with source
			string BackupFileName = Path.Combine(BackupFile.DirectoryName, SourceFile.Name);
			SourceFile.CopyTo(BackupFileName, true);

			// make sure creation time and last access are the same
			BackupFile = new FileInfo(BackupFileName);
			if(BackupFile.IsReadOnly)
				{
				BackupFile.IsReadOnly = false;
				BackupFile.CreationTimeUtc = SourceFile.CreationTimeUtc;
				BackupFile.LastAccessTimeUtc = SourceFile.LastAccessTimeUtc;
				BackupFile.IsReadOnly = true;
				}
			else
				{
				BackupFile.CreationTimeUtc = SourceFile.CreationTimeUtc;
				BackupFile.LastAccessTimeUtc = SourceFile.LastAccessTimeUtc;
				}

			// update counter
			FileOverrideCounter++;
			}
		catch (Exception Ex)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Update exception: " + Ex.Message);
			}
		return;
		}

	/// <summary>
	/// Copy all files from source to backup (no override)
	/// </summary>
	/// <param name="SourceFiles">Source files</param>
	/// <param name="BackupFullName">Backup directory full name</param>
	private void CopyFiles
			(
			FileInfo[] SourceFiles,
			string BackupFullName
			)
		{
		foreach(FileInfo SourceFile in SourceFiles)
			{
			if(BackupWorker.CancellationPending) throw new CanceBackupException();
			CopyFile(SourceFile, BackupFullName);
			}
		return;
		}

	/// <summary>
	/// Delete one file in backup folder
	/// </summary>
	/// <param name="BackupFile">Backup file</param>
	private void DeleteFile
			(
			FileInfo BackupFile
			)
		{
		// log
		WriteToLogFile?.Invoke(LogControl.Log, "Delete: " + BackupFile.FullName);
		FormatAttributes(BackupFile.Attributes);

		try
			{
			// if file is read only, remove attribute
			if(BackupFile.IsReadOnly) BackupFile.IsReadOnly = false;

			// delete file
			BackupFile.Delete();

			// update counter
			FileDeleteCounter++;
			}
		catch (Exception Ex)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Delete exception: " + Ex.Message);
			}
		return;
		}

	/// <summary>
	/// Delete all files in backup folder
	/// </summary>
	/// <param name="BackupFiles">Backup files</param>
	private void DeleteFiles
			(
			FileInfo[] BackupFiles
			)
		{
		foreach(FileInfo BackupFile in BackupFiles)
			{
			if(BackupWorker.CancellationPending) throw new CanceBackupException();
			DeleteFile(BackupFile);
			}
		return;
		}

	/// <summary>
	/// Make backup folder equal to source folder
	/// </summary>
	/// <param name="SourceFolders">Source folders</param>
	/// <param name="BackupFolders">Backup folders</param>
	/// <param name="BackupFullName">Backup folder full name</param>
	private void EqualizeFolders
			(
			DirectoryInfo[] SourceFolders,
			DirectoryInfo[] BackupFolders,
			string BackupFullName
			)
		{
		// sort both array by file name
		Array.Sort<DirectoryInfo>(SourceFolders, this);
		Array.Sort<DirectoryInfo>(BackupFolders, this);

		// pointers to source and backup folders arrays
		int SourcePtr = 0;
		int BackupPtr = 0;

		// merge the two arrays
		for(;;)
			{
			// cancel backup
			if(BackupWorker.CancellationPending) throw new CanceBackupException();

			// test source pointer
			if(SourcePtr < SourceFolders.Length)
				{
				// test backup pointer
				if(BackupPtr < BackupFolders.Length)
					{
					// get both directory infos
					DirectoryInfo SourceFolder = SourceFolders[SourcePtr];
					DirectoryInfo BackupFolder = BackupFolders[BackupPtr];

					// compare folders names
					int Cmp = Compare(SourceFolder, BackupFolder);

					// the two folders have the same name
					if(Cmp == 0)
						{
						// recursive folder backup
						PerformFolderBackup(SourceFolder, BackupFolder);
						SourcePtr++;
						BackupPtr++;
						continue;
						}

					// source is less than backup
					if(Cmp < 0)
						{
						// copy the file
						CreateFolder(SourceFolder, BackupFullName);
						SourcePtr++;
						continue;
						}

					// source is more than backup
					// delete file
					DeleteFolder(BackupFolder);
					BackupPtr++;
					continue;
					}

				// copy the file
				CreateFolder(SourceFolders[SourcePtr++], BackupFullName);
				continue;
				}

			// test backup pointer
			if(BackupPtr < BackupFolders.Length)
				{
				// delete file
				DeleteFolder(BackupFolders[BackupPtr++]);
				continue;
				}

			// source and backup pointers are at end of their arrays
			return;
			}
		}

	/// <summary>
	/// Create backup folders based on source folders list
	/// </summary>
	/// <param name="SourceFolders">Source folders</param>
	/// <param name="BackupFullName">Backup folder full name</param>
	private void CreateFolders
			(
			DirectoryInfo[] SourceFolders,
			string BackupFullName
			)
		{
		foreach(DirectoryInfo SourceFolder in SourceFolders)
			{
			// cancel backup
			if(BackupWorker.CancellationPending) throw new CanceBackupException();
			CreateFolder(SourceFolder, BackupFullName);
			}
		return;
		}

	/// <summary>
	/// Create single backup folder based on source folder info
	/// </summary>
	/// <param name="SourceFolder">Source folder</param>
	/// <param name="BackupFullName">Backup folder full name</param>
	private void CreateFolder
			(
			DirectoryInfo SourceFolder,
			string BackupFullName
			)
		{
		// log file
		WriteToLogFile?.Invoke(LogControl.Log, "Create folder: " + SourceFolder.FullName);
		FormatAttributes(SourceFolder.Attributes);

		try
			{
			// directory info for new backup folder
			DirectoryInfo BackupFolder = new DirectoryInfo(Path.Combine(BackupFullName, SourceFolder.Name));

			// create new folder
			BackupFolder.Create();

			// update counter
			FolderCreateCounter++;

			// get source child folders and files
			DirectoryInfo[] SourceSubFolders = SourceFolder.GetDirectories();
			FileInfo[] SourceFiles = SourceFolder.GetFiles();

			// source has files
			if(SourceFiles.Length != 0) CopyFiles(SourceFiles, BackupFolder.FullName);

			// source has folders
			if(SourceSubFolders.Length != 0) CreateFolders(SourceSubFolders, BackupFolder.FullName);

			// set hidden directory flag
			if((SourceFolder.Attributes & FileAttributes.Hidden) != 0) BackupFolder.Attributes |= FileAttributes.Hidden;
			//if((SourceFolder.Attributes & FileAttributes.System) != 0) BackupFolder.Attributes |= FileAttributes.System;

			// set creation and last write time
			BackupFolder.CreationTimeUtc = SourceFolder.CreationTimeUtc;
			BackupFolder.LastWriteTimeUtc = SourceFolder.LastWriteTimeUtc;
			}
		catch (CanceBackupException)
			{
			throw;
			}
		catch (Exception Ex)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Create folder exception: " + Ex.Message);
			}
		return;
		}

	/// <summary>
	/// Delete all folders
	/// </summary>
	/// <param name="BackupFolders">Backup folders</param>
	private void DeleteFolders
			(
			DirectoryInfo[] BackupFolders
			)
		{
		foreach(DirectoryInfo BackupFolder in BackupFolders)
			{
			// cancel backup
			if(BackupWorker.CancellationPending) throw new CanceBackupException();

			// delete folder
			DeleteFolder(BackupFolder);
			}
		return;
		}

	/// <summary>
	/// Delete single folder
	/// </summary>
	/// <param name="BackupFolder">Backup folder</param>
	private void DeleteFolder
			(
			DirectoryInfo BackupFolder
			)
		{
		// get backup child files
		FileInfo[] BackupFiles = BackupFolder.GetFiles();

		// delete all files in backup folder
		if(BackupFiles.Length != 0) DeleteFiles(BackupFiles);

		// backup folder has folders
		DirectoryInfo[] BackupSubFolders = BackupFolder.GetDirectories();

		// delete all folders in backup folder
		if(BackupSubFolders.Length != 0) DeleteFolders(BackupSubFolders);

		// log file
		WriteToLogFile?.Invoke(LogControl.Log, "Delete folder: " + BackupFolder.FullName);
		FormatAttributes(BackupFolder.Attributes);

		try
			{
			if((BackupFolder.Attributes & FileAttributes.ReadOnly) != 0) BackupFolder.Attributes &= ~FileAttributes.ReadOnly;

			// delete the empty backup folder
			BackupFolder.Delete();

			// update counter
			FolderDeleteCounter++;
			}
		catch (Exception Ex)
			{
			WriteToLogFile?.Invoke(LogControl.Error, "Delete folder exception: " + Ex.Message);
			}
		return;
		}

	/// <summary>
	/// Compare two file info classes based on name
	/// </summary>
	/// <param name="FileInfo1">File 1</param>
	/// <param name="FileInfo2">File 2</param>
	/// <returns></returns>
	public int Compare
			(
			FileInfo FileInfo1,
			FileInfo FileInfo2
			)
		{
		return string.Compare(FileInfo1.Name, FileInfo2.Name, true);
		}	

	/// <summary>
	/// Compare two DirectoryInfo classes based on dir name
	/// </summary>
	/// <param name="DirInfo1">Directory 1</param>
	/// <param name="DirInfo2">Directory 2</param>
	/// <returns></returns>
	public int Compare
			(
			DirectoryInfo DirInfo1,
			DirectoryInfo DirInfo2
			)
		{
		return string.Compare(DirInfo1.Name, DirInfo2.Name, true);
		}	

	/// <summary>
	/// File attributes names
	/// </summary>
	public static string[] FileAttrText = 
		{
		"ReadOnly", // = 1,
		"Hidden", // = 2,
		"System", // = 4,
		"Reserved", // = 8,
		"Directory", // = 16,
		"Archive", // = 32,
		"Device", // = 64, RESERVED
		"Normal", // = 128, Must be alone
		"Temporary", // = 256,
		"SparseFile", // = 512,
		"ReparsePoint", // = 1024, Link
		"Compressed", // = 2048,
		"Offline", // = 4096,
		"NotContentIndexed", // = 8192,
		"Encrypted", // = 16384,
		"IntegrityStream", // = 32768,
		"Reserved", // = 65536,
		"NoScrubData", // = 131072
		};

	/// <summary>
	/// Format file attributes for log file
	/// </summary>
	/// <param name="Attr"></param>
	private void FormatAttributes
			(
			FileAttributes Attr
			)
		{
		Attr &= ~FileAttributes.Archive;
		if(Attr == 0 || Attr == FileAttributes.Normal || Attr == FileAttributes.Directory) return;
		StringBuilder Str = new StringBuilder("Attributes:");
		for(int Index = 0; Index < 18; Index++)
			if((Attr & (FileAttributes) (1 << Index)) != 0) Str.Append(" " + FileAttrText[Index]);
		WriteToLogFile?.Invoke(LogControl.Log, Str.ToString()); 
		}
	}
}
