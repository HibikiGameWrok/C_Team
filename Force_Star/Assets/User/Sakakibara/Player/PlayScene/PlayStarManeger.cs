using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

//*|***|***|***|***|***|***|***|***|***|***|***|
// オーダー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehouseOrder = WarehouseData.WarehouseOrder;
using Object_Order_Number = WarehouseData.WarehouseOrder.Object_Order_Number;

//*|***|***|***|***|***|***|***|***|***|***|***|
// オブジェクト倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehouseObject = WarehouseData.WarehouseObject;

public class PlayStarManeger : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星オブジェクト(隠し)(ばらまく)
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_starObjOriginDiffusion = null;
    private StarPieceMove m_starObjOriginDiffusionMove = null;
    private Common_Order m_starObjOriginDiffusionOrder = null;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星オブジェクト(隠し)(跳ねる)
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_starObjOriginBounce = null;
    private StarPieceBounceMove m_starObjOriginBounceMove = null;
    private Common_Order m_starObjOriginBounceOrder = null;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星オブジェクト
    //*|***|***|***|***|***|***|***|***|***|***|***|

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星のレイヤーに変更
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameObject.layer = 12;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクト(隠し)
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starObjOriginDiffusion = new GameObject("starOriginDiffusion");
        m_starObjOriginDiffusionMove = m_starObjOriginDiffusion.AddComponent<StarPieceMove>();
        m_starObjOriginDiffusionOrder = m_starObjOriginDiffusion.AddComponent<Common_Order>();
        m_starObjOriginDiffusionOrder.SetNumber(Object_Order_Number.STAR);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクト(隠し)(跳ねる)
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starObjOriginBounce = new GameObject("starOriginBounce");
        m_starObjOriginBounceMove = m_starObjOriginBounce.AddComponent<StarPieceBounceMove>();
        m_starObjOriginBounceOrder = m_starObjOriginBounce.AddComponent<Common_Order>();
        m_starObjOriginBounceOrder.SetNumber(Object_Order_Number.STAR);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ここにしまう
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starObjOriginDiffusion.transform.parent = gameObject.transform;
        m_starObjOriginDiffusion.SetActive(false);
        m_starObjOriginBounce.transform.parent = gameObject.transform;
        m_starObjOriginBounce.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 指定位置に星のピースの生成関数
    // 引数(出す場所,星の取得数)
    public void CreateStarDiffusionPisce(Vector3 pos, int max)
    {
        GameObject newStarObj = null;
        StarPieceMove newStarObjMove = null;
        for (int i = 0; i < max; i++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 生成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj = Instantiate(m_starObjOriginDiffusion) as GameObject;
            newStarObj.SetActive(true);
            newStarObjMove = newStarObj.GetComponent<StarPieceMove>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ここにしまう
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj.transform.parent = transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 飛んでいく角度をランダムに決める
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObjMove.SetVec(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
            newStarObjMove.SetPosition(pos);
            newStarObjMove.SetSpeed(0.01f, 0.01f);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 角度の方向に発射
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void CreateStarDiffusionPisce(Vector3 pos, float angleCenter, float angleSwing, float speedMax, float speedMin, int starNum)
    {
        GameObject newStarObj = null;
        StarPieceMove newStarObjMove = null;
        float angle = 0;
        float angleMax = 0;
        float angleMin = 0;
        float speed = 0;
        Vector2 vec;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 生成機関
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int count = 0; count < starNum; count++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 生成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj = Instantiate(m_starObjOriginDiffusion) as GameObject;
            newStarObj.SetActive(true);
            newStarObjMove = newStarObj.GetComponent<StarPieceMove>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ランダム作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            angleMax = angleCenter + (angleSwing / 2.0f);
            angleMin = angleCenter - (angleSwing / 2.0f);
            angle = Random.Range(angleMin, angleMax);
            speed = Random.Range(speedMin, speedMax);
            vec = ChangeData.AngleDegToVector2(angle);
            vec *= speed;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ここにしまう
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj.transform.parent = transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 飛んでいく角度をランダムに決める
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObjMove.SetVec(vec.x, vec.y);
            newStarObjMove.SetPosition(pos);
            newStarObjMove.SetSpeed(0.01f, 0.01f);
        }
    }

    // 指定位置に星のピースの生成関数
    // 引数(出す場所,星の取得数)
    public void CreateStarBouncePisce(Vector3 pos, int max)
    {
        GameObject newStarObj = null;
        StarPieceBounceMove newStarObjMove = null;
        for (int i = 0; i < max; i++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 生成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj = Instantiate(m_starObjOriginBounce) as GameObject;
            newStarObj.SetActive(true);
            newStarObjMove = newStarObj.GetComponent<StarPieceBounceMove>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ここにしまう
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj.transform.parent = transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 飛んでいく角度をランダムに決める
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObjMove.SetVec(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
            newStarObjMove.SetPosition(pos);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 角度の方向に発射
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void CreateStarBouncePisce(Vector3 pos, float angleCenter, float angleSwing, float speedMax, float speedMin, int starNum)
    {
        GameObject newStarObj = null;
        StarPieceBounceMove newStarObjMove = null;
        float angle = 0;
        float angleMax = 0;
        float angleMin = 0;
        float speed = 0;
        Vector2 vec;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 生成機関
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int count = 0; count < starNum; count++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 生成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj = Instantiate(m_starObjOriginBounce) as GameObject;
            newStarObj.SetActive(true);
            newStarObjMove = newStarObj.GetComponent<StarPieceBounceMove>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ランダム作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            angleMax = angleCenter + (angleSwing / 2.0f);
            angleMin = angleCenter - (angleSwing / 2.0f);
            angle = Random.Range(angleMin, angleMax);
            speed = Random.Range(speedMin, speedMax);
            vec = ChangeData.AngleDegToVector2(angle);
            vec *= speed;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ここにしまう
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj.transform.parent = transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 飛んでいく角度をランダムに決める
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObjMove.SetVec(vec.x, vec.y);
            newStarObjMove.SetPosition(pos);
        }
    }
}
