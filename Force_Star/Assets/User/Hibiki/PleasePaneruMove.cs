using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleasePaneruMove : MonoBehaviour
{
    // プレイヤーのデータ倉庫
    private PlayerDirectorIndex playerIndex;

    // プレイシーンのデータ倉庫
    private PlaySceneDirectorIndex playDrectorIndex;

    // コンポーネント
    private SpriteRenderer spraite;

    private void Awake()
    {
        playDrectorIndex = PlaySceneDirectorIndex.GetInstance();
        playerIndex = PlayerDirectorIndex.GetInstance();
        spraite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIndex.GetHaveStarParsent() >= 1 && playerIndex.GetHaveAllPartsFlag())
        {
            spraite.color = new Color(1.0f,1.0f,1.0f,1.0f);
        }
        else
        {
            spraite.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 終了
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool clearAnime = playDrectorIndex.GetClearAnimation();
        if (clearAnime)
        {
            spraite.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
}
