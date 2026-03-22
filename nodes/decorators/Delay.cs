using Godot;
using System;
[GlobalClass]
public partial class Delay : BTDecorator
{
	[Export] private float _baseDelayTimeInSeconds { get; set; } = 1f;
	[Export] private float _randomVariationInSeconds { get; set; } = 0f;
	private float DelayTimeInSeconds => Mathf.Clamp(_baseDelayTimeInSeconds + (float)GD.RandRange(-_randomVariationInSeconds, _randomVariationInSeconds), 0f, float.MaxValue);
	private ulong _startTimeMsec = 0;
	private bool _isDelayFinished = false;

	protected override void OnEnter()
	{
		_startTimeMsec = Time.GetTicksMsec();
		_isDelayFinished = false;
	}

	protected override Status OnUpdate()
	{
		if (IsChildMissing) return Status.Failure;

		if (_isDelayFinished) return Child.Tick();

		ulong elapsedTime = Time.GetTicksMsec() - _startTimeMsec;

		if (elapsedTime >= (ulong)(DelayTimeInSeconds * 1000f))
		{
			_isDelayFinished = true;
			return Child.Tick();
		}
		else
		{
			return Status.Running;
		}
	}

	protected override void OnExit()
	{
		_isDelayFinished = false;
		_startTimeMsec = 0;
	}
}