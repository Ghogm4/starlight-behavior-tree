using Godot;
using System;
[GlobalClass]
public partial class UntilFail : BTDecorator
{
    protected sealed override Status OnUpdate()
    {
        if (IsChildMissing) return Status.Failure;

        var result = Child.Tick();
        return result == Status.Failure ? Status.Success : Status.Running;
    }
}
