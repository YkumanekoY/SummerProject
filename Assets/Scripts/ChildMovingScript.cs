using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMovingScript : MonoBehaviour
{
    private Vector3 child_pos; //プレイヤーのポジション
    private float x; //x方向のImputの値
    private float y; //y方向のInputの値
    private Rigidbody2D rigd;
    float childSpeed = 5.0f;// 子供のスピード
    private bool escape = true;
    public GameObject itemPrefab;
    [SerializeField]
    Transform itemList;

    // Start is called before the first frame update
    void Start()
    {
        child_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
        rigd = GetComponent<Rigidbody2D>(); //プレイヤーのRigidbodyを取得
    }

    // Update is called once per frame
    void Update()
    {
        if(escape == true)
        {
        x = Input.GetAxis("Horizontal"); //x方向のInputの値を取得
        y = Input.GetAxis("Vertical"); //z方向のInputの値を取得

        rigd.velocity = new Vector2(x * childSpeed, y * childSpeed); //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動

        child_pos = transform.position; //プレイヤーの位置を更新
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
            Instantiate(itemPrefab, transform.position, Quaternion.identity, itemList);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            rigd.bodyType = RigidbodyType2D.Kinematic;
            escape = false;
        }
    }
}
