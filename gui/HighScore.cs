using Godot;
using System;

public partial class HighScore : PanelContainer
{
	[Export]
	public Database database;
	[Export]
	public Container container;
	public PackedScene registerScene = GD.Load<PackedScene>("res://gui/points_register.tscn");
	public void clear()
    {
        foreach (var register in container.GetChildren()){
            register.QueueFree();
        }
    }
	public async void show()
	{
		Show();
		clear();
		var data = await database.GetScores();
		int x = 1;
		foreach (var register in data){
			var instance = registerScene.Instantiate<PointsRegister>();
			instance.setPosition(x);
			instance.setName(register.Nick);
			instance.setPoints(register.Points);
			instance.setLevel(register.Level);
			container.AddChild(instance);
			x++;
		}
	}
	public void hide()
	{
		Hide();
		clear();
	}
	public void OnHighscoreButtonPressed()
	{
		show();
	}
	public void OnTextureButtonPressed()
	{
		Hide();
	}
}
