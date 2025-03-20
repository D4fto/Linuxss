using Godot;
using System;

public partial class Bullet : Area2D
{
	// Called when the node enters the scene tree for the first time
	public const float Speed = 700.0f;

	public float angle;
	private Vector2 direction;

	public int life = 1;
	public override void _Ready()
	{
		Rotate(angle);
		direction = new Vector2(-(float)Math.Cos(angle + (float)Math.PI/2), -(float)Math.Sin(angle + (float)Math.PI/2));
	}
	public override void _PhysicsProcess(double delta)
	{
		Position += direction * Speed * (float)delta;
	}
	public void setScale(float scale)
	{
		Scale = new Vector2(scale, scale);
	}
	public void OnVisibleOnScreenEnabler2dScreenExited(){
		QueueFree();
	}
	public void OnEnteredArea(Area2D area){
		if(area.IsInGroup("Enemies")){
			life-=1;
		}
		if(life <= 0){
			QueueFree();
		}
	}
}
