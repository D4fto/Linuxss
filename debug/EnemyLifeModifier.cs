using Godot;
using System;

public partial class EnemyLifeModifier : Label
{
	public void OnUpdateTimeout()
	{
		Text="Enemy Life Modifier: "+GameManager.Instance.EnemiesLifeModifier;
	}
}
