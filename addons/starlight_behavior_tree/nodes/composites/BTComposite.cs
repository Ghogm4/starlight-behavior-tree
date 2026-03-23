using Godot;
using System;
using StarlightBT.Data;

namespace StarlightBT.Nodes;
[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTComposite.svg")]
public partial class BTComposite : BTNode
{
	protected bool IsChildrenMissing = false;
	protected int CurrentIndex = 0;
	protected Godot.Collections.Array<BTNode> Children
	{
		get
		{
			if (field != null) return field;

			if (GetChildCount() == 0)
			{
				GD.PushWarning($"{Name} has no children. A BTComposite must have at least one child.");
				IsChildrenMissing = true;
				return null;
			}

			var children = GetChildren();
			field = new Godot.Collections.Array<BTNode>();
			foreach (var child in children)
			{
				if (child is BTNode btNode)
				{
					field.Add(btNode);
				}
				else
				{
					GD.PushWarning($"{Name}'s child {child.Name} is not a BTNode and will be ignored.");
				}
			}
			if (field.Count == 0)
			{
				GD.PushWarning($"{Name} has no valid BTNode children.");
				IsChildrenMissing = true;
				return field = null;
			}
			return field;
		}
	} = null;
	protected override void OnInternalInit()
	{
		var _ = Children;
	}
	protected sealed override void InitBlackboardForChildren(Blackboard blackboard)
	{
		if (IsChildrenMissing) return;

		foreach (var child in Children)
		{
			child.Init(Blackboard);
		}
	}
	protected override void OnEnter()
	{
		CurrentIndex = -1;
	}
	protected override void OnExit()
	{
		CurrentIndex = -1;
	}
	protected override void PassAbort()
	{
		if (IsChildrenMissing || CurrentIndex == -1) return;

		Children[CurrentIndex].Abort();
	}
}
