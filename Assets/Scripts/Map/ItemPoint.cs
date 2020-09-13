using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
	public bool isSealedCharmContain = false;
	public bool isRevivalCharmContain = false;

	ItemPrefabManager itemPrefabManager;

	private void Start()
	{
		itemPrefabManager = GameObject.Find("GameManager").GetComponent<ItemPrefabManager>();
	}

	public void SearchItem(GameObject Player)
	{
		if (isSealedCharmContain)
		{
			isSealedCharmContain = false;
			Player.GetComponent<ChildMovingScript>().GetItem(itemPrefabManager.itemPrefabs[0]);
			Debug.Log("封印のお札だ");
		}
		else if (isRevivalCharmContain)
		{
			isRevivalCharmContain = false;
			Player.GetComponent<ChildMovingScript>().GetItem(itemPrefabManager.itemPrefabs[1]);
			Debug.Log("復活のお札だ");
		}
		else
		{
			Debug.Log("何も入っていなかった");
		}
	}

	public bool isItemPut()
	{
		if(isSealedCharmContain == true || isRevivalCharmContain == true) return false;
		else return true;
	}

	public void HidingItem(string itemTag)
	{
		if (itemTag == "SealedCharm") isSealedCharmContain = true;
		else if (itemTag == "RevivalCharm") isRevivalCharmContain = true;
	}
}
