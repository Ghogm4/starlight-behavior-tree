using Godot;
using System;

public partial class Action_MoveTowardPlayer : BTAction
{
	private Player Player => field ??= GetTree().GetFirstNodeInGroup("Player") as Player;
	protected override Status OnUpdate()
	{
		var self = Blackboard.Get<Enemy>("Self");
		Vector2 direction = (Player.GlobalPosition - self.GlobalPosition).Normalized();
		self.Velocity = direction * 400f;
		self.MoveAndSlide();
		return Status.Running;
	}

	protected override void OnExit()
	{
		if (Blackboard.TryGet("Self", out Enemy self) && self != null)
		{
			self.Velocity = Vector2.Zero;
		}
	}
}
