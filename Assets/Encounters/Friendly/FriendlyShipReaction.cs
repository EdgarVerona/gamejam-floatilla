using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyShipReaction : ReactionComponentBase
{
	[SerializeField]
	Ship FriendlyShip;

	public override void React()
	{
		print("Yep, that happened");
	}
}
