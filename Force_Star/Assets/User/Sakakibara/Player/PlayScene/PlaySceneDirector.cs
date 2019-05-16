using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneDirector : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_directorIndex;
    PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の運営者
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayStarManeger m_starManeger;
    GameObject m_starManegerObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の運営者
    //*|***|***|***|***|***|***|***|***|***|***|***|
    SEManager m_seManager;
    GameObject m_seManagerObject;

    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクター登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        m_playerIndex = PlayerDirectorIndex.GetInstance();
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

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 権限にて、星の運営者をもらうぞ！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_seManagerObject = new GameObject("SEManager");
        m_seManager = m_seManagerObject.AddComponent<SEManager>();

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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // SEの運営者を登録する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.SetPointerSEManeger(m_seManager);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float air = m_playerIndex.GetAirParsent();
        bool clearAnime = m_directorIndex.GetClearAnimation();
        bool clearFlag = m_directorIndex.GetClearFlag();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // おしまい
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (air == 0 && !clearAnime)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 死にました～
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_directorIndex.SetAliveFlagPlayScene(false);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // シーン切り替え＞リザルト行き
            //*|***|***|***|***|***|***|***|***|***|***|***|
            SceneManager.LoadScene("ResultScene");


        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 帰還
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (clearFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // やったぜ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_directorIndex.SetAliveFlagPlayScene(true);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // シーン切り替え＞リザルト行き
            //*|***|***|***|***|***|***|***|***|***|***|***|
            SceneManager.LoadScene("ResultScene");


        }
    }

    void OnApplicationQuit()
    {
        PlaySceneDirectorIndex.Remove();
        PlayerDirectorIndex.Remove();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 倉庫の終了
        //*|***|***|***|***|***|***|***|***|***|***|***|
        WarehouseData.WarehouseObject.Remove();
        WarehouseData.WarehouseUnity.Remove();
        WarehouseData.PlayerData.WarehousePlayer.Remove();
    }
}
