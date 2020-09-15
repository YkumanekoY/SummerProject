using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance; //  GameManagerは唯一無二の存在
										// State管理一覧
	public enum GameState
	{
		Start,
		Prepare,
		Playing,
		TimeUp,
		EnemyWin,
		EnemyLose,
		Result
	}

	// 現在のState
	[SerializeField] private GameState currentGameState;
	public GameState GetCurrentGameState() { return currentGameState; }

	[SerializeField] private MainMap_UIManager uIManager;
	private int sealedCharmCount, revivalCharmCount;
	private bool isJailSellOpen = false; //牢屋が閉じているかどうか

	// タイマー関係
	[SerializeField] private float timer;
	public float GetTimer() { return timer; }
	private const float timeLimit = 180;

	// プレイヤーの操作が可能か
	public bool isPlayerControl;

	//子供が全員捕まっているか
	public bool[] isPlayerKidnappedArray;

	//子供を格納する配列
	public GameObject[] childrenObjects;

	// 建物関連
	[SerializeField] private StageManager stageManager;

	// シーン関連
	[SerializeField] private SceneController sceneController;

	// 最初はスタートに
	void Awake()
	{
		timer = timeLimit; // 時間制限を設定する
		Instance = this;
		SetCurrentState(GameState.Start);
		childrenObjects = GameObject.FindGameObjectsWithTag("Child");
		isPlayerKidnappedArray = new bool [childrenObjects.Length];
	}

	void Start()
	{
		sceneController = GameObject.Find("SceneManager").GetComponent<SceneController>();
	}

	//　ほかのスクリプトから更新できる
	private void SetCurrentState(GameState gameState)
	{
		currentGameState = gameState;
		OnGameStateChanged(currentGameState);
	}


	// ステートが更新されたときに
	void OnGameStateChanged(GameState state)
	{
		previousGamseState = currentGameState;
		switch (state)
		{
			case GameState.Start:
				StartAction();
				break;
			case GameState.Prepare:
				StartCoroutine(PrepareCoroutine());
				break;
			case GameState.Playing:
				PlayingAction();
				break;
			case GameState.TimeUp:
				StartCoroutine(TimeUpCoroutine());
				break;
			case GameState.EnemyWin:
				EmemyWinAction();
				break;
			case GameState.EnemyLose:
				EnemyLoseAction();
				break;
			case GameState.Result:
				ResultAction();
				break;
			default:
				break;
		}
	}

	// Startになったときの処理
	void StartAction()
	{
		SetCurrentState(GameState.Prepare);
	}

	// Prepareになったときの処理
	IEnumerator PrepareCoroutine()
	{
		//カウントダウンの表示
		uIManager.SetCountDownLabel("3");
		yield return new WaitForSeconds(1);
		uIManager.SetCountDownLabel("2");
		yield return new WaitForSeconds(1);
		uIManager.SetCountDownLabel("1");
		yield return new WaitForSeconds(1);
		uIManager.SetCountDownLabel("Start!");
		yield return new WaitForSeconds(1f);
		SetCurrentState(GameState.Playing);
	}

	// Playingになったときの処理
	void PlayingAction()
	{
		isPlayerControl = true; //プレイヤーの操作を可能にする
		Debug.Log("Playing");
	}

	// SealedCharmが増えた時の処理
	public void IncreaseSealedCharm()
	{
		sealedCharmCount++;
		if (sealedCharmCount >= 4) SetCurrentState(GameManager.GameState.EnemyLose);
	}

	// RevivalCharmが増えた時の処理
	public void IncreaseRevivalCharm()
	{
		revivalCharmCount++;
		if (revivalCharmCount >= 1) OpenJailCell();
	}

	public void JudgingKidnappedChild(int childnumber)
    {
		isPlayerKidnappedArray[childnumber] = true;
    }

	//復活のお札が集まった時の処理
	public void OpenJailCell()
	{
		revivalCharmCount = 0;
		isPlayerKidnappedArray = new bool[childrenObjects.Length];
		stageManager.OpenjailCell();
	}

	IEnumerator TimeUpCoroutine()
	{
		timer = 0;
		yield return new WaitForSeconds(1);
		SetCurrentState(GameState.EnemyWin);
	}

	private void EmemyWinAction()
	{
		uIManager.SetResultPanel("鬼");
		SetCurrentState(GameState.Result);
	}

	private void EnemyLoseAction()
	{
		uIManager.SetResultPanel("子供");
		SetCurrentState(GameState.Result);
	}

	// Resultになったときの処理
	void ResultAction()
	{
		isPlayerControl = false; // プレイヤーの操作を不可能にする
	}

	GameState previousGamseState; // デバッグ用
	void Update()
	{
		if (currentGameState != previousGamseState) OnGameStateChanged(currentGameState); // デバッグ用 インスペクターで変更しても対応するように

		previousGamseState = currentGameState;

		if (timer >= 0 && currentGameState == GameState.Playing) timer -= Time.deltaTime; // ゲーム中ならば制限時間を減らす
		if (timer <= 0 && currentGameState == GameState.Playing) SetCurrentState(GameState.TimeUp); // 時間制限きたら, GameState.TimeUpに切り替える
	}
}
