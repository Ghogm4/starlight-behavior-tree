using Godot;
using System;

public partial class Action_SpreadBullet : BTAction
{
	[Export] public PackedScene BulletScene;
	[Export] public float BulletCount = 5;
	[Export] public float ChargeTimeInSeconds = 1f;
	private ulong _currentTime = 0;
	protected override void OnEnter()
	{
		_currentTime = Time.GetTicksMsec();
		if (Blackboard.TryGet("Self", out Enemy self) && self != null)
		{
			self.Velocity = Vector2.Zero;
			self.Rotation = 0f;
		}
	}
	protected override Status OnUpdate()
	{
		if (Time.GetTicksMsec() - _currentTime < ChargeTimeInSeconds * 1000)
		{
			if (Blackboard.TryGet("Self", out Enemy self) && self != null)
			{
				self.Rotation += 0.15f;
			}
			return Status.Running;
		}
		float rotation = 0f;
		for (int i = 0; i < BulletCount; i++)
		{
			rotation += Mathf.Tau / BulletCount;
			var bullet = BulletScene.Instantiate<Bullet>();
			bullet.Position = (Owner as Enemy).GlobalPosition;
			bullet.Rotation = rotation;
			bullet.Scale *= 0.5f;
			GetTree().CurrentScene.AddChild(bullet);
		}
		return Status.Success;
	}
	protected override void OnExit()
	{
		if (Blackboard.TryGet("Self", out Enemy self) && self != null)
		{
			self.Velocity = Vector2.Zero;
			self.Rotation = 0f;
		}
	}
}
