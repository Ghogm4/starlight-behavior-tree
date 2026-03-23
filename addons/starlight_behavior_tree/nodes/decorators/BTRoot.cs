using Godot;
using System;

namespace StarlightBT.Nodes;

[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTRoot.svg")]
public partial class BTRoot : BTDecorator
{
	private bool IsActive
	{
		get => field;
		set
		{
			if (field == value) return;

			field = value;
			if (!field)
			{
				Child?.Abort();
			}
		}
	} = false;
	public void Start()
	{
		if (IsChildMissing) return;

		IsActive = true;
	}
	public void Stop()
	{
		if (IsChildMissing) return;

		IsActive = false;
	}
	public override void _PhysicsProcess(double delta)
	{
		if (IsActive)
		{
			_ = Child?.Tick();
		}
	}
}
