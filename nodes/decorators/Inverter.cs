using Godot;
using System;
[GlobalClass]
public partial class Inverter : BTDecorator
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
