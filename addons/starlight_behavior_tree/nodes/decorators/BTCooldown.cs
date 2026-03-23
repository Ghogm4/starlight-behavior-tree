using Godot;
using System;

namespace StarlightBT.Nodes;
[GlobalClass, Icon("res://addons/starlight_behavior_tree/icons/BTCooldown.svg")]
public partial class BTCooldown : BTDecorator
{
    [Export] private float CooldownTimeInSeconds { get; set; } = 1f;
    private ulong CooldownTime => (ulong)(Mathf.Max(0f, CooldownTimeInSeconds) * 1000f);
    private ulong _lastExecutionTime = 0;

    protected sealed override Status OnUpdate()
    {
        if (IsChildMissing) return Status.Failure;

        ulong currentTime = Time.GetTicksMsec();

        if (_lastExecutionTime != 0 && (currentTime - _lastExecutionTime) < CooldownTime)
        {
            return Status.Failure;
        }

        Status result = Child.Tick();

        if (result != Status.Running)
        {
            _lastExecutionTime = currentTime;
        }

        return result;
    }
}