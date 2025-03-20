using Godot;
using System;

public partial class Zap : CharacterBody2D
{
	public Vector2 direction;
	[Export]
	public PointsComponent Points;
	public XPMesh PointsMesh
	{
		get => Points?.xPMesh;
		set { if (Points != null) Points.xPMesh = value; }
	}
	[Export]
	public SpeedComponent speedComponent;
	public override void _Ready()
	{
		var rng = new RandomNumberGenerator();
		float angle = rng.RandiRange(0, 360) * (float)Math.PI / 180;
		direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
		Position = GameManager.Instance.PlayerPos + direction * 1000;
	} 

	public override void _Process(double delta)
	{
		MoveAndSlide();
	}
	public void OnSpeedComponentDirectionChange(Vector2 direction){
		Velocity = direction * speedComponent.Speed;
	}
	public void OnSpeedComponentUpdatePosition(Vector2 position){
		Position = position;
	}
	
}
