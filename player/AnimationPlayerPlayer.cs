using Godot;
using System;

public partial class AnimationPlayerPlayer : AnimationPlayer
{
	[Signal]
	public delegate void DeathAnimationfinishedEventHandler();
	public void OnPlayerInternalDeath(){
		Play("Death");
	}
	public void OnAnimationFinished(StringName anim){
		if(anim == "Death"){
			EmitSignal(nameof(DeathAnimationfinished));
		}

	}
}
