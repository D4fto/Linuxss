using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export]
	public Cards cards;
	[Export]
	public Button FirstButton;
	public bool paused;
    public override void _Ready()
    {
		
    }
    public override void _Process(double delta)
    {
		if(Input.IsActionJustPressed("pause")){
			if(paused){
				hide();
			}else{
				show();
			}
		}
    }

    public void OnMainMenuButtonPressed()
	{
		Engine.TimeScale = 1;
		GameManager.Instance.actualScene.changeScene("res://menus_/MainMenu.tscn");
	}
	public void OnPlayerInternalDeath()
	{
		QueueFree();
	}
	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
	public void OnResumeButtonPressed()
	{
		hide();
	}
	public void hide()
	{
		if(!cards.Visible){
			Engine.TimeScale = 1;
		}
		paused = false;
		Hide();
	}
	public void show()
	{
		paused = true;
		Engine.TimeScale = 0;
		Show();
		FirstButton.GrabFocus();
	}
}
