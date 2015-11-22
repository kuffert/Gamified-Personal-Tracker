using UnityEngine;
using System.Collections.Generic;

public class Skill {
	
	private string id;
	private string skillName;
	private string[] dimensions;

	
	public Skill()
	{
	}

	public string Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}

	public string SkillName {
		get {
			return this.skillName;
		}
		set {
			skillName = value;
		}
	}

	public string[] Dimensions {
		get {
			return this.dimensions;
		}
		set {
			dimensions = value;
		}
	}
}
