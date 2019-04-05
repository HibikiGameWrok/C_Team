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
// DebugPlayerは眠らない
//*|***|***|***|***|***|***|***|***|***|***|***|
//[ExecuteInEditMode]
public class DebugPlayer : MonoBehaviour
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
    [SerializeField]
    public bool m_updateFlag;
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
        public PlayerDataNum playerDataNum;
        public TexImageData texImageData;
        public RenderImageData renderImageData;
        public Vector3 localPos;
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

    public Vector3 m_mistary;
    public PartsData m_mistary2;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {

        m_mistary = new Vector3();
        m_mistary2 = new PartsData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerCenter = new GameObject("Player");
        m_playerDirector = gameObject;
        m_updateFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー親子
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //m_playerBoalObject.transform.parent = m_playerCenter.transform;
        //m_bodyTopObject.transform.parent = m_playerCenter.transform;
        //m_bodyBottomObject.transform.parent = m_playerCenter.transform;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // アニメの制御
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if(gameObject.GetComponent<Animator>())
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
            makeDataAllParts.playerDataNum = PlayerDataNum.BLANK;
            makeDataAllParts.texImageData = new TexImageData();
            makeDataAllParts.renderImageData = new RenderImageData();
            makeDataAllParts.localPos = new Vector3();
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
            makeDataAnimeParts.objectData = new GameObject(partsName);
            makeDataAnimeParts.spriteData = makeDataAnimeParts.objectData.AddComponent<AnimePlayerSprite>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ここのプレイヤー親子
            //*|***|***|***|***|***|***|***|***|***|***|***|
            makeDataAnimeParts.objectData.transform.parent = m_playerDirector.transform;
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
        fileName = "SavePosBook3.txt";
        string areaName = "Assets\\Resources\\";
        areaName = areaName + "PlayerData\\";
        m_fileConnection = new FileConnection();
        m_fileConnection.SetFileName(fileName);
        m_fileConnection.SetAreaName(areaName);
        m_fileName = fileName;

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start()
    {
        ReadTexStart();
        m_myAnime.Play("Player");
        //m_playerBoalObject.AddComponent<BoxCollider2D>();
        //m_bodyTopObject.AddComponent<BoxCollider2D>();
        //m_bodyBottomObject.AddComponent<BoxCollider2D>();

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 開始時実行！！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateReadFile();
        ReadTex();
        UpdateUnity();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    UpdateReadFile();
        //}
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    UpdateMakeFile();
        //}
        ReadTex();
        AnimeStudyFrild();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_updateFlag = !m_updateFlag;
        }
        if(m_updateFlag)
        {
            UpdateUnity();
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ReadTexStart()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツデータリストデータ初期設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        WarehousePlayer warehousePlayer = WarehousePlayer.GetInstance();
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
            m_listDataAll[partsNum].playerDataNum = m_listData.listData[partsNum].dataNum;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listDataAll[partsNum].texImageData.image = warehousePlayer.GetTexture2D(m_listDataAll[partsNum].playerDataNum);
            m_listDataAll[partsNum].texImageData.rextParsent = MyCalculator.RectSize(0, 1, 1, 1, 1);
            m_listDataAll[partsNum].texImageData.size = m_listData.listData[partsNum].size;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listDataAll[partsNum].localPos = m_listData.listData[partsNum].localPos;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listDataAll[partsNum].renderImageData.depth = m_listData.listData[partsNum].depth;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ReadTex()
    {

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツデータリストデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        WarehousePlayer warehousePlayer = WarehousePlayer.GetInstance();
        for (int partsNum = 0; partsNum < m_listDataAll.Count; partsNum++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 反映
            //*|***|***|***|***|***|***|***|***|***|***|***|

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listDataAll[partsNum].playerDataNum = m_listData.listData[partsNum].dataNum;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listDataAll[partsNum].texImageData.image = warehousePlayer.GetTexture2D(m_listDataAll[partsNum].playerDataNum);
            m_listDataAll[partsNum].texImageData.rextParsent = MyCalculator.RectSize(0, 1, 1, 1, 1);
            m_listDataAll[partsNum].texImageData.size = m_listData.listData[partsNum].size;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listDataAll[partsNum].localPos = m_listData.listData[partsNum].localPos;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_listDataAll[partsNum].renderImageData.depth = m_listData.listData[partsNum].depth;

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 実施
            //*|***|***|***|***|***|***|***|***|***|***|***|

            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// イメージを作成
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //m_listDataAll[partsNum].spriteData.SetImageUpdate(m_listDataAll[partsNum].texImageData);
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// 場所を作成
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //m_listDataAll[partsNum].spriteData.SetPositionLocal(m_listDataAll[partsNum].localPos);
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// 描画を作成
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //m_listDataAll[partsNum].spriteData.SetRenderUpdate(m_listDataAll[partsNum].renderImageData);

        }


    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ReadTexDX()
    {

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
        m_myAnime.SetInteger("MoveEnum", 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_myAnime.SetInteger("MoveEnum", 1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            m_myAnime.SetInteger("MoveEnum", 3);
        }
        if (Input.GetKey(KeyCode.C))
        {
            m_myAnime.SetInteger("MoveEnum", 2);
        }


        //m_myAnime.SetBool("Right", false);
        //m_myAnime.SetBool("Left", false);
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