using Godot;
using System;
[GlobalClass]
public partial class BTCondition : BTLeaf
{
    /// <summary>
    /// Override this method to implement the condition logic. It should return true if the condition is met, and false otherwise.
    /// </summary>
    /// <returns></returns>
    protected virtual bool Check()
    {
        GD.PushWarning($"{Name} does not implement Check(), returning false by default.");
        return false;
    }
    protected sealed override Status OnUpdate()
    {
        return Check() ? Status.Success : Status.Failure;
    }
}
