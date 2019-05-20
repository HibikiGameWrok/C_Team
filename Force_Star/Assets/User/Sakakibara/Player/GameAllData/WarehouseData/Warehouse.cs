using System.Collections;
using System.Collections.Generic;
using System;
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

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 初期乱数
            //*|***|***|***|***|***|***|***|***|***|***|***|
            DateTime nowTime = DateTime.Now;
            int magnification = 1;
            int timeSend = 0;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 時間で初期シード作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            timeSend += nowTime.Millisecond * magnification;
            magnification *= 1000;
            timeSend += nowTime.Second * magnification;
            magnification *= 60;
            timeSend += nowTime.Minute * magnification;
            magnification *= 60;
            timeSend += nowTime.Hour * magnification;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 初期シードで乱数作成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            XORShiftRand.SetSeed(timeSend);


        }
    }
}
