using Godot;
using System;

public partial class Marker2dPlayer : Marker2D
{
	public override void _Process(double delta)
	{
		Rotate(GetAngleTo(GetGlobalMousePosition()));
	}
}
