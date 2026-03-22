using Godot;
using System;

public partial class Condition_IsPlayerNearby : BTCondition
{
	private Player Player => field ??= GetTree().GetFirstNodeInGroup("Player") as Player;
	protected override bool Check()
	{
		return Blackboard.Get<Area2D>("Detector").GetOverlappingBodies().Contains(Player);
	}
}
