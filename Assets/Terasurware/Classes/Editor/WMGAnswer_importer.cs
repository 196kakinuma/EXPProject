using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class WMGAnswer_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Excels/Games/WMG/WMGAnswer.xlsx";
	private static readonly string exportPath = "Assets/Excels/Games/WMG/WMGAnswer.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			WMGAnswer data = (WMGAnswer)AssetDatabase.LoadAssetAtPath (exportPath, typeof(WMGAnswer));
			if (data == null) {
				data = ScriptableObject.CreateInstance<WMGAnswer> ();
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

					WMGAnswer.Sheet s = new WMGAnswer.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						WMGAnswer.Param p = new WMGAnswer.Param ();
						
					cell = row.GetCell(0); p.Answer1 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.Answer2 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.Answer3 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.Answer4 = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.Answer5 = (int)(cell == null ? 0 : cell.NumericCellValue);
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
