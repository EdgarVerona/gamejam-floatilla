using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField]
	float UpdateSpeedInSeconds = 0.2f;

	[SerializeField]
	float PositionOffset = 2.0f;

	[SerializeField]
	Image ForegroundImage;

	private Health _health;
	private Camera _mainCamera;

	private void Start()
	{
		_mainCamera = Camera.main;
	}

	internal void SetHealth(Health health)
	{
		_health = health;
		_health.OnHealthPercentChanged += HandleHealthChanged;
	}

	private void HandleHealthChanged(float percent)
	{
		StartCoroutine(ChangeToPercent(percent));
	}

	private IEnumerator ChangeToPercent(float percent)
	{
		float preChangePercent = this.ForegroundImage.fillAmount;
		float elapsed = 0.0f;

		while (elapsed < this.UpdateSpeedInSeconds)
		{
			elapsed += Time.deltaTime;
			this.ForegroundImage.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsed / this.UpdateSpeedInSeconds);
			yield return null;
		}

		this.ForegroundImage.fillAmount = percent;
	}

	private void LateUpdate()
	{
		// Screen point for the person's health bar, and a configured offset above it.
		// Note that _health is attached to the object in the game that has health.
		this.transform.position = _mainCamera.WorldToScreenPoint(_health.transform.position + Vector3.up * this.PositionOffset);
	}
}
