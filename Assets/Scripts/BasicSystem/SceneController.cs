using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	private static SceneController sceneController;

	public enum SceneName
	{
		Title,
		Home,
		Matching,
		MainMapScene
	}

	//現在のシーン
	[SerializeField] private SceneName currentScene;


	void Awake()
	{
		if (!sceneController) sceneController = this;
		else Destroy(this.gameObject);
		currentScene = (currentScene == null) ? SceneName.Title : currentScene;
	}

	public void ChangeScene(SceneName sceneName)
	{
		currentScene = sceneName;
		switch (sceneName)
		{
			case SceneName.Title:
				SceneManager.LoadScene("Title");
				break;
			case SceneName.Home:
				SceneManager.LoadScene("Home");
				break;
			case SceneName.Matching:
				SceneManager.LoadScene("Matching");
				break;
			case SceneName.MainMapScene:
				SceneManager.LoadScene("MainMapScene");
				break;
		}
	}

	void Update()
	{

	}
}
