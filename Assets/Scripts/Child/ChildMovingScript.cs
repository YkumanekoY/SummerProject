using System.Collections;
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
        child_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
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

            child_pos = transform.position; //プレイヤーの位置を更新
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    { 
       if (collider.gameObject.tag == "Item" && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            collider.gameObject.GetComponent<ItemPoint>().SearchItem(this.gameObject);
            itemPoint = collider.gameObject.GetComponent<ItemPoint>();
        }
    }

    public void GetItem(GameObject item)
    {
        Instantiate(item, transform.position, Quaternion.identity, itemList);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            StopPlayer();
        }


    }

    //プレイヤーの動きを止めるメソッド
    public void StopPlayer()
    {
        rigd.bodyType = RigidbodyType2D.Kinematic;
    }

}

