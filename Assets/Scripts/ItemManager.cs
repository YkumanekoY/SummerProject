using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum Item
    {
        sealedCharm,//復活のためのお札
        revivalCharm,//復活のためのお札
        torch,//トーチ
        key//鍵
    };

    //　アイテムを持っているかどうかのフラグ
    [SerializeField]
    private bool[] itemFlags = new bool[6];

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {

    }
}
