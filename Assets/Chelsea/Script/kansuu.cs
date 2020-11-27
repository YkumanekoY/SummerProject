using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class kansuu : MonoBehaviour
{
    /// <summary>
    /// 指定された文字列が null または空であるか、空白文字だけで構成されているかどうかを返します
    /// </summary>
    public static bool IsNullOrWhiteSpace(string value)
    {
        return value == null || value.Trim() == "";
    }
}
