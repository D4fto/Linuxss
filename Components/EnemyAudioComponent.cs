using Godot;
using System;

public partial class EnemyAudioComponent : Node
{
	[Export]
	public AudioStreamPlayer2D Hurt;
	[Export]
	public AudioStreamPlayer2D Die;

	public void OnHealthComponentDie(int damage, String type)
	{
		Die.Play();
	}
	public void OnHealthComponentTakeDamage(int damage, String type)
	{
		Hurt.Play();
	}
}
