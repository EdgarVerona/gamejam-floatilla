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
            if (healthBar.gameObject != null)
			{
                Destroy(healthBar.gameObject);
            }
            _healthBars.Remove(health);
		}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
