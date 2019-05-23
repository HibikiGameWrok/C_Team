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
    private TargetFollow m_mainCameraTarget;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // カメラ子のシェイク取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private ShakeCamera m_mainCameraShake;
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
        m_mainCameraTarget = m_mainCamera.GetComponent<TargetFollow>();
        if (!m_mainCameraTarget)
        {
            m_mainCameraTarget = m_mainCamera.AddComponent<TargetFollow>();
        }
        m_mainCameraCamera = m_mainCamera.GetComponent<Camera>();
        if (!m_mainCameraCamera)
        {
            m_mainCameraCamera = m_mainCamera.AddComponent<Camera>();
        }
        m_mainCameraShake = m_mainCamera.GetComponent<ShakeCamera>();
        if (!m_mainCameraShake)
        {
            m_mainCameraShake = m_mainCamera.AddComponent<ShakeCamera>();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ポインターのデータを登録する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndexRocket.SetPointerMainCamera(m_mainCameraCamera);
        m_directorIndexRocket.SetPointerTargetCamera(m_mainCameraTarget);
        m_directorIndexRocket.SetPointerShakeCamera(m_mainCameraShake);
    }
}
