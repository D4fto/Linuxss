using Godot;
using System;

public partial class GuiDebug : Control
{
	[Export]
	public Player Player;
	[Export]
	public Node EnemiesNode;
	[Signal]
	public delegate void DebugOnEventHandler();
	[Signal]
	public delegate void DebugOffEventHandler();
	public bool debugging = false;

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("debug")){
			if(!debugging){
				EmitSignal(nameof(DebugOn));
				Show();
			}else{
				EmitSignal(nameof(DebugOff));
				Hide();
			}
			debugging = !debugging;
		}
	}
}
