using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class SimplePun : MonoBehaviourPunCallbacks
{
    int oniNumber;
	// Use this for initialization
	void Start()
	{
		//旧バージョンでは引数必須でしたが、PUN2では不要です。
		PhotonNetwork.ConnectUsingSettings();
        oniNumber = 1;
        //oniNumber = Random.Range(1, 4);
        Debug.Log(oniNumber);

    }

    void OnGUI()
	{
		//ログインの状態を画面上に出力
		GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
	}

	//ルームに入室前に呼び出される
	public override void OnConnectedToMaster()
	{
		// "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
		PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
	}

	//ルームに入室後に呼び出される
	public override void OnJoinedRoom()
	{
        //if (PhotonNetwork.LocalPlayer.ActorNumber == oniNumber)
        //{
        //    GameObject ghost = PhotonNetwork.Instantiate("Prefabs/Player/Ghost", Vector3.zero, Quaternion.identity, 0);
        //    EnemyMovingScript enemyMovingScript = ghost.GetComponent<EnemyMovingScript>();
        //    enemyMovingScript.enabled = true;
        //}
        //else
        //{
        //    GameObject child = PhotonNetwork.Instantiate("Prefabs/Player/Child", Vector3.zero, Quaternion.identity, 0);
        //    ChildMovingScript childMovingScript = child.GetComponent<ChildMovingScript>();
        //    childMovingScript.enabled = true;
        //}
        ////GameObject gameManager = PhotonNetwork.Instantiate("GameManager", Vector3.zero, Quaternion.identity, 0);
        ////GameObject yashiro = PhotonNetwork.Instantiate("Prefabs/Map/yashiro/yashiro", Vector3.zero, Quaternion.identity, 0);
    }

}
