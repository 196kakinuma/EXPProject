using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class WMGQuestion2_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Excels/Games/WMG/WMGQuestion2.xlsx";
	private static readonly string exportPath = "Assets/Excels/Games/WMG/WMGQuestion2.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			WMGQuestion data = (WMGQuestion)AssetDatabase.LoadAssetAtPath (exportPath, typeof(WMGQuestion));
			if (data == null) {
				data = ScriptableObject.CreateInstance<WMGQuestion> ();
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

					WMGQuestion.Sheet s = new WMGQuestion.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						WMGQuestion.Param p = new WMGQuestion.Param ();
						
					cell = row.GetCell(0); p.Col1 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.Col2 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.Col3 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.Col4 = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.Col5 = (cell == null ? "" : cell.StringCellValue);
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
