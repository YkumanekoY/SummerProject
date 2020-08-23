using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidnappingScript : MonoBehaviour
{
    GameObject[] playersObject;
    float angleDir;
    Vector3 dir;
    float[] dirArray;
    bool isCatchable = false;

    // Start is called before the first frame update
    void Start()
    {
       playersObject =  GameObject.FindGameObjectsWithTag("Human");
        dirArray = new float[playersObject.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0 ; i < playersObject.Length; i++)
            {
                dirArray[i] = Vector3.Distance(playersObject[i].transform.position, transform.position);
                Debug.Log(dirArray[i]);
            }

            if(Mathf.Min(dirArray) < 2)
            {
                for (int i = 0; i < dirArray.Length; i++)
                {
                    if(Mathf.Min(dirArray) == dirArray[i])
                    {
                        playersObject[i].transform.position = new Vector3(Random.Range(2f, 4f), Random.Range(2f, 4f), 0);
                    }
                }
            }
        }
    }
}
