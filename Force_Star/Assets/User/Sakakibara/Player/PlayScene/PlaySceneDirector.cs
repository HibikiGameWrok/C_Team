using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneDirector : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_directorIndex;
    
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクター登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 権限にて、メインカメラのターゲットをもらうぞ！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Camera mainCamera = Camera.main;
        GameObject mainCameraObject = mainCamera.gameObject;
        TargetFollow mainCameraTarget = null;
        if (mainCameraObject.GetComponent<TargetFollow>())
        {
            mainCameraTarget = mainCameraObject.GetComponent<TargetFollow>();
        }
        else
        {
            mainCameraTarget = mainCameraObject.AddComponent<TargetFollow>();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ポインターのデータを登録する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.SetPointerMainCamera(mainCamera);
        m_directorIndex.SetPointerTargetCamera(mainCameraTarget);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        PlaySceneDirectorIndex.Remove();
        PlayerDirectorIndex.Remove();
    }
}
