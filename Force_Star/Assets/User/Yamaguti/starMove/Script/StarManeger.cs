using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject Star = null; // 星保存用
    [SerializeField]
    private GameObject Player = null; // プレイヤー保存用

    private GameObject StarObj = null; // 星オブジェクト
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
    public void CreateStarPisce(Vector3 pos,int max)
    {
        if (Star != null)
        {
            for (int i = 0; i < max; i++)
            {
                StarObj = Instantiate(Star, pos, Quaternion.identity) as GameObject; // 生成
                StarObj.transform.parent = transform;                                // 子にする
                StarObj.GetComponent<StarPieceMove>().SetVec(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f)); // 飛んでいく角度をランダムに決める
            }
        }
    }

    // プレイヤーを渡す関数
    public GameObject GetPlayer()
    {
        return Player;
    }
}
