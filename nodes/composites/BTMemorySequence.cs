using Godot;
using System;
[GlobalClass]
public partial class BTMemorySequence : BTComposite
{
    protected sealed override void OnEnter()
    {
        CurrentIndex = 0;
    }
    protected sealed override Status OnUpdate()
    {
        if (IsChildrenMissing) return Status.Failure;

        for (var i = CurrentIndex; i < Children.Count; i++)
        {
            var result = Children[i].Tick();

            if (result == Status.Running)
            {
                CurrentIndex = i;
            }

            if (result != Status.Success)
            {
                return result;
            }
        }

        return Status.Success;
    }
}
