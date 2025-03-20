using Godot;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

public partial class EnemiesSpawn : Timer
{
	[Export]
	XPMesh PointsMesh;
	public PackedScene chrome = GD.Load<PackedScene>("res://Enemies/Chrome/chrome.tscn");
	public PackedScene Zap = GD.Load<PackedScene>("res://Enemies/Zap/zap.tscn");
	private void OnTimeout()
	{
		if(Owner.GetNode("Enemies").GetChildCount()<450){
			var rng = new RandomNumberGenerator();
			float number = rng.Randf();
			if(number<.1){
				Zap zapinstance = Zap.Instantiate() as Zap;
				zapinstance.PointsMesh = PointsMesh;
				GetParent().GetNode<Node>("Enemies").AddChild(zapinstance);
			}
			Chrome chromeinstance = chrome.Instantiate() as Chrome;
			chromeinstance.PointsMesh = PointsMesh;
			GetParent().GetNode<Node>("Enemies").AddChild(chromeinstance);

		}
	}
	public void OnGuiOnGameIncreaseLevel(int level)
	{
		if(WaitTime<=0.03) return;
		if(WaitTime>3)
		{
			WaitTime-=1;
		}
		else if(WaitTime>2)
		{
			WaitTime-=1;
		}else if(WaitTime>1)
		{
			WaitTime-=.5;
		}else if(WaitTime>.5)
		{
			WaitTime-=.05;
		}else{
			WaitTime-=0.01;
		}
	}

	
}
