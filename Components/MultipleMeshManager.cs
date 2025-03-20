using Godot;
using System;
using System.Collections.Generic;

public partial class MultipleMeshManager : MultiMeshInstance2D
{
    [Signal]
	public delegate void InstanceRegisteredEventHandler(int id);
    [Signal]
	public delegate void InstanceRemovedEventHandler(int id);
    

	public List<int> RetiredIds = new List<int>();
	
	public void MoveInstanceTo(int id, Vector2 targetPosition, float speed, float delta)
	{
		Transform2D transform = Multimesh.GetInstanceTransform2D(id);
		Vector2 currentPosition = transform.Origin;
		Vector2 direction = (targetPosition - currentPosition).Normalized();
		Vector2 newPosition = currentPosition + direction * speed * delta;
		transform.Origin = newPosition;
		Multimesh.SetInstanceTransform2D(id, transform);
	}
	public int RegisterInstance(Color color)
	{
        int id;
		if(!(RetiredIds.Count == 0)){
			id = RetiredIds[RetiredIds.Count-1];
			RetiredIds.RemoveAt(RetiredIds.Count-1);
			Multimesh.SetInstanceColor(id, color);
            EmitSignal(nameof(InstanceRegistered), id);
			return id;
		}
		id = Multimesh.VisibleInstanceCount;
		Multimesh.VisibleInstanceCount++;
		Multimesh.SetInstanceColor(id, color);
        EmitSignal(nameof(InstanceRegistered), id);
		return id;
	}


	public void RemoveInstance(int id)
	{
		RetiredIds.Add(id);
		Multimesh.SetInstanceColor(id, new Color(0,0,0,0));
		Multimesh.SetInstanceTransform2D(id,new Transform2D(0.8f,new Vector2(1.3f, 1.4f),0.54f,new Vector2(float.MaxValue, float.MaxValue)));
        EmitSignal(nameof(InstanceRemoved), id);

	}
}
