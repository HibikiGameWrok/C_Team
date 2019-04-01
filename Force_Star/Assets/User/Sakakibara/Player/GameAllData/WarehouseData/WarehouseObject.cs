using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//*|***|***|***|***|***|***|***|***|***|***|***|
// 倉庫データ
//*|***|***|***|***|***|***|***|***|***|***|***|
namespace WarehouseData
{

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
            m_texture2D = new Texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.NUM];

            ReadData();
        }



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データの倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public static string SimpleImage = "SimpleImage/";


        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIの倉庫
        //*|***|***|***|***|***|***|***|***|***|***|***|

        Texture2D[] m_texture2D;



        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データの入れどころ
        //*|***|***|***|***|***|***|***|***|***|***|***|


        private void ReadData()
        {
//            string MyFile;

//            //*|***|***|***|***|***|***|***|***|***|***|***|
//            // UITexture2Dのデータ
//            //*|***|***|***|***|***|***|***|***|***|***|***|
//            {
//                //*|***|***|***|***|***|***|***|***|***|***|***|
//                // SimpleImageのデータ
//                //*|***|***|***|***|***|***|***|***|***|***|***|
//                MyFile = Warehouse + SimpleImage;
//                {
//                    //*|***|***|***|***|***|***|***|***|***|***|***|
//                    // 全てで使うデータ
//                    //*|***|***|***|***|***|***|***|***|***|***|***|

////                    Blank
////Full
////horizontalShadow
////verticalShadow
////Numbers_Blue
////Numbers_Red
////Numbers_Green
////Numbers_Yellow
////Numbers_Black
////Numbers_White


//                    m_texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.BLANK] = Resources.Load<Texture2D>(MyFile + "Blank");
//                    m_texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.FULL] = Resources.Load<Texture2D>(MyFile + "Full");
//                    m_texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.HORIZONTALSHADOW] = Resources.Load<Texture2D>(MyFile + "horizontalShadow");
//                    m_texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.VERTICALSHADOW] = Resources.Load<Texture2D>(MyFile + "verticalShadow");
//                    //*|***|***|***|***|***|***|***|***|***|***|***|
//                    // 数字のデータ
//                    //*|***|***|***|***|***|***|***|***|***|***|***|
//                    m_texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.NUMBERS_BULE] = Resources.Load<Texture2D>(MyFile + "Numbers_Blue");
//                    m_texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.NUMBERS_RED] = Resources.Load<Texture2D>(MyFile + "Numbers_Red");
//                    m_texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.NUMBERS_GREEN] = Resources.Load<Texture2D>(MyFile + "Numbers_Green");
//                    m_texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.NUMBERS_YELLOW] = Resources.Load<Texture2D>(MyFile + "Numbers_Yellow");
//                    m_texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.NUMBERS_BLACK] = Resources.Load<Texture2D>(MyFile + "Character16");
//                    m_texture2D[(int)WarehouseStaticData.Object2D_Numbers_Common.NUMBERS_WHITE] = Resources.Load<Texture2D>(MyFile + "Character16");
//                }
//            }


        }

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // UIデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Texture2D GetTexture2D(WarehouseStaticData.Object2D_Numbers_Common textureNum)
        {
            int type = ChangeData.AmongLess((int)textureNum, 0, (int)WarehouseStaticData.Object2D_Numbers_Common.NUM);
            return m_texture2D[type];
        }
        public Texture2D GetUITexture(int type)
        {
            type = ChangeData.AmongLess(type, 0, (int)WarehouseStaticData.Object2D_Numbers_Common.NUM);
            return m_texture2D[type];
        }




    }
}
