using Godot;
using System;
[GlobalClass]
public partial class Succeeder : BTDecorator
{
    protected sealed override Status OnUpdate()
    {
        if (IsChildMissing) return Status.Failure;

        _ = Child.Tick();
        return Status.Success;
    }
}
