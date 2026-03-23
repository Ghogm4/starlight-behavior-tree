using Godot;
using System;

namespace StarlightBT.Nodes;

[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTFail.svg")]
public partial class BTFail : BTDecorator
{
	protected sealed override Status OnUpdate()
	{
		if (IsChildMissing) return Status.Failure;

		_ = Child.Tick();
		return Status.Failure;
	}
}
