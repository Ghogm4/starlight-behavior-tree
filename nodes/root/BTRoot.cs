using Godot;
using System;
[GlobalClass]
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
