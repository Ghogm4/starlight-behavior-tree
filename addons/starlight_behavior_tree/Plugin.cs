#if TOOLS
using Godot;
using System;

[Tool]
public partial class Plugin : EditorPlugin
{
	// private Texture2D PlaceholderIcon => field ??= ResourceLoader.Load<Texture2D>("res://icon.svg");
	// private static string[] BaseClasses = [
	// 	"BTComposite",
	// 	"BTDecorator",
	// 	"BTLeaf",
	// ];
	// private static string[] Composites = [
	// 	"BTMemorySequence",
	// 	"BTMemorySelector",
	// 	"BTParallel",
	// 	"BTReactiveSequence",
	// 	"BTReactiveSelector",
	// ];
	// private static string[] Decorators = [
	// 	"Cooldown",
	// 	"Delay",
	// 	"Failer",
	// 	"Inverter",
	// 	"Succeeder",
	// 	"Repeat",
	// 	"UntilFail",
	// 	"UntilSucceed",
	// ];
	// private static string[] Leaves = [
	// 	"BTAction",
	// 	"BTCondition",
	// 	"BTPlaceholder",
	// ];
	// private Script GetScript(string relativePath) => GD.Load<Script>("res://addons/starlight_behavior_tree/" + relativePath);
	// private void AddBaseClasses()
	// {
	// 	AddCustomType("BTNode", "Node", GetScript("nodes/base/BTNode.cs"),
	// 		PlaceholderIcon);
	// 	AddCustomType("BTComposite", "BTNode", GetScript($"nodes/composites/BTComposite.cs"),
	// 		PlaceholderIcon);
	// 	AddCustomType("BTDecorator", "BTNode", GetScript($"nodes/decorators/BTDecorator.cs"),
	// 		PlaceholderIcon);
	// 	AddCustomType("BTLeaf", "BTNode", GetScript($"nodes/leaves/BTLeaf.cs"),
	// 		PlaceholderIcon);
	// }
	// private void AddComposites()
	// {
	// 	foreach (var composite in Composites)
	// 	{
	// 		AddCustomType(composite, "BTComposite", GetScript($"nodes/composites/{composite}.cs"),
	// 			PlaceholderIcon);
	// 	}
	// }
	// private void AddDecorators()
	// {
	// 	foreach (var decorator in Decorators)
	// 	{
	// 		AddCustomType(decorator, "BTDecorator", GetScript($"nodes/decorators/{decorator}.cs"),
	// 			PlaceholderIcon);
	// 	}
	// }
	// private void AddLeaves()
	// {
	// 	foreach (var leaf in Leaves)
	// 	{
	// 		AddCustomType(leaf, "BTLeaf", GetScript($"nodes/leaves/{leaf}.cs"),
	// 			PlaceholderIcon);
	// 	}
	// }
	// private void RemoveAll()
	// {
	// 	foreach (var composite in Composites)
	// 	{
	// 		RemoveCustomType(composite);
	// 	}
	// 	foreach (var decorator in Decorators)
	// 	{
	// 		RemoveCustomType(decorator);
	// 	}
	// 	foreach (var leaf in Leaves)
	// 	{
	// 		RemoveCustomType(leaf);
	// 	}
	// 	foreach (var baseClass in BaseClasses)
	// 	{
	// 		RemoveCustomType(baseClass);
	// 	}
	// 	RemoveCustomType("Blackboard");
	// }
	public override void _EnterTree()
	{
		// AddCustomType("Blackboard", "RefCounted", GetScript($"blackboard/Blackboard.cs"),
		// 	PlaceholderIcon);
		// AddBaseClasses();
		// AddComposites();
		// AddDecorators();
		// AddLeaves();
	}

	public override void _ExitTree()
	{
		// RemoveAll();
	}
}
#endif
