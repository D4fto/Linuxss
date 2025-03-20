using Godot;
using System;

public partial class PointsRegister : HBoxContainer
{
	public void setPosition(int text)
	{
		GetNode<Label>("Position").Text = text.ToString();
	}
	public void setName(string text)
	{
		GetNode<Label>("Name").Text = "  " + text;
	}
	public void setPoints(int text)
	{
		GetNode<Label>("Points").Text = "  " + text;
	}
	public void setLevel(int text)
	{
		GetNode<Label>("Level").Text = "  " + text;
	}
}
