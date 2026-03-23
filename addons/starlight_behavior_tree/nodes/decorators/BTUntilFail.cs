using Godot;
using System;

namespace StarlightBT.Nodes;

[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTUntilFail.svg")]
public partial class BTUntilFail : BTDecorator
{
    protected sealed override Status OnUpdate()
    {
        if (IsChildMissing) return Status.Failure;

        var result = Child.Tick();
        return result == Status.Failure ? Status.Success : Status.Running;
    }
}
