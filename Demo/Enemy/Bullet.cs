using Godot;
using System;

public partial class Bullet : Sprite2D
{
    [Export] public float Speed = 1000.0f;
    [Export] public float LifetimeInSeconds = 2.0f;
    private float _lifetime = 0.0f;
    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Right.Rotated(Rotation) * Speed * (float)delta;
        _lifetime += (float)delta;
        if (_lifetime >= LifetimeInSeconds)
        {
            QueueFree();
        }
    }
}
