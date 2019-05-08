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
                // ゲージのUIのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                AIR_GAUGE_MAIN,
                AIR_GAUGE_FRAME,
                AIR_GAUGE_SHADOW,

                STAR_GAUGE_MAIN,
                STAR_GAUGE_FRAME,
                STAR_GAUGE_SHADOW,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 回復のUIのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                RECOVERYUI_ALLBODY,
                RECOVERYUI_BACKBLACK,
                RECOVERYUI_BODYLINE,
                RECOVERYUI_HEALARM,
                RECOVERYUI_HEALBACK,
                RECOVERYUI_HEALBODY,
                RECOVERYUI_HEALHEAD,
                RECOVERYUI_HEALLEG,
                RECOVERYUI_REDARM,
                RECOVERYUI_REDBODY,
                RECOVERYUI_REDHEAD,
                RECOVERYUI_REDLEG,
                RECOVERYUI_RETURN,

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
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // ゲージのUIのデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    SubFile = Gauge_UI;
                    {
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.AIR_GAUGE_MAIN] = Resources.Load<Texture2D>(MyFile + SubFile + "air_gauge3");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.AIR_GAUGE_FRAME] = Resources.Load<Texture2D>(MyFile + SubFile + "air_gauge1");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.AIR_GAUGE_SHADOW] = Resources.Load<Texture2D>(MyFile + SubFile + "air_gauge2");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.STAR_GAUGE_MAIN] = Resources.Load<Texture2D>(MyFile + SubFile + "star_gauge3");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.STAR_GAUGE_FRAME] = Resources.Load<Texture2D>(MyFile + SubFile + "star_gauge1");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.STAR_GAUGE_SHADOW] = Resources.Load<Texture2D>(MyFile + SubFile + "star_gauge2");
                    }
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // ゲージのUIのデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    SubFile = Recovery_UI;
                    {
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_ALLBODY] = Resources.Load<Texture2D>(MyFile + SubFile + "UIAllBody");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_BACKBLACK] = Resources.Load<Texture2D>(MyFile + SubFile + "UIBackBlack");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_BODYLINE] = Resources.Load<Texture2D>(MyFile + SubFile + "UIBodyLine");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALARM] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealArm");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALBACK] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealBack");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALBODY] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealBody");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALHEAD] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealHead");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALLEG] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealLeg");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_REDARM] = Resources.Load<Texture2D>(MyFile + SubFile + "UIRedArm");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_REDBODY] = Resources.Load<Texture2D>(MyFile + SubFile + "UIRedBody");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_REDHEAD] = Resources.Load<Texture2D>(MyFile + SubFile + "UIRedHead");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_REDLEG] = Resources.Load<Texture2D>(MyFile + SubFile + "UIRedLeg");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_RETURN] = Resources.Load<Texture2D>(MyFile + SubFile + "UIReturn");
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
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // タグの名前
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 攻撃ボールのタグ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static string Tag_AttackBoal = "AttackBoal";
            public static string GetTag_AttackBoal()
            {
                return Tag_AttackBoal;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーがダメージを受けるのタグ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static string Tag_PlayerHitParts = "PlayerHitParts";
            public static string GetTag_PlayerHitParts()
            {
                return Tag_PlayerHitParts;
            }

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーがダメージを受けるのタグ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static string Tag_PlayerHitArmParts = "PlayerHitArmParts";
            public static string GetTag_PlayerHitArmParts()
            {
                return Tag_PlayerHitArmParts;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーがダメージを受けるのタグ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static string Tag_PlayerHitBodyParts = "PlayerHitBodyParts";
            public static string GetTag_PlayerHitBodyParts()
            {
                return Tag_PlayerHitBodyParts;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーがダメージを受けるのタグ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static string Tag_PlayerHitHeadParts = "PlayerHitHeadParts";
            public static string GetTag_PlayerHitHeadParts()
            {
                return Tag_PlayerHitHeadParts;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーがダメージを受けるのタグ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static string Tag_PlayerHitLegParts = "PlayerHitLegParts";
            public static string GetTag_PlayerHitLegParts()
            {
                return Tag_PlayerHitLegParts;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 敵がダメージを与えるタグ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static string Tag_EnemyAttackParts = "EnemyAttackParts";
            public static string GetTag_EnemyAttackParts()
            {
                return Tag_EnemyAttackParts;
            }

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーがダメージを受けるレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static int Layer_PlayerHitParts = 10;
            public static int GetLayer_PlayerHitParts()
            {
                return Layer_PlayerHitParts;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 敵がダメージを与えるレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static int Layer_EnemyAttackParts = 11;
            public static int GetLayer_EnemyAttackParts()
            {
                return Layer_EnemyAttackParts;
            }
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //// プレイヤーがダメージを受けるレイヤー
            ////*|***|***|***|***|***|***|***|***|***|***|***|
            //public static int Layer_PlayerHitParts = 10;
            //public static int GetLayer_PlayerHitParts()
            //{
            //    return Layer_PlayerHitParts;
            //}
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

