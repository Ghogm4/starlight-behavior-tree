using Godot;
using System;

namespace StarlightBT.Nodes;

[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTUntilSucceed.svg")]
public partial class BTUntilSucceed : BTDecorator
{
    protected sealed override Status OnUpdate()
    {
        if (IsChildMissing) return Status.Failure;

        var result = Child.Tick();
        return result == Status.Failure ? Status.Running : result;
    }
}
