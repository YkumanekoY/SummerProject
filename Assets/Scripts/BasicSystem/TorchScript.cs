using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    public int childGameObjectNumber = 0;//子供の配列番号を格納する変数
    float childDirection;//子供たちからの角度をいれる変数
    Vector2 direction;
    GameObject EnemyGameObject;
    KidnappingScript kidnappingScript;
    float torchIntervalTime = 5;
    private Rigidbody rigd;
    private Vector3 Player_pos; //プレイヤーのポジション
    float torchActiveTime = 3;
    bool istorchActive = true; //トーチの使用可能の有無

    // Start is called before the first frame update
    void Start()
    {
        EnemyGameObject = GameObject.Find("Ghost");
        kidnappingScript = EnemyGameObject.GetComponent<KidnappingScript>();
        rigd = GetComponent<Rigidbody>(); //トーチのRigidbodyを取得
    }

    // Update is called once per frame
    void Update()
    {
        if(istorchActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                childDirection = Vector3.Distance(EnemyGameObject.transform.position, transform.position);// 敵との距離を把握
        
                if (childDirection < 5f)
                {
                    GameObject.Find("TorchManager").transform.Find("Torch").gameObject.SetActive(true);
                
                    //秒数のコルーチンを開始
                    StartCoroutine("torchCount");   
                    kidnappingScript.AttackedbyLight(childGameObjectNumber);
                }
            }
        }

    }

    //トーチの制限時間を設けるメソッド
    IEnumerator torchCount()
    {
        //インターバル分停止させる
        yield return new WaitForSeconds(torchActiveTime);

        //再びライトをオフに
        GameObject.Find("TorchManager").transform.Find("Torch").gameObject.SetActive(false);

        StartCoroutine("torchInterval");
        istorchActive = false;

    }

    //トーチのインターバルを設けるメソッド
    IEnumerator torchInterval()
    {
        yield return new WaitForSeconds(torchIntervalTime);

        //再びライトが使えるように
        istorchActive = true;
    }


}
