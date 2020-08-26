using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
	public bool isSealedCharmContain = false;
	public bool isRevivalCharmContain = false;

	public void SearchItem(GameObject Player)
	{
		if (isSealedCharmContain)
		{
			// Player.GetItem(0)
			isSealedCharmContain = false;
			Player.GetComponent
			Debug.Log("封印のお札だ");
		}
		else if (isRevivalCharmContain)
		{
			//Player.GetItem(1) 
			isRevivalCharmContain = false;
			Debug.Log("復活のお札だ");
		}
		else
		{
			Debug.Log("何も入っていなかった");
		}
	}
}
