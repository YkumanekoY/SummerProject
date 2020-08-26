using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    int[] ChildGameNumber;//子供の配列番号を格納する変数
    float childDirection;//子供たちからの角度をいれる変数
    Vector3 direction;//
    GameObject EnemyGameObject;
    ememyMovingScript script;


    // Start is called before the first frame update
    void Start()
    {
        EnemyGameObject = GameObject.Find("Ghost");
        script = gameObject.GetComponent<ememyMovingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            childDirection = Vector3.Distance(EnemyGameObject.transform.position, transform.position);

            if(childDirection < 2.5f)
            {

            }
        }

    }
}
