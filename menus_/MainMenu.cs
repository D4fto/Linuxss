using Godot;
using System;

public partial class MainMenu : Control
{
	[Export]
	public Button FirstButton;

    public override void _Ready()
    {
		FirstButton.GrabFocus();
    }

    public void OnStartButtonPressed()
	{
		GameManager.Instance.actualScene.changeScene("res://maps/Map0.tscn");
	}
    public void OnCreditButtonPressed()
	{
		GameManager.Instance.actualScene.changeScene("res://menus_/credit_menu.tscn");
	}
	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
