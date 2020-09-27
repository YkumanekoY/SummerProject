using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OniOrNingen : MonoBehaviourPunCallbacks
{
    int oniNumber=1;
    public Text Onitext;
    Vector2 v = new Vector2(-2f + PhotonNetwork.LocalPlayer.ActorNumber,0);

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.IsMessageQueueRunning = true;

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

    IEnumerator ChangeToGame()
    {
        //3秒停止
        PhotonNetwork.IsMessageQueueRunning = false;
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("MainMapScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
