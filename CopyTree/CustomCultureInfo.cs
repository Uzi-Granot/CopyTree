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

using System.Globalization;

namespace CopyTree
{
/// <summary>
/// Custom culture info.
/// </summary>
public class CustomCultureInfo : CultureInfo
	{
	// This class ensures that all date time formats will be yyyy/MM/dd HH:mm:ss
	// regardless of OS local settings
	public static CustomCultureInfo CustomDateTime = new CustomCultureInfo();

	/// <summary>
	/// Constructor
	/// </summary>
	public CustomCultureInfo() : base("en-US")
		{
		DateTimeFormat = new DateTimeFormatInfo
			{
			ShortDatePattern = "yyyy/MM/dd",
			ShortTimePattern = "HH:mm:ss"
			};
		return;
		}
	}
}
