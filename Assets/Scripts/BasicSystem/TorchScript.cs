﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    int childNumber;//子供の配列番号を格納する変数
    float childDirection;//子供たちからの角度をいれる変数
    Vector3 direction;//
    GameObject EnemyGameObject;
    KidnappingScript kidnappingScript;
    float torchInterval = 3;

    // Start is called before the first frame update
    void Start()
    {
        EnemyGameObject = GameObject.Find("Ghost");
        kidnappingScript = gameObject.GetComponent<KidnappingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            childDirection = Vector3.Distance(EnemyGameObject.transform.position, transform.position);

            if(childDirection < 5f)
            {
                GameObject.Find("Child").transform.Find("Torch").gameObject.SetActive(true);
                //秒数のコルーチンを開始
                StartCoroutine("torchCount");
                //kidnappingScript.AttackedbyLight();
            }
        }

    }

    //トーチの制限時間を設けるメソッド
    IEnumerator torchCount()
    {
        //インターバル分停止させる
        yield return new WaitForSeconds(torchInterval);

        //再びライトをオフに
        GameObject.Find("Child").transform.Find("Torch").gameObject.SetActive(false);

    }

}
