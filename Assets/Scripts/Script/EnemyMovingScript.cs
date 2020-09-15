using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingScript : MonoBehaviour
{
   
    public readonly float SPEED = 0.1f;
    private Rigidbody2D rigidBody;
    private Vector2 input;
    ItemPoint itemPoint;
    Transform itemList;
    GameManager gameManager;
    GameObject gameManagerObj;
    GameObject itemListObj;
    float inputX; //x方向のImputの値
    float inputY; //y方向のInputの値

    float currentSpeed;
    public const float childSpeed = 5.0f;// 子供のスピード
    public const float ghostSpeed = 6f;

    void Start()
    {
        gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        itemListObj = GameObject.Find("ItemList");//名前違うかもしれん

        this.rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定
        this.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        currentSpeed = childSpeed;
    }

    private void Update()
    {
        // 入力を取得
        if (gameManager.isPlayerControl)
        {
            inputX = Input.GetAxis("Horizontal"); //x方向のInputの値を取得
            inputY = Input.GetAxis("Vertical"); //z方向のInputの値を取得
            rigidBody.velocity = new Vector2(inputX * currentSpeed, inputY * currentSpeed); //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動
        }
       
    }

    public void ChangingGhostSpeedMethod()
    {
        switch (currentSpeed)
        {
            case childSpeed:
                currentSpeed = ghostSpeed;
                break;

            case ghostSpeed:
                currentSpeed = childSpeed;
                break;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Item" && Input.GetKeyDown(KeyCode.Return))
        {
            collider.gameObject.GetComponent<ItemPoint>().SearchItem(this.gameObject);
            itemPoint = collider.gameObject.GetComponent<ItemPoint>();
                
            if(itemListObj.transform.childCount != 0) //リストになんかアイテムがあったら
            {
                if (itemPoint.isItemPut())
                {
                    if(itemListObj.transform.GetChild(0).gameObject.tag == "SealedCharm")
                    {
                        itemPoint.HidingItem("SealedCharm");
                        Destroy(itemListObj.transform.GetChild(0).gameObject);
                    }
                    else if(itemListObj.transform.GetChild(0).gameObject.tag == "RevivalCharm")
                    {
                        itemPoint.HidingItem("RevivalCharm");
                        Destroy(itemListObj.transform.GetChild(0).gameObject);
                    }
                    
                }
            }
        }
    }

    public void GetItem(GameObject item)
    {
        Instantiate(item, transform.position, Quaternion.identity, itemList);
    }

}
