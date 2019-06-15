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

namespace CopyTree
{
/// <summary>
/// Backup folder
/// </summary>
public class Folder : IComparable<Folder>
	{
	public string BackupName;
	public string SourceFolder;

	public Folder
			(
			string BackupName,
			string SourceFolder
			)
		{
		this.BackupName = BackupName;
		this.SourceFolder = SourceFolder;
		return;
		}

	/// <summary>
	/// Parse from saved schema
	/// </summary>
	/// <param name="Line">Line of text</param>
	/// <returns>Folder class</returns>
	public static Folder FromSavedSchema
			(
			string Line
			)
		{
		string[] Field = Line.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
		if(Field.Length != 2) return null;		
		return new Folder(Field[0].Trim(), Field[1].Trim());
		}

	/// <summary>
	/// Folder class to string
	/// </summary>
	/// <returns>String</returns>
	public override string ToString()
		{
		return BackupName + ", " + SourceFolder;
		}

	/// <summary>
	/// Compare for sort
	/// </summary>
	/// <param name="Other">Other record</param>
	/// <returns>Result</returns>
	public int CompareTo
			(
			Folder Other
			)
		{
		return string.Compare(BackupName, Other.BackupName, true);
		}
	}
}
