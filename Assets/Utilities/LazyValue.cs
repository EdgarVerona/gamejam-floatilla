using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LazyValue<T>
{
	private T _currentValue = default(T);

	private Func<T, T> _valueChooserFunction;

	private float _timeToRefreshInSeconds = 1.0f;
	private float _timeLastRefreshed = 0.0f;

	public LazyValue(float timeToRefreshInSeconds, Func<T, T> valueChooserFunction)
	{
		_timeToRefreshInSeconds = timeToRefreshInSeconds;
		_valueChooserFunction = valueChooserFunction;

		GetValue();
	}

	public T GetValue()
	{
		if ((_timeLastRefreshed + _timeToRefreshInSeconds) <= Time.time)
		{
			_currentValue = _valueChooserFunction(_currentValue);

			_timeLastRefreshed = Time.time;
		}

		return _currentValue;
	}
}