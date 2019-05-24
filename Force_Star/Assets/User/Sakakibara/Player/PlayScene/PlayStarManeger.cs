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
    //private StarPieceMove m_starObjOriginDiffusionMove = null;
    //private Common_GameObjectSprite_Order m_starObjOriginDiffusionOrder = null;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星オブジェクト(隠し)(跳ねる)
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_starObjOriginBounce = null;
    //private StarPieceBounceMove m_starObjOriginBounceMove = null;
    //private Common_GameObjectSprite_Order m_starObjOriginBounceOrder = null;
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
        //m_starObjOriginDiffusionMove = m_starObjOriginDiffusion.AddComponent<StarPieceMove>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクトレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //m_starObjOriginDiffusionOrder = m_starObjOriginDiffusion.AddComponent<Common_GameObjectSprite_Order>();
        //m_starObjOriginDiffusionOrder.SetBoth(m_starObjOriginDiffusionMove.GetSpriteData(), Object_Order_Number.STAR);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクト(隠し)(跳ねる)
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starObjOriginBounce = new GameObject("starOriginBounce");
        //m_starObjOriginBounceMove = m_starObjOriginBounce.AddComponent<StarPieceBounceMove>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクトレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //m_starObjOriginBounceOrder = m_starObjOriginBounce.AddComponent<Common_GameObjectSprite_Order>();
        //m_starObjOriginBounceOrder.SetBoth(m_starObjOriginBounceMove.GetSpriteData(), Object_Order_Number.STAR);
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクトの動き
        //*|***|***|***|***|***|***|***|***|***|***|***|
        StarPieceMove starDiffusionMove = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクトのレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Common_GameObjectSprite_Order starDiffusionOrder = null;

        for (int i = 0; i < max; i++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 生成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj = Instantiate(m_starObjOriginDiffusion) as GameObject;
            newStarObj.SetActive(true);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星オブジェクトの動き
            //*|***|***|***|***|***|***|***|***|***|***|***|
            starDiffusionMove = newStarObj.AddComponent<StarPieceMove>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星オブジェクトのレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            starDiffusionOrder = newStarObj.AddComponent<Common_GameObjectSprite_Order>();
            starDiffusionOrder.SetBoth(starDiffusionMove.GetSpriteData(), Object_Order_Number.STAR);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ここにしまう
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj.transform.parent = transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 飛んでいく角度をランダムに決める
            //*|***|***|***|***|***|***|***|***|***|***|***|
            starDiffusionMove.SetVec(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
            starDiffusionMove.SetPosition(pos);
            starDiffusionMove.SetSpeed(0.01f, 0.01f);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 角度の方向に発射
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void CreateStarDiffusionPisce(Vector3 pos, float angleCenter, float angleSwing, float speedMax, float speedMin, int starNum)
    {
        GameObject newStarObj = null;
        float angle = 0;
        float angleMax = 0;
        float angleMin = 0;
        float speed = 0;
        Vector2 vec;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクトの動き
        //*|***|***|***|***|***|***|***|***|***|***|***|
        StarPieceMove starDiffusionMove = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクトのレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Common_GameObjectSprite_Order starDiffusionOrder = null;
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
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星オブジェクトの動き
            //*|***|***|***|***|***|***|***|***|***|***|***|
            starDiffusionMove = newStarObj.AddComponent<StarPieceMove>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星オブジェクトのレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            starDiffusionOrder = newStarObj.AddComponent<Common_GameObjectSprite_Order>();
            starDiffusionOrder.SetBoth(starDiffusionMove.GetSpriteData(), Object_Order_Number.STAR);
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
            starDiffusionMove.SetVec(vec.x, vec.y);
            starDiffusionMove.SetPosition(pos);
            starDiffusionMove.SetSpeed(0.01f, 0.01f);
        }
    }

    // 指定位置に星のピースの生成関数
    // 引数(出す場所,星の取得数)
    public void CreateStarBouncePisce(Vector3 pos, int max)
    {
        GameObject newStarObj = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクトの動き
        //*|***|***|***|***|***|***|***|***|***|***|***|
        StarPieceBounceMove starBounceMove = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクトのレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Common_GameObjectSprite_Order starBounceOrder = null;

        for (int i = 0; i < max; i++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 生成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj = Instantiate(m_starObjOriginBounce) as GameObject;
            newStarObj.SetActive(true);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星オブジェクトの動き
            //*|***|***|***|***|***|***|***|***|***|***|***|
            starBounceMove = newStarObj.AddComponent<StarPieceBounceMove>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星オブジェクトのレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            starBounceOrder = newStarObj.AddComponent<Common_GameObjectSprite_Order>();
            starBounceOrder.SetBoth(starBounceMove.GetSpriteData(), Object_Order_Number.STAR);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ここにしまう
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj.transform.parent = transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 飛んでいく角度をランダムに決める
            //*|***|***|***|***|***|***|***|***|***|***|***|
            starBounceMove.SetVec(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
            starBounceMove.SetPosition(pos);
            starBounceMove.SetSpeed(0.01f, 0.01f);
            starBounceMove.SetTime(300.0f, 200.0f);
            starBounceMove.SetTimeCount(15);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 角度の方向に発射
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void CreateStarBouncePisce(Vector3 pos, float angleCenter, float angleSwing, float speedMax, float speedMin, float timeMax, float timeLevel, int starNum)
    {
        GameObject newStarObj = null;
        float angle = 0;
        float angleMax = 0;
        float angleMin = 0;
        float speed = 0;
        Vector2 vec;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクトの動き
        //*|***|***|***|***|***|***|***|***|***|***|***|
        StarPieceBounceMove starBounceMove = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクトのレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Common_GameObjectSprite_Order starBounceOrder = null;
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
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星オブジェクトの動き
            //*|***|***|***|***|***|***|***|***|***|***|***|
            starBounceMove = newStarObj.AddComponent<StarPieceBounceMove>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星オブジェクトのレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            starBounceOrder = newStarObj.AddComponent<Common_GameObjectSprite_Order>();
            starBounceOrder.SetBoth(starBounceMove.GetSpriteData(), Object_Order_Number.STAR);
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
            starBounceMove.SetVec(vec.x, vec.y);
            starBounceMove.SetPosition(pos);
            starBounceMove.SetSpeed(0.01f, 0.01f);
            starBounceMove.SetTime(timeMax, timeLevel);
            starBounceMove.SetTimeCount(15);
        }
    }
}
