using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 800.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
		}
		else
		{
			velocity = velocity.MoveToward(Vector2.Zero, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
