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
// オブジェクトのID
//*|***|***|***|***|***|***|***|***|***|***|***|
using TheObjectID = System.Int32;
using TheObjectIDUnsinged = System.UInt32;

//*|***|***|***|***|***|***|***|***|***|***|***|
// DebugPlayerは眠らない
//*|***|***|***|***|***|***|***|***|***|***|***|
//[ExecuteInEditMode]
public partial class DebugPlayer : MonoBehaviour
{

    //[SerializeField]
    private Vector3 vector;
    //[SerializeField]
    private float speed;
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
    DebugPlayerMove m_playerMove;
    [SerializeField]
    public bool m_updateFlag;
    [SerializeField]
    public bool m_updateAnime;
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
    [Serializable]
    public class AnimePartsData
    {
        public GameObject objectData;
        public AnimePlayerSprite spriteData;
        [SerializeField]
        public TheObjectID objectId;
        [SerializeField]
        public TheObjectIDUnsinged objectUId;
        [SerializeField]
        public string objectGUID;
    }
    [SerializeField]
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
    // ファイル名
    //*|***|***|***|***|***|***|***|***|***|***|***|
    DebugText m_text;
    float m_time = 0;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コントローラー情報を転換せよ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    DebugPlayerController m_controller;
    [SerializeField]
    public int m_animeNum = 0;
    private bool m_nextMirror = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerCenter = new GameObject("Player");
        m_playerDirector = gameObject;
        m_updateFlag = false;
        m_updateAnime = true;
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
            makeDataAnimeParts.objectId = makeDataAnimeParts.objectData.GetInstanceID();
            uint plush = (uint)(makeDataAnimeParts.objectId);
            plush += int.MaxValue;
            makeDataAnimeParts.objectUId = (uint)makeDataAnimeParts.objectId;
            makeDataAnimeParts.objectUId = (uint)makeDataAnimeParts.objectId;
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



        m_text = gameObject.GetComponent<DebugText>();


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
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePlayer()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーコントローラーを作成せよ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controller = new DebugPlayerController();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーを飾り付けよ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerMove = m_playerCenter.AddComponent<DebugPlayerMove>();
        m_playerMove.LinkController(m_controller);


        m_nextMirror = false;
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
        int num = 0;
        string data;
        num = 20;
        data = num.ToString();
        m_text.textData = data;
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー情報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdatePlayer();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーUI情報
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdatePlayerUI();

        if (Input.GetKeyDown(KeyCode.A))
        {
            m_updateFlag = !m_updateFlag;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_updateAnime = !m_updateAnime;
        }


        if(m_updateFlag)
        {
            UpdateUnity();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdatePlayer()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 操作更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_controller.Update();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメ更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        AnimeStudyFrild();



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
    // アニメの制御
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AnimeStudyFrild()
    {
        bool groundFlag = m_playerMove.GetGroundFlag();

        bool addPowerFlag = m_playerMove.GetAddPowerFlag();
        bool moveingPowerFlag = m_playerMove.GetMoveingPowerFlag();
        bool reverseArrowFlag = m_playerMove.GetReverseArrowFlag();
        bool rightArrow = m_playerMove.GetRightArrowFlag();
        m_myAnime.speed = 1;

        if (m_updateAnime)
        {
            m_myAnime.SetInteger("MoveEnum", 0);
            if (m_controller.ChackStickMove())
            {
                m_myAnime.SetInteger("MoveEnum", 1);
            }
            if (m_controller.ChackAttack())
            {
                m_myAnime.SetInteger("MoveEnum", 2);
            }
            if (m_controller.ChackJump())
            {
                m_myAnime.SetInteger("MoveEnum", 3);
            }
            if (m_controller.ChackStart())
            {
                m_myAnime.SetInteger("MoveEnum", 4);
            }
            m_myAnime.SetBool("ClipLand", groundFlag);
            m_myAnime.SetBool("addPowerFlag", addPowerFlag);
            m_myAnime.SetBool("moveingPowerFlag", moveingPowerFlag);
            m_myAnime.SetBool("reverseArrowFlag", reverseArrowFlag);
        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体の向きを反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_nextMirror)
        {
            m_playerCenter.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            m_playerCenter.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        m_nextMirror = rightArrow;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アニメの制御
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void AnimeStudyGUID(List<string> GUIDDataList)
    {
        int maxValue = (int)PlayerData_Number_List.NUM;
        if (GUIDDataList.Count < maxValue)
        {
            maxValue = GUIDDataList.Count;
        }
        if (m_listAnime.Count < maxValue)
        {
            maxValue = m_listAnime.Count;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // IDを取得
        //*|***|***|***|***|***|***|***|***|***|***|***|   
        for (int index = 0; index < maxValue; index++)
        {
            m_listAnime[index].objectGUID = GUIDDataList[index];
        }
        int infoCount = m_myAnime.GetCurrentAnimatorClipInfoCount(0);
        AnimatorClipInfo[] l_info = m_myAnime.GetCurrentAnimatorClipInfo(0);
        AnimationClip l_clip = null;
        for (int index = 0; index < infoCount; index++)
        {
            l_clip = l_info[index].clip;


            //l_clip.
            //l_clip.
        }



    }

    
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //// アップデート子優先
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //public void UpdateReadFile()
    //{
    //    //*|***|***|***|***|***|***|***|***|***|***|***|
    //    // 名前を取得
    //    //*|***|***|***|***|***|***|***|***|***|***|***|    
    //    m_fileConnection.SetFileName(m_fileName);
    //    m_fileConnection.ReaderFile();
    //    CsvBook book = m_fileConnection.GetBook();
    //    MakeListFile(book);
    //}
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //// アップデート親優先
    ////*|***|***|***|***|***|***|***|***|***|***|***|
    //public void UpdateMakeFile()
    //{
    //    //*|***|***|***|***|***|***|***|***|***|***|***|
    //    // 名前を取得
    //    //*|***|***|***|***|***|***|***|***|***|***|***|    
    //    m_fileConnection.SetFileName(m_fileName);
    //    List<string> data = MakeDataFile();
    //    m_fileConnection.SetListData(data);
    //    m_fileConnection.WriterFile();
    //}
}
////*|***|***|***|***|***|***|***|***|***|***|***|
//// 個別拡張
////*|***|***|***|***|***|***|***|***|***|***|***|
//// 拡張している時だけ
////*|***|***|***|***|***|***|***|***|***|***|***|
//#if UNITY_EDITOR
////*|***|***|***|***|***|***|***|***|***|***|***|
//// Inspector拡張クラス
////*|***|***|***|***|***|***|***|***|***|***|***|
//[CustomEditor(typeof(DebugSceneDirector->PartsData))]               //!< 拡張するときのお決まりとして書いてね
//public class CharacterEditor : Editor           //!< Editorを継承するよ！
//{
//    bool folding = false;

//    public override void OnInspectorGUI()
//    {
//        // target は処理コードのインスタンスだよ！ 処理コードの型でキャストして使ってね！
//        Character chara = target as Character;

//        /* -- カスタム表示 -- */

//        // -- 体力 --
//        EditorGUILayout.LabelField("体力(現在/最大)");
//        EditorGUILayout.BeginHorizontal();
//        chara.m_hp_now = EditorGUILayout.IntField(chara.m_hp_now, GUILayout.Width(48));
//        chara.m_hp_max = EditorGUILayout.IntField(chara.m_hp_max, GUILayout.Width(48));
//        EditorGUILayout.EndHorizontal();

//        // -- 速度 --
//        chara.m_spd = EditorGUILayout.FloatField("速度", chara.m_spd);

//        // -- 名前 --
//        chara.m_name = EditorGUILayout.TextField("名前", chara.m_name);

//        // -- 友達 --
//        List<GameObject> list = chara.m_friends;
//        int i, len = list.Count;

//        // 折りたたみ表示
//        if (folding = EditorGUILayout.Foldout(folding, "友達"))
//        {
//            // リスト表示
//            for (i = 0; i < len; ++i)
//            {
//                list[i] = EditorGUILayout.ObjectField(list[i], typeof(GameObject), true) as GameObject;
//            }

//            GameObject go = EditorGUILayout.ObjectField("追加", null, typeof(GameObject), true) as GameObject;
//            if (go != null)
//                list.Add(go);
//        }
//    }
//}
//#endif

//string animeName = m_myAnime.GetLayerName(0);
//m_myAnime.ApplyBuiltinRootMotion();
//AnimatorStateInfo state = m_myAnime.GetCurrentAnimatorStateInfo(0);
//m_myAnime.Play(0);
//m_myAnime.Play("Wait");
//bool getBool = m_myAnime.is
//m_myAnime.speed = 1;
//m_myAnime.speed = 1;
//m_myAnime.Play("Player");
//m_myAnime.
//if (characterController.isGrounded)
//{
//    velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
//    if (velocity.magnitude > 0.1f)
//    {
//        animator.SetFloat("Speed", velocity.magnitude);
//        transform.LookAt(transform.position + velocity);
//    }
//    else
//    {
//        animator.SetFloat("Speed", 0f);
//    }
//}
//velocity.y += Physics.gravity.y * Time.deltaTime;
//characterController.Move(velocity * walkSpeed * Time.deltaTime);
//m_myAnime.Play();
//m_playerBoalObject.AddComponent<BoxCollider2D>();
//m_bodyTopObject.AddComponent<BoxCollider2D>();
//m_bodyBottomObject.AddComponent<BoxCollider2D>();
//int infoCount = m_myAnime.GetCurrentAnimatorClipInfoCount(0);
//AnimatorClipInfo[] l_info = m_myAnime.GetCurrentAnimatorClipInfo(0);
//AnimationClip l_clip = null;
//for (int index = 0; index < infoCount; index++)
//{
//    l_clip = l_info[index].clip;
//    //l_clip.
//    //l_clip.
//}
//m_myAnime.Play("test1", 0, 0.0f);
//AnimationClip[] l_clips =  AnimationUtility.GetAnimationClips(gameObject);
//AnimationUtility.Get
//AnimationClip 
//Animation m_anime1 = m_myAnime.GetCurrentAnimatorClipInfoCount();