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
        //[ExecuteInEditMode]
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

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤー本体データナンバー
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
                // プレイヤーのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                PLAYERBOAL_DAMAGE,
                BODYTOP_DAMAGE,
                ATTACKBOAL_DAMAGE,
                LEFTLEG_DAMAGE,
                RIGHTLEG_DAMAGE,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 総数
                //*|***|***|***|***|***|***|***|***|***|***|***|
                NUM,
            };

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤー本体データナンバー
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
            // プレイヤーその他データナンバー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public enum PlayerData_Another_Number_List
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // UIのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                AIR_GAUGE_MAIN,
                AIR_GAUGE_FRAME,
                AIR_GAUGE_SHADOW,

                STAR_GAUGE_MAIN,
                STAR_GAUGE_FRAME,
                STAR_GAUGE_SHADOW,

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
            // UIの倉庫 プレイヤー本体
            //*|***|***|***|***|***|***|***|***|***|***|***|
            Texture2D[] m_playerTex2D;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UIの倉庫 プレイヤーその他
            //*|***|***|***|***|***|***|***|***|***|***|***|
            Texture2D[] m_playerAnotherTex2D;



            private WarehousePlayer()
            {
                m_playerTex2D = new Texture2D[(int)PlayerData_Number.NUM];
                m_playerAnotherTex2D = new Texture2D[(int)PlayerData_Another_Number_List.NUM];
                ReadData();
            }
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
                    // プレイヤー本体
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
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // プレイヤーのデータ
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerTex2D[(int)PlayerData_Number.PLAYERBOAL_DAMAGE] = Resources.Load<Texture2D>(MyFile + "PlayerBoal");
                        m_playerTex2D[(int)PlayerData_Number.BODYTOP_DAMAGE] = Resources.Load<Texture2D>(MyFile + "BodyTop");
                        m_playerTex2D[(int)PlayerData_Number.ATTACKBOAL_DAMAGE] = Resources.Load<Texture2D>(MyFile + "AttackBoal");
                        m_playerTex2D[(int)PlayerData_Number.LEFTLEG_DAMAGE] = Resources.Load<Texture2D>(MyFile + "BodyBottom");
                        m_playerTex2D[(int)PlayerData_Number.RIGHTLEG_DAMAGE] = Resources.Load<Texture2D>(MyFile + "JointBoal");
                    }
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // プレイヤーその他
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    MyFile = PlayerData_Another;
                    {
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // UIのデータ
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.AIR_GAUGE_MAIN] = Resources.Load<Texture2D>(MyFile + "air_gauge3");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.AIR_GAUGE_FRAME] = Resources.Load<Texture2D>(MyFile + "air_gauge1");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.AIR_GAUGE_SHADOW] = Resources.Load<Texture2D>(MyFile + "air_gauge2");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.STAR_GAUGE_MAIN] = Resources.Load<Texture2D>(MyFile + "star_gauge3");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.STAR_GAUGE_FRAME] = Resources.Load<Texture2D>(MyFile + "star_gauge1");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.STAR_GAUGE_SHADOW] = Resources.Load<Texture2D>(MyFile + "star_gauge2");
                    }
                }
            }

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UIデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public Texture2D GetPlayerTexture2D(PlayerData_Number textureNum)
            {
                int type = ChangeData.AmongLess((int)textureNum, 0, (int)PlayerData_Number.NUM);
                return m_playerTex2D[type];
            }
            public Texture2D GetPlayerTexture2D(int type)
            {
                type = ChangeData.AmongLess(type, 0, (int)PlayerData_Number.NUM);
                return m_playerTex2D[type];
            }

            public Texture2D GetAnotherTexture2D(PlayerData_Another_Number_List textureNum)
            {
                int type = ChangeData.AmongLess((int)textureNum, 0, (int)PlayerData_Another_Number_List.NUM);
                return m_playerAnotherTex2D[type];
            }
            public Texture2D GetAnotherTexture2D(int type)
            {
                type = ChangeData.AmongLess(type, 0, (int)PlayerData_Another_Number_List.NUM);
                return m_playerAnotherTex2D[type];
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

