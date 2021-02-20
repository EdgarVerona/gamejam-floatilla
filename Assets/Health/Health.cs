using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Health : MonoBehaviour
{
	public static event Action<Health> OnHealthAdded = delegate { };
	public static event Action<Health> OnHealthRemoved = delegate { };

	[SerializeField]
	float MaximumHitpoints = 5.0f;

	public float CurrentHitpoints { get; private set; }

	public event Action<float> OnHealthPercentChanged = delegate { };

	public void Damage(DamageComponent incomingDamage)
	{
		this.CurrentHitpoints -= incomingDamage.Damage;

		if (this.CurrentHitpoints <= 0)
		{
			this.CurrentHitpoints = 0;
		}

		OnHealthPercentChanged(this.CurrentHitpoints / this.MaximumHitpoints);
	}

	private void Start()
	{
		this.CurrentHitpoints = this.MaximumHitpoints;
		Health.OnHealthAdded(this);
	}

	private void OnDisable()
	{
		Health.OnHealthRemoved(this);
	}
}
