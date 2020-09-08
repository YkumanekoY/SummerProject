using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppingGhostScript : MonoBehaviour
{
    KidnappingScript kidnappingScript;
    EnemyMovingScript enemyMovingScript;
    public float stoppingTime = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        kidnappingScript = GetComponent<KidnappingScript>();
        enemyMovingScript = GetComponent<EnemyMovingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void StoppingEnemyMethod()
    {
        kidnappingScript.enabled = false;
        enemyMovingScript.enabled = false;
        Invoke("Release", stoppingTime);
    }
void Release()
    {
        kidnappingScript.enabled = true;
        enemyMovingScript.enabled = true;
    }
}
