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
using System.IO;
using System.Text;

namespace CopyTree
{
/// <summary>
/// Backup Schema class
/// </summary>
public class Schema
	{
	/// <summary>
	/// Selected root index
	/// </summary>
	public int RootIndex;

	/// <summary>
	/// Array of roots
	/// </summary>
	public Root[] Roots;

	/// <summary>
	/// Array of folders
	/// </summary>
	public Folder[] Folders;

	/// <summary>
	/// Saved schema file name
	/// </summary>
	/// <returns></returns>
	public static string FileName()
		{
		return("CopyTreeSchema.txt");
		}

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="Roots">Roots</param>
	/// <param name="Folders">Folders</param>
	public Schema
			(
			Root[] Roots,
			Folder[] Folders
			)
		{
		this.Roots = Roots;
		this.Folders = Folders;
		return;
		}

	/// <summary>
	/// Sort roots (date time name)
	/// </summary>
	public void SortRoots()
		{
		Array.Sort<Root>(Roots);
		return;
		}

	/// <summary>
	/// Sort folders
	/// </summary>
	public void SortFolders()
		{
		Array.Sort<Folder>(Folders);
		return;
		}

	/// <summary>
	/// Save backup schema
	/// </summary>
	public void Save()
		{
		StringBuilder SchemaText = new StringBuilder("CopyTreeSchema\r\n");
		SchemaText.Append(DateTime.Now.ToString(CustomCultureInfo.CustomDateTime) + "\r\n");	
		SchemaText.Append("----\r\n");

		foreach(Root Root in Roots)
			{
			SchemaText.Append(Root.ToString() + "\r\n");	
			}
		SchemaText.Append("----\r\n");

		foreach(Folder Folder in Folders)
			{
			SchemaText.Append(Folder.ToString() + "\r\n");
			}
		SchemaText.Append("----\r\n");

		// create a new program state file
		try
			{
			using(StreamWriter SchemaFile = new StreamWriter(new FileStream(FileName(), FileMode.Create, FileAccess.Write, FileShare.None)))
				{ 
				// create xml serializing object
				SchemaFile.Write(SchemaText.ToString());
				}
			}
		catch {}

		// exit
		return;
		}

	/// <summary>
	/// Load backup schema
	/// </summary>
	/// <returns>Schema</returns>
	public static Schema Load()
		{
		// test for file existance
		if(!File.Exists(FileName()))
			{
			return null;
			}

		// load program state file
		try
			{
			List<Root> Roots = new List<Root>();
			List<Folder> Folders = new List<Folder>();

			using(StreamReader SchemaFile = new StreamReader(new FileStream(FileName(), FileMode.Open, FileAccess.Read, FileShare.None)))
				{
				int State = 0;
				for(;;)
					{
					string Line;
					Line = SchemaFile.ReadLine();
					if(Line == null) break;
					if(string.IsNullOrWhiteSpace(Line)) continue;

					switch(State)
						{
						case 0:
							if(!Line.StartsWith("CopyTreeSchema")) break;
							State++;
							continue;

						case 1:
							if(Line[4] != '/') break;
							State++;
							continue;	

						case 2:
							if(Line != "----") break;
							State++;
							continue;	

						case 3:
							if(Line == "----")
								{
								State++;
								continue;
								}
							Root Root = Root.FromSavedSchema(Line);
							if(Root == null) break;
							Roots.Add(Root);
							continue;

						case 4:
							if(Line == "----") break;
							Folder Folder = Folder.FromSavedSchema(Line);
							if(Folder == null) break;
							Folders.Add(Folder);
							continue;
						}
					break;
					}	

				if(Roots.Count == 0 || Folders.Count == 0) return null;

				// sort
				Roots.Sort();
				Folders.Sort();

				// schema
				Schema Schema = new Schema(Roots.ToArray(), Folders.ToArray());

				// exit
				return Schema;
				}
			}

		catch
			{
			return null;
			}
		}
	}
}
