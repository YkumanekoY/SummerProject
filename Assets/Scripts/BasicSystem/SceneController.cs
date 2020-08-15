using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	private static SceneController sceneController;
	void Awake()
	{
		if (!sceneController)
		{
			DontDestroyOnLoad(this.gameObject);
			sceneController = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private void Start()
	{

	}

	public void ChangeScene(string sceneName)
	{
		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
			if (SceneManager.GetSceneByBuildIndex(i).name == sceneName)
			{
				SceneManager.LoadScene(sceneName);
				return;
			}
	}

	// --------------------以下デバッグ用--------------------
	public void DebugChangeScene(int sceneNum)
	{
		SceneManager.LoadScene(sceneNum);
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Q)) { DebugChangeScene(0); }
		if (Input.GetKey(KeyCode.W)) { DebugChangeScene(1); }
		if (Input.GetKey(KeyCode.E)) { DebugChangeScene(2); }
		if (Input.GetKey(KeyCode.R)) { DebugChangeScene(3); }
		if (Input.GetKey(KeyCode.T)) { DebugChangeScene(4); }
		// if (Input.GetKey(KeyCode.Y)) { DebugChangeScene(5); }
		// if (Input.GetKey(KeyCode.U)) { DebugChangeScene(6); }
		// if (Input.GetKey(KeyCode.I)) { DebugChangeScene(7); }
		// if (Input.GetKey(KeyCode.O)) { DebugChangeScene(8); }
	}
}
