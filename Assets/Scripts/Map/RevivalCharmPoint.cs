using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivalCharmPoint : MonoBehaviour
{
	private GameManager gameManager;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void SetRevivalCharm()
	{
		gameManager.IncreaseRevivalCharm();
	}
}
