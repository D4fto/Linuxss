using Godot;
using System;

public partial class EnemySpeedModifier : Label
{
	public void OnUpdateTimeout()
	{
		Text="Enemy Speed Modifier: "+GameManager.Instance.EnemiesSpeedModifier;
	}
}
