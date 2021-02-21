using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryComponent : MonoBehaviour
{
    [SerializeField]
    GameState EndScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.GetComponentInParent<Floatilla>() != null)
		{
            this.EndScreen.TriggerGameOver();
        }
	}
}
