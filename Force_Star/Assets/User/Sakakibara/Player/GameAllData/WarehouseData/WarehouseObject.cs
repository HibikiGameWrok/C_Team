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
    using CommonImageNum = WarehouseData.WarehouseStaticData.Object2D_Numbers_Common;
    using AppImageNum = WarehouseData.WarehouseStaticData.Object2D_Numbers_App;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 倉庫データは眠らない
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //[ExecuteInEditMode]
    public class WarehouseObject
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // こいつはシングルトンだったんだ！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        private static WarehouseObject m_warehouseObject = null;

        public static WarehouseObject GetInstance()
        {
            if (m_warehouseObject == null)
            {
                m_warehouseObject = new WarehouseObject();
            }
            return m_warehouseObject;
        }

        private WarehouseObject()
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UIの倉庫共通
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_texture2DCommon = new Texture2D[(int)CommonImageNum.NUM];
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // UIの倉庫追加
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_texture2DApp = new Texture2D[(int)AppImageNum.NUM];

            ReadData();
        }



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データの倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //public static string warehouse = "SimpleImage/";
        public static string SimpleImage = "SimpleImage/";
        public static string AppImage = "AppImage/";


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIの倉庫共通
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Texture2D[] m_texture2DCommon;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIの倉庫追加
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Texture2D[] m_texture2DApp;



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
                // SimpleImageのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                MyFile = SimpleImage;
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 全てで使うデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DCommon[(int)CommonImageNum.BLANK] = Resources.Load<Texture2D>(MyFile + "Blank");
                    m_texture2DCommon[(int)CommonImageNum.FULL] = Resources.Load<Texture2D>(MyFile + "Full");
                    m_texture2DCommon[(int)CommonImageNum.HORIZONTALSHADOW] = Resources.Load<Texture2D>(MyFile + "horizontalShadow");
                    m_texture2DCommon[(int)CommonImageNum.VERTICALSHADOW] = Resources.Load<Texture2D>(MyFile + "verticalShadow");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 数字のデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DCommon[(int)CommonImageNum.NUMBERS_BULE] = Resources.Load<Texture2D>(MyFile + "Numbers_Blue");
                    m_texture2DCommon[(int)CommonImageNum.NUMBERS_RED] = Resources.Load<Texture2D>(MyFile + "Numbers_Red");
                    m_texture2DCommon[(int)CommonImageNum.NUMBERS_GREEN] = Resources.Load<Texture2D>(MyFile + "Numbers_Green");
                    m_texture2DCommon[(int)CommonImageNum.NUMBERS_YELLOW] = Resources.Load<Texture2D>(MyFile + "Numbers_Yellow");
                    m_texture2DCommon[(int)CommonImageNum.NUMBERS_BLACK] = Resources.Load<Texture2D>(MyFile + "Numbers_Black");
                    m_texture2DCommon[(int)CommonImageNum.NUMBERS_WHITE] = Resources.Load<Texture2D>(MyFile + "Numbers_White");

                    
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // プレイヤーのカラーパネルデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DCommon[(int)CommonImageNum.COLOR_BULE] = Resources.Load<Texture2D>(MyFile + "Color_Bule");
                    m_texture2DCommon[(int)CommonImageNum.COLOR_RED] = Resources.Load<Texture2D>(MyFile + "Color_Red");
                    m_texture2DCommon[(int)CommonImageNum.COLOR_GREEN] = Resources.Load<Texture2D>(MyFile + "Color_Green");
                    m_texture2DCommon[(int)CommonImageNum.COLOR_YELLOW] = Resources.Load<Texture2D>(MyFile + "Color_Yellow");
                    m_texture2DCommon[(int)CommonImageNum.COLOR_BLACK] = Resources.Load<Texture2D>(MyFile + "Color_Black");
                    m_texture2DCommon[(int)CommonImageNum.COLOR_WHITE] = Resources.Load<Texture2D>(MyFile + "Color_White");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // プレイヤーのパステルパネルデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_BULE] = Resources.Load<Texture2D>(MyFile + "Pastel_Bule");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_RED] = Resources.Load<Texture2D>(MyFile + "Pastel_Red");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_GREEN] = Resources.Load<Texture2D>(MyFile + "Pastel_Green");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_YELLOW] = Resources.Load<Texture2D>(MyFile + "Pastel_Yellow");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_BLACK] = Resources.Load<Texture2D>(MyFile + "Pastel_Black");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_WHITE] = Resources.Load<Texture2D>(MyFile + "Pastel_White");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_ALL_BULE] = Resources.Load<Texture2D>(MyFile + "PastelAll_Bule");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_ALL_RED] = Resources.Load<Texture2D>(MyFile + "PastelAll_Red");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_ALL_GREEN] = Resources.Load<Texture2D>(MyFile + "PastelAll_Green");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_ALL_YELLOW] = Resources.Load<Texture2D>(MyFile + "PastelAll_Yellow");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_ALL_BLACK] = Resources.Load<Texture2D>(MyFile + "PastelAll_Black");
                    m_texture2DCommon[(int)CommonImageNum.PASTEL_ALL_WHITE] = Resources.Load<Texture2D>(MyFile + "PastelAll_White");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // プレイヤーのダークグレイッシュパネルデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_BULE] = Resources.Load<Texture2D>(MyFile + "DarkGrayish_Bule");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_RED] = Resources.Load<Texture2D>(MyFile + "DarkGrayish_Red");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_GREEN] = Resources.Load<Texture2D>(MyFile + "DarkGrayish_Green");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_YELLOW] = Resources.Load<Texture2D>(MyFile + "DarkGrayish_Yellow");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_BLACK] = Resources.Load<Texture2D>(MyFile + "DarkGrayish_Black");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_WHITE] = Resources.Load<Texture2D>(MyFile + "DarkGrayish_White");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_ALL_BULE] = Resources.Load<Texture2D>(MyFile + "DarkGrayishAll_Bule");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_ALL_RED] = Resources.Load<Texture2D>(MyFile + "DarkGrayishAll_Red");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_ALL_GREEN] = Resources.Load<Texture2D>(MyFile + "DarkGrayishAll_Green");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_ALL_YELLOW] = Resources.Load<Texture2D>(MyFile + "DarkGrayishAll_Yellow");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_ALL_BLACK] = Resources.Load<Texture2D>(MyFile + "DarkGrayishAll_Black");
                    m_texture2DCommon[(int)CommonImageNum.DARKGRAYISH_ALL_WHITE] = Resources.Load<Texture2D>(MyFile + "DarkGrayishAll_White");
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // SimpleImageのデータ
                //*|***|***|***|***|***|***|***|***|***|***|***|
                MyFile = AppImage;
                {
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // 星イメージ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DApp[(int)AppImageNum.STARIMAGE] = Resources.Load<Texture2D>(MyFile + "StarImage");
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    // N数字のデータ
                    //*|***|***|***|***|***|***|***|***|***|***|***|
                    m_texture2DApp[(int)AppImageNum.NUMBERS_DATA16_N1] = Resources.Load<Texture2D>(MyFile + "Number16_N1");
                    m_texture2DApp[(int)AppImageNum.NUMBERS_DATA16_N2] = Resources.Load<Texture2D>(MyFile + "Number16_N2");
                }
            }


        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Texture2D GetTexture2DCommon(CommonImageNum textureNum)
        {
            int type = ChangeData.AmongLess((int)textureNum, 0, (int)CommonImageNum.NUM);
            return m_texture2DCommon[type];
        }
        public Texture2D GetTexture2DCommon(int type)
        {
            type = ChangeData.AmongLess(type, 0, (int)CommonImageNum.NUM);
            return m_texture2DCommon[type];
        }


        public Texture2D GetTexture2DApp(AppImageNum textureNum)
        {
            int type = ChangeData.AmongLess((int)textureNum, 0, (int)CommonImageNum.NUM);
            return m_texture2DApp[type];
        }
        public Texture2D GetTexture2DApp(int type)
        {
            type = ChangeData.AmongLess(type, 0, (int)AppImageNum.NUM);
            return m_texture2DApp[type];
        }




    }
}
