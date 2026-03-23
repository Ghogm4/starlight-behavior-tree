using Godot;
using System;
using StarlightBT.Nodes;
using StarlightBT.Data;

public partial class Action_LaunchBullet : BTAction
{
	[Export] public PackedScene BulletScene;
	private Player Player => field ??= GetTree().GetFirstNodeInGroup("Player") as Player;
	protected override Status OnUpdate()
	{
		if (!Blackboard.TryGet("Self", out Enemy self) || self == null)
		{
			GD.PushWarning($"{Name}: Missing 'Self' in blackboard.");
			return Status.Failure;
		}

		if (Player == null)
		{
			GD.PushWarning($"{Name}: Player not found in group 'Player'.");
			return Status.Failure;
		}

		if (BulletScene == null)
		{
			GD.PushWarning($"{Name}: BulletScene is not assigned.");
			return Status.Failure;
		}

		var scene = GetTree().CurrentScene;
		if (scene == null)
		{
			GD.PushWarning($"{Name}: CurrentScene is null, cannot spawn bullet.");
			return Status.Failure;
		}

		var bullet = BulletScene.Instantiate<Bullet>();
		bullet.GlobalPosition = self.GlobalPosition;
		Vector2 direction = (Player.GlobalPosition - self.GlobalPosition).Normalized();
		bullet.Rotation = direction.Angle();
		bullet.Scale *= 0.5f;
		scene.AddChild(bullet);
		return Status.Success;
	}
}
