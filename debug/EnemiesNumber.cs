using Godot;
using System;

public partial class EnemiesNumber : Label
{
	public void OnUpdateTimeout(){
		Text = "Enemies: " + GetParent<GuiDebug>().EnemiesNode.GetChildCount().ToString();
	}
}
