using Godot;
using System;

public partial class EnemyDamageAnimationComponent : AnimationPlayer
{
	[Export]
	public PointsComponent pointsComponent;
	[Export]
	public SpeedComponent speedComponent;
	[Export]
	public CollisionShape2D collisionShape2D;
	[Export]
	public Hitbox hitbox;
	public void OnHealthComponentTakeDamage(int damage, String type)
	{
		Play("enemiesDamage/white");
	}
	public void OnHealthComponentDie(int damage, String type)
	{
		Play("enemiesDamage/die");
	}
	public void OnAnimationFinished(StringName AnimName)
	{
		if(AnimName == "enemiesDamage/die"){
			Owner.QueueFree(); 
		}
	}
	public void OnAnimationStarted(StringName AnimName)
	{
		if(AnimName == "enemiesDamage/die"){
			speedComponent.Speed = 0f;
			speedComponent.calcDirection(GetOwner<CharacterBody2D>().Position);
			collisionShape2D.QueueFree();
			hitbox.QueueFree();
			GameManager.Instance.EmitSignal("Kill");
		}

	}
}
