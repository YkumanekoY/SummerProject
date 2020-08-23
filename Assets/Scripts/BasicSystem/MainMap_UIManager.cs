using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMap_UIManager : MonoBehaviour
{
	[SerializeField]
	private Text
		timeLabel, countDownLabel;


	private GameManager gameManager;

	// Start is called before the first frame update
	void Start()
	{
		countDownLabel.text = "3";
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Update()
	{
		UpdateTimerLabel();
	}

	public void SetCountDownLabel(string countDownText) { countDownLabel.text = countDownText; }

	// Update is called once per frame
	void UpdateTimerLabel()
	{
		if (gameManager.GetCurrentGameState() == GameManager.GameState.Playing)
		{
			timeLabel.gameObject.SetActive(true);
			countDownLabel.gameObject.SetActive(false);
		}
		// 少数以下表示させる
		int minutes = Mathf.FloorToInt(gameManager.GetTimer() / 60F);
		int seconds = Mathf.FloorToInt(gameManager.GetTimer() - minutes * 60);
		int mseconds = Mathf.FloorToInt((gameManager.GetTimer() - minutes * 60 - seconds) * 100);
		string niceTime = string.Format("{0:0}:{1:00}.{2:00}", minutes, seconds, mseconds);

		timeLabel.text = niceTime;
	}
}
