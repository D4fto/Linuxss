using Godot;
using System;

public partial class ActualScene : Node
{
	public override void _Ready()
	{
		GameManager.Instance.actualScene = this;
	}
	public void changeScene(string sceneName)
	{
		PackedScene newScene = (PackedScene)ResourceLoader.Load(sceneName);
        if (newScene == null)
        {
            GD.PrintErr("Erro: Não foi possível carregar a cena: " + sceneName);
            return;
        }

        Node Scene = newScene.Instantiate();

		foreach (Node child in GetChildren()) 
        {
            child.QueueFree();
        }
		AddChild(Scene);
	}
}
