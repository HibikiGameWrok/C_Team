using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWall : MonoBehaviour
{
    [SerializeField]
    private GameObject WaterWallObject=null;　　　// 滝の水の部分

    [SerializeField]
    private int MaxWater = 1;                     //　最初の滝の長さ
    [SerializeField]
    private float Createcount = 3.0f;                     //　滝の生成速度

    private List<GameObject> myList = new List<GameObject>();   // 滝の格納用(通常時)

    private List<GameObject> sMyList = new List<GameObject>();  // 滝の格納用(追加分)

    private GameObject WaterWallClone = null;                   // 滝のインスタンス用

    float count;                                                // 滝の生成のタイマー用

    bool HitPW = false;                                         // 滝が割られているか

    Vector3 StartPosition;                                      // 滝の最初生成位置
    // Start is called before the first frame update
    void Start()
    {
        // 初期位置
        StartPosition = transform.position;
        if (WaterWallObject!=null)
        {
            //StartPosition.y = StartPosition.y - WaterWallObject.GetComponent<Renderer>().bounds.size.y * (MaxWater + 1);
            // 長さ分滝を作る
            for (int i=0;i<MaxWater;i++)
            {
                WaterWallClone = Instantiate(WaterWallObject, StartPosition, Quaternion.identity) as GameObject;    // インスタンス
                WaterWallClone.transform.localScale = transform.localScale;                                         // 根本と滝の大きさを合わせる
                WaterWallClone.transform.position=new Vector3(WaterWallClone.transform.position.x, WaterWallClone.transform.position.y-WaterWallClone.GetComponent<Renderer>().bounds.size.y*(MaxWater-i), WaterWallClone.transform.position.z); // 下から順に滝を作る
               // StartPosition.y = StartPosition.y - WaterWallObject.GetComponent<Renderer>().bounds.size.y * (MaxWater - i);
                WaterWallClone.transform.parent = transform;                                                        // 根元を親にする
                myList.Add(WaterWallClone);                                                                         // リストに登録
            }
        }
        count = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        count+=0.1f;
        // タイマーが生成を超えたら
        if(count> Createcount)
        {
            //　次足し用の滝を作る
            WaterWallClone = Instantiate(WaterWallObject, transform.position, Quaternion.identity) as GameObject;
            WaterWallClone.transform.localScale = transform.localScale;
            WaterWallClone.transform.position = new Vector3(WaterWallClone.transform.position.x, WaterWallClone.transform.position.y - WaterWallClone.GetComponent<Renderer>().bounds.size.y, WaterWallClone.transform.position.z);
            WaterWallClone.transform.parent = transform;
            myList.Add(WaterWallClone);
            count = 0.0f;
        }
    }

    public void WaterList(GameObject Wobject)
    {
        // オブジェクトが通常時のリストに入っているか確認(要素無しで-1が帰ってくる)
        if(myList.IndexOf(Wobject)>-1)
        {
            // 対応した要素のオブジェクトを消して消した要素を詰める
            int i = myList.IndexOf(Wobject);
            Destroy(myList[i]);
            myList.RemoveAt(i);
        }
        else　　　　　　　　　// 追加分ならこっち
        {
            // 対応した要素のオブジェクトを消して消した要素を詰める
            int i = sMyList.IndexOf(Wobject);
            Destroy(sMyList[i]);
            sMyList.RemoveAt(i);
        }

    }
    public void WaterListAt(GameObject Wobject)
    {
        // オブジェクトが通常時のリストに入っているか確認(要素無しで-1が帰ってくる)
        if (myList.IndexOf(Wobject) > -1)
        {
            // 当たったオブジェクトから下の滝は消す
            for (int i = 0; i < myList.IndexOf(Wobject) + 1; i++)
            {
                Destroy(myList[i]);
            }
            // 追加分が存在したら消す
            if (sMyList.Count > 0)
            {
                for (int i = 0; i < sMyList.Count; i++)
                {
                    Debug.Log(sMyList.Count);
                    Debug.Log(i);
                    Destroy(sMyList[i]);
                    Debug.Log(i);
                }
                // 詰める
                for (int i = 0; i < sMyList.Count; i++)
                {
                    Debug.Log(i);
                    sMyList.RemoveAt(i);
                }
                //   sMyList.RemoveAll(sMyList => sMyList == sMyList);
                //sMyList.RemoveAt(sMyList.Count);
            }
            // 通常時を消したところから詰める
            myList.RemoveAll(myList => myList == Wobject);            
        }
        else　　　　　　　// 追加分ならこっち
        {
            // 当たったオブジェクトから下の滝は消す
            for (int i = 0; i < sMyList.IndexOf(Wobject) + 1; i++)
            {
                Destroy(sMyList[i]);
            }
            // 消したところから詰める
            sMyList.RemoveAll(sMyList => sMyList == Wobject);
        }
        // 滝がプレイヤーに当たって敵が割れたのでtrueを入れておく
        HitPW = true;

    }

    public void SetSList(GameObject obj)
    {
        // 初期位置
        StartPosition = obj.transform.position;
        // 滝の下からプレイヤーがいなくなったので滝を埋める
        for (int i = 0; i < MaxWater; i++)
        {
            WaterWallClone = Instantiate(WaterWallObject, StartPosition, Quaternion.identity) as GameObject;
            WaterWallClone.transform.localScale = transform.localScale;
            WaterWallClone.transform.position = new Vector3(WaterWallClone.transform.position.x, WaterWallClone.transform.position.y - WaterWallClone.GetComponent<Renderer>().bounds.size.y * (i + 1), WaterWallClone.transform.position.z);
            WaterWallClone.transform.parent = transform;
            sMyList.Add(WaterWallClone);                // 追加分に入れる
        }
        HitPW = false;                                  // 滝が割れたことをなかったことにする
        sMyList.Reverse();                              // 要素を逆にする
    }
    public bool CheckListNum(GameObject obj)
    {
        // なにもしていない滝が勝手に追加分を作るのを止める
        if (myList.IndexOf(obj) > -1)
        {
            int i = myList.IndexOf(obj);
            if (i == 0)
            {
                return true;
            }
        }
        else
        {
            int i = sMyList.IndexOf(obj);
            if (i == 0)
            {
                return false;
            }
        }

        //int i = myList.IndexOf(obj);
        return false;
    }
    public bool GetHitCheck()
    {
        return HitPW;           // 割れてる？
    }
    public void SetHitflag(bool flag)
    {
        HitPW = flag;           // 割ったかどうか
    }
}
