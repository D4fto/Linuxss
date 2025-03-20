using Godot;
using System;
using System.IO;

public partial class DeathMenu : Control
{


	[Export]
	public Database database;
	[Export]
	public GuiOnGame GuiOnGame;
	[Export]
	public Player Player;
	[Export]
	public Control RegisterContainer;
	[Export]
	public Control OptionsContainer;
	[Export]
	public Button RegisterButton;
	[Export]
	public Label Time;
	[Export]
	public Label Kills;
	[Export]
	public Label Points;
	[Export]
	public Label Level;
	[Export]
	public LineEdit FirstButton;
	[Export]
	public string Map;

    public void OnPlayerDeath(){
		int seconds = GuiOnGame.GetNode<TimeCount>("TimeCount").seconds;
		Time.Text = $"⏳:{seconds/60:D2}:{seconds%60:D2}";
		Kills.Text = $"💀:{Player.Kills}";
		Points.Text = $"PTS:{Player.Points}";
		Level.Text = $"LVL:{GuiOnGame.GetNode<LevelBar>("LevelBar").LVL}";
		show();
	}
    public void OnRetryButtonPressed(){
		Engine.TimeScale = 1;
		GameManager.Instance.actualScene.changeScene(Map);
	}
    public void OnMainMenuButtonPressed()
	{
		Engine.TimeScale = 1;
		GameManager.Instance.actualScene.changeScene("res://menus_/MainMenu.tscn");
	}
	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
	public void show()
	{
		Engine.TimeScale = 0;
		Show();
		FirstButton.GrabFocus();
	}
	public void OnLineEditTextChanged(string text)
	{
		if(text.Length > 0){
			RegisterButton.Disabled = false;
			return;
		}
		RegisterButton.Disabled = true;
	}
	public void registered()
	{
		RegisterContainer.QueueFree();
		OptionsContainer.Show();
		OptionsContainer.GetNode<Button>("RetryButton").GrabFocus();
	}
	public void OnNoRegisterButtonButtonDown()
	{
		registered();
	}
	public void OnRegisterButtonButtonDown()
	{
		if(FirstButton.Text.Length > 0){
			database.InsertScore(FirstButton.Text, Player.Points, GuiOnGame.GetNode<LevelBar>("LevelBar").LVL);
			registered();
		}
	}
	public void OnLineEditTextSubmitted(string text)
	{
		if(FirstButton.Text.Length > 0){
			database.InsertScore(FirstButton.Text, Player.Points, GuiOnGame.GetNode<LevelBar>("LevelBar").LVL);
			registered();
		}
	}
}
