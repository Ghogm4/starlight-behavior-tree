using Godot;
using System;
[GlobalClass]
public partial class BTNode : Node
{
    /// <summary>
    /// Represents the possible statuses of a behavior tree node after being ticked. The statuses include:
    /// <para>
    /// - Success: The node has completed its task successfully.
    /// </para>
    /// <para>
    /// - Failure: The node has failed to complete its task.
    /// </para>
    /// <para>
    /// - Running: The node is still in the process of executing its task and has not yet completed.
    /// </para>
    /// <para>
    /// - Idle: The node is not currently active or executing any task.
    /// </para>
    /// </summary>
    public enum Status
    {
        Success,
        Failure,
        Running,
        Idle
    }

    /// <summary>
    /// The current status of the behavior tree node. This field is updated each time the node is ticked and reflects the result of the most recent execution of the node's behavior.
    /// </summary>
    public Status CurrentStatus { get; private set; } = Status.Idle;
    /// <summary>
    /// A reference to the blackboard associated with this behavior tree node. The blackboard is a shared data structure that allows nodes to store and access information during the execution of the behavior tree.
    /// It can be used to store variables, state information, or any other relevant data that nodes may need to access or modify during their execution.
    /// </summary>
    protected Blackboard Blackboard = null;

    /// <summary>
    /// A flag to track whether the OnUpdate() method has been implemented by derived classes. This is used to provide a warning if a node does not override the OnUpdate() method.
    /// </summary>
    private bool _isOnUpdateImplemented = false;

    /// <summary>
    /// A flag to track whether the node has been initialized. This can be used to ensure that initialization logic is only executed once.
    /// </summary>
    private bool _isInitialized = false;

    /// <summary>
    /// Initializes the behavior tree node with the given blackboard.
    /// </summary>
    /// <param name="blackboard"></param>
    public void Init(Blackboard blackboard)
    {
        if (_isInitialized)
        {
            GD.PushWarning($"{Name} has already been initialized.");
            return;
        }
        OnInternalInit();
        Blackboard = blackboard;
        InitBlackboardForChildren(Blackboard);
        OnInit();
        _isInitialized = true;
    }

    /// <summary>
    /// Ticks the behavior tree node and returns its status.
    /// </summary>
    /// <returns></returns>
    public Status Tick()
    {
        if (CurrentStatus != Status.Running) OnEnter();

        CurrentStatus = OnUpdate();

        if (CurrentStatus != Status.Running) OnExit();

        return CurrentStatus;
    }

    /// <summary>
    /// Aborts the execution of the behavior tree node. This method can be called to stop the node's execution and perform any necessary cleanup.
    /// Derived classes can override OnAbort() to implement specific abort logic and OnExit() to implement cleanup logic when it is exited.
    /// <para>
    /// Note that OnAbort() and OnExit() are both called during an abort, with OnAbort() being called first. 
    /// This allows for flexibility in defining abort behavior and cleanup logic, as some actions may need to be performed specifically during an abort, while others may be appropriate for both normal exits and aborts.
    /// </para>
    /// </summary>
    public void Abort()
    {
        if (CurrentStatus != Status.Running) return;

        PassAbort();
        OnAbort();
        OnExit();
        CurrentStatus = Status.Idle;
    }

    /// <summary>
    /// Called during node initialization to perform children setup. This method is used internally and should not be overridden by external code.
    /// </summary>
    protected virtual void OnInternalInit() { }
    
    /// <summary>
    /// Called to pass down the blackboard to child nodes during initialization. This method is used internally and should not be overridden by external code.
    /// </summary>
    /// <param name="blackboard"></param>
    protected virtual void InitBlackboardForChildren(Blackboard blackboard) { }

    /// <summary>
    /// Called during node initialization after the internal initialization is complete.
    /// This method can be overridden to implement additional initialization logic that should run after the internal initialization.
    /// </summary>
    protected virtual void OnInit() { }

    /// <summary>
    /// Called when the node is entered (from non-Running to Running). This method can be overridden to implement specific logic that should run when the node is entered.
    /// </summary>
    protected virtual void OnEnter() { }

    /// <summary>
    /// Called when the node is ticked. This method can be overridden to implement specific logic that should run when the node is ticked.
    /// </summary>
    /// <returns></returns>
    protected virtual Status OnUpdate()
    {
        if (!_isOnUpdateImplemented)
        {
            GD.PushWarning($"{Name} does not override OnUpdate(), which means it will not perform any actions when ticked. Please override OnUpdate() to define the node's behavior.");
            _isOnUpdateImplemented = true;
        }
        return Status.Failure;
    }

    /// <summary>
    /// Called when the node is exited (from Running to non-Running). This method can be overridden to implement specific logic that should run when the node is exited.
    /// </summary>
    protected virtual void OnExit() { }

    /// <summary>
    /// Called during an abort to propagate the abort signal to child nodes if necessary. This method is used internally and should not be overridden by external code.
    /// </summary>
    protected virtual void PassAbort() { }

    /// <summary>
    /// Called when the node is aborted to perform any necessary cleanup or state reset. This method should only be overridden if you need to
    /// implement specific logic that should run when the node is aborted but not exited.
    /// If the cleanup logic should also run when the node is exited normally, it may be more appropriate to put it in the OnExit() method instead.
    /// </summary>
    protected virtual void OnAbort() { }
}
