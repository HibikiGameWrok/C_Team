using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//*|***|***|***|***|***|***|***|***|***|***|***|
//! @brief XORShiftを参考にしたRand
//!
//! @param[int]　seed 初期seed値
//*|***|***|***|***|***|***|***|***|***|***|***|
public static class XORShiftRand
{
    static uint mySeed = 123456789;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //! @brief 乱数ツールスタート
    //!
    //! @param[uint]　seed 初期seed値
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void SetSeed(uint seed)
    {
        mySeed = seed;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //! @brief 乱数ツールスタート
    //!
    //! @param[int]　seed 初期seed値
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void SetSeed(int seed)
    {
        mySeed = (uint)seed;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //! @brief 乱数ツールゲット
    //!
    //! @return uint 乱数値
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static uint GetSeedRand()
    {
        uint rand;
        rand = mySeed;
        rand ^= rand << 4;
        rand ^= rand >> 3;
        rand ^= rand << 17;
        mySeed = rand;
        return mySeed;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //! @brief 乱数ツールゲット　%で変化
    //!
    //! @param[int]　num 割る数
    //!
    //! @return int 乱数値
    //*|***|***|***|***|***|***|***|***|***|***|***|
    static int GetSeedDivRand(int num)
    {
        uint rand = GetSeedRand();
        int randI = (int)rand;
        if (randI < 0)
        {
            randI += int.MaxValue;
        }
        if (num <= 0)
        {
            num = 1;
        }
        int returnNumber = randI % num;
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //! @brief 乱数ツールゲット　%で変化
    //!
    //! @param[int]　max 最大値
    //! @param[int]　min 最小値
    //!
    //! @return int 乱数値
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int GetSeedMaxMinRand(int max, int min)
    {
        if (max < min)
        {
            ChangeData.Change2Data(ref max, ref min);
        }
        int ans;
        ans = GetSeedDivRand(max - min + 1);
        ans += min;
        return ans;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //! @brief 乱数ツール移動
    //!
    //! @param[int]  num 回数
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void MoveSeedRand(int num)
    {
        int i;
        for (i = 0; i < num; i++)
        {
            GetSeedRand();
        }
    }
}
