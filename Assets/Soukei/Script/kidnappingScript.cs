using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidnappingScript : MonoBehaviour
{
    GameObject[] playersObject;
    GameObject jail;
    Vector3 dir;
    float[] dirArray;
    float catchableDistance = 4;
    float catchableAngle = 60;
    float[] angle ;
    float kidnappingInterval = 2;
    float catchingInterval = 2;
    float releasingDirection = 3;
    bool isCatchable = true;
    bool[] isInterruptedArray;
    int isInterruptedArrayNumber;
    int childNumber = 0;
    float lightActiveTime = 4;
    StoppingGhostScript stoppingGhostScript;
    TorchScript torchScript;
    GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        jail = GameObject.FindGameObjectWithTag("RevivalCharmPoint");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        
       
    }

    private void Start()
    {
		playersObject = gameManager.childrenObjects;
		isInterruptedArray = new bool[playersObject.Length];
        stoppingGhostScript = GetComponent<StoppingGhostScript>();

        for (int i = 0; i < playersObject.Length; i++)
        {
            torchScript = playersObject[i].transform.Find("TorchManager").gameObject.GetComponent<TorchScript>();
            torchScript.childGameObjectNumber = i;
        }

        for (int i = 0; i < playersObject.Length; i++)
        {
            isInterruptedArray[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCatchable)
            {
                isCatchable = false;
                dirArray = new float[playersObject.Length];
                angle = new float[playersObject.Length];
                Vector3 enemyLookingVector = transform.TransformDirection(Vector3.up);

                //配列に幽霊とプレイヤーの距離と角度を格納
                for (int i = 0; i < playersObject.Length; i++)
                {
                    Vector3 targetDirection = playersObject[i].transform.position - transform.position;
                    angle[i] = Vector3.Angle(enemyLookingVector, targetDirection);
                    dirArray[i] = Vector3.Distance(playersObject[i].transform.position, transform.position);

                }
                StartCoroutine("KidnappingMethod");
            }
            StartCoroutine("IntervalSecond");
        }
    }

    //一番近くにいて射程角度内にいる子供を数秒間捕まえたのち封印するスクリプト
    IEnumerator KidnappingMethod()
    {
        if (Mathf.Min(dirArray) < catchableDistance)
        {
            for (int i = 0; i < dirArray.Length; i++)
            {
                if (Mathf.Min(dirArray) == dirArray[i])
                {
                    if (-catchableAngle < angle[i] && angle[i] < catchableAngle)
                    {
                        childNumber = i;
                        playersObject[i].transform.position = this.transform.position + new Vector3(0.5f, 0.5f, 0);
                        playersObject[i].transform.parent = this.transform;
                        yield return new WaitForSeconds(kidnappingInterval);
                        playersObject[i].transform.parent = null;
                        playersObject[i].transform.position = jail.transform.position;
                        gameManager.JudgingKidnappedChild(i);
                    }
                }
            }
        }
    }

    //幽霊の攻撃のインターバルを管理するメソッド
    IEnumerator IntervalSecond()
    {
        yield return new WaitForSeconds(catchingInterval);
        isCatchable = true;
    }

    //子供攻撃された時に子供の配列番号に対応する配列の箇所をtrueにしてそれが全部trueだったら攻撃を中断しスタンするメソッド
    public void AttackedbyLight(int childNumber)
    {
        isInterruptedArray[childNumber] = true;
        StartCoroutine(MakingTruetoFalseMethod(childNumber));
        if (IsAllInterruptedArrayTrue() > playersObject.Length-2 )
        {
            playersObject[childNumber].transform.position = this.transform.position + Vector3.up * releasingDirection;
            StopCoroutine("KidnappingMethod");
            stoppingGhostScript.StoppingEnemyMethod();
            for (int i = 0; i < playersObject.Length; i++)
            {
                isInterruptedArray[i] = false;
            }
        }
    }

    //子供に攻撃されているかどうかを管理している配列のtrueの合計数を数えるメソッド
    public int IsAllInterruptedArrayTrue()
    {
        for (int i = 0; i < playersObject.Length; i++)
        {
            if (isInterruptedArray[i] == false)
            {
                continue;
            }
            if (isInterruptedArray[i] == true)
            {
                isInterruptedArrayNumber++;
                continue;
            }
            return isInterruptedArrayNumber ;
        }
        return isInterruptedArrayNumber;
    }

    //攻撃されてから数秒後に配列のtrueをfalseに変えるメソッド
    IEnumerator MakingTruetoFalseMethod(int arrayNumber)
    {
        yield return new WaitForSeconds(lightActiveTime);
        isInterruptedArray[arrayNumber] = false;
    }

}

