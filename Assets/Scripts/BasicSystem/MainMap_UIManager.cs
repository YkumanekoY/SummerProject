using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMap_UIManager : MonoBehaviour
{
	[SerializeField]
	private Text timeLabel;

	private GameManager gameManager;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Update()
	{
		UpdateTimerLabel();
	}

	// Update is called once per frame
	void UpdateTimerLabel()
	{
		// 少数以下表示させる
		int minutes = Mathf.FloorToInt(gameManager.GetTimer() / 60F);
		int seconds = Mathf.FloorToInt(gameManager.GetTimer() - minutes * 60);
		int mseconds = Mathf.FloorToInt((gameManager.GetTimer() - minutes * 60 - seconds) * 100);
		string niceTime = string.Format("{0:0}:{1:00}.{2:00}", minutes, seconds, mseconds);

		timeLabel.text = niceTime;
	}
}
