using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscGameOverScript : MonoBehaviour
{
    void Awake()
    {

        // 自分自身だったり
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Quit();
    }
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    UnityEngine.Application.Quit();
#endif
    }

}
