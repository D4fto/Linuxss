using Godot;
using System;

public partial class PlayerCamera2d : Camera2D
{
	[Export]
	public float randomStrength = 30.0f;
	[Export]
	public float shakeFade = 5.0f;
	public RandomNumberGenerator rng = new RandomNumberGenerator();
	public float shakeStrength = 0f;
	public void OnPlayerTakeDamage()
	{
		applyShake();
	}
	public void applyShake()
	{
		shakeStrength = randomStrength;
	}
	public override void _Process(double delta)
	{
		if (shakeStrength > 0)
		{
			shakeStrength = Mathf.Lerp(shakeStrength, 0, shakeFade * (float)delta);
			Offset = randomOffset();
		}
	}
	public Vector2 randomOffset()
	{
		return new Vector2(rng.RandfRange(-shakeStrength,shakeStrength),rng.RandfRange(-shakeStrength,shakeStrength));
	}
}
