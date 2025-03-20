using Godot;
using System;

public partial class MenuMouseEffect : Marker2D
{
	public override void _Process(double delta)
	{
		GetParent<Camera2D>().Position = GetLocalMousePosition()/10;
	}
}
