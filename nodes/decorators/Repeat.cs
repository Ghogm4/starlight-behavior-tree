using Godot;
using System;
[GlobalClass]
public partial class Repeat : BTDecorator
{
    protected sealed override Status OnUpdate()
    {
        if (IsChildMissing) return Status.Failure;

        _ = Child.Tick();
        return Status.Running;
    }
}
