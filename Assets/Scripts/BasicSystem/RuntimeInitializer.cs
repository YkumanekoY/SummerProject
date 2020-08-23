using UnityEngine;

public class RuntimeInitializer
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void InitializeBeforeSceneLoad()
	{
		// sceneControllerを生成、およびシーンの変更時にも破棄されないようにする。
		var manager = new GameObject("SceneManager", typeof(SceneController));
		GameObject.DontDestroyOnLoad(manager);
	}
}