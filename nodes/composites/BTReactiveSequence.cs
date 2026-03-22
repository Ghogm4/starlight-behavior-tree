using Godot;
using System;
[GlobalClass]
public partial class BTReactiveSequence : BTComposite
{
	protected sealed override Status OnUpdate()
	{
		if (IsChildrenMissing) return Status.Failure;

		for (var i = 0; i < Children.Count; i++)
		{
			var child = Children[i];
			var result = child.Tick();
			if (result == Status.Success) continue;

			if (i < CurrentIndex)
				Children[CurrentIndex].Abort();
			
			if (result == Status.Running)
				CurrentIndex = i;

			return result;
		}

		return Status.Success;
	}
}
