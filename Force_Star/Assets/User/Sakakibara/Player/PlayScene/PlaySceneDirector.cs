using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneDirector : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_directorIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の運営者
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayStarManeger m_starManeger;
    GameObject m_starManegerObject;


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
        // 権限にて、星の運営者をもらうぞ！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starManegerObject = new GameObject("starManeger");
        m_starManeger = m_starManegerObject.AddComponent<PlayStarManeger>();
        
        //m_starManeger.

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ポインターのデータを登録する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.SetPointerMainCamera(mainCamera);
        m_directorIndex.SetPointerTargetCamera(mainCameraTarget);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ポインターの星の運営者を登録する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.SetPointerStarManeger(m_starManeger);
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
