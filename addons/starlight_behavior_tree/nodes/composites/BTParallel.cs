using Godot;
using System;
using System.Collections.Generic;

namespace StarlightBT.Nodes;
[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTParallel.svg")]
public partial class BTParallel : BTComposite
{
	public enum PriorityMode
	{
		FailureFirst,
		SuccessFirst
	}

	[Export] private int _successThreshold { get; set; } = 1;
	[Export] private int _failureThreshold { get; set; } = 1;
	[Export] private PriorityMode _priorityMode { get; set; } = PriorityMode.FailureFirst;
	private Status[] _childStatuses = null;
	protected sealed override void OnInternalInit()
	{
		base.OnInternalInit();
		CheckThresholds();
	}
	protected sealed override Status OnUpdate()
	{
		if (IsChildrenMissing) return Status.Failure;

		int successCount = 0;
		int failureCount = 0;
		int runningCount = 0;

		for (int i = 0; i < Children.Count; i++)
		{
			if (_childStatuses[i] == Status.Running)
				_childStatuses[i] = Children[i].Tick();

			if (_childStatuses[i] == Status.Success) successCount++;
			else if (_childStatuses[i] == Status.Failure) failureCount++;
			else runningCount++;
		}

		Status finalStatus = DecideStatus(successCount, failureCount);

		if (finalStatus != Status.Running)
		{
			return finalStatus;
		}
		else if (runningCount == 0)
		{
			GD.PushWarning($"{Name}: All children finished but thresholds not met. Returning Failure.");
			return Status.Failure;
		}

		return Status.Running;
	}
	protected sealed override void OnEnter()
	{
		ResetStatuses();
	}
	protected sealed override void OnExit()
	{
		AbortRunningChildren();
	}
	// Intentionally set empty as there is an OnExit() that will handle aborting running children, and we don't want to call it twice during an abort.
	protected sealed override void PassAbort() {}
    protected sealed override void OnAbort() {}

	private void CheckThresholds()
	{
		if (IsChildrenMissing) return;

		int count = Children.Count;
		if (_successThreshold < 0 || _successThreshold > count)
		{
			GD.PushWarning($"{Name}: SuccessThreshold {_successThreshold} is out of bounds. Clamping to valid range.");
		}
		if (_failureThreshold < 0 || _failureThreshold > count)
		{
			GD.PushWarning($"{Name}: FailureThreshold {_failureThreshold} is out of bounds. Clamping to valid range.");
		}
		_successThreshold = Mathf.Clamp(_successThreshold, 0, count);
		_failureThreshold = Mathf.Clamp(_failureThreshold, 0, count);
	}
	private Status DecideStatus(int successCount, int failureCount)
	{
		if (_priorityMode == PriorityMode.FailureFirst)
		{
			if (failureCount >= _failureThreshold) return Status.Failure;
			if (successCount >= _successThreshold) return Status.Success;
		}
		else
		{
			if (successCount >= _successThreshold) return Status.Success;
			if (failureCount >= _failureThreshold) return Status.Failure;
		}
		return Status.Running;
	}
	private void ResetStatuses()
	{
		if (IsChildrenMissing) return;

		if (_childStatuses == null)
		{
			_childStatuses = new Status[Children.Count];
		}
		for (int i = 0; i < _childStatuses.Length; i++)
		{
			_childStatuses[i] = Status.Running;
		}
	}
	private void AbortRunningChildren()
	{
		if (IsChildrenMissing || _childStatuses == null) return;

		for (int i = 0; i < _childStatuses.Length; i++)
		{
			if (_childStatuses[i] == Status.Running)
			{
				Children[i].Abort();
			}
		}
	}
}