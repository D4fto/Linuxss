using Godot;
using System;

public partial class TimeCount : Label
{
	private float modifier = .05f;
	public int seconds = 0;
	public void OnTimerTimeout()
	{
		seconds++;
		if(seconds % 30 == 0 && seconds>0){
			GameManager.Instance.EnemiesLifeModifier+=modifier;
			GameManager.Instance.EnemiesSpeedModifier+=modifier;
		}
		if(seconds % GameManager.Instance.Regen == 0 && GameManager.Instance.Regen != -1){
			GetParent<GuiOnGame>().player.heal();
		}
		if(seconds/60 == 5){
			modifier=.02f;
		}
		if(seconds/60 == 10){
			modifier=.5f;
		}
		Text = $"{seconds/60:D2}:{seconds%60:D2}";
	}

}
