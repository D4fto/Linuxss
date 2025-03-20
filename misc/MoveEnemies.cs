using Godot;
using System;

public partial class MoveEnemies : Timer
{

	public void OnTimeout()
	{
		var enemies = Owner.GetNode("Enemies").GetChildren();
		Render(enemies);
		
	}
	public  void Render(Godot.Collections.Array<Godot.Node> enemies){
		foreach (CharacterBody2D enemy in enemies)
		{
			enemy.GetNode<SpeedComponent>("SpeedComponent").calcDirection(enemy.Position);
		}
	}
}
