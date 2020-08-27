using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformingScript : MonoBehaviour
{
    GameObject humanLooking;
    GameObject ghostLooking;
    GameObject instantiatedObject;
    public GameObject particlePrefab;

    float timeToUsingKidnapping = 8;

    bool isGhostLooking = false;

    KidnappingScript kidnapping;
   

    // Start is called before the first frame update
    void Start()
    {
        humanLooking = transform.GetChild(0).gameObject;
        ghostLooking = transform.GetChild(1).gameObject;
        kidnapping = GetComponent<KidnappingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
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

    IEnumerator TransformingFromGhostToHuman()
    {
        kidnapping.enabled = false;
        instantiatedObject = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);
        instantiatedObject.transform.parent = this.transform;
        yield return new WaitForSeconds(1);
        humanLooking.gameObject.SetActive(true);
        ghostLooking.gameObject.SetActive(false);
        isGhostLooking = false;
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
    }

    IEnumerator KidappingEnabledMethod()
    {
        yield return new WaitForSeconds(timeToUsingKidnapping);
        kidnapping.enabled = true;
    }
}
