using Godot;
using System;

namespace StarlightBT.Nodes;
[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTMemorySelector.svg")]
public partial class BTMemorySelector : BTComposite
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

            if (result != Status.Failure)
            {
                return result;
            }
        }

        return Status.Failure;
    }
}
