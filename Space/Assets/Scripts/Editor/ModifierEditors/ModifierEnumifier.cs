﻿using UnityEngine;
using UnityEditor;
using System.IO;

public class ModifierEnumifier : MonoBehaviour
{
	public const string DATA_PATH = "Assets/Scripts/Editor/ModifierEditors/Data/WeaponModifiers.csv";
	public const string TEMPLATE_PATH = "Assets/Scripts/Editor/ModifierEditors/Data/WeaponModifier.txt";
	public const string CODE_PATH = "Assets/Scripts/Gameplay/Weapon/WeaponModifier.cs";

	[ MenuItem( "Space/Generate Modifier Code" ) ]
	static void GenerateFile()
	{
		string[,] data;
		LoadAndParseData( out data );
		WriteFile( data );
	}

	private static void LoadAndParseData( out string[,] data )
	{
		if( File.Exists( DATA_PATH ) )
		{
			string[] dataStr = File.ReadAllLines( DATA_PATH );
			string[] lineData = dataStr[ 0 ].Split( ',' );

			int numLines = dataStr.Length;
			int numIndices = lineData.Length;

			data = new string[ numLines, numIndices ];

			for( int i = 0; i < numLines; i++ )
			{
				lineData = dataStr[ i ].Split( ',' );
				for( int j = 0; j < numIndices; j++ )
				{
					if( j < lineData.Length )
					{
						data[ i, j ] = lineData[ j ];
					}
					else
					{
						print( "ERROR: Data missing at line " + i + "m index " + j );
						data = null;
						return;
					}
				}
			}
		}
		else
		{
			print( "ERROR: Couldn't find data file." );
			data = null;
		}
	}

	private static void WriteFile( string[,] data )
	{
		if( data == null )
		{
			print( "Code generation cancelled." );
			return;
		}

		if( !File.Exists( TEMPLATE_PATH ) )
		{
			print( "ERROR: Could not find WeaponModifier template" );
			return;
		}

		int rows = data.GetLength( 0 );
		int columns = data.GetLength( 1 );

		/*for( int i = 0; i < rows; i++ )
		{
			for( int j = 0; j < columns; j++ )
			{
				print( data[ i, j ] );
			}
		}*/

		/*
		 * [r,c]	col1	col2	col3	col4
		 * row1		mods	stat1 	stat2	stat3
		 * row2		mod1	stat1	stat2	stat3
		 * row3		mod2	stat1	stat2	stat3
		 * 
		 */

		print( "Writing Code File..." );

		string code = File.ReadAllText( TEMPLATE_PATH );

		// Write in the modifier names
		string modifierNames = "";
		for( int i = 1; i < rows; i++ )
		{
			modifierNames += "\t\t" + data[ i, 0 ] + ",\n";
		}
		code = code.Replace( "\\ModifierNames\\", modifierNames );

		// Not using yet
		code = code.Replace( "\\AltModifierNames\\", "" );

		// Write in the stats
		string statNames = "";
		for( int i = 1; i < columns; i++ )
		{
			statNames += "\t\t" + data[ 0, i ] + ",\n";
		}
		code = code.Replace( "\\StatNames\\", statNames );

		// Not using yet
		code = code.Replace( "\\AltStatNames\\", "" );

		// Write in the stat values
		string statValues = "";
		for( int i = 1; i < rows; i++ )
		{
			statValues += "\t\t{ ";
			for( int j = 1; j < columns - 1; j++ )
			{
				statValues += data[ i, j ] + ", ";
			}
			
			statValues += data[ i, columns - 1 ] + " },\n";
		}
		code = code.Replace( "\\StatValues\\", statValues );

		File.WriteAllText( CODE_PATH, code );
		print( "Finished writing file to " + CODE_PATH );
	}
}
