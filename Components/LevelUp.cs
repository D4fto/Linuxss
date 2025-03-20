using Godot;
using System;

public partial class LevelUp : AudioStreamPlayer
{
	public void OnGuiOnGameIncreaseLevel(int Level)
	{
		Play();
	}
}
