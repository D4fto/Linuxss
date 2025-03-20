using Godot;
using System;

public partial class SpeedComponent : Node
{
	
	[Signal]
	public delegate void DirectionChangeEventHandler(Vector2 direction);
	[Signal]
	public delegate void UpdatePositionEventHandler(Vector2 position);
	public float lastModifier = 1f;
	[Export]
	public float Speed = 150f;
	public void calcDirection(Vector2 Position)
	{
		if(verifyPosition(Position)){
			EmitSignal(nameof(UpdatePosition),RandomPosition());
		}
		EmitSignal(nameof(DirectionChange),(GameManager.Instance.PlayerPos - Position).Normalized());
		
	}
	public bool verifyPosition(Vector2 Position)
	{
		return Position.DistanceTo(GameManager.Instance.PlayerPos) > 2000;
	}
	public Vector2 RandomPosition()
	{
		var rng = new RandomNumberGenerator();
		float angle = rng.RandiRange(0, 360) * (float)Math.PI / 180;
		Vector2 dir = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
		return GameManager.Instance.PlayerPos + dir * rng.RandiRange(1000, 1500);
	}
	public override void _Ready(){
		calcDirection(GetOwner<CharacterBody2D>().Position);
		Speed = (int)(Speed*GameManager.Instance.EnemiesSpeedModifier);
	}
}
