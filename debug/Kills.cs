using Godot;
using System;

public partial class Kills : Label
{
	public void OnUpdateTimeout(){
		Text = "Kills: " + GetParent<GuiDebug>().Player.Kills;
	}
}
