using Godot;
using System;
[GlobalClass]
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
