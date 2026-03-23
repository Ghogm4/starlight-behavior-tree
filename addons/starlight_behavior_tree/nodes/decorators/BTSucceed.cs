using Godot;
using System;

namespace StarlightBT.Nodes;

[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTSucceed.svg")]
public partial class BTSucceed : BTDecorator
{
    protected sealed override Status OnUpdate()
    {
        if (IsChildMissing) return Status.Failure;

        _ = Child.Tick();
        return Status.Success;
    }
}
