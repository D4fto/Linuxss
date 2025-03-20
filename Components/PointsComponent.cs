using Godot;
using System;
using System.Collections.Generic;

public partial class PointsComponent : Node
{
	[Export]
	public int Points;
	[Export]
	public Color color = new Color(1,1,1);
	[Export]
	public Node2D PointsSpawn;
	public XPMesh xPMesh;

	public void OnHealthComponentDie(int damage, String type){
		int InstanceID = xPMesh.RegisterInstance(Points, color);
		xPMesh.Multimesh.SetInstanceTransform2D(InstanceID,new Transform2D(0, PointsSpawn.Position));
	}
}
