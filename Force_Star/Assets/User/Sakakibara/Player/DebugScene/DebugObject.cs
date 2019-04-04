using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObject : GameObjectSprite
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        OriginAwake();

        
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 開始時
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Start()
    {
        m_texImageData.Reset();
        m_texImageData.rextParsent = MyCalculator.RectSize(0, 1, 1, 1, 1);
        m_texImageData.image = Resources.Load<Texture2D>(WarehouseData.WarehouseObject.SimpleImage + "Basis_Red");
        MakeImage();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // アップデート
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Update()
    {

    }
}
