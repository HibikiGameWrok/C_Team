using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//*|***|***|***|***|***|***|***|***|***|***|***|
// パーツ言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using PartsID = PlayStaticData.PartsID;

public class PlaySceneDirectorRocket : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // カメラ親の取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_parentMainCamera;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // カメラ子の取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Transform m_mainCameraTrans;
    private Camera m_mainCameraCamera;
    private GameObject m_mainCamera;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // カメラ子のターゲット取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private CameraArts m_mainCameraArts;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ロケットのディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorRocketIndex m_directorIndexRocket;

    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ロケットのディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndexRocket = PlaySceneDirectorRocketIndex.GetInstance();
        m_directorIndexRocket.AllReset();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // カメラ親の取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_parentMainCamera = GameObject.Find("ParentMainCamera");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // カメラ子の取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mainCameraTrans = m_parentMainCamera.transform.Find("Main Camera");
        m_mainCamera = m_mainCameraTrans.gameObject;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // カメラ子のターゲット取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mainCameraArts = m_mainCamera.GetComponent<CameraArts>();
        if (!m_mainCameraArts)
        {
            m_mainCameraArts = m_mainCamera.AddComponent<CameraArts>();
        }
        m_mainCameraCamera = m_mainCamera.GetComponent<Camera>();
        if (!m_mainCameraCamera)
        {
            m_mainCameraCamera = m_mainCamera.AddComponent<Camera>();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ポインターのデータを登録する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndexRocket.SetPointerMainCamera(m_mainCameraCamera);
        m_directorIndexRocket.SetPointerMainCameraArts(m_mainCameraArts);
    }
}
