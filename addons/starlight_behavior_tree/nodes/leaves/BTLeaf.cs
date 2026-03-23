using Godot;
using System;
using StarlightBT.Data;

namespace StarlightBT.Nodes;

[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTLeaf.svg")]
public partial class BTLeaf : BTNode
{
    protected sealed override void OnInternalInit()
    {
        if (GetChildCount() > 0)
        {
            GD.PushWarning($"{Name} is a leaf node but has children. Leaf nodes should not have any children and will ignore them.");
        }
    }
    protected sealed override void InitBlackboardForChildren(Blackboard blackboard) {}
    protected sealed override void PassAbort() {}
}
