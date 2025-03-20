using Godot;
using System;
using System.Collections.Generic;

public partial class XPMesh : MultipleMeshManager
{
	public List<int> Magneticzed = new List<int>();
	public float Speed = 0f;
	[Export]
	public AudioStreamPlayer CollectSound;
	public Dictionary<int, List<int>> instances = new Dictionary<int, List<int>>{};
	
	public override void _Ready()
	{
		Multimesh.InstanceCount = 40000;
		Multimesh.VisibleInstanceCount = 0;
	}
	public override void _Process(double delta)
	{
		foreach(int i in Magneticzed){
			MoveInstanceTo(i, GameManager.Instance.PlayerPos, Speed, (float)delta);
		}
	}
	public int VerifyPlayerProximity(int distance, int magnetic, Vector2 playerPos, float speed)
	{
		List<int> collected = new List<int>();
		Speed = speed;
		Magneticzed = new List<int>();
		for(int i = 0; i < Multimesh.VisibleInstanceCount; i++){
			if(RetiredIds.Contains(i)){
				continue;
			}
			if(playerPos.DistanceTo(Multimesh.GetInstanceTransform2D(i).Origin) < distance){
				collected.Add(i);
				continue;
			}
			if(playerPos.DistanceTo(Multimesh.GetInstanceTransform2D(i).Origin) < magnetic){
				Magneticzed.Add(i);
				continue;
			}
		}
		int sum = 0;
		foreach(int i in collected){
			sum += RemoveInstance(i);
		}
		
		return sum;
	}
	public int RegisterInstance(int points, Color color)
	{
		if(!(RetiredIds.Count == 0)){
			int idl = RetiredIds[RetiredIds.Count-1];
			RetiredIds.RemoveAt(RetiredIds.Count-1);
			AddOnPoints(idl, points);
			Multimesh.SetInstanceColor(idl, color);
			return idl;
		}
		int id = Multimesh.VisibleInstanceCount;
		AddOnPoints(id, points);
		Multimesh.VisibleInstanceCount++;
		Multimesh.SetInstanceColor(id, color);
		return id;
	}
	public void AddOnPoints(int id, int value)
	{
		if(!instances.TryGetValue(value, out var instance)){
			instances[value] = new List<int>();
		}
		instances[value].Add(id);
	}
	public int RemoveFromPoints(int id)
	{
		int x = -696969;
		foreach(KeyValuePair<int, List<int>> item in instances){
			if(item.Value.Contains(id)){
				x = item.Key;
				CollectSound.Play();
				break;
			}
		}
		if(x!=-696969){
			instances[x].Remove(id);
		}
		
		return x;
	}
	public new int RemoveInstance(int id)
	{
		RetiredIds.Add(id);
		Multimesh.SetInstanceColor(id, new Color(0,0,0,0));
		Multimesh.SetInstanceTransform2D(id,new Transform2D(0.8f,new Vector2(1.3f, 1.4f),0.54f,new Vector2(float.MaxValue, float.MaxValue)));
		return RemoveFromPoints(id);
	}
}
