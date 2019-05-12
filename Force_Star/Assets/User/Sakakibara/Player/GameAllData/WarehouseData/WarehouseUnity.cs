using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//*|***|***|***|***|***|***|***|***|***|***|***|
// 倉庫データ
//*|***|***|***|***|***|***|***|***|***|***|***|
namespace WarehouseData
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 番号データ共通
    //*|***|***|***|***|***|***|***|***|***|***|***|
    using UnityTitleNum = WarehouseData.WarehouseStaticData.Object2D_UnityNumbers_Title;
    using UnityPlayNum = WarehouseData.WarehouseStaticData.Object2D_UnityNumbers_Play;
    using UnityResultNum = WarehouseData.WarehouseStaticData.Object2D_UnityNumbers_Result;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 倉庫データは眠らない
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //[ExecuteInEditMode]
    public class WarehouseUnity
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // こいつはシングルトンだったんだ！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        private static WarehouseUnity m_warehouseObject = null;

        public static WarehouseUnity GetInstance()
        {
            if (m_warehouseObject == null)
            {
                m_warehouseObject = new WarehouseUnity();
            }
            return m_warehouseObject;
        }

        private WarehouseUnity()
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UIの倉庫タイトル
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_texture2DTitleScene = new Texture2D[(int)UnityTitleNum.NUM];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UIの倉庫ゲーム中
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_texture2DPlayScene = new Texture2D[(int)UnityPlayNum.NUM];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UIの倉庫結果
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_texture2DResultScene = new Texture2D[(int)UnityResultNum.NUM];

            ReadData();
        }



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データの倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //public static string warehouse = "SimpleImage/";
        public static string warehouse = "UnityData/";
        public static string PlayScene_Pass = "PlayScene/";
        public static string ResultScene_Pass = "ResultScene/";
        public static string TitleScene_Pass = "TitleScene/";


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIの倉庫タイトル
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Texture2D[] m_texture2DTitleScene;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIの倉庫ゲーム中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Texture2D[] m_texture2DPlayScene;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIの倉庫結果
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Texture2D[] m_texture2DResultScene;
        

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データの入れどころ
        //*|***|***|***|***|***|***|***|***|***|***|***|


        private void ReadData()
        {
            string MyFile;

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UITexture2Dのデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // タイトルのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                MyFile = warehouse + TitleScene_Pass;
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // N数字のデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DTitleScene[(int)UnityTitleNum.NO1TEXT] = Resources.Load<Texture2D>(MyFile + "No.1Text");
                    m_texture2DTitleScene[(int)UnityTitleNum.NO2TEXT] = Resources.Load<Texture2D>(MyFile + "No.2Text");
                    m_texture2DTitleScene[(int)UnityTitleNum.NO3TEXT] = Resources.Load<Texture2D>(MyFile + "No.3Text");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 銀河
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DTitleScene[(int)UnityTitleNum.SPACE] = Resources.Load<Texture2D>(MyFile + "space");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // メニューロケット
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DTitleScene[(int)UnityTitleNum.MENU_ROCKET] = Resources.Load<Texture2D>(MyFile + "メニューロケット");
                    m_texture2DTitleScene[(int)UnityTitleNum.SMOKE] = Resources.Load<Texture2D>(MyFile + "smoke");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // メニュー火の星
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DTitleScene[(int)UnityTitleNum.MENU_FIRESTAR] = Resources.Load<Texture2D>(MyFile + "メニュー火の星");
                    m_texture2DTitleScene[(int)UnityTitleNum.VOLCINO_LOCK] = Resources.Load<Texture2D>(MyFile + "Volcino_NOT");
                    m_texture2DTitleScene[(int)UnityTitleNum.VOLCINO_UNLOCK] = Resources.Load<Texture2D>(MyFile + "Volcino");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // メニュー機械の星
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DTitleScene[(int)UnityTitleNum.MENU_MACHINESTAR] = Resources.Load<Texture2D>(MyFile + "メニュー機械の星");
                    m_texture2DTitleScene[(int)UnityTitleNum.FACTORY_LOCK] = Resources.Load<Texture2D>(MyFile + "Factory_Not");
                    m_texture2DTitleScene[(int)UnityTitleNum.FACTORY_UNLOCK] = Resources.Load<Texture2D>(MyFile + "Factory");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // メニュー水の星
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DTitleScene[(int)UnityTitleNum.MENU_WATERSTAR] = Resources.Load<Texture2D>(MyFile + "メニュー水の星");
                    m_texture2DTitleScene[(int)UnityTitleNum.SEASIDE_LOCK] = Resources.Load<Texture2D>(MyFile + "Seaside");
                    m_texture2DTitleScene[(int)UnityTitleNum.SEASIDE_UNLOCK] = Resources.Load<Texture2D>(MyFile + "Seaside");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // メニュー背景
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DTitleScene[(int)UnityTitleNum.MENU_BACKGROUND] = Resources.Load<Texture2D>(MyFile + "メニュー背景");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // メニュー隕石
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DTitleScene[(int)UnityTitleNum.MENU_METEORITE] = Resources.Load<Texture2D>(MyFile + "メニュー隕石");
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // ゲーム中のデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                MyFile = warehouse + PlayScene_Pass;
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 背景\海
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DPlayScene[(int)UnityPlayNum.PANEL_OPE] = Resources.Load<Texture2D>(MyFile + "panel_ope");
                    m_texture2DPlayScene[(int)UnityPlayNum.SANDYBEACH_IMAGE] = Resources.Load<Texture2D>(MyFile + "SandyBeach_Image");
                    m_texture2DPlayScene[(int)UnityPlayNum.SEA] = Resources.Load<Texture2D>(MyFile + "sea");
                    m_texture2DPlayScene[(int)UnityPlayNum.SEA_BACKGROUND_IMAGE] = Resources.Load<Texture2D>(MyFile + "Sea_BackGround_Image");
                    m_texture2DPlayScene[(int)UnityPlayNum.SEAIMAGE] = Resources.Load<Texture2D>(MyFile + "SeaImage");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 敵\海
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DPlayScene[(int)UnityPlayNum.ENEMY_SHARK] = Resources.Load<Texture2D>(MyFile + "Shark");
                    m_texture2DPlayScene[(int)UnityPlayNum.ENEMY_STARFISH] = Resources.Load<Texture2D>(MyFile + "StarFish");
                    m_texture2DPlayScene[(int)UnityPlayNum.ENEMY_YADO] = Resources.Load<Texture2D>(MyFile + "Yado");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // ロケット\海
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DPlayScene[(int)UnityPlayNum.ROCKET] = Resources.Load<Texture2D>(MyFile + "Rocket");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 星\海
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DPlayScene[(int)UnityPlayNum.HITEFFECT] = Resources.Load<Texture2D>(MyFile + "ヒットエフェクト");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 石\海
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DPlayScene[(int)UnityPlayNum.STONE] = Resources.Load<Texture2D>(MyFile + "Stone");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 木\海
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DPlayScene[(int)UnityPlayNum.PALMTREE] = Resources.Load<Texture2D>(MyFile + "PalmTree");
                    m_texture2DPlayScene[(int)UnityPlayNum.PALMTREE_LEAF1] = Resources.Load<Texture2D>(MyFile + "PalmTree_leaf1");
                    m_texture2DPlayScene[(int)UnityPlayNum.PALMTREE_LEAF2] = Resources.Load<Texture2D>(MyFile + "PalmTree_leaf2");
                    m_texture2DPlayScene[(int)UnityPlayNum.PALMTREE_LEAF3] = Resources.Load<Texture2D>(MyFile + "PalmTree_leaf3");
                    m_texture2DPlayScene[(int)UnityPlayNum.WETLANDTREES3] = Resources.Load<Texture2D>(MyFile + "WetlandTrees3");
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // リザルトのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                MyFile = warehouse + ResultScene_Pass;
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // やった！
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DResultScene[(int)UnityResultNum.CLEARIMAGE] = Resources.Load<Texture2D>(MyFile + "clearImage");
                    m_texture2DResultScene[(int)UnityResultNum.CLEARIMAGEX] = Resources.Load<Texture2D>(MyFile + "clearImage_x");
                    m_texture2DResultScene[(int)UnityResultNum.CLEARIMAGE2] = Resources.Load<Texture2D>(MyFile + "clearImage2");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // ばたんきゅ～
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DResultScene[(int)UnityResultNum.OVERIMAGE] = Resources.Load<Texture2D>(MyFile + "overImage");
                    m_texture2DResultScene[(int)UnityResultNum.OVERIMAGEX] = Resources.Load<Texture2D>(MyFile + "overImage_x");
                    m_texture2DResultScene[(int)UnityResultNum.OVERIMAGE2] = Resources.Load<Texture2D>(MyFile + "overImage2");
                    m_texture2DResultScene[(int)UnityResultNum.OVERIMAGE3] = Resources.Load<Texture2D>(MyFile + "overImage3");
                }
            }


        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIの倉庫タイトル
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Texture2D GetTexture2DTitle(UnityTitleNum textureNum)
        {
            int type = ChangeData.AmongLess((int)textureNum, 0, (int)UnityTitleNum.NUM);
            return m_texture2DTitleScene[type];
        }
        public Texture2D GetTexture2DTitle(int type)
        {
            type = ChangeData.AmongLess(type, 0, (int)UnityTitleNum.NUM);
            return m_texture2DTitleScene[type];
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIの倉庫ゲーム中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Texture2D GetTexture2DPlay(UnityPlayNum textureNum)
        {
            int type = ChangeData.AmongLess((int)textureNum, 0, (int)UnityPlayNum.NUM);
            return m_texture2DPlayScene[type];
        }
        public Texture2D GetTexture2DPlay(int type)
        {
            type = ChangeData.AmongLess(type, 0, (int)UnityPlayNum.NUM);
            return m_texture2DPlayScene[type];
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIの倉庫結果
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Texture2D GetTexture2DResult(UnityResultNum textureNum)
        {
            int type = ChangeData.AmongLess((int)textureNum, 0, (int)UnityResultNum.NUM);
            return m_texture2DResultScene[type];
        }
        public Texture2D GetTexture2DResult(int type)
        {
            type = ChangeData.AmongLess(type, 0, (int)UnityResultNum.NUM);
            return m_texture2DResultScene[type];
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ground
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public static string Tag_ground = "ground";
        public static string GetTag_Ground()
        {
            return Tag_ground;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Shell
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public static string Tag_Shell = "Shell";
        public static string GetTag_Shell()
        {
            return Tag_Shell;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // StartRoketPoint
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public static string Tag_StartRoketPoint = "StartRoketPoint";
        public static string GetTag_StartRoketPoint()
        {
            return Tag_StartRoketPoint;
        }


    }

}
