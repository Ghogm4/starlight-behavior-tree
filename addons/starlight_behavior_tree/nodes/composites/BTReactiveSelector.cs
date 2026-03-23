using Godot;
using System;

namespace StarlightBT.Nodes;
[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTReactiveSelector.svg")]
public partial class BTReactiveSelector : BTComposite
{
    protected sealed override Status OnUpdate()
    {
        if (IsChildrenMissing) return Status.Failure;

        for (var i = 0; i < Children.Count; i++)
        {
            var child = Children[i];
            var result = child.Tick();
            if (result == Status.Failure) continue;

            if (i < CurrentIndex)
                Children[CurrentIndex].Abort();

            if (result == Status.Running)
                CurrentIndex = i;
            else
                CurrentIndex = -1;

            return result;
        }

        return Status.Failure;
    }
}
