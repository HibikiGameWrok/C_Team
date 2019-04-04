using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerMove : MonoBehaviour
{

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コントローラー情報を転換せよ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    DebugPlayerController m_controller;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        m_controller = new DebugPlayerController();
    }

    void Start()
    {

    }
    
    void Update()
    {
        
    }
}
