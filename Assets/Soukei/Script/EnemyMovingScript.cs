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

    void Start()
    {
        gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        this.rigidBody = GetComponent<Rigidbody2D>();
        // 衝突時にobjectを回転させない設定
        this.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        // 入力を取得
        if (gameManager.isPlayerControl)
        {
            input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
        }
       
    }

    private void FixedUpdate()
    {
        if (input == Vector2.zero)
        {
            return;
        }
        // 既存のポジションに対して、移動量(vector)を加算する
        rigidBody.position += input * SPEED;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Item" && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            collider.gameObject.GetComponent<ItemPoint>().SearchItem(this.gameObject);
            itemPoint = collider.gameObject.GetComponent<ItemPoint>();
            
                if(itemPoint.isItemPut())
                    {
                /*if(選択したやつがsealedcharmだったら){itemPoint.HidingItem("SealedCharm");}
                elseif(選択したやつがrevaialだったら){itemPoint.HidingItem("RevivalCharm");}
                else{ return;}*/
                    }
        }
    }

    public void GetItem(GameObject item)
    {
        Instantiate(item, transform.position, Quaternion.identity, itemList);
    }

}
