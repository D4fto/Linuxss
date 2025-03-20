using Godot;


public partial class ShinyPlayer : AnimationPlayer
{
	public override void _Ready()
	{
		Play("shiny");
	}

	public void OnAnimationFinished(StringName anim)
	{
		Play("shiny");
	}
}
