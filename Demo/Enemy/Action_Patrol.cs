using Godot;
using System;
using StarlightBT.Nodes;
using StarlightBT.Data;

public partial class Action_Patrol : BTAction
{
	private Vector2 _patrolPivot = Vector2.Zero;
	private float _patrolRadius = 800f;
	private Vector2 _nextPatrolPoint = Vector2.Zero;
	private void GetNextPatrolPoint()
	{
		_nextPatrolPoint = _patrolPivot + Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau)) * _patrolRadius;
	}
	protected override void OnEnter()
	{
		var self = Blackboard.Get<Enemy>("Self");
		_patrolPivot = self.Position;
	}
	protected override Status OnUpdate()
	{
		var self = Blackboard.Get<Enemy>("Self");
		if (self.Position.DistanceTo(_patrolPivot) >= _patrolRadius) return Status.Running;

		if (_nextPatrolPoint == Vector2.Zero || self.Position.DistanceTo(_nextPatrolPoint) <= 10f)
		{
			GetNextPatrolPoint();
		}
		self.Velocity = (_nextPatrolPoint - self.Position).Normalized() * 200;
		self.MoveAndSlide();

		return Status.Running;
	}
}
