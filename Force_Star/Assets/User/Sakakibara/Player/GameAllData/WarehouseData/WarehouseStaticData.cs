﻿using System.Collections;
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Object2Dの追加された
        // データナンバー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public enum Object2D_Numbers_App
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星イメージ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            STARIMAGE,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // N数字のデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            NUMBERS_DATA16_N1,
            NUMBERS_DATA16_N2,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 記号のデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            SYMBOL_N1,
            SYMBOL_N2,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 強化のデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            POWERUP_ARM,
            POWERUP_BODY,
            POWERUP_HEAD,
            POWERUP_LEG,
            POWERUP_TIME,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 大爆発のデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            EXPROSION,
            EXPROSION_RED,
            EXPROSION_BLUE,
            EXPROSION_GREEN,
            EXPROSION_YELLOW,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // パーツのデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ROCKETPARTS,
            ROCKETPARTSSHADOW,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 警報のデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ALARMRED,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 映像のデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            MOVIEFRAME,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星アニメイメージ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            STARIMAGE_EFFECT,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 総数
            //*|***|***|***|***|***|***|***|***|***|***|***|
            NUM,
        };
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 記号データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public enum Symbol_ENUM
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // N数字のデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            PERCENT,
            MULTIPLICATION,
            STAR_IMAGE,
            KO,
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // みんなのタイトルの
        // データナンバー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public enum Object2D_UnityNumbers_Title
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // N数字のデータ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            NO1TEXT,
            NO2TEXT,
            NO3TEXT,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 銀河
            //*|***|***|***|***|***|***|***|***|***|***|***|
            SPACE,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // メニューロケット
            //*|***|***|***|***|***|***|***|***|***|***|***|
            MENU_ROCKET,
            SMOKE,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // メニュー火の星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            MENU_FIRESTAR,
            VOLCINO_LOCK,
            VOLCINO_UNLOCK,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // メニュー機械の星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            MENU_MACHINESTAR,
            FACTORY_LOCK,
            FACTORY_UNLOCK,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // メニュー水の星
            //*|***|***|***|***|***|***|***|***|***|***|***|
            MENU_WATERSTAR,
            SEASIDE_LOCK,
            SEASIDE_UNLOCK,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // メニュー背景
            //*|***|***|***|***|***|***|***|***|***|***|***|
            MENU_BACKGROUND,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // メニュー隕石
            //*|***|***|***|***|***|***|***|***|***|***|***|
            MENU_METEORITE,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 総数
            //*|***|***|***|***|***|***|***|***|***|***|***|
            NUM,
        };
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // みんなのゲーム中の
        // データナンバー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public enum Object2D_UnityNumbers_Play
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 背景\海
            //*|***|***|***|***|***|***|***|***|***|***|***|
            PANEL_OPE,
            SANDYBEACH_IMAGE,
            SEA,
            SEA_BACKGROUND_IMAGE,
            SEAIMAGE,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 敵\海
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ENEMY_SHARK,
            ENEMY_STARFISH,
            ENEMY_YADO,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ロケット\海
            //*|***|***|***|***|***|***|***|***|***|***|***|
            ROCKET,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 星\海
            //*|***|***|***|***|***|***|***|***|***|***|***|
            HITEFFECT,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 石\海
            //*|***|***|***|***|***|***|***|***|***|***|***|
            STONE,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 木\海
            //*|***|***|***|***|***|***|***|***|***|***|***|
            PALMTREE,
            PALMTREE_LEAF1,
            PALMTREE_LEAF2,
            PALMTREE_LEAF3,
            WETLANDTREES3,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 総数
            //*|***|***|***|***|***|***|***|***|***|***|***|
            NUM,
        };
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // みんなのリザルトの
        // データナンバー
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public enum Object2D_UnityNumbers_Result
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // やった！
            //*|***|***|***|***|***|***|***|***|***|***|***|
            CLEARIMAGE,
            CLEARIMAGEX,
            CLEARIMAGE2,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ばたんきゅ～
            //*|***|***|***|***|***|***|***|***|***|***|***|
            OVERIMAGE,
            OVERIMAGEX,
            OVERIMAGE2,
            OVERIMAGE3,
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 総数
            //*|***|***|***|***|***|***|***|***|***|***|***|
            NUM,
        };
    }
}
