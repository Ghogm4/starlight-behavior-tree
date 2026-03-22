using Godot;
using System;
using System.Collections.Generic;
public partial class Blackboard : RefCounted
{
    public Blackboard ParentBlackboard { get; set; }
    private Dictionary<string, object> _data = new();
    public void Set<T>(string key, T value, int parentLevel = 0)
    {
        Blackboard current = this;
        for (int i = 0; i < parentLevel; i++)
        {
            current = current.ParentBlackboard;
        }
        if (current == null)
        {
            GD.PushError($"Cannot set key '{key}' at parent level {parentLevel} because it exceeds the hierarchy.");
            return;
        }
        current._data[key] = value;
    }
    public T Get<T>(string key)
    {
        if (TryGet<T>(key, out var value))
        {
            return value;
        }
        GD.PushError($"Key '{key}' not found in blackboard.");
        return default;
    }

    public bool TryGet<T>(string key, out T value)
    {
        if (_data.TryGetValue(key, out var rawValue) && rawValue is T typedValue)
        {
            value = typedValue;
            return true;
        }

        if (ParentBlackboard != null)
        {
            return ParentBlackboard.TryGet(key, out value);
        }

        value = default;
        return false;
    }

    public bool Contains(string key, bool includeParent = true)
    {
        if (_data.ContainsKey(key)) return true;
        if (!includeParent || ParentBlackboard == null) return false;
        return ParentBlackboard.Contains(key, true);
    }
    public void Remove(string key) => _data.Remove(key);
    public void Clear() => _data.Clear();
}
