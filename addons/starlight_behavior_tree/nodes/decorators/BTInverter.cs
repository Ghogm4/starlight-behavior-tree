using Godot;
using System;

namespace StarlightBT.Nodes;

[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTInverter.svg")]
public partial class BTInverter : BTDecorator
{
	protected sealed override Status OnUpdate()
	{
		if (IsChildMissing) return Status.Failure;

		var result = Child.Tick();
		return result switch
		{
			Status.Success => Status.Failure,
			Status.Failure => Status.Success,
			Status.Running => Status.Running,
			_ => throw new InvalidOperationException("Invalid status returned by child node.")
		};
	}
}
