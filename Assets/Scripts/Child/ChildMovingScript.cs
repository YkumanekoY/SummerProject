<<<<<<< HEAD

using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ChildMovingScript : MonoBehaviourPunCallbacks
{
    private Vector3 child_pos; //プレイヤーのポジション
    public float inputX; //x方向のImputの値
    public float inputY; //y方向のInputの値
    private Rigidbody2D rigd;
    float childSpeed = 5.0f;// 子供のスピード
    bool escape = true;
    [SerializeField]
    Transform itemList;
    [SerializeField]
    Camera playerCamera;
    GameManager gameManager;
    GameObject managerObject;
    //ItemPoint itemPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            child_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
            managerObject = GameObject.Find("GameManager");
            gameManager = managerObject.GetComponent<GameManager>();
            rigd = GetComponent<Rigidbody2D>(); //プレイヤーのRigidbodyを取得
            itemList.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (gameManager.isPlayerControl)
            {
                inputX = Input.GetAxis("Horizontal"); //x方向のInputの値を取得
                inputY = Input.GetAxis("Vertical"); //z方向のInputの値を取得
                rigd.velocity = new Vector2(inputX * childSpeed, inputY * childSpeed); //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動
                child_pos = transform.position; //プレイヤーの位置を更新
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (photonView.IsMine)
        {
            if (collider.gameObject.tag == "Item" && Input.GetKeyDown(KeyCode.Return))
            {
                collider.gameObject.GetComponent<ItemPoint>().SearchItem(this.gameObject);
                //itemPoint = collider.gameObject.GetComponent<ItemPoint>();
            }
        }
    }

    public void GetItem(GameObject item)
    {
        Instantiate(item, transform.position, Quaternion.identity, itemList);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.IsMine)
        {

            if (itemList.childCount == 0) return;
            if (other.gameObject.CompareTag("SealedCharmPoint"))
            {
                if (!itemList.Find("SealedCharm(Clone)")) return;
                gameManager.IncreaseSealedCharm();
                Destroy(itemList.Find("SealedCharm(Clone)").gameObject);
            }

            if (other.gameObject.CompareTag("RevivalCharmPoint"))
            {
                if (!itemList.Find("RevivalCharm(Clone)")) return;
                gameManager.IncreaseRevivalCharm();
                Destroy(itemList.Find("RevivalCharm(Clone)").gameObject);
            }
        }
	}
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMovingScript : MonoBehaviour
{
    private Vector3 child_pos; //プレイヤーのポジション
    public float inputX; //x方向のImputの値
    public float inputY; //y方向のInputの値
    private Rigidbody2D rigd;
    float childSpeed = 5.0f;// 子供のスピード
    bool escape = true;
    [SerializeField]
    Transform itemList;
    GameManager gameManager;
    GameObject managerObject;
    ItemPoint itemPoint;

    // Start is called before the first frame update
    void Start()
    {
        managerObject = GameObject.Find("GameManager");
        gameManager = managerObject.GetComponent<GameManager>();
        rigd = GetComponent<Rigidbody2D>(); //プレイヤーのRigidbodyを取得
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isPlayerControl)
        {
            inputX = Input.GetAxis("Horizontal"); //x方向のInputの値を取得
            inputY = Input.GetAxis("Vertical"); //z方向のInputの値を取得
            rigd.velocity = new Vector2(inputX * childSpeed, inputY * childSpeed); //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Item" && Input.GetKeyDown(KeyCode.Return))
        {
            collider.gameObject.GetComponent<ItemPoint>().SearchItem(this.gameObject);
            itemPoint = collider.gameObject.GetComponent<ItemPoint>();
        }
    }

    public void GetItem(GameObject item)
    {
        Instantiate(item, transform.position, Quaternion.identity, itemList);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(itemList.childCount == 0) return;
		if (other.gameObject.CompareTag("SealedCharmPoint"))
		{
            if(!itemList.Find("SealedCharm(Clone)")) return;
			gameManager.IncreaseSealedCharm();
			Destroy(itemList.Find("SealedCharm(Clone)").gameObject);
		}

        if (other.gameObject.CompareTag("RevivalCharmPoint"))
		{
            if(!itemList.Find("RevivalCharm(Clone)")) return;
			gameManager.IncreaseRevivalCharm();
			Destroy(itemList.Find("RevivalCharm(Clone)").gameObject);
		}
	}
}

>>>>>>> 31d21da39d677211c7ecabe5ac78b97d4f8ede50
