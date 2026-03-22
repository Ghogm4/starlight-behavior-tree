using Godot;
using System;
[GlobalClass]
public partial class UntilSucceed : BTDecorator
{
    protected sealed override Status OnUpdate()
    {
        if (IsChildMissing) return Status.Failure;

        var result = Child.Tick();
        return result == Status.Failure ? Status.Running : result;
    }
}
