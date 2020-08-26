using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidnappingScript : MonoBehaviour
{
    GameObject[] playersObject;
    Vector3 dir;
    float[] dirArray;
    float catchableDistance = 4;
    float catchableAngle = 60;
    float[] angle ;
    float catchingInterval = 2;
    bool isCatchable = true;

    // Start is called before the first frame update
    void Start()
    {
       playersObject =  GameObject.FindGameObjectsWithTag("Human");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCatchable)
            {
                dirArray = new float[playersObject.Length];
                angle = new float[playersObject.Length];
                Vector3 enemyLookingVector = transform.TransformDirection(Vector3.up);

                //配列に幽霊とプレイヤーの距離と角度を格納
                for (int i = 0; i < playersObject.Length; i++)
                {
                    Vector3 targetDirection = playersObject[i].transform.position - transform.position;
                    angle[i] = Vector3.Angle(enemyLookingVector, targetDirection);
                    dirArray[i] = Vector3.Distance(playersObject[i].transform.position, transform.position);
                    Debug.Log(dirArray[i]);

                }
                KidnappingMethod();
            }
            StartCoroutine("IntervalSecond");
        }
    }

    //距離の長さがつかめる距離より短いかどうかを判別
    void KidnappingMethod()
    {
        if (Mathf.Min(dirArray) < catchableDistance)
        {
            for (int i = 0; i < dirArray.Length; i++)
            {
                if (Mathf.Min(dirArray) == dirArray[i])
                {
                    if (-catchableAngle < angle[i] && angle[i] < catchableAngle)
                    {
                        playersObject[i].transform.position = new Vector3(Random.Range(2f, 4f), Random.Range(2f, 4f), 0);
                    }
                }
            }
        }
    }

    IEnumerator IntervalSecond()
    {
        yield return new WaitForSeconds(catchingInterval);
        isCatchable = true;
    }
}
