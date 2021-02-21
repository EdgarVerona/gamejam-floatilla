using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryComponent : MonoBehaviour
{
    [SerializeField]
    GameState EndScreen;

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.GetComponentInParent<Floatilla>() != null)
		{
            this.EndScreen.TriggerGameOver();
        }
	}
}
