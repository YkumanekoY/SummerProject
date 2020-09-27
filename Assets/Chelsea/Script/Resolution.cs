using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    // Start is called before the first frame update
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(1280, 960, false, 60);

    }

    // Update is called once per frame

}