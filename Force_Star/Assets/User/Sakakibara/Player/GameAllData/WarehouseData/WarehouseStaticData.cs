using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 倉庫データ
//*|***|***|***|***|***|***|***|***|***|***|***|
namespace WarehouseData
{

    public static class WarehouseStaticData
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Object2Dの初期から入っている
        // データナンバー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public enum Object2D_Numbers_Common
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 全てで使うデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            BLANK,
            FULL,
            HORIZONTALSHADOW,
            VERTICALSHADOW,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 数字のデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            NUMBERS_BULE,
            NUMBERS_RED,
            NUMBERS_GREEN,
            NUMBERS_YELLOW,
            NUMBERS_BLACK,
            NUMBERS_WHITE,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーのカラーパネルデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            COLOR_BULE,
            COLOR_RED,
            COLOR_GREEN,
            COLOR_YELLOW,
            COLOR_BLACK,
            COLOR_WHITE,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーのパステルパネルデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            PASTEL_BULE,
            PASTEL_RED,
            PASTEL_GREEN,
            PASTEL_YELLOW,
            PASTEL_BLACK,
            PASTEL_WHITE,
            PASTEL_ALL_BULE,
            PASTEL_ALL_RED,
            PASTEL_ALL_GREEN,
            PASTEL_ALL_YELLOW,
            PASTEL_ALL_BLACK,
            PASTEL_ALL_WHITE,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // プレイヤーのダークグレイッシュパネルデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            DARKGRAYISH_BULE,
            DARKGRAYISH_RED,
            DARKGRAYISH_GREEN,
            DARKGRAYISH_YELLOW,
            DARKGRAYISH_BLACK,
            DARKGRAYISH_WHITE,
            DARKGRAYISH_ALL_BULE,
            DARKGRAYISH_ALL_RED,
            DARKGRAYISH_ALL_GREEN,
            DARKGRAYISH_ALL_YELLOW,
            DARKGRAYISH_ALL_BLACK,
            DARKGRAYISH_ALL_WHITE,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 総数
            //*|***|***|***|***|***|***|***|***|***|***|***|
            NUM,
        };
    }
}
