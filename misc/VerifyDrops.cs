using Godot;
using System;

public partial class VerifyDrops : Timer
{
	[Export]
	public XPMesh xPMesh;
	[Export]
	public Player player;
	private int actualPoints = 0;
	public void OnTimeout()
	{
		actualPoints = xPMesh.VerifyPlayerProximity(player.CollectDistance, player.MagneticDistance, player.Position, player.Speed*1.5f*player.SpeedModifier);
		if(actualPoints != 0){
			GameManager.Instance.EmitSignal("ChangePoints",  actualPoints);
			actualPoints = 0;
		}
	}
}
