using Godot;
using System;

public partial class LevelBar : ColorRect
{
	[Signal]
	public delegate void ChangeEventHandler();
	[Signal]
	public delegate void IncreaseLevelEventHandler(int Level);
	[Export]
	public Done done;
	[Export]
	public float MaxValue;
	[Export]
	public float Value
	{
		get => done.value;
		set { if (done != null) done.value = value; }
	}
	public int LVL = 1;
	public float XpMissing = 0f;
    public override void _Process(double delta)
    {
        if(XpMissing<=0||Engine.TimeScale==0) return;
		if(XpMissing>MaxValue*0.01f){
			Value+=MaxValue*0.01f;
			XpMissing-=MaxValue*0.01f;
		}else{
			Value+=XpMissing;
			XpMissing=0;
		}
		EmitSignal(nameof(Change));
		if(Value>=MaxValue){
			Level();
		}
    }
    public void OnGuiOnGamePointsChange(int points)
	{
		XpMissing+=points*GameManager.Instance.playerXpModifier;
	}
	public void Level()
	{
		Value -=  MaxValue;
		MaxValue += 12.5f*(LVL/10+1);
		MaxValue = (float)Math.Round(MaxValue);
		LVL++;
		EmitSignal(nameof(IncreaseLevel),LVL);
		EmitSignal(nameof(Change));
	}
}
