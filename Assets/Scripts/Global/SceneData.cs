using System.Collections.Generic;

public class SceneData
{
    private Dictionary<string, object> _data = new Dictionary<string, object>();

    public void Add(string key, object value)
    {
        _data[key] = value;
    }

    public T Get<T>(string key)
    {
        if (_data.ContainsKey(key))
        {
            return (T)_data[key];
        }

        return default(T);
    }

    public T GetAndRemove<T>(string key)
    {
        var data = Get<T>(key);
        Remove(key);

        return data;
    }

    public void Remove(string key)
    {
        _data.Remove(key);
    }
}
