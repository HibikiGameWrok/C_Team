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
    public class WarehouseOrder
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // こいつはシングルトンだったんだ！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        private static WarehouseOrder m_warehouseObject = null;

        public static WarehouseOrder GetInstance()
        {
            if (m_warehouseObject == null)
            {
                m_warehouseObject = new WarehouseOrder();
            }
            return m_warehouseObject;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // シングルトン消滅
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public static void Remove()
        {
            if (m_warehouseObject != null)
            {
                m_warehouseObject = null;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー本体データナンバー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public enum Object_Order_Number
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ０
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ZERO,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 敵
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ENEMY,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            STAR,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ギミック
            //*|***|***|***|***|***|***|***|***|***|***|***|
            GIMMICK,
            GIMMICK_P1,
            GIMMICK_P2,
            GIMMICK_P3,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 床
            //*|***|***|***|***|***|***|***|***|***|***|***|
            FLOOR,
            FLOOR_P1,
            FLOOR_P2,
            FLOOR_P3,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 壁
            //*|***|***|***|***|***|***|***|***|***|***|***|
            WALL,
            WALL_P1,
            WALL_P2,
            WALL_P3,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 背景
            //*|***|***|***|***|***|***|***|***|***|***|***|
            BG,
            BG_P1,
            BG_P2,
            BG_P3,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            PLAYER,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 建築物
            //*|***|***|***|***|***|***|***|***|***|***|***|
            CONSTRUCTION_NORMAL,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 木
            //*|***|***|***|***|***|***|***|***|***|***|***|
            CONSTRUCTION_WOOD_SEA,
            CONSTRUCTION_WOOD_SEA_P1,
            CONSTRUCTION_WOOD_SEA_P2,
            CONSTRUCTION_WOOD_SEA_P3,
            CONSTRUCTION_WOOD_TOWN,
            CONSTRUCTION_WOOD_TOWN_P1,
            CONSTRUCTION_WOOD_TOWN_P2,
            CONSTRUCTION_WOOD_TOWN_P3,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // マンション
            //*|***|***|***|***|***|***|***|***|***|***|***|
            CONSTRUCTION_APARTMENT,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 滝
            //*|***|***|***|***|***|***|***|***|***|***|***|
            CONSTRUCTION_WATERFALL,
            CONSTRUCTION_WATERFALL_P1,
            CONSTRUCTION_WATERFALL_P2,
            CONSTRUCTION_WATERFALL_P3,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 総数
            //*|***|***|***|***|***|***|***|***|***|***|***|
            NUM,
        };
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データの倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public static string SimpleData = "SimpleImage/";
        public static string PlayerData = "PlayerData/";
        public static string PlayerData_Another = "PlayerData_Another/";

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // サブファイル名
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public static string Gauge_UI = "Gauge_UI/";
        public static string Recovery_UI = "Recovery_UI/";




        //*|***|***|***|***|***|***|***|***|***|***|***|
        // スプライトのレイヤー番号
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int[] m_orderToLayerSprite;



        private WarehouseOrder()
        {
            m_orderToLayerSprite = new int[(int)Object_Order_Number.NUM];
            ReadData();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データの入れどころ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        private void ReadData()
        {
            string SimpleFile;
            string MyFile;
            string SubFile;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UITexture2Dのデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレイヤー本体
                //*|***|***|***|***|***|***|***|***|***|***|***|
                SimpleFile = SimpleData;
                MyFile = PlayerData;
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // ０
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.ZERO] = 0;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 敵
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.ENEMY] = 30;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 星
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.STAR] = 50;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // ギミック
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.GIMMICK] = 20;
                    m_orderToLayerSprite[(int)Object_Order_Number.GIMMICK_P1] = 21;
                    m_orderToLayerSprite[(int)Object_Order_Number.GIMMICK_P2] = 22;
                    m_orderToLayerSprite[(int)Object_Order_Number.GIMMICK_P3] = 23;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 床
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.FLOOR] = 0;
                    m_orderToLayerSprite[(int)Object_Order_Number.FLOOR_P1] = 1;
                    m_orderToLayerSprite[(int)Object_Order_Number.FLOOR_P2] = 2;
                    m_orderToLayerSprite[(int)Object_Order_Number.FLOOR_P3] = 3;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 壁
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.WALL] = -10;
                    m_orderToLayerSprite[(int)Object_Order_Number.WALL_P1] = -9;
                    m_orderToLayerSprite[(int)Object_Order_Number.WALL_P2] = -8;
                    m_orderToLayerSprite[(int)Object_Order_Number.WALL_P3] = -7;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 背景
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.BG] = -20;
                    m_orderToLayerSprite[(int)Object_Order_Number.BG_P1] = -19;
                    m_orderToLayerSprite[(int)Object_Order_Number.BG_P2] = -18;
                    m_orderToLayerSprite[(int)Object_Order_Number.BG_P3] = -17;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // プレイヤー
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.PLAYER] = 40;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 建築物
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_NORMAL] = 10;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 木
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WOOD_SEA] = 10;
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WOOD_SEA_P1] = 11;
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WOOD_SEA_P2] = 12;
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WOOD_SEA_P3] = 13;
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WOOD_TOWN] = 10;
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WOOD_TOWN_P1] = 11;
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WOOD_TOWN_P2] = 12;
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WOOD_TOWN_P3] = 13;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // マンション
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_APARTMENT] = 14;
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 滝
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WATERFALL] = 15;
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WATERFALL_P1] = 16;
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WATERFALL_P2] = 17;
                    m_orderToLayerSprite[(int)Object_Order_Number.CONSTRUCTION_WATERFALL_P3] = 18;
                }
            }
        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public int GetOrderToLayerSprite(Object_Order_Number textureNum)
        {
            int type = ChangeData.AmongLess((int)textureNum, 0, (int)Object_Order_Number.NUM);
            return m_orderToLayerSprite[type];
        }
        public int GetOrderToLayerSprite(int type)
        {
            type = ChangeData.AmongLess(type, 0, (int)Object_Order_Number.NUM);
            return m_orderToLayerSprite[type];
        }
    }
}

