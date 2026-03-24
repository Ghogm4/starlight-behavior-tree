# Starlight Behavior Tree 🌟

## Introduction
**Starlight Behavior Tree** is a lightweight behavior tree plugin designed specifically for Godot C#.
It provides a simple, intuitive, and easily extensible framework to help developers quickly build complex AI logic.

## Available Nodes

### Composite Nodes (Composites)
Control the execution flow of child nodes.
- **`BTMemorySelector`**: Memory Selector that remembers the child node that was Running in the last execution, starting directly from that child on the next Tick.
- **`BTMemorySequence`**: Memory Sequence.
- **`BTReactiveSelector`**: Reactive Selector that re-evaluates all child nodes every frame; higher-priority child nodes can interrupt currently running lower-priority ones.
- **`BTReactiveSequence`**: Reactive Sequence.
- **`BTParallel`**: Parallel Node. Executes all child nodes simultaneously.

### Decorator Nodes (Decorators)
Modify the result or behavior of a single child node.
- **`BTRoot`**: Behavior Tree Root Node.
- **`BTCooldown`**: Cooldown Node. Prevents the child node from running for a specified period.
- **`BTDelay`**: Delay Node. Waits for a specified time before running the child node.
- **`BTFail`**: Force Failure. Always returns Failure, regardless of what the child node returns.
- **`BTSucceed`**: Force Success. Always returns Success, regardless of what the child node returns.
- **`BTInverter`**: Inverter Node. Changes Success to Failure, and Failure to Success.
- **`BTRepeat`**: Repeat Node. Repeats the execution of the child node a specified number of times or infinitely.
- **`BTUntilFail`**: Until Fail. Repeats the execution of the child node until it returns Failure.
- **`BTUntilSucceed`**: Until Succeed. Repeats the execution of the child node until it returns Success.

### Leaf Nodes (Leaves)
Nodes that perform actual logic, typically requiring user extension.
- **`BTAction`**: Action Node Base Class. Used to execute specific game logic (e.g., move, attack).
- **`BTCondition`**: Condition Node Base Class. Used to check game state (e.g., is the enemy in range).

### Lifecycle Functions
When inheriting from `BTAction` and `BTCondition` to write custom nodes, you can override the following lifecycle functions to control node behavior:

- **`virtual void OnInit()`**
  - Called when the node is initialized. Used to retrieve Blackboard data or perform one-time setup.

- **`virtual void OnEnter()`**
  - Called when the node's status changes from non-`Running` to `Running`. Typically used to reset temporary variables or start a new logic cycle.

- **`virtual Status OnUpdate()`**
  - The core logic function. Called on every Tick.
  - **Must return** the execution result of the node:
    - `Status.Success`: Execution succeeded.
    - `Status.Failure`: Execution failed.
    - `Status.Running`: Execution is in progress.
  - It is not advised to return `Status.Idle`, as it is reserved to represent an initial state or an aborted state.

- **`virtual void OnExit()`**
  - Called when the node finishes running (whether it returns Success, Failure, or is aborted). Used to clean up logic started in `OnEnter`.

- **`virtual void OnAbort()`**
  - Called when the node is forcibly interrupted (e.g., a higher-priority Reactive node interrupts the current branch). Typically used for special interruption cleanup logic.
  - Note that it is called after `OnExit()` on `Abort()`, so beware of duplicate behavior.

- **`virtual bool Check()`**
  - This function is only for `BTCondition` node. Override this for your specific condition logic.

## Blackboard
`Blackboard` is used for data storage and sharing, and each `BTNode` will hold a `Blackboard` reference.

### Initialization
To pass a `Blackboard` to entire behavior tree, you can do something like this:
```csharp
var root = GetNode<BTRoot>("%BTRoot");
Blackboard blackboard = new Blackboard();
root.Init(blackboard);
```

### Write & Read
Here is an example to write data to and read data from `Blackboard`.
```csharp
Blackboard.Set<Player>("Player", this);
Blackboard.Set<int>("Health", 100);
if (Blackboard.TryGet<Player>("Self", out Player self))
{
}
int health = Blackboard.Get<int>("Health");
```

### Hierarchy
`Blackboard` supports hierarchical structure (`ParentBlackboard`), which is very useful if you want different scope of data sharing.

By default, `Set` writes to current blackboard. However, it has an optional `parentLevel` parameter which allows you to climb up the hierarchy to store the data in specific `Blackboard`.
```csharp
// This stores GlobalTarget in Blackboard.ParentBlackboard
Blackboard.Set("GlobalTarget", targetNode, parentLevel: 1);
```

If `Get` and `TryGet` can't find the data, it goes up recursively to find it until it is found or the top level is reached.

### Other Functions
```csharp
// Check if a Key exists (default includeParent = true, will look upwards)
if (Blackboard.Contains("Target")) 
{
    // ...
}

// Look for a Key only in the current Blackboard
if (Blackboard.Contains("Target", includeParent: false))
{
    // ...
}

// Remove a specific Key from the current Blackboard
Blackboard.Remove("TempData");

// Clear all data from the current Blackboard
Blackboard.Clear();
```

## Quick Setup
To build a behavior tree. You must add a `BTRoot` to the current scene, from which you can start adding functional nodes. An example scene setup would look like:

- `Enemy (CharacterBody2D)`
  - `BTRoot`
    - `BTReactiveSequence`
      - `Condition_IsPlayerNearby`
      - `Action_ChasePlayer`

Then, to really make the root start ticking, you can do this:
```csharp
var root = GetNode<BTRoot>("%BTRoot");
Blackboard blackboard = new Blackboard();
root.Init(blackboard);
root.Start();

// If you want to stop it, call root.Stop()
// root.Stop();
```
