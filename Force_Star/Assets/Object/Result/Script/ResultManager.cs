using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    //private O2BarCtr aliveFlag;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_directorIndex;

    public GameObject ClearManager;
    public GameObject OverManeger;

    //ゲームの合否判定
    [SerializeField]
    bool gameFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲームに生き残れたか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameFlag = m_directorIndex.GetAliveFlagPlayScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameFlag)
        {
            //クリアした時
            ClearManager.SetActive(true);
            OverManeger.SetActive(false);
        }
        else
        {
            //ゲームオーバーした時
            ClearManager.SetActive(false);
            OverManeger.SetActive(true);
        }
    }

    public bool GetClearFlag()
    {
        return gameFlag;
    }
}
