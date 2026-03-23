using Godot;
using System;

namespace StarlightBT.Nodes;

[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTPlaceholder.svg")]
public partial class BTPlaceholder : BTLeaf
{
    [Export] private Status _returnStatus = Status.Success;
    protected sealed override Status OnUpdate()
    {
        return _returnStatus;
    }
}
