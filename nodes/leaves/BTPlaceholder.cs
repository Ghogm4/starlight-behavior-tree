using Godot;
using System;
[GlobalClass]
public partial class BTPlaceholder : BTLeaf
{
    [Export] private Status _returnStatus = Status.Success;
    protected sealed override Status OnUpdate()
    {
        return _returnStatus;
    }
}
