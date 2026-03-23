using Godot;
using System;
using StarlightBT.Nodes;
using StarlightBT.Data;

public partial class Enemy : CharacterBody2D
{
	private BTRoot Root => field ??= GetNode<BTRoot>("%BTRoot");
	private Blackboard _blackboard = new();
	public override void _Ready()
	{
		_blackboard.Set("Self", this);
		_blackboard.Set("Detector", GetNode<Area2D>("%Detector"));
		_blackboard.Set("Sprite", GetNode<Sprite2D>("%Sprite2D"));
		Root.Init(_blackboard);
		Root.Start();
	}
}
