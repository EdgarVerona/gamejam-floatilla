using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class KeyedLists<TKey, TValue> : IReadOnlyDictionary<TKey, List<TValue>>
{
	private Dictionary<TKey, List<TValue>> _data = new Dictionary<TKey, List<TValue>>();

	public IEnumerable<TKey> Keys => _data.Keys;

	public IEnumerable<List<TValue>> Values => _data.Values;

	public int Count => _data.Sum(kvp => kvp.Value.Count);

	public List<TValue> this[TKey key] => _data[key];

	public void Add(TKey key, TValue value)
	{
		if (!_data.TryGetValue(key, out List<TValue> valueList))
		{
			valueList = new List<TValue>();
			_data.Add(key, valueList);
		}

		valueList.Add(value);
	}

	public void ForEachAllKeys(Action<TValue> action)
	{
		foreach (var list in _data.Values)
		{
			list.ForEach(action);
		}
	}

	public void ForEach(TKey key, Action<TValue> action)
	{
		if (_data.TryGetValue(key, out List<TValue> valueList))
		{
			valueList.ForEach(action);
		}
	}

	public bool ContainsKey(TKey key)
	{
		return _data.ContainsKey(key);
	}

	public bool TryGetValue(TKey key, out List<TValue> value)
	{
		return _data.TryGetValue(key, out value);
	}

	public IEnumerator<KeyValuePair<TKey, List<TValue>>> GetEnumerator()
	{
		return _data.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return _data.GetEnumerator();
	}
}
