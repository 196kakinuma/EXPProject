using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WMGQuestion : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public string Col1;
		public string Col2;
		public string Col3;
		public string Col4;
		public string Col5;
	}
}

