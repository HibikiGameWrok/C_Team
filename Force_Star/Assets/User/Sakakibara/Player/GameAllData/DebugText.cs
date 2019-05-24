using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    public Text textObject = null; 
    public string textData = "";

    // 初期化
    void Start()
    {

    }

    // 更新
    void Update()
    {
        // テキストの表示を入れ替える
        textObject.text = textData;
    }
}
