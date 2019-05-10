using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//*|***|***|***|***|***|***|***|***|***|***|***|
// 倉庫データ
//*|***|***|***|***|***|***|***|***|***|***|***|
namespace WarehouseData
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 倉庫データは眠らない
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //[ExecuteInEditMode]
    public class Warehouse
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadManagerScene()
        {
            WarehouseUnity.GetInstance();
            WarehouseObject.GetInstance();
            WarehouseData.PlayerData.WarehousePlayer.GetInstance();
        }
    }
}
