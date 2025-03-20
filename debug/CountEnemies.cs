using Godot;
using System;

public partial class CountEnemies : Timer
{
	// Called when the node enters the scene tree for the first time.
	public  void OnGuiDebugDebugOff()
	{
		Stop();
	}
	public  void OnGuiDebugDebugOn()
	{
		Start();
	}

}
