using Godot;
using System;
using System.Threading;


public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void TakeDamageEventHandler();
	[Signal]
	public delegate void InternalDeathEventHandler();
	[Signal]
	public delegate void DeathEventHandler();
	[Signal]
	public delegate void LifeChangeEventHandler(int life, int maxLife);
	[Signal]
	public delegate void PointsChangeEventHandler(int points);
	[Export]
	public Cards cards;
	[Export]
	public int bulletPerfuration = 1;
	[Export]
	public int CollectDistance;
	[Export]
	public int MagneticDistance;
	public Material WhiteShaderMaterial = GD.Load<Material>("res://materials/white.tres");
	public PackedScene bullet = GD.Load<PackedScene>("res://player/attacks/Bullet.tscn");
	private int NumberOfEnemies = 0;
	public float Speed = 300.0f;
	public bool clicking = false;
	private bool invincible = false;
	private int life = 5;
	private int Maxlife = 5;
	private bool alive = true;
	public int Kills = 0;
	public int Points = 0;
	public float AttackReloadModifier = 1;
	public float SpeedModifier = 1;
	public float BulletScale = 1;
	public override void _Ready()
	{
		
		GameManager.Instance.Kill += OnKill;
		GameManager.Instance.ChangePoints += OnChangePoints;
		EmitSignal(nameof(LifeChange),life,Maxlife);
	}
	public override void _PhysicsProcess(double delta)
	{
		if(!alive){
			return;
		}
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction.Normalized() * Speed * SpeedModifier;
			
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed * SpeedModifier);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed * SpeedModifier);
		}

		Velocity = velocity;
		MoveAndSlide();
		GameManager.Instance.PlayerPos = this.Position;
	}
	public override void _Input(InputEvent @event)
	{

		if (@event.IsActionPressed("click"))
		{
			clicking = true;
		}
		if (@event.IsActionReleased("click"))
		{
			clicking = false;
		}
	}

	public void OnTimerTimeout()
	{
		if(clicking && alive)
		{
			
			var instance = bullet.Instantiate<Bullet>();

			instance.GlobalPosition = GetNode<Marker2D>("Marker2D/BulletSpawn").GlobalPosition;
			instance.life = bulletPerfuration;

			instance.angle = GetAngleTo(GetGlobalMousePosition()) + (float)Math.PI/2;
			instance.setScale(BulletScale);

			GetParent().GetNode<Node>("Projectiles").AddChild(instance);

			GetNode<AudioStreamPlayer2D>("Shoot").Play();
		}
	}
	public void OnInternalDeath(){
		GetNode<Hitbox>("Hitbox").QueueFree();
		GetNode("CollisionShape2D").QueueFree();
		GetNode<AudioStreamPlayer>("Death").Play();
		GameManager.Instance.playerDamage = 10;
		GameManager.Instance.playerXpModifier = 1;
		GameManager.Instance.EnemiesLifeModifier = 1;
		GameManager.Instance.EnemiesSpeedModifier = 1;
		GameManager.Instance.Regen = -1;
		CollectDistance = -1;
		MagneticDistance = -1;
		cards.hide();
		cards.clear();
		alive = false;
	}
	public void OnHitboxEnemyEntered()
	{
		NumberOfEnemies += 1;
		if(!invincible){
			life -= 1;
			GetNode<AudioStreamPlayer2D>("Hurt").Play();
			EmitSignal(nameof(TakeDamage));
			if(life <= 0) {
				EmitSignal(nameof(InternalDeath));
				return;
			};
			EmitSignal(nameof(LifeChange),life,Maxlife);
			GetNode<Godot.Timer>("Invincibility").Start();
			invincible = true;
			GetNode<Sprite2D>("Marker2D/Sprite2D").Material = WhiteShaderMaterial;
		}
	}
	public void OnHitboxEnemyExited()
	{
		NumberOfEnemies -= 1;
	}
	public int GetLife()
	{
		return life;
	}

	public void OnInvincibilityTimeout()
	{
		GetNode<Sprite2D>("Marker2D/Sprite2D").Material = null;
		if(NumberOfEnemies>0){
			life -= 1;
			GetNode<AudioStreamPlayer2D>("Hurt").Play();
			EmitSignal(nameof(TakeDamage));
			if(life <= 0) {
				EmitSignal(nameof(InternalDeath));
				return;
			};
			EmitSignal(nameof(LifeChange),life,Maxlife);
			GetNode<Godot.Timer>("Invincibility").Start();
			BackWhite();
			return;
		}
		invincible = false;
		GetNode<Sprite2D>("Marker2D/Sprite2D").Material = null;
	}
	public void OnAnimationPlayerDeathAnimationfinished(){
		EmitSignal(nameof(Death));
	}
	private async void BackWhite()
	{
		await System.Threading.Tasks.Task.Delay(10);
		GetNode<Sprite2D>("Marker2D/Sprite2D").Material = WhiteShaderMaterial;
	}
	private void GoToMainMenu()
	{
		GetTree().ChangeSceneToFile("res://menus_/MainMenu.tscn");
	}
	public void OnKill()
	{
		Kills+=1;
	}
	public void OnChangePoints(int points)
	{
		Points+=points;
		EmitSignal(nameof(PointsChange), points);
	}
	public void UpdatePoints()
	{
		EmitSignal(nameof(PointsChange), 0);
	}
	public override void _ExitTree()
	{
		GameManager.Instance.ChangePoints -= OnChangePoints;
	}
	public void heal(){
		if(life<Maxlife){
			life++;
		}
		EmitSignal(nameof(LifeChange),life,Maxlife);
	}
	public void increaseMaxLife(){
		Maxlife++;
		life++;
		EmitSignal(nameof(LifeChange),life,Maxlife);
	}
	public void LifeToMaxLife(){
		life = Maxlife;
		EmitSignal(nameof(LifeChange),life,Maxlife);
	}
	public void DecreaseAttackReload(float percent){
		AttackReloadModifier-=percent;
		GetNode<Godot.Timer>("Timer").WaitTime = .2f*AttackReloadModifier; 
	}
}
