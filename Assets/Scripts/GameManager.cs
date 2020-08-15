using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance; // 唯一無二の存在

	// State管理一覧
	public enum GameState
	{
		Start,
		Prepare,
		Playing,
		TimeUp,
		EmemyWin,
		EnemyLose,
		Result
	}

	// 現在のState
	[SerializeField] private GameState currentGameState;

	// プレイヤーの操作が可能か
	public bool isPlayerControl;



	//最初はスタートに
	void Awake()
	{
		Instance = this;
		SetCurrentState(GameState.Start);
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
				TimeUpAction();
				break;
			case GameState.EmemyWin:
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
	}

	// Prepareになったときの処理
	IEnumerator PrepareCoroutine()
	{
		//カウントダウンの表示
		Debug.Log("3");
		yield return new WaitForSeconds(1);
		Debug.Log("2");
		yield return new WaitForSeconds(1);
		Debug.Log("1");
		yield return new WaitForSeconds(1);
		Debug.Log("Start!");
		SetCurrentState(GameState.Playing);
	}

	// Playingになったときの処理
	void PlayingAction()
	{
		isPlayerControl = true; //プレイヤーの操作を可能にする
		Debug.Log("Playing");
	}

	private void TimeUpAction()
	{
	}

	private void EmemyWinAction()
	{
	}

	private void EnemyLoseAction()
	{
	}

	// Resultになったときの処理
	void ResultAction()
	{
		isPlayerControl = false; //プレイヤーの操作を不可能にする
	}

	GameState previousGamseState; //デバッグ用
	void Update()
	{
		if (currentGameState != previousGamseState) OnGameStateChanged(currentGameState); // デバッグ用 インスペクターで変更しても対応するように
		previousGamseState = currentGameState;


	}
}
