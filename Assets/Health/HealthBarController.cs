using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    HealthBar HealthBarPrefab;

    private Dictionary<Health, HealthBar> _healthBars = new Dictionary<Health, HealthBar>();

	private void Awake()
	{
        Health.OnHealthAdded += AddHealthBar;
        Health.OnHealthRemoved += RemoveHealthBar;
	}

	private void OnDestroy()
	{
        Health.OnHealthAdded -= AddHealthBar;
        Health.OnHealthRemoved -= RemoveHealthBar;
    }

	private void AddHealthBar(Health health)
	{
        if (!_healthBars.ContainsKey(health))
		{
            var newHealthBar = Instantiate(this.HealthBarPrefab, this.transform);
            _healthBars.Add(health, newHealthBar);
            newHealthBar.SetHealth(health);
		}
	}

    private void RemoveHealthBar(Health health)
    {
        if (_healthBars.TryGetValue(health, out HealthBar healthBar))
		{
            if (healthBar != null)
			{
                Destroy(healthBar.gameObject);
            }
            _healthBars.Remove(health);
		}
    }
}
