using Godot;
using System;
using StarlightBT.Nodes;
using StarlightBT.Data;

public partial class Action_GetConfused : BTAction
{
	private bool _isTweenFinished = false;
	private Tween _tween;
	private void ResetScale()
	{
		Blackboard.Get<Sprite2D>("Sprite").Scale = Vector2.One;
	}
	protected override void OnInit()
	{
		Blackboard.Set("TargetScale", Vector2.One * 1.2f);
	}
	private void OnTweenFinished()
	{
		_isTweenFinished = true;
	}
	protected override void OnEnter()
	{
		ResetScale();
		_isTweenFinished = false;
		_tween = CreateTween();
		_tween.TweenProperty(Blackboard.Get<Sprite2D>("Sprite"), "scale", Blackboard.Get<Vector2>("TargetScale"), 0.5f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
		_tween.TweenProperty(Blackboard.Get<Sprite2D>("Sprite"), "scale", Vector2.One, 0.5f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
		_tween.Finished += OnTweenFinished;
	}
	protected override Status OnUpdate()
	{
		if (!_isTweenFinished) return Status.Running;
		return Status.Success;
	}
	protected override void OnAbort()
	{
		_tween?.Finished -= OnTweenFinished;
		_tween?.Kill();
	}
	protected override void OnExit()
	{
		ResetScale();
		_isTweenFinished = false;
	}
}
