using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//*|***|***|***|***|***|***|***|***|***|***|***|
// パーツ言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using PartsID = PlayStaticData.PartsID;
//*|***|***|***|***|***|***|***|***|***|***|***|
// オーダー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehouseOrder = WarehouseData.WarehouseOrder;
using Object_Order_Number = WarehouseData.WarehouseOrder.Object_Order_Number;


public class PlaySceneDirector : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ロケットのディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorRocketIndex m_directorIndexRocket;
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
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツたち
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private class RocketPartsData
    {
        public GameObject m_rocketPartsObject;
        public PlayRocketParts m_rocketParts;
        public PartsID m_rocketPartsID;
        public Vector3 m_rocketPartsPosition;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツの場所とか
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private int m_rocketDataNum;
    List<RocketPartsData> m_rocketData;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル関係
    //*|***|***|***|***|***|***|***|***|***|***|***|
    FileConnection m_fileConnectionCandidatePosition;
    List<Vector3> m_rocketPartsCandidatePosition;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル名
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    string m_fileName;

    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ロケットのディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndexRocket = PlaySceneDirectorRocketIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクター登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        m_directorIndex.AllReset();
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        m_playerIndex.AllReset();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 権限にて、メインカメラのターゲットをもらうぞ！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Camera mainCamera = m_directorIndexRocket.GetPointerMainCamera();
        TargetFollow mainCameraTarget = m_directorIndexRocket.GetPointerTargetCamera();
        ShakeCamera mainCameraShake = m_directorIndexRocket.GetPointerShakeCamera();
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
        // パーツ出現！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakeRokcetData();


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ポインターのデータを登録する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.SetPointerMainCamera(mainCamera);
        m_directorIndex.SetPointerTargetCamera(mainCameraTarget);
        m_directorIndex.SetPointerShakeCamera(mainCameraShake);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ポインターの星の運営者を登録する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.SetPointerStarManeger(m_starManeger);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // SEの運営者を登録する
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.SetPointerSEManeger(m_seManager);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ出現！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeRokcetData()
    {
        Common_GameObjectSprite_Order order = null;
        m_rocketDataNum = (int)PartsID.NUM;
        m_rocketData = new List<RocketPartsData>();
        m_rocketPartsCandidatePosition = new List<Vector3>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_rocketDataNum; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // データの原型
            //*|***|***|***|***|***|***|***|***|***|***|***|
            RocketPartsData partsData = new RocketPartsData();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // データオブジェクト作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GameObject partsObject = new GameObject();
            partsObject.name = "RocketParts";
            partsData.m_rocketPartsObject = partsObject;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // データスクリプト作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            PlayRocketParts partsScript = partsObject.AddComponent<PlayRocketParts>();
            partsData.m_rocketParts = partsScript;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // データの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            partsData.m_rocketPartsID = (PartsID)index;
            partsData.m_rocketPartsPosition = Vector3.zero;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // オーダーの情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            order = partsData.m_rocketPartsObject.AddComponent<Common_GameObjectSprite_Order>();
            order.SetBoth(partsScript.GetSpriteData(), Object_Order_Number.STAR);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 桁挿入
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_rocketData.Add(partsData);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 乱数設定
        //*|***|***|***|***|***|***|***|***|***|***|***|


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイル関係
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string fileName = "Book";
        fileName = "PlayPartsPointData.csv";
        string areaName = "";
        areaName = areaName + "PlaySceneData/";
        m_fileConnectionCandidatePosition = new FileConnection();
        m_fileConnectionCandidatePosition.SetFileName(fileName);
        m_fileConnectionCandidatePosition.SetAreaName(areaName);
        m_fileName = fileName;
        //m_fileConnectionCandidatePosition



    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ更新！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateParts()
    {

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 目標数星量数字
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_rocketData.Count; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パーツがあるなら
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (m_rocketData[index].m_rocketParts)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 種類のデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_rocketData[index].m_rocketParts.SetPartsID(m_rocketData[index].m_rocketPartsID);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 場所のデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_rocketData[index].m_rocketParts.SetPointArea(m_rocketData[index].m_rocketPartsPosition);
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 解禁のデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_rocketData[index].m_rocketParts.SetPlayHit();
            }

        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 乱数移動
        //*|***|***|***|***|***|***|***|***|***|***|***|
        XORShiftRand.MoveSeedRand(1);
    }

    void Start()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 開始時実行！！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        SetCandidatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        float air = m_playerIndex.GetAirParsent();
        bool clearAnime = m_directorIndex.GetClearAnimation();
        bool clearFlag = m_directorIndex.GetClearFlag();

        bool deathAnime = m_directorIndex.GetGameOverAnimation();
        bool deathFlag = m_directorIndex.GetGameOver();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツの場所更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateParts();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // おしまい
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (deathFlag && !clearAnime)
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
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート読み込み
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void UpdateReadFile()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 名前を取得
        //*|***|***|***|***|***|***|***|***|***|***|***|    
        m_fileConnectionCandidatePosition.SetFileName(m_fileName);
        m_fileConnectionCandidatePosition.ReaderFile();
        CsvBook book = m_fileConnectionCandidatePosition.GetBook();
        MakeListFile(book);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート書き出し
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void UpdateMakeFile()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 名前を取得
        //*|***|***|***|***|***|***|***|***|***|***|***|    
        m_fileConnectionCandidatePosition.SetFileName(m_fileName);
        List<string> data = MakeDataFile();
        m_fileConnectionCandidatePosition.SetListData(data);
        m_fileConnectionCandidatePosition.WriterFile();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void MakeListFile(CsvBook book)
    {
        Vector3 makeData;
        int partsPaze = book.GetBookPaze();
        int pazeWidth = 3;
        int partsNum = MyCalculator.Division(partsPaze, pazeWidth);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 削除して開始
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rocketPartsCandidatePosition.Clear();
        for (int paze = 0; paze < partsNum; paze++)
        {
            makeData = new Vector3();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 地点情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeData = book.GetValueVector3(pazeWidth, 0, paze);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 登録
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_rocketPartsCandidatePosition.Add(makeData);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    List<string> MakeDataFile()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 登録するデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        List<string> fileData = new List<string>();
        string makeString;
        float getDataFloat;

        for (int partsNum = 0; partsNum < m_rocketPartsCandidatePosition.Count; partsNum++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            getDataFloat = m_rocketPartsCandidatePosition[partsNum].x;
            makeString = getDataFloat.ToString();
            fileData.Add(makeString);
            getDataFloat = m_rocketPartsCandidatePosition[partsNum].y;
            makeString = getDataFloat.ToString();
            fileData.Add(makeString);
            getDataFloat = m_rocketPartsCandidatePosition[partsNum].z;
            makeString = getDataFloat.ToString();
            fileData.Add(makeString);
        }
        return fileData;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 情報交換だ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public List<Vector3> GetPointCandidatePosition()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return m_rocketPartsCandidatePosition;
    }
    public void SetPointCandidatePosition(List<Vector3> candidatePosition)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_rocketPartsCandidatePosition = candidatePosition;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 場所を設定する
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetCandidatePosition()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 開始時実行！！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateReadFile();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号シャッフル
        //*|***|***|***|***|***|***|***|***|***|***|***|
        List<int> suffleList = XORShiftRand.Shuffle(m_rocketPartsCandidatePosition.Count);
        int getIndexSuffle = 0;
        int getIndexData = 0;
        Vector3 getIndexPoint;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 番号を適応
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < (int)PartsID.NUM; index++)
        {
            getIndexPoint = new Vector3();
            if (suffleList.Count > 0)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 対応番号は？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                getIndexSuffle = ChangeData.AmongLess(index, 0, suffleList.Count);
                getIndexData = suffleList[getIndexSuffle];
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 対応番号に対する位置は？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                getIndexData = ChangeData.AmongLess(getIndexData, 0, m_rocketPartsCandidatePosition.Count);
                getIndexPoint = m_rocketPartsCandidatePosition[getIndexData];
            }
            else
            {
                getIndexPoint = Vector3.zero;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 位置を適用
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_rocketData[index].m_rocketPartsPosition = getIndexPoint;
        }


    }


    void OnApplicationQuit()
    {
        PlaySceneDirectorIndex.Remove();
        PlaySceneDirectorRocketIndex.Remove();
        PlayerDirectorIndex.Remove();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 倉庫の終了
        //*|***|***|***|***|***|***|***|***|***|***|***|
        WarehouseData.WarehouseObject.Remove();
        WarehouseData.WarehouseUnity.Remove();
        WarehouseData.PlayerData.WarehousePlayer.Remove();
    }
}
