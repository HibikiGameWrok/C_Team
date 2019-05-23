using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDirector : MonoBehaviour
{

    [SerializeField]
    private GameObject Star = null; // 星保存用


    PlaySceneDirectorIndex m_playIndex;
    PlayerDirectorIndex m_playerIndex;

    private GameObject StarObj = null; // 星オブジェクト

    // public GameObject star; //星
    StarMove starCreate;
    //public GameObject playerObject;
    public float starX;

    // なくてもいい
    public float correctionX;
    float vecX1;
    float vecX2;
    float vecY;

    // Use this for initialization
    void Start()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイシーン共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        //// 仮のポジション
        //vecX1 = playerObject.transform.position.x + playerObject.GetComponent<Renderer>().bounds.size.x / 2 + correctionX;
        //vecX2 = playerObject.transform.position.x - playerObject.GetComponent<Renderer>().bounds.size.x / 2 - correctionX;
        //vecY = playerObject.transform.position.y - playerObject.GetComponent<Renderer>().bounds.size.y / 2;
    }
   
    // 仮の床用星作成関数
    public void CreateStar()
    {
        //GameObject go = Instantiate(star) as GameObject;
        //GameObject go2 = Instantiate(star) as GameObject;

        //starCreate = go.GetComponent<StarMove>();
        //starCreate.SetVecX(starX);

        //starCreate = go2.GetComponent<StarMove>();
        //starCreate.SetVecX(-starX);

        //go.transform.position = new Vector3(vecX1, vecY + go.GetComponent<Renderer>().bounds.size.y / 2, go.transform.position.z);
        //go2.transform.position = new Vector3(vecX2, vecY + go.GetComponent<Renderer>().bounds.size.y / 2, go.transform.position.z);
    }

    public void CreateStar(int max)
    {
        //GameObject go = Instantiate(star) as GameObject;
        //GameObject go2 = Instantiate(star) as GameObject;

        //starCreate = go.GetComponent<StarMove>();
        //starCreate.SetMaxStar(max);
        //starCreate.SetVecX(starX);

        //starCreate = go2.GetComponent<StarMove>();
        //starCreate.SetMaxStar(max);
        //starCreate.SetVecX(-starX);

        //go.transform.position = new Vector3(vecX1, vecY + go.GetComponent<Renderer>().bounds.size.y / 2, go.transform.position.z);
        //go2.transform.position = new Vector3(vecX2, vecY + go.GetComponent<Renderer>().bounds.size.y / 2, go.transform.position.z);
    }


    // 外部から出現位置と星の取得数を入力し生成する関数　
    // 引数(星1の位置,星2の位置,星の取得数)
    public void CreateStar(Vector2 objectPosR, Vector2 objectPosL, int maxStar)
    {

        if (Star != null)
        {
            for (int i = 0; i < maxStar; i++)
            {
                StarObj = Instantiate(Star, objectPosR, Quaternion.identity) as GameObject; // 生成
                StarObj.transform.parent = transform;                                // 子にする
                float vecX = Random.Range(-0.2f, 0.2f);
                while (vecX == 0)                       // 0ならもっかい
                {
                    vecX = Random.Range(-0.2f, 0.2f);
                }
                StarObj.GetComponent<StarMove>().SetVecX(vecX); // 飛んでいく角度をランダムに決める
            }
        }
    }


    // 外部から出現位置と星の取得数を入力し生成する関数(壁衝突用)
    // 引数(星1の位置,星2の位置,星の取得数,X軸の方向(flase:左　true:右))
    public void CreateStar(Vector2 objectPosT, Vector2 objectPosB, int maxStar, bool flag)
    {
        // 星の生成
        GameObject go = Instantiate(Star) as GameObject;
        GameObject go2 = Instantiate(Star) as GameObject;

        //  flaseであれば左へ
        if (!flag)
            starX = -starX;

        // 星1
        starCreate = go.GetComponent<StarMove>();
        starCreate.SetVecX(starX);         // 横移動の向き
        starCreate.SetMaxStar(maxStar);    // 星の所有数
        // 星2
        starCreate = go2.GetComponent<StarMove>();
        starCreate.SetVecX(starX);         // 横移動の向き
        starCreate.SetMaxStar(maxStar);    // 星の所有数

        // 配置
        go.transform.position = new Vector3(objectPosT.x, objectPosT.y, go.transform.position.z);   //　右の星
        go2.transform.position = new Vector3(objectPosB.x, objectPosB.y, go.transform.position.z);  //　左の星

    }

    // 外部から出現位置と星の取得数を入力し生成する関数(壁衝突,ジャンプ力を外部で操作する用)
    // 引数(星1の位置,星2の位置,星の取得数,X軸の方向(flase:左　true:右),星1のジャンプ力,星2のジャンプ力)
    public void CreateStar(Vector2 objectPosT, Vector2 objectPosB, int maxStar, bool flag, float jump1, float jump2)
    {
        // 星の生成
        GameObject go = Instantiate(Star) as GameObject;
        GameObject go2 = Instantiate(Star) as GameObject;

        //  flaseであれば左へ
        if (!flag)
            starX = -starX;

        // 星1
        starCreate = go.GetComponent<StarMove>();
        starCreate.SetVecX(starX);         // 横移動の向き
        starCreate.SetMaxStar(maxStar);    // 星の所有数
        starCreate.SetJumpF(jump1);        // 星1のジャンプ力
        // 星2
        starCreate = go2.GetComponent<StarMove>();
        starCreate.SetVecX(starX);         // 横移動の向き
        starCreate.SetMaxStar(maxStar);    // 星の所有数
        starCreate.SetJumpF(jump2);        // 星1のジャンプ力

        // 配置
        go.transform.position = new Vector3(objectPosT.x, objectPosT.y, go.transform.position.z);   //　右の星
        go2.transform.position = new Vector3(objectPosB.x, objectPosB.y, go.transform.position.z);  //　左の星

    }


    // 星を個別で作るようの関数
    // 外部から出現位置と星の取得数を入力し生成する関数(壁衝突,ジャンプ力を外部で操作する用)
    // 引数(星の位置,星の取得数,X軸の方向(flase:左　true:右),最初のジャンプ力)
    public void CreateOneStar(Vector2 pos, int maxStar, bool flag, float jump,bool center=false)
    {
        if(!center)
        {
            if (m_playerIndex.GetPlayerPosition().x < pos.x)
            {
                m_playIndex.ApplyStarBounceRightSide(pos, 45.0f, 0.2f, 0.02f, maxStar);
            }
            else
            {
                m_playIndex.ApplyStarBounceLeftSide(pos, 45.0f, 0.2f, 0.02f, maxStar);
            }
        }
        else
        {
            m_playIndex.ApplyStarBounce(pos, maxStar);
        }
      
    }

    public void CreateOneStar(Vector2 pos, int maxStar, bool center = false)
    {

        if (!center)
        {
            if (m_playerIndex.GetPlayerPosition().x < pos.x)
            {
                Debug.Log("a");
                m_playIndex.ApplyStarBounceRightSide(pos, 90.0f, 0.2f,0.1f, maxStar);
            }
            else
            {
                m_playIndex.ApplyStarBounceLeftSide(pos, 45.0f, 0.2f, 0.02f, maxStar);
            }
        }
        else
        {
            m_playIndex.ApplyStarBounce(pos, maxStar);
        }
    }


    // 星の画像サイズの半分の取得用関数
    public float GetStarSize()
    {
        return Star.GetComponent<Renderer>().bounds.size.y / 2;
    }
    // 星のジャンプ力の取得用関数
    public float GetStarJump()
    {
        return Star.GetComponent<StarMove>().GetJumpF();
    }
}
