using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 拡張している時だけ
//*|***|***|***|***|***|***|***|***|***|***|***|
#if UNITY_EDITOR
using UnityEditor;
#endif

//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤーナンバー言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
using PlayerDataNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number;
using PlayerData_Number_List = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number_List;

using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;
using PartsData = GameDataPublic.PartsData;


//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤーの管理者であり
// プレイヤーのUI、状態を管理するものである
//*|***|***|***|***|***|***|***|***|***|***|***|
public partial class PlayerDirector : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_directorIndex;
    PlaySceneDirectorRocketIndex m_directorRocketIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーの実際のデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーここ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObject m_playerDirector;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObject m_playerCenter;
    PlayerMove m_playerMove;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツのデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [Serializable]
    public class ListPartsData
    {
        [SerializeField]
        public List<PartsData> listData;
    }
    [SerializeField]
    public ListPartsData m_listData;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アニメパーツのデータ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public class AnimePartsData
    {
        public GameObject objectData;
        public AnimePlayerSprite spriteData;
    }
    public List<AnimePartsData> m_listAnime;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アニメの制御
    //*|***|***|***|***|***|***|***|***|***|***|***|
    protected Animator m_myAnime;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツの実データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public class AllPartsData
    {
        public GameObject objectData;
        public GameObjectSprite spriteData;
    }
    public List<AllPartsData> m_listDataAll;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル関係
    //*|***|***|***|***|***|***|***|***|***|***|***|
    FileConnection m_fileConnection;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル名
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    string m_fileName;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクター登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        m_directorRocketIndex = PlaySceneDirectorRocketIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター登録
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        m_playerIndex.SetPointerPlayDirector(this);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerCenter = new GameObject("Player");
        m_playerDirector = gameObject;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー親子
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメの制御
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (gameObject.GetComponent<Animator>())
        {
            m_myAnime = gameObject.GetComponent<Animator>();
        }
        else
        {
            m_myAnime = gameObject.AddComponent<Animator>();
        }
        m_listData = new ListPartsData();
        m_listAnime = new List<AnimePartsData>();
        m_listDataAll = new List<AllPartsData>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツデータリストデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AllPartsData makeDataAllParts;
        AnimePartsData makeDataAnimeParts;
        PlayerData_Number_List partsListNum;
        //Common_GameObjectSprite_Order makeSpriteOrder;
        string partsName;
        m_listData.listData = new List<PartsData>();
        for (int partsNum = 0; partsNum < (int)PlayerData_Number_List.NUM; partsNum++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パーツの名
            //*|***|***|***|***|***|***|***|***|***|***|***|
            partsListNum = (PlayerData_Number_List)partsNum;
            partsName = partsListNum.ToString();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パーツのデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listData.listData.Add(new PartsData());
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パーツの実データ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeDataAllParts = new AllPartsData();



            makeDataAllParts.objectData = new GameObject(partsName);
            makeDataAllParts.spriteData = makeDataAllParts.objectData.AddComponent<GameObjectSprite>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤー親子
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeDataAllParts.objectData.transform.parent = m_playerCenter.transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 登録
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listDataAll.Add(makeDataAllParts);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // アニメパーツのデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeDataAnimeParts = new AnimePartsData();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 子どもが無い場合作成
            // 子どもがある場合取得
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeDataAnimeParts.objectData = FindChildName(partsName);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // アニメパーツが無い場合作成
            // アニメパーツがある場合取得
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeDataAnimeParts.spriteData = FindAnimePart(makeDataAnimeParts.objectData);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ここのプレイヤー親子
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeDataAnimeParts.spriteData.SetSprite(makeDataAllParts.spriteData);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 登録
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime.Add(makeDataAnimeParts);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツデータリスト親子設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            GameObject parent = null;
            GameObject child = null;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親            子
            // 体下          体上
            //*|***|***|***|***|***|***|***|***|***|***|***|
            parent = m_listDataAll[(int)PlayerData_Number_List.BODYBOTTOM].objectData;
            child = m_listDataAll[(int)PlayerData_Number_List.BODYTOP].objectData;
            child.transform.parent = parent.transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親            子
            // 体上          プレイヤーボール
            //*|***|***|***|***|***|***|***|***|***|***|***|
            parent = m_listDataAll[(int)PlayerData_Number_List.BODYTOP].objectData;
            child = m_listDataAll[(int)PlayerData_Number_List.PLAYERHEAD].objectData;
            child.transform.parent = parent.transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親            子
            // 体上          右腕関節
            //*|***|***|***|***|***|***|***|***|***|***|***|
            parent = m_listDataAll[(int)PlayerData_Number_List.BODYTOP].objectData;
            child = m_listDataAll[(int)PlayerData_Number_List.RARMJOINT].objectData;
            child.transform.parent = parent.transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親            子
            // 右腕関節       右腕
            //*|***|***|***|***|***|***|***|***|***|***|***|
            parent = m_listDataAll[(int)PlayerData_Number_List.RARMJOINT].objectData;
            child = m_listDataAll[(int)PlayerData_Number_List.RIGHTHAND].objectData;
            child.transform.parent = parent.transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親            子
            // 体上          左腕関節
            //*|***|***|***|***|***|***|***|***|***|***|***|
            parent = m_listDataAll[(int)PlayerData_Number_List.BODYTOP].objectData;
            child = m_listDataAll[(int)PlayerData_Number_List.LARMJOINT].objectData;
            child.transform.parent = parent.transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親            子
            // 左腕関節       左腕
            //*|***|***|***|***|***|***|***|***|***|***|***|
            parent = m_listDataAll[(int)PlayerData_Number_List.LARMJOINT].objectData;
            child = m_listDataAll[(int)PlayerData_Number_List.LEFTHAND].objectData;
            child.transform.parent = parent.transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親            子
            // 体下          右足関節
            //*|***|***|***|***|***|***|***|***|***|***|***|
            parent = m_listDataAll[(int)PlayerData_Number_List.BODYBOTTOM].objectData;
            child = m_listDataAll[(int)PlayerData_Number_List.RLEGJOINT].objectData;
            child.transform.parent = parent.transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親            子
            // 右足関節       右足
            //*|***|***|***|***|***|***|***|***|***|***|***|
            parent = m_listDataAll[(int)PlayerData_Number_List.RLEGJOINT].objectData;
            child = m_listDataAll[(int)PlayerData_Number_List.RIGHTLEG].objectData;
            child.transform.parent = parent.transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親            子
            // 体下          左足関節
            //*|***|***|***|***|***|***|***|***|***|***|***|
            parent = m_listDataAll[(int)PlayerData_Number_List.BODYBOTTOM].objectData;
            child = m_listDataAll[(int)PlayerData_Number_List.LLEGJOINT].objectData;
            child.transform.parent = parent.transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親            子
            // 左足関節       左足
            //*|***|***|***|***|***|***|***|***|***|***|***|
            parent = m_listDataAll[(int)PlayerData_Number_List.LLEGJOINT].objectData;
            child = m_listDataAll[(int)PlayerData_Number_List.LEFTLEG].objectData;
            child.transform.parent = parent.transform;
        }




        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイル関係
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string fileName = "Book";
        fileName = "PlayerPartsPointData.csv";
        string areaName = "";
        areaName = areaName + "PlayerData/";
        m_fileConnection = new FileConnection();
        m_fileConnection.SetFileName(fileName);
        m_fileConnection.SetAreaName(areaName);
        m_fileName = fileName;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー情報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakePlayer();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーUI情報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AwakePlayerUI();

    }



    //*|***|***|***|***|***|***|***|***|***|***|***|
    // nameと同じCHILDを返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObject FindChildName(string name)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 子どもを探して
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject getData = null;
        GameObject returnData = null;
        for (int number = 0; number < m_playerDirector.transform.childCount; number++)
        {
            getData = m_playerDirector.transform.GetChild(number).gameObject;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 同じ名前のものを検索する
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (getData.name == name)
            {
                returnData = getData;
                return returnData;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ここのプレイヤー親子
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return MakeChildName(name);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // nameと同じCHILDを返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    GameObject MakeChildName(string name)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ここのプレイヤー親子
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject returnData = new GameObject(name);
        returnData.transform.parent = m_playerDirector.transform;
        return returnData;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アニメパーツはもうあるか？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    AnimePlayerSprite FindAnimePart(GameObject data)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 子どもを探して
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AnimePlayerSprite sprite = data.GetComponent<AnimePlayerSprite>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スプライトは取得できているか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (sprite)
        {
            return sprite;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スプライトは取得できていないから作る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        sprite = data.AddComponent<AnimePlayerSprite>();
        return sprite;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start()
    {
        ReadTexStart();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 落下するプレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        FallPlayerStart();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // レイヤーの名前
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string animeName = m_myAnime.GetLayerName(0);
        m_myAnime.Play("Wait");
        m_myAnime.speed = 1;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 開始時実行！！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateReadFile();
        UpdateUnity();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクターに自らが狙われると名乗り出る
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex.SetObjectTargetCamera(m_playerCenter);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ReadTexStart()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツデータリストデータ初期設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int partsNum = 0; partsNum < m_listDataAll.Count; partsNum++)
        {

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 設定
            //*|***|***|***|***|***|***|***|***|***|***|***|

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listData.listData[partsNum].dataNum = PlayerDataNum.BLANK;
            m_listData.listData[partsNum].size = Vector2.one;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listData.listData[partsNum].localPos = Vector3.zero;
            m_listData.listData[partsNum].imagePos = Vector3.zero;
            m_listData.listData[partsNum].localAngle = Vector3.zero;
            m_listData.listData[partsNum].imageAngle = Vector3.zero;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listData.listData[partsNum].depth = 0;

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 反映
            //*|***|***|***|***|***|***|***|***|***|***|***|

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[partsNum].spriteData.m_dataNum = (int)m_listData.listData[partsNum].dataNum;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[partsNum].spriteData.m_size = m_listData.listData[partsNum].size;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[partsNum].spriteData.m_localPos = m_listData.listData[partsNum].localPos;
            m_listAnime[partsNum].spriteData.m_imagePos = m_listData.listData[partsNum].imagePos;
            m_listAnime[partsNum].spriteData.m_localAngle = m_listData.listData[partsNum].localAngle;
            m_listAnime[partsNum].spriteData.m_imageAngle = m_listData.listData[partsNum].imageAngle;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[partsNum].spriteData.m_depth = m_listData.listData[partsNum].depth;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {


        if (m_fall.end)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤー情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            UpdatePlayer();
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 落下するプレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            FallUpdatePlayer();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーUI情報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdatePlayerUI();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲームオーバーの状況起因処理
        //*|***|***|***|***|***|***|***|***|***|***|***|
        StateBasedAction();

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲームオーバーの状況起因処理
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void StateBasedAction()
    {
        bool partsArive = true;
        bool airArive = true;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツの一つでも耐久０か
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ耐久チェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataBace.GetArmDurable() == 0.0f)
        {
            partsArive = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ耐久チェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataBace.GetBodyDurable() == 0.0f)
        {
            partsArive = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ耐久チェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataBace.GetHeadDurable() == 0.0f)
        {
            partsArive = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ耐久チェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataBace.GetLegDurable() == 0.0f)
        {
            partsArive = false;
        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 空気チェック
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_dataBace.GetTimeParsent() == 0.0f)
        {
            airArive = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 死
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (!partsArive || !airArive)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ゲームオーバーを予告する
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_directorIndex.SetGameOverAnimation();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ゲームオーバーを遂行する
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GameOverPlayer();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void FixedUpdate()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーUI情報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        FixedUpdatePlayerUI();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void LateUpdate()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー情報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        LateUpdatePlayer();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート読み込み
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void UpdateReadFile()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 名前を取得
        //*|***|***|***|***|***|***|***|***|***|***|***|    
        m_fileConnection.SetFileName(m_fileName);
        m_fileConnection.ReaderFile();
        CsvBook book = m_fileConnection.GetBook();
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
        m_fileConnection.SetFileName(m_fileName);
        List<string> data = MakeDataFile();
        m_fileConnection.SetListData(data);
        m_fileConnection.WriterFile();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void MakeListFile(CsvBook book)
    {
        PartsData makeData = null;
        int partsPaze = book.GetBookPaze();
        int pazeWidth = 7;
        int partsNum = MyCalculator.Division(partsPaze, pazeWidth);

        m_listData.listData.Clear();
        for (int paze = 0; paze < partsNum; paze++)
        {
            makeData = new PartsData();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 番号情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeData.dataNum = (PlayerDataNum)book.GetValueInt(pazeWidth, 0, paze);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeData.localPos = book.GetValueVector3(pazeWidth, 1, paze);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画サイズ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeData.size = book.GetValueVector2(pazeWidth, 4, paze);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画順
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeData.depth = book.GetValueInt(pazeWidth, 6, paze);
            m_listData.listData.Add(makeData);
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
        int getDataInt;
        float getDataFloat;

        for (int partsNum = 0; partsNum < m_listData.listData.Count; partsNum++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 番号情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            getDataInt = (int)m_listData.listData[partsNum].dataNum;
            makeString = getDataInt.ToString();
            fileData.Add(makeString);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所情報
            //*|***|***|***|***|***|***|***|***|***|***|***|
            getDataFloat = m_listData.listData[partsNum].localPos.x;
            makeString = getDataFloat.ToString();
            fileData.Add(makeString);
            getDataFloat = m_listData.listData[partsNum].localPos.y;
            makeString = getDataFloat.ToString();
            fileData.Add(makeString);
            getDataFloat = m_listData.listData[partsNum].localPos.z;
            makeString = getDataFloat.ToString();
            fileData.Add(makeString);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画サイズ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            getDataFloat = m_listData.listData[partsNum].size.x;
            makeString = getDataFloat.ToString();
            fileData.Add(makeString);
            getDataFloat = m_listData.listData[partsNum].size.y;
            makeString = getDataFloat.ToString();
            fileData.Add(makeString);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画順
            //*|***|***|***|***|***|***|***|***|***|***|***|
            getDataFloat = m_listData.listData[partsNum].depth;
            makeString = getDataFloat.ToString();
            fileData.Add(makeString);
        }
        return fileData;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート親優先
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void UpdateUnity()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 名前を取得
        //*|***|***|***|***|***|***|***|***|***|***|***|    
        for (int partsNum = 0; partsNum < m_listData.listData.Count; partsNum++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 反映
            //*|***|***|***|***|***|***|***|***|***|***|***|

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[partsNum].spriteData.m_dataNum = (int)m_listData.listData[partsNum].dataNum;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[partsNum].spriteData.m_size = m_listData.listData[partsNum].size;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[partsNum].spriteData.m_localPos = m_listData.listData[partsNum].localPos;
            m_listAnime[partsNum].spriteData.m_imagePos = m_listData.listData[partsNum].imagePos;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 角度を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[partsNum].spriteData.m_localAngle = m_listData.listData[partsNum].localAngle;
            m_listAnime[partsNum].spriteData.m_imageAngle = m_listData.listData[partsNum].imageAngle;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[partsNum].spriteData.m_depth = m_listData.listData[partsNum].depth;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 謎
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void UpdateHushigi()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭だけ変更
        //*|***|***|***|***|***|***|***|***|***|***|***|    
        if (Input.GetKey(KeyCode.H))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // アニメ実行
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_myAnime.SetBool("Hushigi", true);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 実に素晴らしい顔をしている。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[(int)PlayerData_Number_List.PLAYERHEAD].spriteData.m_dataNum = (int)PlayerDataNum.PLAYERBOAL_HUSHIGI;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // アニメ実行
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_myAnime.SetBool("Hushigi", false);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // やったぜ。
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void UpdateYeah()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭だけ変更
        //*|***|***|***|***|***|***|***|***|***|***|***|    
        if (Input.GetKey(KeyCode.Y))
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // アニメ実行
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_myAnime.SetBool("Yeah", true);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 実に素晴らしい顔をしている。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[(int)PlayerData_Number_List.PLAYERHEAD].spriteData.m_dataNum = (int)PlayerDataNum.PLAYERBOAL_YEAH;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // アニメ実行
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_myAnime.SetBool("Yeah", false);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 白々しい
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void UpdateWhite()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 名前を取得
        //*|***|***|***|***|***|***|***|***|***|***|***|    
        for (int partsNum = 0; partsNum < m_listData.listData.Count; partsNum++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // まるで透明みたいだ。
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listAnime[partsNum].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツが白々しい
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void UpdateWhiteArm()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // まるで透明みたいだ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕の先
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_listAnime[(int)PlayerData_Number_List.RIGHTHAND].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
        m_listAnime[(int)PlayerData_Number_List.LEFTHAND].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕の関節
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_listAnime[(int)PlayerData_Number_List.RARMJOINT].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
        m_listAnime[(int)PlayerData_Number_List.LARMJOINT].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
    }
    private void UpdateWhiteBody()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // まるで透明みたいだ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_listAnime[(int)PlayerData_Number_List.BODYTOP].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
        m_listAnime[(int)PlayerData_Number_List.BODYBOTTOM].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
    }
    private void UpdateWhiteHead()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // まるで透明みたいだ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_listAnime[(int)PlayerData_Number_List.PLAYERHEAD].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
    }
    private void UpdateWhiteLeg()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // まるで透明みたいだ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚の先
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_listAnime[(int)PlayerData_Number_List.RIGHTLEG].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
        m_listAnime[(int)PlayerData_Number_List.LEFTLEG].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚の関節
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_listAnime[(int)PlayerData_Number_List.RLEGJOINT].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
        m_listAnime[(int)PlayerData_Number_List.LLEGJOINT].spriteData.m_dataNum = (int)PlayerDataNum.BLANK;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 全てとおさらば。
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ClearAllFlags()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発とおさらば。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_exprosionArm.SetActive(false);
        m_exprosionBody.SetActive(false);
        m_exprosionHead.SetActive(false);
        m_exprosionLeg.SetActive(false);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 爆発とおさらば。
        //*|***|***|***|***|***|***|***|***|***|***|***|


    }

}
