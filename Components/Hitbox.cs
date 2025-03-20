using Godot;
using System;

public partial class Hitbox : Area2D
{
	[Export]
	public HealthComponent HealthComponent;
	[Signal]

	public delegate void EnemyEnteredEventHandler();
	[Signal]
	public delegate void EnemyExitedEventHandler();

	public void OnAreaEntered(Area2D area)
	{
		if(area.IsInGroup("bullets"))
		{
			HealthComponent.STakeDamage(GameManager.Instance.playerDamage);
		}
		if(area.IsInGroup("Enemies"))
		{
			EmitSignal(nameof(EnemyEntered));
		}
	}
	public void OnAreaExited(Area2D area)
	{
		if(area.IsInGroup("Enemies"))
		{
			EmitSignal(nameof(EnemyExited));
		}
	}
}
