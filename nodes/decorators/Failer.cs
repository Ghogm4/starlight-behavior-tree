using Godot;
using System;
[GlobalClass]
public partial class Failer : BTDecorator
{
	protected sealed override Status OnUpdate()
	{
		if (IsChildMissing) return Status.Failure;

		_ = Child.Tick();
		return Status.Failure;
	}
}
