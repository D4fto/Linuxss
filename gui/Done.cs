using Godot;

public partial class Done : ColorRect
{
	public float value;
	public float MaxValue;

	public void Update(){
		Vector2 tam = GetParent<LevelBar>().Size * new Vector2(value/GetParent<LevelBar>().MaxValue, 1);
		Size = tam;
		GetNode<ColorRect>("ColorRect").Size = new Vector2(tam.X, 3);
	}
	public override void _Ready()
	{
		Update();
	}
	public void OnLevelBarChange(){
		Update();
	}
}
