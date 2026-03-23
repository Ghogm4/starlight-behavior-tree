using Godot;
using System;

namespace StarlightBT.Nodes;

[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTRepeat.svg")]
public partial class BTRepeat : BTDecorator
{
    protected sealed override Status OnUpdate()
    {
        if (IsChildMissing) return Status.Failure;

        _ = Child.Tick();
        return Status.Running;
    }
}
