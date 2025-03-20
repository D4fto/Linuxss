using Godot;
using System;

public partial class BasicButton : Button
{
	public void OnMouseEntered()
	{
		if (!Disabled){
			GrabFocus();
		}
	}
}
