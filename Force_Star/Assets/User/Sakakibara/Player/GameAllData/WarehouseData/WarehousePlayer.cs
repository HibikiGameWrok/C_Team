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
                BODYBOTTOM_DAMAGE,
                JOINTBOAL_DAMAGE,
                ATTACKBOAL_DAMAGE,
                LEFTLEG_DAMAGE,
                RIGHTLEG_DAMAGE,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレイヤーのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                PLAYERBOAL_STRONG,
                BODYTOP_STRONG,
                BODYBOTTOM_STRONG,
                JOINTBOAL_STRONG,
                ATTACKBOAL_STRONG,
                LEFTLEG_STRONG,
                RIGHTLEG_STRONG,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレイヤーその他のデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                PLAYERBOAL_HUSHIGI,
                PLAYERBOAL_YEAH,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // プレイヤー死のデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                PLAYERBOAL_DEAD,
                PLAYERBOAL_ODOROKI,
                BODYTOP_DEAD,
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
                // 所持数
                //*|***|***|***|***|***|***|***|***|***|***|***|
                SHOJIRYO,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 目標
                //*|***|***|***|***|***|***|***|***|***|***|***|
                MOKUHYO,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 回復のUIのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 全体図
                //*|***|***|***|***|***|***|***|***|***|***|***|
                RECOVERYUI_ALLBODY,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // ダメージ受けた赤
                //*|***|***|***|***|***|***|***|***|***|***|***|
                RECOVERYUI_REDARM,
                RECOVERYUI_REDBODY,
                RECOVERYUI_REDHEAD,
                RECOVERYUI_REDLEG,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 強化受けた黄
                //*|***|***|***|***|***|***|***|***|***|***|***|
                RECOVERYUI_STRONGARM,
                RECOVERYUI_STRONGBODY,
                RECOVERYUI_STRONGHEAD,
                RECOVERYUI_STRONGLEG,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 回復のための背景のもの
                //*|***|***|***|***|***|***|***|***|***|***|***|
                RECOVERYUI_HEALBACK_GREEN,
                RECOVERYUI_HEALBACK_RED,
                RECOVERYUI_HEALBACK_GOLD,

                RECOVERYUI_BACKBLACK,
                RECOVERYUI_BODYLINE,
                RECOVERYUI_HEALARM,
                RECOVERYUI_HEALBODY,
                RECOVERYUI_HEALHEAD,
                RECOVERYUI_HEALLEG,
                RECOVERYUI_RETURN,
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 強化受けた黄
                //*|***|***|***|***|***|***|***|***|***|***|***|
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
                        m_playerTex2D[(int)PlayerData_Number.PLAYERBOAL_DAMAGE] = Resources.Load<Texture2D>(MyFile + "PlayerBoalDamage");
                        m_playerTex2D[(int)PlayerData_Number.BODYTOP_DAMAGE] = Resources.Load<Texture2D>(MyFile + "BodyTopDamage");
                        m_playerTex2D[(int)PlayerData_Number.BODYBOTTOM_DAMAGE] = Resources.Load<Texture2D>(MyFile + "BodyBottom");
                        m_playerTex2D[(int)PlayerData_Number.JOINTBOAL_DAMAGE] = Resources.Load<Texture2D>(MyFile + "JointBoal");
                        m_playerTex2D[(int)PlayerData_Number.ATTACKBOAL_DAMAGE] = Resources.Load<Texture2D>(MyFile + "AttackBoalDamage");
                        m_playerTex2D[(int)PlayerData_Number.LEFTLEG_DAMAGE] = Resources.Load<Texture2D>(MyFile + "LeftLegDamage");
                        m_playerTex2D[(int)PlayerData_Number.RIGHTLEG_DAMAGE] = Resources.Load<Texture2D>(MyFile + "RightLegDamage");
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // プレイヤーのデータ
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerTex2D[(int)PlayerData_Number.PLAYERBOAL_STRONG] = Resources.Load<Texture2D>(MyFile + "PlayerBoalStrong");
                        m_playerTex2D[(int)PlayerData_Number.BODYTOP_STRONG] = Resources.Load<Texture2D>(MyFile + "BodyTopStrong");
                        m_playerTex2D[(int)PlayerData_Number.BODYBOTTOM_STRONG] = Resources.Load<Texture2D>(MyFile + "BodyBottomStrong");
                        m_playerTex2D[(int)PlayerData_Number.JOINTBOAL_STRONG] = Resources.Load<Texture2D>(MyFile + "JointBoalStrong");
                        m_playerTex2D[(int)PlayerData_Number.ATTACKBOAL_STRONG] = Resources.Load<Texture2D>(MyFile + "AttackBoalStrong");
                        m_playerTex2D[(int)PlayerData_Number.LEFTLEG_STRONG] = Resources.Load<Texture2D>(MyFile + "LeftLegStrong");
                        m_playerTex2D[(int)PlayerData_Number.RIGHTLEG_STRONG] = Resources.Load<Texture2D>(MyFile + "RightLegStrong");
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // プレイヤーその他のデータ
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerTex2D[(int)PlayerData_Number.PLAYERBOAL_HUSHIGI] = Resources.Load<Texture2D>(MyFile + "PlayerBoal_Fushigi");
                        m_playerTex2D[(int)PlayerData_Number.PLAYERBOAL_YEAH] = Resources.Load<Texture2D>(MyFile + "PlayerBoal_Yeah");
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // プレイヤー死のデータ
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerTex2D[(int)PlayerData_Number.PLAYERBOAL_DEAD] = Resources.Load<Texture2D>(MyFile + "PlayerBoalDead");
                        m_playerTex2D[(int)PlayerData_Number.PLAYERBOAL_ODOROKI] = Resources.Load<Texture2D>(MyFile + "PlayerBoal_odoroki");
                        m_playerTex2D[(int)PlayerData_Number.BODYTOP_DEAD] = Resources.Load<Texture2D>(MyFile + "PlayerTopDie");

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
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // 所持数
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.SHOJIRYO] = Resources.Load<Texture2D>(MyFile + SubFile + "UIShojiryo");
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // 目標
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.MOKUHYO] = Resources.Load<Texture2D>(MyFile + SubFile + "UIMokuhyo");
                    }
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 回復のUIのデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    SubFile = Recovery_UI;
                    {
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // 全体図
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_ALLBODY] = Resources.Load<Texture2D>(MyFile + SubFile + "UIAllBody");
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // ダメージ受けた赤
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_REDARM] = Resources.Load<Texture2D>(MyFile + SubFile + "UIRedArm");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_REDBODY] = Resources.Load<Texture2D>(MyFile + SubFile + "UIRedBody");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_REDHEAD] = Resources.Load<Texture2D>(MyFile + SubFile + "UIRedHead");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_REDLEG] = Resources.Load<Texture2D>(MyFile + SubFile + "UIRedLeg");
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // 強化受けた黄
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_STRONGARM] = Resources.Load<Texture2D>(MyFile + SubFile + "UIStrongArm");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_STRONGBODY] = Resources.Load<Texture2D>(MyFile + SubFile + "UIStrongBody");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_STRONGHEAD] = Resources.Load<Texture2D>(MyFile + SubFile + "UIStrongHead");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_STRONGLEG] = Resources.Load<Texture2D>(MyFile + SubFile + "UIStrongLeg");
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        // 回復のための背景のもの
                        //*|***|***|***|***|***|***|***|***|***|***|***|
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALBACK_GREEN] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealBack");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALBACK_RED] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealBackRed");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALBACK_GOLD] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealBackGold");

                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_BACKBLACK] = Resources.Load<Texture2D>(MyFile + SubFile + "UIBackBlack");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_BODYLINE] = Resources.Load<Texture2D>(MyFile + SubFile + "UIBodyLine");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALARM] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealArm");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALBODY] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealBody");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALHEAD] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealHead");
                        m_playerAnotherTex2D[(int)PlayerData_Another_Number_List.RECOVERYUI_HEALLEG] = Resources.Load<Texture2D>(MyFile + SubFile + "UIHealLeg");
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
            // プレイヤーが触れるレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static int Layer_PlayerCatchParts = 17;
            public static int GetLayer_PlayerCatchParts()
            {
                return Layer_PlayerCatchParts;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 敵がダメージを与えるレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static int Layer_EnemyAttackParts = 11;
            public static int GetLayer_EnemyAttackParts()
            {
                return Layer_EnemyAttackParts;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 敵がダメージを受けるレイヤー
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static int Layer_EnemyBodyParts = 13;
            public static int GetLayer_EnemyBodyParts()
            {
                return Layer_EnemyBodyParts;
            }


            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーの一部か？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            public static bool BoolTagIsPlayer(string tag)
            {
                if (tag == GetTag_PlayerHitArmParts())
                {
                    return true;
                }
                if (tag == GetTag_PlayerHitBodyParts())
                {
                    return true;
                }
                if (tag == GetTag_PlayerHitHeadParts())
                {
                    return true;
                }
                if (tag == GetTag_PlayerHitLegParts())
                {
                    return true;
                }
                if (tag == GetTag_PlayerHitParts())
                {
                    return true;
                }
                if (tag == GetTag_AttackBoal())
                {
                    return true;
                }
                return false;
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

