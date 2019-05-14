using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneDirectorIndex
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シングルトン
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private static PlaySceneDirectorIndex director = null;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シングルトン生成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static PlaySceneDirectorIndex GetInstance()
    {
        if (director == null)
        {
            director = new PlaySceneDirectorIndex();
        }
        return director;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シングルトン消滅
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void Remove()
    {
        if (director != null)
        {
            director = null;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // メインカメラ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    Camera m_mainCamera;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // メインカメラにターゲットされている
    //*|***|***|***|***|***|***|***|***|***|***|***|
    TargetFollow m_targetCamera;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の運営者
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayStarManeger m_starManeger;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ初期化
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メインカメラ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mainCamera = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メインカメラにターゲットされている
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_targetCamera = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星の運営者
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starManeger = null;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ終了
    //*|***|***|***|***|***|***|***|***|***|***|***|
    ~PlaySceneDirectorIndex()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メインカメラ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_mainCamera = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メインカメラにターゲットされている
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_targetCamera = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星の運営者
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starManeger = null;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポインター受付
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointerMainCamera(Camera camera)
    {
        m_mainCamera = camera;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポインター受付
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointerTargetCamera(TargetFollow follow)
    {
        m_targetCamera = follow;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポインター受付
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointerStarManeger(PlayStarManeger maneger)
    {
        m_starManeger = maneger;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポインター受付
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //public void SetPointerTargetCamera(TargetFollow follow)
    //{
    //    m_targetCamera = follow;
    //}
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 狙われたい人を受け付けています
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetObjectTargetCamera(GameObject target)
    {
        if (m_targetCamera != null && target != null)
        {
            m_targetCamera.SetTarget(target);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // スクリーンのどこでしょう？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 GetScreenPos(Vector3 position)
    {
        if (m_mainCamera != null)
        {
            Vector3 ansPos = m_mainCamera.WorldToScreenPoint(position);
            return ansPos;
        }
        return Vector3.zero;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の管理者に報告
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ApplyStar(Vector3 position, int max)
    {
        m_starManeger.CreateStarPisce(position, max);
    }

}
