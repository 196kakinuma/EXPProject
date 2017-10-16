using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DCGAnswer : ScriptableObject
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
		
		public int Knob1;
		public int Knob2;
		public int Knob3;
		public int Knob4;
		public int Knob5;
	}
}

