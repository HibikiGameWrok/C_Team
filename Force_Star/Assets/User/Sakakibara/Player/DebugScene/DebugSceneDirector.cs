using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TexImageData = GameDataPublic.TexImageData;
using RenderImageData = GameDataPublic.RenderImageData;
using PartsData = GameDataPublic.PartsData;


public class DebugSceneDirector : MonoBehaviour
{

    List<GameObjectSprite> m_listGameObjectSprite;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 倉庫を作るのだ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_listGameObjectSprite = new List<GameObjectSprite>();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start()
    {
        ReadTex();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {


    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void ReadTex()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 倉庫を作るのだ。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject newGameObject = null;
        GameObjectSprite newSptite = null;
        RenderImageData newRender = null;
        TexImageData texImageData = new TexImageData();

        Texture2D image = Resources.Load<Texture2D>(WarehouseData.WarehouseObject.SimpleImage + "Basis_Red");
        Vector3 point = Vector3.zero;
        float pointX = 0;
        float pointY = 0;

        for (int index = 0; index < 30; index++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 基礎を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newGameObject = new GameObject();
            newSptite = newGameObject.AddComponent<GameObjectSprite>();
            m_listGameObjectSprite.Add(newSptite);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージを作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            texImageData.image = image;
            texImageData.rextParsent = MyCalculator.RectSize(0, 1, 1, 1, 1);
            texImageData.size = Vector2.one;
            newSptite.SetImageUpdate(texImageData);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 場所を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            pointX = MyCalculator.Get10Digit(index, 0);
            pointY = MyCalculator.Get10Digit(index, 1);
            point.x = pointX - ((10 - 1) * 0.5f);
            point.y = pointY * -1;
            newSptite.SetPosition(point);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 描画を作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newRender = new RenderImageData();
            newRender.depth = -10;
            newSptite.SetRenderUpdate(newRender);
        }


    }
}
