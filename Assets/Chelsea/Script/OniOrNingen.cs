using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OniOrNingen : MonoBehaviourPunCallbacks
{
    int oniNumber;
    public Text Onitext;
    Vector2 v = new Vector2(-2f + PhotonNetwork.LocalPlayer.ActorNumber,0);

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        oniNumber = Random.Range(1,5);
        Debug.Log(oniNumber);
        //PhotonNetwork.IsMessageQueueRunning = true;
        // シーンの読み込みコールバックを登録.
        SceneManager.sceneLoaded += OnLoadedScene;

        if (PhotonNetwork.LocalPlayer.ActorNumber == oniNumber)
        {
            //GameObject ghost = PhotonNetwork.Instantiate("Prefabs/Player/Ghost", v, Quaternion.identity, 0);
            
            Onitext.text = "あなたは鬼です";
        }
        else
        {
            //GameObject child = PhotonNetwork.Instantiate("Prefabs/Player/Child", v, Quaternion.identity, 0);
            
            Onitext.text = "あなたは逃げです";
        }
        StartCoroutine("ChangeToGame");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ChangeToGame()
    {
        //3秒停止
        PhotonNetwork.IsMessageQueueRunning = false;
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("MainMapScene");
    }
    private void OnLoadedScene(Scene i_scene, LoadSceneMode i_mode)
    {
        PhotonNetwork.IsMessageQueueRunning = true;

        // シーンの遷移が完了したら自分用のオブジェクトを生成.
        if (i_scene.name == "MainMapScene")
        {
            //Vector3 pos = Random.insideUnitCircle * m_randomCircle;
            if (PhotonNetwork.LocalPlayer.ActorNumber == oniNumber)
            {
                GameObject stagemanager = PhotonNetwork.Instantiate("Prefabs/StageManager", Vector3.zero, Quaternion.identity, 0);
                GameObject ghost = PhotonNetwork.Instantiate("Prefabs/Player/Ghost", Vector3.zero, Quaternion.identity, 0);
                EnemyMovingScript enemyMovingScript = ghost.GetComponent<EnemyMovingScript>();
                enemyMovingScript.enabled = true;
            }
            else
            {
                GameObject child = PhotonNetwork.Instantiate("Prefabs/Player/Child", Vector3.zero, Quaternion.identity, 0);
                ChildMovingScript childMovingScript = child.GetComponent<ChildMovingScript>();
                childMovingScript.enabled = true;
            }
        }
    }
}
