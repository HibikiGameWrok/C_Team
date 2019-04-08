using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDirector : MonoBehaviour {

    public GameObject star; //星
    StarMove starCreate;
    ParticleMove particleSet;
    public GameObject playerObject;
    public float starX;

    // なくてもいい
    public float correctionX;
    float vecX1;
    float vecX2;
    float vecY;

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        // 仮のポジション
        vecX1 = playerObject.transform.position.x + playerObject.GetComponent<Renderer>().bounds.size.x / 2 + correctionX;
        vecX2 = playerObject.transform.position.x - playerObject.GetComponent<Renderer>().bounds.size.x / 2 - correctionX;
        vecY = playerObject.transform.position.y - playerObject.GetComponent<Renderer>().bounds.size.y / 2;
    }

    // 仮の床用星作成関数
    public void CreateStar()
    {
        GameObject go = Instantiate(star) as GameObject;
        GameObject go2 = Instantiate(star) as GameObject;

        starCreate = go.GetComponent<StarMove>();
        particleSet= go.transform.Find("StarParticle").GetComponent<ParticleMove>();
        particleSet.SetGameObject(playerObject);
        starCreate.SetVecX(starX);

        starCreate = go2.GetComponent<StarMove>();
        particleSet = go2.transform.Find("StarParticle").GetComponent<ParticleMove>();
        particleSet.SetGameObject(playerObject);
        starCreate.SetVecX(-starX);

        go.transform.position = new Vector3(vecX1, vecY + go.GetComponent<Renderer>().bounds.size.y / 2, go.transform.position.z);
        go2.transform.position = new Vector3(vecX2, vecY + go.GetComponent<Renderer>().bounds.size.y / 2, go.transform.position.z);
    }

    public void CreateStar(int max)
    {
        GameObject go = Instantiate(star) as GameObject;
        GameObject go2 = Instantiate(star) as GameObject;

        starCreate = go.GetComponent<StarMove>();
        particleSet = go.transform.Find("StarParticle").GetComponent<ParticleMove>();
        starCreate.SetMaxStar(max);
        particleSet.SetGameObject(playerObject);
        starCreate.SetVecX(starX);

        starCreate = go2.GetComponent<StarMove>();
        particleSet = go2.transform.Find("StarParticle").GetComponent<ParticleMove>();
        starCreate.SetMaxStar(max);
        particleSet.SetGameObject(playerObject);
        starCreate.SetVecX(-starX);

        go.transform.position = new Vector3(vecX1, vecY + go.GetComponent<Renderer>().bounds.size.y / 2, go.transform.position.z);
        go2.transform.position = new Vector3(vecX2, vecY + go.GetComponent<Renderer>().bounds.size.y / 2, go.transform.position.z);
    }


    // 外部から出現位置と星の取得数を入力し生成する関数　
    // 引数(星1の位置,星2の位置,星の取得数)
    public void CreateStar(Vector2 objectPosR, Vector2 objectPosL,int maxStar)
    {
        // 星の生成
        GameObject go = Instantiate(star) as GameObject;
        GameObject go2 = Instantiate(star) as GameObject;
        // 星1
        starCreate = go.GetComponent<StarMove>();
        particleSet = go.transform.Find("StarParticle").GetComponent<ParticleMove>();
        particleSet.SetGameObject(playerObject);   // パーティクル用のオブジェクト渡し
        starCreate.SetVecX(starX);         // 横移動の向き
        starCreate.SetMaxStar(maxStar);    // 星の所有数
        // 星2
        starCreate = go2.GetComponent<StarMove>();
        particleSet = go2.transform.Find("StarParticle").GetComponent<ParticleMove>();
        particleSet.SetGameObject(playerObject);   // パーティクル用のオブジェクト渡し
        starCreate.SetVecX(-starX);        // 横移動の向き
        starCreate.SetMaxStar(maxStar);    // 星の所有数
        // 配置
        go.transform.position = new Vector3(objectPosR.x, objectPosR.y, go.transform.position.z);       //　右の星
        go2.transform.position = new Vector3(objectPosL.x, objectPosL.y, go.transform.position.z);      //　左の星
    }

    // 外部から出現位置と星の取得数を入力し生成する関数(壁衝突用)
    // 引数(星1の位置,星2の位置,星の取得数,X軸の方向(flase:左　true:右))
    public void CreateStar(Vector2 objectPosT, Vector2 objectPosB, int maxStar, bool flag )
    {
        // 星の生成
        GameObject go = Instantiate(star) as GameObject;
        GameObject go2 = Instantiate(star) as GameObject;

        //  flaseであれば左へ
        if (!flag)
            starX = -starX;

        // 星1
        starCreate = go.GetComponent<StarMove>();
        particleSet = go.transform.Find("StarParticle").GetComponent<ParticleMove>();
        particleSet.SetGameObject(playerObject);   // パーティクル用のオブジェクト渡し
        starCreate.SetVecX(starX);         // 横移動の向き
        starCreate.SetMaxStar(maxStar);    // 星の所有数
        // 星2
        starCreate = go2.GetComponent<StarMove>();
        particleSet = go2.transform.Find("StarParticle").GetComponent<ParticleMove>();
        particleSet.SetGameObject(playerObject);    // パーティクル用のオブジェクト渡し
        starCreate.SetVecX(starX);         // 横移動の向き
        starCreate.SetMaxStar(maxStar);    // 星の所有数

        // 配置
        go.transform.position = new Vector3(objectPosT.x, objectPosT.y, go.transform.position.z);   //　右の星
        go2.transform.position = new Vector3(objectPosB.x, objectPosB.y, go.transform.position.z);  //　左の星

    }

    // 外部から出現位置と星の取得数を入力し生成する関数(壁衝突,ジャンプ力を外部で操作する用)
    // 引数(星1の位置,星2の位置,星の取得数,X軸の方向(flase:左　true:右),星1のジャンプ力,星2のジャンプ力)
    public void CreateStar(Vector2 objectPosT, Vector2 objectPosB, int maxStar, bool flag,float jump1,float jump2)
    {
        // 星の生成
        GameObject go = Instantiate(star) as GameObject;
        GameObject go2 = Instantiate(star) as GameObject;

        //  flaseであれば左へ
        if (!flag)
            starX = -starX;

        // 星1
        starCreate = go.GetComponent<StarMove>();
        particleSet = go.transform.Find("StarParticle").GetComponent<ParticleMove>();
        particleSet.SetGameObject(playerObject); // パーティクル用のオブジェクト渡し
        starCreate.SetVecX(starX);         // 横移動の向き
        starCreate.SetMaxStar(maxStar);    // 星の所有数
        starCreate.SetJumpF(jump1);        // 星1のジャンプ力
        // 星2
        starCreate = go2.GetComponent<StarMove>();
        particleSet = go2.transform.Find("StarParticle").GetComponent<ParticleMove>();
        particleSet.SetGameObject(playerObject);   // パーティクル用のオブジェクト渡し
        starCreate.SetVecX(starX);         // 横移動の向き
        starCreate.SetMaxStar(maxStar);    // 星の所有数
        starCreate.SetJumpF(jump2);        // 星1のジャンプ力

        // 配置
        go.transform.position = new Vector3(objectPosT.x, objectPosT.y, go.transform.position.z);   //　右の星
        go2.transform.position = new Vector3(objectPosB.x, objectPosB.y, go.transform.position.z);  //　左の星

    }

    public float GetStarSize()
    {
        return star.GetComponent<Renderer>().bounds.size.y / 2;
    }
}
