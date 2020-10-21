using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    //public GameObject score_object = null; // Textオブジェクト
    public GameObject roomCreatePanel;
    public GameObject ToOnline;
    public GameObject inRoomPanel;
    public InputField roomName;
    public Text message;
    bool inRoom = false;

    string mode;                 // モード(ONLINE, OFFLINE)
    string dispStatus;           // 画面項目：状態
    string dispMessage;          // 画面項目：メッセージ
    string dispRoomName;         // 画面項目：ルーム名
    List<string> info = new List<string>();
    //string[] info = new string[100];

    List<RoomInfo> roomDispList; // 画面項目：ルーム一覧

    //private float step_time;
    //int ActorNumber;

    int[] PlayerID = new int[4];
    int count = 1;

    // ルームオプションの基本設定
    RoomOptions roomOptions = new RoomOptions
    {
        // 部屋の最大人数
        MaxPlayers = 4,

        // 公開
        IsVisible = true,

        // 入室可
        IsOpen = true


    };

    // 状態
    public enum Status
    {
        ONLINE,   // オンライン
        OFFLINE,  // オフライン
    };

    private void Start()
    {
        initParam();
        roomCreatePanel.SetActive(false);
        inRoomPanel.SetActive(false);
        info.Clear();
    }



    // 変数初期化処理
    private void initParam()
    {
        dispRoomName = "";
        dispMessage = "";
        dispStatus = Status.OFFLINE.ToString();
        roomDispList = new List<RoomInfo>();
    }

    // Photonサーバ接続処理
    public void ConnectPhoton(bool boolOffline)
    {
        if (boolOffline)
        {
            // オフラインモードを設定
            mode = Status.OFFLINE.ToString();
            PhotonNetwork.OfflineMode = true; // OnConnectedToMaster()が呼ばれる
            dispMessage = "OFFLINEモードで起動しました。";
            return;
        }
        // Photonサーバに接続する
        mode = Status.ONLINE.ToString();
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    // Photonサーバ切断処理
    public void DisConnectPhoton()
    {
        PhotonNetwork.Disconnect();
        // 変数初期化
        initParam();
        roomCreatePanel.SetActive(false);
        ToOnline.SetActive(true);
        message.text = "切断しました";
    }

    // コールバック：Photonサーバ接続完了
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        if (Status.ONLINE.ToString().Equals(mode))
        {
            dispStatus = Status.ONLINE.ToString();
            dispMessage = "サーバに接続しました。";
            // ロビーに接続
            PhotonNetwork.JoinLobby();
        }
    }

    // コールバック：Photonサーバ接続失敗
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        dispMessage = "サーバから切断しました。";
        dispStatus = Status.OFFLINE.ToString();
    }

    // コールバック：ロビー入室完了
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    // ルーム一覧更新処理
    // (ロビーに入室した時、他のプレイヤーが更新した時のみ)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        // ルーム一覧更新
        foreach (var info in roomList)
        {
            if (!info.RemovedFromList)
            {
                // 更新データが削除でない場合
                roomDispList.Add(info);
            }
            else
            {
                // 更新データが削除の場合
                roomDispList.Remove(info);
            }
        }
    }

    // ルーム作成処理
    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        PlayerID[0] = PhotonNetwork.LocalPlayer.ActorNumber;
    }

    // ルーム入室処理
    public void ConnectToRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
        roomCreatePanel.SetActive(false);
        inRoom = true;
    }

    // コールバック：ルーム作成完了
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        dispMessage = "ルームを作成しました。";
        message.text = "現在人数：" + "" + PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "";
    }

    // コールバック：ルーム作成失敗
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        dispMessage = "ルーム作成に失敗しました。";
    }

    // コールバック：ルームに入室した時
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        // 表示ルームリストに追加する
        roomDispList.Add(PhotonNetwork.CurrentRoom);
        dispMessage = "【" + PhotonNetwork.CurrentRoom.Name + "】" + "に入室しました。";
        info.Add(dispMessage);
        inRoom = true;
        inRoomPanel.SetActive(true);
        message.text = "現在人数：" + "" + PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "";

        /*
        Vector2 v2 = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        GameObject monster2 = PhotonNetwork.Instantiate("Player", v2, Quaternion.identity, 0);
        */

        //Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            StartCoroutine("ChangeToMatch");
            Player[] allPlayers = PhotonNetwork.PlayerList; // プレイヤーの配列（自身を含む）
        }
    }
    //他プレイヤーが入ってきた時
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        dispMessage = "【" + newPlayer + "】" + "が入室しました。";
        info.Add(dispMessage);
        message.text = "現在人数：" + "" + PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "";
        /*PlayerID[count] = PhotonNetwork.LocalPlayer.ActorNumber;
        count++;*/


        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            /*for(int i = 0; i < PlayerID.Length-1; i++)
            {
                if (PlayerID[i] < PlayerID[i + 1])
                {
                    int t = PlayerID[i+1];
                    PlayerID[i + 1] = PlayerID[i];
                    PlayerID[i] = t;
                }
            }
            Debug.Log(PlayerID[0]);
            Debug.Log(PlayerID[1]);
            Debug.Log(PlayerID[2]);
            Debug.Log(PlayerID[3]);*/

            PhotonNetwork.CurrentRoom.IsOpen = false;
            StartCoroutine("ChangeToMatch");
        }
        Debug.Log("OnPlayerEnteredRoom");
        Debug.Log(newPlayer.ActorNumber);
    }

    //他プレイヤーが退出した時
    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        dispMessage = "【" + newPlayer + "】" + "が退出しました。";
        message.text = "現在人数：" + "" + PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "";
        info.Add(dispMessage);

        Debug.Log(newPlayer.ActorNumber);
    }

    IEnumerator ChangeToMatch()
    {
        //3秒停止
        PhotonNetwork.IsMessageQueueRunning = false;
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Matching");
    }

    void Update()
    {
        //Text score_text = score_object.GetComponent<Text>();
        // テキストの表示を入れ替える

        //score_text.text = PhotonNetwork.LocalPlayer.ActorNumber.ToString();
    }

    //オンライン化する
    public void GameToMatch()
    {
        ConnectPhoton(false);
        
        if (Status.OFFLINE.ToString().Equals(dispStatus))
        {
            Debug.Log("オンライン");
            roomCreatePanel.SetActive(true);
            ToOnline.SetActive(false);
            message.text = "ルームを作成or参加して\nゲームを開始";
        }
    }

    //ルーム作成
    public void RoomCreate()
    {
        CreateRoom(roomName.text);
        roomCreatePanel.SetActive(false);
        inRoomPanel.SetActive(true);
        //Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        //message.text = "現在人数：" + "【" + PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "】";
        //↑PhotonNetwork.CurrentRoom.PlayerCountはコールバック内でしか使えない
        inRoom = true;
    }

    //ルーム退出時
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        inRoomPanel.SetActive(false);
        roomCreatePanel.SetActive(true);
        inRoom = false;
        message.text = "ルームを作成or参加して\nゲームを開始";
        info.Clear();

    }

   

    // ---------- 設定GUI ----------
    void OnGUI()
    {
        float scale = Screen.height / 480.0f;
        GUI.matrix = Matrix4x4.TRS(new Vector3(
            Screen.width * 0.5f, Screen.height * 0.5f, 0),
            Quaternion.identity,
            new Vector3(scale, scale, 1.0f));

        if (!Status.OFFLINE.ToString().Equals(dispStatus))
        {
            GUI.Window(0, new Rect(-200, 0, 400, 200),//真ん中から横、縦、大きさ、大きさ
                NetworkSettingWindow, "他のルームに参加する");
        }
        if(inRoom == true)
        {
            GUI.Window(0, new Rect(-200, -100, 400, 300),//真ん中から横、縦、大きさ、大きさ
                            NetworkSettingWindow, "入室状況");
        }
        
    }

    Vector2 scrollPosition1;
    Vector2 scrollPosition2;

    public object MaxPlayers { get; private set; }

    void NetworkSettingWindow(int windowID)
    {
        if (inRoom == true)
        {
            scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1, GUILayout.Width(380), GUILayout.Height(300));
            int i = 0;
            while (i < info.Count)
            {
                GUILayout.BeginVertical();
                GUILayout.Label(info[i]);
                GUILayout.EndVertical();
                i++;
            }
            GUILayout.EndScrollView();
        }
        
        //if (creater == true)
        //{
        //    GUILayout.BeginHorizontal();
        //    GUILayout.Label(dispMessage);
        //    GUILayout.EndHorizontal();
        //}

        if (Status.ONLINE.ToString().Equals(dispStatus))
        {
            // --- ONLINEモードのみ表示
            if (!(PhotonNetwork.CurrentRoom != null))
            {
                // ルーム一覧
                GUILayout.Label("【ルーム一覧 (クリックで入室)】");
                // 一覧表示
                scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2, GUILayout.Width(380), GUILayout.Height(100));
                if (roomDispList != null && roomDispList.Count > 0)
                {
                    // 更新ボタン
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("更新"))
                    {
                        // ロビーに入り直す
                        roomDispList = new List<RoomInfo>();
                        PhotonNetwork.LeaveLobby();
                        PhotonNetwork.JoinLobby();
                    }
                    // ルーム一覧
                    GUILayout.EndHorizontal();
                    foreach (RoomInfo roomInfo in roomDispList)
                        if (GUILayout.Button(roomInfo.Name, GUI.skin.box, GUILayout.Width(360)))
                            ConnectToRoom(roomInfo.Name);
                }
                GUILayout.EndScrollView();
            }
        }
    }
}