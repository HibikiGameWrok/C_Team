﻿using System.Collections;
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲームクリアフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ClearReset();
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲームクリアフラグ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ClearReset();
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
    public void ApplyStar(Vector3 position, int num)
    {
        m_starManeger.CreateStarPisce(position, num);
    }
    public void ApplyStar(Vector3 position, float angle, float angleSwing, float speedMax, float speedMin, int num)
    {
        m_starManeger.CreateStarPisce(position, angle, angleSwing, speedMax, speedMin, num);
    }
    public void ApplyStar(Vector3 position, float angle, float angleSwing, float speedMax, int num)
    {
        m_starManeger.CreateStarPisce(position, angle, angleSwing, speedMax, 0, num);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 左右にぶちまける
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ApplyStarLeftSide(Vector3 position, float angleSwing, float speedMax, int num)
    {
        Vector2 vec = new Vector2(-1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarPisce(position, angle, angleSwing, speedMax, 0, num);
    }
    public void ApplyStarLeftSide(Vector3 position, float angleSwing, float speedMax, float speedMin, int num)
    {
        Vector2 vec = new Vector2(-1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarPisce(position, angle, angleSwing, speedMax, speedMin, num);
    }
    public void ApplyStarRightSide(Vector3 position, float angleSwing, float speedMax, int num)
    {
        Vector2 vec = new Vector2(1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarPisce(position, angle, angleSwing, speedMax, 0, num);
    }
    public void ApplyStarRightSide(Vector3 position, float angleSwing, float speedMax, float speedMin, int num)
    {
        Vector2 vec = new Vector2(1, 0);
        float angle = ChangeData.Vector2ToAngleDeg(vec);
        m_starManeger.CreateStarPisce(position, angle, angleSwing, speedMax, speedMin, num);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲームクリア！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_platinumAngel;
    bool m_gameClearFlag;
    public void ClearReset()
    {
        m_platinumAngel = false;
        m_gameClearFlag = false;
    }
    public void SetClearAnimation()
    {
        m_platinumAngel = true;
    }
    public void SetClearFlag()
    {
        m_platinumAngel = true;
        m_gameClearFlag = true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲームクリア！取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetClearAnimation()
    {
        return m_platinumAngel;
    }
    public bool GetClearFlag()
    {
        return m_gameClearFlag;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲームクリア！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    bool m_aliveFlagPlayScene;
    public void SetAliveFlagPlayScene(bool setBool)
    {
        m_aliveFlagPlayScene = setBool;
    }
    public bool GetAliveFlagPlayScene()
    {
        return m_aliveFlagPlayScene;
    }
}
