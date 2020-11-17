using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TransformingScript : MonoBehaviourPunCallbacks
{
    GameObject humanLooking;
    GameObject ghostLooking;
    GameObject instantiatedObject;
    public GameObject particlePrefab;

    float timeToUsingKidnapping = 8;

    public bool isGhostLooking = false;

    KidnappingScript kidnapping;

    GameManager gameManager;
    GameObject gameManagerObj;

    EnemyMovingScript enemyMovingScript;


    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            humanLooking = transform.GetChild(0).gameObject;
            ghostLooking = transform.GetChild(1).gameObject;
            kidnapping = GetComponent<KidnappingScript>();
            gameManagerObj = GameObject.Find("GameManager");
            gameManager = gameManagerObj.GetComponent<GameManager>();
            enemyMovingScript = GetComponent<EnemyMovingScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.V) && gameManager.isPlayerControl)
            {
                if (isGhostLooking)
                {
                    StartCoroutine("TransformingFromGhostToHuman");
                }
                else
                {
                    StartCoroutine("TransformingFromHumanToGhost");
                }
            }
        }
    }

    IEnumerator TransformingFromGhostToHuman()
    {
        kidnapping.enabled = false;
        instantiatedObject = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);
        instantiatedObject.transform.parent = this.transform;
        yield return new WaitForSeconds(1);
        humanLooking.gameObject.SetActive(true);
        ghostLooking.gameObject.SetActive(false);
        isGhostLooking = false;
        enemyMovingScript.ChangingGhostSpeedMethod();
    }
    IEnumerator TransformingFromHumanToGhost()
    {
        instantiatedObject = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);
        instantiatedObject.transform.parent = this.transform;
        yield return new WaitForSeconds(1);
        humanLooking.gameObject.SetActive(false);
        ghostLooking.gameObject.SetActive(true);
        StartCoroutine("KidappingEnabledMethod");
        isGhostLooking = true;
        enemyMovingScript.ChangingGhostSpeedMethod();
    }

    IEnumerator KidappingEnabledMethod()
    {
        yield return new WaitForSeconds(timeToUsingKidnapping);
        kidnapping.enabled = true;
    }
}
