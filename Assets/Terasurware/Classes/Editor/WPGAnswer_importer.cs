using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class WPGAnswer_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Excels/Games/WPG/WPGAnswer.xlsx";
	private static readonly string exportPath = "Assets/Excels/Games/WPG/WPGAnswer.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			WPGAnswer data = (WPGAnswer)AssetDatabase.LoadAssetAtPath (exportPath, typeof(WPGAnswer));
			if (data == null) {
				data = ScriptableObject.CreateInstance<WPGAnswer> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					WPGAnswer.Sheet s = new WPGAnswer.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						WPGAnswer.Param p = new WPGAnswer.Param ();
						
					cell = row.GetCell(0); p.answer1 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.answer2 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.answer3 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.answer4 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.answer5 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.answer6 = (int)(cell == null ? 0 : cell.NumericCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
