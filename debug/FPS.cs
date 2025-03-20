using Godot;

public partial class FPS : Label
{
	public bool active = false;
	public override void _Process(double delta)
	{
		if(active){
			Text = $"FPS: {Engine.GetFramesPerSecond()}";
		}
	}
	public void OnGuiDebugDebugOff()
	{
		active = false;
	}
	public void OnGuiDebugDebugOn()
	{
		active = true;
	}
}
