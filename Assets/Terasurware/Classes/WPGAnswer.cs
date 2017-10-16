using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WPGAnswer : ScriptableObject
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
		
		public int answer1;
		public int answer2;
		public int answer3;
		public int answer4;
		public int answer5;
		public int answer6;
	}
}

