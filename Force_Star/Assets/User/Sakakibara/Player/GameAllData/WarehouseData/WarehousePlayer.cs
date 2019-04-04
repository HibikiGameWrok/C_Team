using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//*|***|***|***|***|***|***|***|***|***|***|***|
// 倉庫データ
//*|***|***|***|***|***|***|***|***|***|***|***|
namespace WarehouseData
{
    namespace PlayerData
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 倉庫データは眠らない
        //*|***|***|***|***|***|***|***|***|***|***|***|
        [ExecuteInEditMode]
        public class WarehousePlayer
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // こいつはシングルトンだったんだ！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            private static WarehousePlayer m_warehouseObject = null;

            public static WarehousePlayer GetInstance()
            {
                if (m_warehouseObject == null)
                {
                    m_warehouseObject = new WarehousePlayer();
                }
                return m_warehouseObject;
            }

            private WarehousePlayer()
            {
                m_playerTex2D = new Texture2D[(int)PlayerData_Number.NUM];

                ReadData();
            }

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // データナンバー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public enum PlayerData_Number
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 全てで使うデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                BLANK,
                FULL,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレイヤーのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                PLAYERBOAL,
                BODYTOP,
                BODYBOTTOM,
                JOINTBOAL,
                ATTACKBOAL,
                LEFTLEG,
                RIGHTLEG,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 総数
                //*|***|***|***|***|***|***|***|***|***|***|***|
                NUM,
            };

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // データナンバー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public enum PlayerData_Number_List
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレイヤーのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                PLAYERHEAD,
                BODYTOP,
                BODYBOTTOM,
                LARMJOINT,
                RARMJOINT,
                LEFTHAND,
                RIGHTHAND,
                LLEGJOINT,
                RLEGJOINT,
                LEFTLEG,
                RIGHTLEG,
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




            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UIの倉庫
            //*|***|***|***|***|***|***|***|***|***|***|***|

            Texture2D[] m_playerTex2D;



            //*|***|***|***|***|***|***|***|***|***|***|***|
            // データの入れどころ
            //*|***|***|***|***|***|***|***|***|***|***|***|


            private void ReadData()
            {
                string SimpleFile;
                string MyFile;

                //*|***|***|***|***|***|***|***|***|***|***|***|
                // UITexture2Dのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // PlayerDataのデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    SimpleFile = SimpleData;
                    MyFile = PlayerData;
                    {
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // 全てで使うデータ
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerTex2D[(int)PlayerData_Number.FULL] = Resources.Load<Texture2D>(SimpleFile + "Full");
                        m_playerTex2D[(int)PlayerData_Number.BLANK] = Resources.Load<Texture2D>(SimpleFile + "Blank");
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // プレイヤーのデータ
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerTex2D[(int)PlayerData_Number.PLAYERBOAL] = Resources.Load<Texture2D>(MyFile + "PlayerBoal");
                        m_playerTex2D[(int)PlayerData_Number.BODYTOP] = Resources.Load<Texture2D>(MyFile + "BodyTop");
                        m_playerTex2D[(int)PlayerData_Number.BODYBOTTOM] = Resources.Load<Texture2D>(MyFile + "BodyBottom");
                        m_playerTex2D[(int)PlayerData_Number.JOINTBOAL] = Resources.Load<Texture2D>(MyFile + "JointBoal");
                        m_playerTex2D[(int)PlayerData_Number.ATTACKBOAL] = Resources.Load<Texture2D>(MyFile + "AttackBoal");
                        m_playerTex2D[(int)PlayerData_Number.LEFTLEG] = Resources.Load<Texture2D>(MyFile + "LeftLeg");
                        m_playerTex2D[(int)PlayerData_Number.RIGHTLEG] = Resources.Load<Texture2D>(MyFile + "RightLeg");
                    }
                }
            }

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UIデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public Texture2D GetTexture2D(PlayerData_Number textureNum)
            {
                int type = ChangeData.AmongLess((int)textureNum, 0, (int)PlayerData_Number.NUM);
                return m_playerTex2D[type];
            }
            public Texture2D GetUITexture(int type)
            {
                type = ChangeData.AmongLess(type, 0, (int)PlayerData_Number.NUM);
                return m_playerTex2D[type];
            }




        }
    }
}
//PlayerBoal
//BodyTop
//BodyBottom
//JointBoal
//AttackBoal
//LeftLeg
//RightLeg

