using Godot;
using System;

public partial class HealthComponent : Node
{
	[Signal]
	public delegate void TakeDamageEventHandler(int damage, String type = "normal");
	[Signal]
	public delegate void LifeChangeEventHandler(int Life);
	[Signal]
	public delegate void DieEventHandler(int damage, String type = "normal");
	[Signal]
	public delegate void HealEventHandler(int health, String type = "normal");
	[Export]
	public int MaxLife;
	public int currentLife;
	

	public override void _Ready()
	{
		MaxLife = (int)(MaxLife*GameManager.Instance.EnemiesLifeModifier);
		currentLife = MaxLife;
	}
	public void STakeDamage(int damage, String type = "normal")
	{
		currentLife -= damage;
		EmitSignal(nameof(TakeDamage), damage, type);
		EmitSignal(nameof(LifeChange), currentLife);
		if(currentLife <= 0)
		{
			EmitSignal(nameof(Die), damage, type);
		}
	}
	public void SHeal(int health, String type = "normal")
	{
		currentLife += health;
		EmitSignal(nameof(Heal), health, type);
		EmitSignal(nameof(LifeChange), currentLife);
	}
}
