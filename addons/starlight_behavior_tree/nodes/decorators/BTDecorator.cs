using Godot;
using System;
using StarlightBT.Data;
namespace StarlightBT.Nodes;
[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTDecorator.svg")]
public partial class BTDecorator : BTNode
{
	protected bool IsChildMissing = false;
	protected BTNode Child
	{
		get
		{
			if (IsChildMissing) return null;
			if (field != null) return field;
			if (GetChildCount() == 0)
			{
				GD.PushWarning($"{Name} has no children. A BTDecorator must have exactly one child.");
				IsChildMissing = true;
				return null;
			}
			if (GetChildCount() > 1)
			{
				GD.PushWarning($"{Name} has more than one child, only the first BTNode will be used.");
			}

			foreach (var child in GetChildren())
			{
				if (child is BTNode btNode)
				{
					field = btNode;
					break;
				}
			}

			if (field == null)
			{
				GD.PushWarning($"{Name} has no BTNode child.");
				IsChildMissing = true;
				return null;
			}

			return field;
		}
	} = null;
	protected sealed override void OnInternalInit()
	{
		var _ = Child;
	}
	protected sealed override void InitBlackboardForChildren(Blackboard blackboard)
	{
		if (IsChildMissing) return;

		Child.Init(Blackboard);
	}
	protected sealed override void PassAbort()
	{
		if (IsChildMissing || !(Child.CurrentStatus == Status.Running)) return;

		Child.Abort();
	}
	protected override Status OnUpdate()
	{
		if (IsChildMissing) return Status.Failure;

		return Child.Tick();
	}
}
