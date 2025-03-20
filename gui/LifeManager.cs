using Godot;
using System;

public partial class LifeManager : Node
{
	int max = 0;
	public PackedScene heartScene = GD.Load<PackedScene>("res://gui/heart.tscn");
	public void OnGuiOnGameLifeChange(int life, int maxlife)
	{
		while (maxlife > max){
			var instance = heartScene.Instantiate<Node2D>();

			instance.GlobalPosition = new Vector2(32*max+(8*(max+1)), 540-8);
			AddChild(instance);
			max+=1;
		}
		int i = 0;
		foreach (AnimatedSprite2D heart in GetChildren()){
			if(life>i){
				heart.Frame = 0;
			}else{
				heart.Frame = 1;
			}
			i++ ;
		}

	}
}
