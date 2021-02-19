using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class KeyedLists<TKey, TValue>
{
	private Dictionary<TKey, List<TValue>> _data = new Dictionary<TKey, List<TValue>>();

	public void Add(TKey key, TValue value)
	{
		if (!_data.TryGetValue(key, out List<TValue> valueList))
		{
			valueList = new List<TValue>();
			_data.Add(key, valueList);
		}

		valueList.Add(value);
	}

	public void ForEach(TKey key, Action<TValue> action)
	{
		if (_data.TryGetValue(key, out List<TValue> valueList))
		{
			valueList.ForEach(action);
		}
	}
}
