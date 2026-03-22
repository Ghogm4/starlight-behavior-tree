using Godot;
using System;

public partial class Condition_IsPlayerWithinDistance : BTCondition
{
	[Export] public float Distance = 100f;

	private Player Player => field ??= GetTree().GetFirstNodeInGroup("Player") as Player;

	protected override bool Check()
	{
		if (!Blackboard.TryGet("Self", out Enemy self) || self == null)
		{
			GD.PushWarning($"{Name}: Missing 'Self' in blackboard.");
			return false;
		}

		if (Player == null)
		{
			GD.PushWarning($"{Name}: Player not found in group 'Player'.");
			return false;
		}

		float threshold = Mathf.Max(0f, Distance);
        bool isWithinDistance = self.GlobalPosition.DistanceTo(Player.GlobalPosition) <= threshold;
        
        return isWithinDistance;
	}
}
