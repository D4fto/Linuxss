using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	[Signal]
	public delegate void KillEventHandler();
	[Signal]	
	public delegate void ChangePointsEventHandler(int points);	
	public Action<int, int> ThingHappened;
	public static GameManager Instance { get; private set; }
	public Vector2 PlayerPos = new Vector2(0, 0);
	public float EnemiesLifeModifier = 1;
	public float EnemiesSpeedModifier = 1;
	public int playerDamage = 10;
	public float playerXpModifier = 1;
	public int Regen = -1;
	public ActualScene actualScene;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("fullscreen"))
		{
			if(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.ExclusiveFullscreen){
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
			}else{
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
			}
		}
	}
}
