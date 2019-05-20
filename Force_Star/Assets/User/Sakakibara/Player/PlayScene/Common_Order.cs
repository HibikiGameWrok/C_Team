using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// オーダー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehouseOrder = WarehouseData.WarehouseOrder;
using Object_Order_Number = WarehouseData.WarehouseOrder.Object_Order_Number;


public class Common_Order : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    WarehouseOrder m_warehouseOrder;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // オブジェクトの番号
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Object_Order_Number m_number = Object_Order_Number.ZERO;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 起動
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // これの情報を選択
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int order = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーダー倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_warehouseOrder = WarehouseOrder.GetInstance();
        order = m_warehouseOrder.GetOrderToLayerSprite(m_number);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーダー設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer)
        {
            renderer.sortingOrder = order;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetNumber(Object_Order_Number number)
    {
        m_number = number;
        AwakeSecond();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 更新
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakeSecond()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // これの情報を選択
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int order = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーダー倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_warehouseOrder = WarehouseOrder.GetInstance();
        order = m_warehouseOrder.GetOrderToLayerSprite(m_number);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーダー設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer)
        {
            renderer.sortingOrder = order;
        }
    }
}
