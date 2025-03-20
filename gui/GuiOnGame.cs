using Godot;


public partial class GuiOnGame : Control
{
	[Export]
	public Player player;
	[Signal]
	public delegate void PointsChangeEventHandler(int points);
	[Signal]
	public delegate void LifeChangeEventHandler(int life, int maxlife);
	[Signal]
	public delegate void IncreaseLevelEventHandler(int Level);

	public override void _Process(double delta)
	{
	}
	public void OnPlayerPointsChange(int points){
		GetNode<Label>("Points").Text = "PTS:"+player.Points;
		EmitSignal(nameof(PointsChange), points);
	}
	public void OnPlayerInternalDeath(){
		EmitSignal(nameof(LifeChange), 0, -1);

	}
	public void OnPlayerLifeChange(int life, int maxLife){
		EmitSignal(nameof(LifeChange), life, maxLife);

	}
	public void OnLevelBarIncreaseLevel(int Level){
		EmitSignal(nameof(IncreaseLevel), Level);
	}
}
