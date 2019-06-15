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
using System.Globalization;

namespace CopyTree
{
/// <summary>
/// Backup root class
/// </summary>
public class Root : IComparable<Root>
	{
	public DateTime LastBackup;
	public string RootName;

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="LastBackup">Last backup date time</param>
	/// <param name="RootName">Backup name</param>
	public Root
			(
			DateTime LastBackup,
			string RootName
			)
		{
		this.LastBackup = LastBackup;
		this.RootName = RootName;
		return;
		}

	/// <summary>
	/// Parse root record from saved file
	/// </summary>
	/// <param name="Line">Input line</param>
	/// <returns>DateTime result</returns>
	public static Root FromSavedSchema
			(
			string Line
			)
		{
		string[] Fields = Line.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
		if(Fields.Length != 2) return null;
		DateTime LastBackup = ParseDateTime(Fields[0]);
		return LastBackup == DateTime.MinValue ? null : new Root(LastBackup, Fields[1].Trim());
		}

	/// <summary>
	/// Convert record to string
	/// </summary>
	/// <returns>String representation</returns>
	public override string ToString()
		{
		return LastBackup.ToString(CustomCultureInfo.CustomDateTime) + ", " + RootName;
		}

	/// <summary>
	/// Parse date time string
	/// </summary>
	/// <param name="DateTimeStr">Text</param>
	/// <returns>DateTime result</returns>
	public static DateTime ParseDateTime
			(
			string DateTimeStr
			)
		{
		DateTime DateTimeValue;
		DateTime.TryParse(DateTimeStr.Trim(), CustomCultureInfo.CustomDateTime,
			DateTimeStyles.None, out DateTimeValue);
		return DateTimeValue;
		}

	/// <summary>
	/// Compare records by date and name
	/// </summary>
	/// <param name="Other">Other record</param>
	/// <returns>Compare result</returns>
	public int CompareTo
			(
			Root Other
			)
		{
		if(LastBackup == Other.LastBackup)
			{
			return string.Compare(RootName, Other.RootName, true);
			}
		return LastBackup > Other.LastBackup ? 1 : -1;
		}
	}
}
