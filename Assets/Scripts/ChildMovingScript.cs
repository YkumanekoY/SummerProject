using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMovingScript : MonoBehaviour
{
    private Vector3 child_pos; //プレイヤーのポジション
    private float x; //x方向のImputの値
    private float y; //y方向のInputの値
    private Rigidbody2D rigd;
    float childSpeed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        child_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
        rigd = GetComponent<Rigidbody2D>(); //プレイヤーのRigidbodyを取得
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal"); //x方向のInputの値を取得
        y = Input.GetAxis("Vertical"); //z方向のInputの値を取得

        rigd.velocity = new Vector2(x * childSpeed, y * childSpeed); //プレイヤーのRigidbodyに対してInputにspeedを掛けた値で更新し移動

        Vector2 diff = (transform.position - child_pos).normalized; //プレイヤーがどの方向に進んでいるかがわかるように、初期位置と現在地の座標差分を取得

        if (diff.magnitude > 0.01f) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);  //ベクトルの情報をQuaternion.LookRotationに引き渡し回転量を取得しプレイヤーを回転させる
        }

        child_pos = transform.position; //プレイヤーの位置を更新
    }
}
