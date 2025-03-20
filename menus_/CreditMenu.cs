using Godot;
using System;

public partial class CreditMenu : Control
{
	[Export]
	public Button FirstButton;

    public override void _Ready()
    {
		FirstButton.GrabFocus();
    }

    public void OnMainMenuButtonPressed()
	{
		GameManager.Instance.actualScene.changeScene("res://menus_/MainMenu.tscn");
	}
}
