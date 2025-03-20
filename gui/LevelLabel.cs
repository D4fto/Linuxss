using Godot;
using System;

public partial class LevelLabel : Label
{
	public void OnLevelBarIncreaseLevel(int Level)
	{
		Text = "LVL:"+Level.ToString();
	}
}
