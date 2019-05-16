using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyCalculator
{

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 桁へ挿入する
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static uint DigitInsertion(uint number, uint digit, bool flag)
    {
        uint returnNumber = number;
        uint flagNumber = MultiplicationBinary(digit);
        if (flag)
        {
            returnNumber = DigitInsertionOn(returnNumber, flagNumber);
        }
        else
        {
            returnNumber = DigitInsertionOff(returnNumber, flagNumber);
        }
        return returnNumber;
    }
    public static uint DigitInsertionOn(uint number, uint flag)
    {
        uint numberSum;
        numberSum = DigitOR(number, flag);
        return numberSum;
    }
    public static uint DigitInsertionOff(uint number, uint flag)
    {
        uint numberSum;
        numberSum = DigitAND(number, DigitReverse(flag));
        return numberSum;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 二進数でnumber桁目が1の数を返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static uint MultiplicationBinary(uint digit)
    {
        uint returnNumber = 1;
        for (uint count = 0; count < digit; count++)
        {
            returnNumber *= 2;
        }
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 二進数でnumber桁目が1かどうか返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool DigitBoolean(uint shouchNumber, uint digit)
    {
        uint returnNumber = MultiplicationBinary(digit);
        return (DigitAND(returnNumber, shouchNumber) != 0);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 桁同士の計算
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static uint DigitAND(uint number1, uint number2)
    {
        uint numberSum;
        numberSum = number1 & number2;
        return numberSum;
    }
    public static uint DigitOR(uint number1, uint number2)
    {
        uint numberSum;
        numberSum = number1 | number2;
        return numberSum;
    }
    public static uint DigitXOR(uint number1, uint number2)
    {
        uint numberSum;
        numberSum = number1 ^ number2;
        return numberSum;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 3D2D桁同士の計算
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static uint Digit3DNum(uint number, uint numberDepth)
    {
        uint numberAns = DigitAND(number, Multiplication(2u, 3 * (int)numberDepth) - 1);
        return numberAns;
    }
    public static uint Digit3DAdd(uint number, uint numberAdd)
    {
        uint numberAns;
        numberAdd = DigitAND(numberAdd, Multiplication(2u, 3) - 1);
        numberAns = number * Multiplication(2u, 3);
        numberAns += numberAdd;
        return numberAns;
    }
    public static uint Digit2DNum(uint number, uint numberDepth)
    {
        uint numberAns = DigitAND(number, Multiplication(2u, 2 * (int)numberDepth) - 1);
        return numberAns;
    }
    public static uint Digit2DAdd(uint number, uint numberAdd)
    {
        uint numberAns;
        numberAdd = DigitAND(numberAdd, Multiplication(2u, 2) - 1);
        numberAns = number << 2;
        numberAns += numberAdd;
        return numberAns;
    }
    public static uint Digit3DZip(uint number, uint oldNumber, uint numberDepth)
    {
        uint num = 0;
        uint eight = Multiplication(2u, 3);
        num = Division(Multiplication(eight, (int)numberDepth) - 1, eight - 1);
        num += number + (oldNumber * eight);
        return num;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 桁の反転
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static uint DigitReverse(uint number)
    {
        uint numberSum;
        numberSum = ~number;
        return numberSum;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // numberの10進数でのdigit桁目の数字一桁を入手
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int Get10Digit(int number, int digit)
    {
        int numberR;
        numberR = (number / Multiplication(10, digit)) % 10;
        return numberR;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // number の n 乗を返す int
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int Multiplication(int number, int n)
    {
        int returnNumber = 1;
        if (n < 0)
        {
            return MultiplicationReverse(number, n);
        }
        for (int count = 0; count < n; count++)
        {
            returnNumber *= number;
        }
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // number の n 乗を返す float
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float Multiplication(float number, int n)
    {
        float returnNumber = 1;
        if (n < 0)
        {
            return MultiplicationReverse(number, n);
        }
        for (int count = 0; count < n; count++)
        {
            returnNumber *= number;
        }
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // number の n 乗を返す uint
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static uint Multiplication(uint number, int n)
    {
        uint returnNumber = 1;
        if (n < 0)
        {
            return MultiplicationReverse(number, n);
        }
        for (int count = 0; count < n; count++)
        {
            returnNumber *= number;
        }
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // number の n 乗を返す double
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static double Multiplication(double number, int n)
    {
        double returnNumber = 1;
        if (n < 0)
        {
            return MultiplicationReverse(number, n);
        }
        for (int count = 0; count < n; count++)
        {
            returnNumber *= number;
        }
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // number の n 乗を返す int
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int MultiplicationReverse(int number, int n)
    {
        int returnNumber = 1;
        if (number == 0)
        {
            return 0;
        }
        for (int count = 0; count > n; count--)
        {
            returnNumber *= 1 / number;
        }
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // number の n 乗を返す float
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float MultiplicationReverse(float number, int n)
    {
        float returnNumber = 1;
        if (number == 0)
        {
            return 0;
        }
        for (int count = 0; count > n; count--)
        {
            returnNumber *= 1 / number;
        }
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // number の n 乗を返す uint
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static uint MultiplicationReverse(uint number, int n)
    {
        uint returnNumber = 1;
        if (number == 0)
        {
            return 0;
        }
        for (int count = 0; count > n; count--)
        {
            returnNumber *= 1 / number;
        }
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // number の n 乗を返す double
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static double MultiplicationReverse(double number, int n)
    {
        double returnNumber = 1;
        if (number == 0)
        {
            return 0;
        }
        for (int count = 0; count > n; count--)
        {
            returnNumber *= 1 / number;
        }
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 割合の逆転
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float InversionOfProportion(float number)
    {
        float ans = 1.0f - number;
        return ans;
    }
    public static float InversionOfPercentage(float number)
    {
        float ans = 100.0f - number;
        return ans;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 個数の逆転
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int InversionOfIndex(int number, int count)
    {
        int maxIndex = ChangeData.AmongLess(count, 0, count);
        int ans = maxIndex - number;
        ans = ChangeData.AmongLess(ans, 0, count);
        return ans;
    }
    public static uint InversionOfIndex(uint number, uint count)
    {
        int intAns = InversionOfIndex((int)number, (int)count);
        uint ans = (uint)intAns;
        return ans;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 中心化での地点割合
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float IndexCenterPos(int number, int count)
    {
        int max = count;
        if (max <= 0)
        {
            max = 1;
        }
        float wideWidth = Division((float)max - 1, 2.0f);
        float ans = (number * 1.0f) - wideWidth;
        return ans;
    }
    public static float IndexCenterPos(uint number, uint count)
    {
        float intAns = InversionOfIndex((int)number, (int)count);
        float ans = intAns;
        return ans;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 平方根　時間がかかるので注意！
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int SquareRoot(int number)
    {
        int returnNumber;
        returnNumber = (int)Mathf.Sqrt(number);
        return returnNumber;
    }
    public static float SquareRoot(float number)
    {
        float returnNumber;
        returnNumber = Mathf.Sqrt(number);
        return returnNumber;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // kazu / syou を返す int
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int Division(int katu, int syou)
    {
        if (syou == 0)
        {
            return 0;
        }
        return katu / syou;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // kazu / syou を返す float
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float Division(float katu, float syou)
    {
        if (syou == 0)
        {
            return 0;
        }
        return katu / syou;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // kazu / syou を返す uint
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static uint Division(uint katu, uint syou)
    {
        if (syou == 0)
        {
            return 0;
        }
        return katu / syou;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // kazu / syou を返す double
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static double Division(double katu, double syou)
    {
        if (syou == 0)
        {
            return 0;
        }
        return katu / syou;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 一番大きい値を返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float MaxData(Vector2 number)
    {
        float maxNow = number.x;
        if (maxNow < number.y)
        {
            maxNow = number.y;
        }
        return maxNow;
    }
    public static int MaxData(int number1, int number2)
    {
        int maxNow = number1;
        if (maxNow < number2)
        {
            maxNow = number2;
        }
        return maxNow;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 一番大きい値を返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float MaxData(Vector3 number)
    {
        float maxNow = number.x;
        maxNow = MaxData(new Vector2(maxNow, number.y));
        maxNow = MaxData(new Vector2(maxNow, number.z));
        return maxNow;
    }
    public static int MaxData(int number1, int number2, int number3)
    {
        int maxNow = number1;
        maxNow = MaxData(maxNow, number2);
        maxNow = MaxData(maxNow, number3);
        return maxNow;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 一番大きい値を返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float MaxData(Vector4 number)
    {
        float maxNow = number.x;
        maxNow = MaxData(new Vector2(maxNow, number.y));
        maxNow = MaxData(new Vector2(maxNow, number.z));
        maxNow = MaxData(new Vector2(maxNow, number.w));
        return maxNow;
    }
    public static int MaxData(int number1, int number2, int number3, int number4)
    {
        int maxNow = number1;
        maxNow = MaxData(maxNow, number2);
        maxNow = MaxData(maxNow, number3);
        maxNow = MaxData(maxNow, number4);
        return maxNow;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 各成分を掛け算する
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector2 EachTimes(Vector2 number1, Vector2 number2)
    {
        Vector2 ans;
        ans.x = number1.x * number2.x;
        ans.y = number1.y * number2.y;
        //ans.z = number1.z * number2.z;
        return ans;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 各成分を掛け算する
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 EachTimes(Vector3 number1, Vector3 number2)
    {
        Vector3 ans;
        ans.x = number1.x * number2.x;
        ans.y = number1.y * number2.y;
        ans.z = number1.z * number2.z;
        return ans;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 各成分を掛け算する
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector4 EachTimes(Vector4 number1, Vector4 number2)
    {
        Vector4 ans;
        ans.x = number1.x * number2.x;
        ans.y = number1.y * number2.y;
        ans.z = number1.z * number2.z;
        ans.w = number1.w * number2.w;
        return ans;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 周回の隙間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 周回の中、startとgoalの間にいるか？
    // start>goalとなると判定が周回開始点を含んだ方の弧になる
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // startやgoalと同値はTRUE
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool BetweenRing(int getPoint, int start, int goal, int loopLong)
    {
        bool returnFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 輪になってないよ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopLong <= 0)
        {
            return returnFlag;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 周回の輪のどこか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int getPointLoop = ChangeData.AntiOverflow(getPoint, loopLong);
        int startLoop = ChangeData.AntiOverflow(start, loopLong);
        int goalLoop = ChangeData.AntiOverflow(goal, loopLong);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 判定が周回開始点を含んだ方の弧か？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool overflowRoop = false;
        if (startLoop > goalLoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // startLoopがgoalLoopより大きければ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            overflowRoop = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プログラム分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (overflowRoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含まないの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.BetweenNoTouch(getPointLoop, startLoop, goalLoop);
            returnFlag = !returnFlag;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含むの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.Between(getPointLoop, startLoop, goalLoop);
        }
        return returnFlag;
    }
    public static bool BetweenRing(float getPoint, float start, float goal, float loopLong)
    {
        bool returnFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 輪になってないよ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopLong <= 0)
        {
            return returnFlag;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 周回の輪のどこか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float getPointLoop = ChangeData.AntiOverflow(getPoint, loopLong);
        float startLoop = ChangeData.AntiOverflow(start, loopLong);
        float goalLoop = ChangeData.AntiOverflow(goal, loopLong);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 判定が周回開始点を含んだ方の弧か？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool overflowRoop = false;
        if (startLoop > goalLoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // startLoopがgoalLoopより大きければ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            overflowRoop = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プログラム分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (overflowRoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含まないの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.BetweenNoTouch(getPointLoop, startLoop, goalLoop);
            returnFlag = !returnFlag;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含むの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.Between(getPointLoop, startLoop, goalLoop);
        }
        return returnFlag;
    }
    public static bool BetweenRing(uint getPoint, uint start, uint goal, uint loopLong)
    {
        bool returnFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 輪になってないよ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopLong <= 0)
        {
            return returnFlag;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 周回の輪のどこか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        uint getPointLoop = ChangeData.AntiOverflow(getPoint, loopLong);
        uint startLoop = ChangeData.AntiOverflow(start, loopLong);
        uint goalLoop = ChangeData.AntiOverflow(goal, loopLong);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 判定が周回開始点を含んだ方の弧か？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool overflowRoop = false;
        if (startLoop > goalLoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // startLoopがgoalLoopより大きければ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            overflowRoop = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プログラム分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (overflowRoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含まないの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.BetweenNoTouch(getPointLoop, startLoop, goalLoop);
            returnFlag = !returnFlag;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含むの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.Between(getPointLoop, startLoop, goalLoop);
        }
        return returnFlag;
    }
    public static bool BetweenRing(double getPoint, double start, double goal, double loopLong)
    {
        bool returnFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 輪になってないよ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopLong <= 0)
        {
            return returnFlag;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 周回の輪のどこか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        double getPointLoop = ChangeData.AntiOverflow(getPoint, loopLong);
        double startLoop = ChangeData.AntiOverflow(start, loopLong);
        double goalLoop = ChangeData.AntiOverflow(goal, loopLong);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 判定が周回開始点を含んだ方の弧か？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool overflowRoop = false;
        if (startLoop > goalLoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // startLoopがgoalLoopより大きければ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            overflowRoop = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プログラム分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (overflowRoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含まないの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.BetweenNoTouch(getPointLoop, startLoop, goalLoop);
            returnFlag = !returnFlag;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含むの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.Between(getPointLoop, startLoop, goalLoop);
        }
        return returnFlag;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // startやgoalと同値はFALSE
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool BetweenRingNoTouch(int getPoint, int start, int goal, int loopLong)
    {
        bool returnFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 輪になってないよ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopLong <= 0)
        {
            return returnFlag;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 周回の輪のどこか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int getPointLoop = ChangeData.AntiOverflow(getPoint, loopLong);
        int startLoop = ChangeData.AntiOverflow(start, loopLong);
        int goalLoop = ChangeData.AntiOverflow(goal, loopLong);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 判定が周回開始点を含んだ方の弧か？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool overflowRoop = false;
        if (startLoop > goalLoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // startLoopがgoalLoopより大きければ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            overflowRoop = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プログラム分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (overflowRoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含まないの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.Between(getPointLoop, startLoop, goalLoop);
            returnFlag = !returnFlag;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含むの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.BetweenNoTouch(getPointLoop, startLoop, goalLoop);
        }
        return returnFlag;
    }
    public static bool BetweenRingNoTouch(float getPoint, float start, float goal, float loopLong)
    {
        bool returnFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 輪になってないよ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopLong <= 0)
        {
            return returnFlag;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 周回の輪のどこか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float getPointLoop = ChangeData.AntiOverflow(getPoint, loopLong);
        float startLoop = ChangeData.AntiOverflow(start, loopLong);
        float goalLoop = ChangeData.AntiOverflow(goal, loopLong);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 判定が周回開始点を含んだ方の弧か？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool overflowRoop = false;
        if (startLoop > goalLoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // startLoopがgoalLoopより大きければ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            overflowRoop = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プログラム分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (overflowRoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含まないの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.Between(getPointLoop, startLoop, goalLoop);
            returnFlag = !returnFlag;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含むの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.BetweenNoTouch(getPointLoop, startLoop, goalLoop);
        }
        return returnFlag;
    }
    public static bool BetweenRingNoTouch(uint getPoint, uint start, uint goal, uint loopLong)
    {
        bool returnFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 輪になってないよ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopLong <= 0)
        {
            return returnFlag;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 周回の輪のどこか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        uint getPointLoop = ChangeData.AntiOverflow(getPoint, loopLong);
        uint startLoop = ChangeData.AntiOverflow(start, loopLong);
        uint goalLoop = ChangeData.AntiOverflow(goal, loopLong);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 判定が周回開始点を含んだ方の弧か？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool overflowRoop = false;
        if (startLoop > goalLoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // startLoopがgoalLoopより大きければ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            overflowRoop = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プログラム分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (overflowRoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含まないの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.Between(getPointLoop, startLoop, goalLoop);
            returnFlag = !returnFlag;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含むの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.BetweenNoTouch(getPointLoop, startLoop, goalLoop);
        }
        return returnFlag;
    }
    public static bool BetweenRingNoTouch(double getPoint, double start, double goal, double loopLong)
    {
        bool returnFlag = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 輪になってないよ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopLong <= 0)
        {
            return returnFlag;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 周回の輪のどこか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        double getPointLoop = ChangeData.AntiOverflow(getPoint, loopLong);
        double startLoop = ChangeData.AntiOverflow(start, loopLong);
        double goalLoop = ChangeData.AntiOverflow(goal, loopLong);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 判定が周回開始点を含んだ方の弧か？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool overflowRoop = false;
        if (startLoop > goalLoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // startLoopがgoalLoopより大きければ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            overflowRoop = true;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プログラム分岐
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (overflowRoop)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含まないの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.Between(getPointLoop, startLoop, goalLoop);
            returnFlag = !returnFlag;
        }
        else
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 周回開始点を含むの弧に含まれる？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnFlag = ChangeData.BetweenNoTouch(getPointLoop, startLoop, goalLoop);
        }
        return returnFlag;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 目標角度を向いているか？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool AngleWheelDeg(float angle, float targetAngle, float difAngle)
    {
        float loopLong = 360.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ弾き
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (difAngle < 0)
        {
            return false;
        }
        if (difAngle >= loopLong)
        {
            return false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 差の角度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float difAngleLong = Division(difAngle, 2.0f);
        float start = targetAngle - difAngleLong;
        float goal = targetAngle + difAngleLong;
        return BetweenRing(angle, start, goal, loopLong);
    }
    public static bool AngleWheelRad(float angle, float targetAngle, float difAngle)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ディグリー化
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float angleDeg = ChangeData.RadToDeg(angle);
        float targetAngleDeg = ChangeData.RadToDeg(targetAngle);
        float difAngleDeg = ChangeData.RadToDeg(difAngle);
        return AngleWheelDeg(angleDeg, targetAngleDeg, difAngleDeg);
    }
    #region 角度の輪の方向
    public static bool AngleWheelDeg(double angle, double targetAngle, double difAngle)
    {
        double loopLong = 360.0f;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データ弾き
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (difAngle < 0)
        {
            return false;
        }
        if (difAngle >= loopLong)
        {
            return false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 差の角度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        double difAngleLong = Division(difAngle, 2.0);
        double start = targetAngle - difAngleLong;
        double goal = targetAngle + difAngleLong;
        return BetweenRing(angle, start, goal, loopLong);
    }
    public static bool AngleWheelRad(double angle, double targetAngle, double difAngle)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ディグリー化
        //*|***|***|***|***|***|***|***|***|***|***|***|
        double angleDeg = ChangeData.RadToDeg(angle);
        double targetAngleDeg = ChangeData.RadToDeg(targetAngle);
        double difAngleDeg = ChangeData.RadToDeg(difAngle);
        return AngleWheelDeg(angleDeg, targetAngleDeg, difAngleDeg);
    }
    #endregion
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポイント１からポイント２までの
    // 直線距離を返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float Hypotenuse(Vector2 point1, Vector2 point2)
    {
        Vector2 dif;
        float difR, difAns;
        dif = point1 - point2;
        difR = Mathf.Atan2(dif.y, dif.x);
        if (Mathf.Cos(difR) != 0.0f)
        {
            difAns = dif.x / Mathf.Cos(difR);
        }
        else
        {
            difAns = dif.y;
        }
        return difAns;
    }


    public static float LongVector2(Vector2 point)
    {
        Vector2 dif;
        float difR, difAns;
        float difX;
        float floatEpsilon = 1.192092896e-07F;
        dif = point;
        difR = Mathf.Atan2(dif.y, dif.x);
        difX = Mathf.Abs(Mathf.Cos(difR));
        if (Mathf.Abs(Mathf.Cos(difR)) > floatEpsilon)
        {
            difAns = dif.x / Mathf.Cos(difR);
        }
        else
        {
            difAns = dif.y;
        }
        return difAns;
    }

    public static float LongVector2(Vector2 start, Vector2 goal)
    {
        Vector2 makeVector2;
        makeVector2 = goal - start;
        return LongVector2(makeVector2);
    }

    public static float LongVector3(Vector3 point)
    {
        float MultiplicationX, MultiplicationY, MultiplicationZ, MultiplicationV;
        MultiplicationX = Multiplication(point.x, 2);
        MultiplicationY = Multiplication(point.y, 2);
        MultiplicationZ = Multiplication(point.z, 2);
        MultiplicationV = SquareRoot(MultiplicationX + MultiplicationY + MultiplicationZ);
        return MultiplicationV;
    }
    public static float LongVector3(Vector3 start, Vector3 goal)
    {
        Vector3 makeVector3;
        makeVector3 = goal - start;
        return LongVector3(makeVector3);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 小の長さより
    // 大の長さが大きいと
    // ＴＵＲＥが返ってくる
    // radiusLongは半径と扱う
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool JudgmentVector2(Vector2 smallLong, Vector2 bigLong)
    {
        float minLong2 = (smallLong.x * smallLong.x) + (smallLong.y * smallLong.y);
        float maxLong2 = (bigLong.x * bigLong.x) + (bigLong.y * bigLong.y);
        if (minLong2 < maxLong2)
        {
            return true;
        }
        return false;
    }

    public static bool JudgmentVector2(Vector2 difLong, float radiusLong)
    {
        float difLong2 = (difLong.x * difLong.x) + (difLong.y * difLong.y);
        float radiusLong2 = radiusLong * radiusLong;
        if (difLong2 < radiusLong2)
        {
            return true;
        }
        return false;
    }

    public static bool JudgmentVector3(Vector3 smallLong, Vector3 bigLong)
    {
        float minLong2 = (smallLong.x * smallLong.x) + (smallLong.y * smallLong.y) + (smallLong.z * smallLong.z);
        float maxLong2 = (bigLong.x * bigLong.x) + (bigLong.y * bigLong.y) + (bigLong.z * bigLong.z);
        if (minLong2 < maxLong2)
        {
            return true;
        }
        return false;
    }

    public static bool JudgmentVector3(Vector3 difLong, float radiusLong)
    {
        float difLong2 = (difLong.x * difLong.x) + (difLong.y * difLong.y) + (difLong.z * difLong.z);
        float radiusLong2 = radiusLong * radiusLong;
        if (difLong2 < radiusLong2)
        {
            return true;
        }
        return false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // triangleの中にpointがあるならTRUE
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool TriangleAndPoint(Vector2 point, Vector2 triangleA, Vector2 triangleB, Vector2 triangleC)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 間を求める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 AB = SubVector2(ref triangleB, ref triangleA);
        Vector2 BP = SubVector2(ref point, ref triangleB);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 間を求める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 BC = SubVector2(ref triangleC, ref triangleB);
        Vector2 CP = SubVector2(ref point, ref triangleC);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 間を求める
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector2 CA = SubVector2(ref triangleA, ref triangleC);
        Vector2 AP = SubVector2(ref point, ref triangleA);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 外積計算
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float pointBcos = OuterProduct(ref AB, ref BP);
        float pointCcos = OuterProduct(ref BC, ref CP);
        float pointAcos = OuterProduct(ref CA, ref AP);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 方向は全て同じか？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if ((pointAcos > 0 && pointBcos > 0 && pointCcos > 0) || (pointAcos < 0 && pointBcos < 0 && pointCcos < 0))
        {
            return true;
        }
        return false;
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 割合計算
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 豆腐カット計算
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Rect RectSize(int number, int width, int height, int pixtureSize_X = 1, int pixtureSize_Y = 1)
    {
        float Fx, Fy;
        Vector2 pos;
        Vector2 tanka;

        pos = Position(number, width, height);

        tanka = TankaSize(width, height, pixtureSize_X, pixtureSize_Y);

        Fx = tanka.x * (pos.x);
        Fy = tanka.y * (pos.y);

        Rect rect = new Rect();
        rect.xMin = Fx;
        rect.yMin = Fy;
        rect.xMax = (rect.xMin + tanka.x);
        rect.yMax = (rect.yMin + tanka.y);
        return rect;
    }
    public static Rect RectSizeReverse_Axis(int number, int width, int height, int pixtureSize_X, int pixtureSize_Y)
    {
        float Fx, Fy;
        Vector2 pos;
        Vector2 tanka;

        pos = Position(number, width, height);

        tanka = TankaSize(width, height, pixtureSize_X, pixtureSize_Y);

        Fx = tanka.x * (pos.x);
        Fy = tanka.y * (pos.y);

        Rect rect = new Rect();
        rect.xMin = Fy;
        rect.yMin = Fx;
        rect.xMax = (rect.xMin + tanka.y);
        rect.yMax = (rect.yMin + tanka.x);
        return rect;
    }
    public static Rect RectSizeReverse_Y(int number, int width, int height, int pixtureSize_X = 1, int pixtureSize_Y = 1)
    {
        float Fx, Fy;
        Vector2 pos;
        Vector2 tanka;

        pos = Position(number, width, height);

        tanka = TankaSize(width, height, pixtureSize_X, pixtureSize_Y);

        Fx = tanka.x * (pos.x);
        Fy = tanka.y * ((height - 1) - pos.y);

        Rect rect = new Rect();
        rect.xMin = Fx;
        rect.yMin = Fy;
        rect.xMax = (rect.xMin + tanka.x);
        rect.yMax = (rect.yMin + tanka.y);
        return rect;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 豆腐カット場所計算
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector2 Position(int number, int width, int height)
    {
        Vector2 pos;

        if (width < 0)
        {
            width = 1;
        }
        if (height < 0)
        {
            height = 1;
        }
        if (number < 0 || number >= width * height)
        {
            number = 0;
        }
        pos.x = (float)(number % width);
        pos.y = (float)(number / width);

        return pos;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 豆腐カット単価計算
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector2 TankaSize(int width, int height, int pixtureSize_X = 1, int pixtureSize_Y = 1)
    {
        Vector2 tanka;
        float tankax, tankay;

        tankax = (1.0f / width) * pixtureSize_X;
        tankay = (1.0f / height) * pixtureSize_Y;

        tanka = new Vector2(tankax, tankay);

        return tanka;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ピボット位置が効いているように移動する
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector2 RestoreOrigin(Vector2 size, Vector2 origin, Vector2 center)
    {
        Vector2 difMove = new Vector2(0, 0);
        Vector2 difOrigin = CreateVector2(ref center, ref origin);
        difMove = EachTimes(difOrigin, size);
        return difMove;
    }
    public static Vector3 RestoreOrigin(Vector3 size, Vector3 origin, Vector3 center)
    {
        Vector3 difMove = new Vector3(0, 0);
        Vector3 difOrigin = CreateVector3(ref center, ref origin);
        difMove = EachTimes(difOrigin, size);
        return difMove;
    }
    //*|**|***|***|***|***|***|***|***|***|***|***|
    // 豆腐カット実数値
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void GetEnum2Dimensions(int number, int horizontalNum, int verticalNum, ref int dataHorizontal, ref int dataVertical)
    {
        int horizontalX;
        int horizontalY;
        int verticalX;
        int verticalY;

        if (horizontalNum < 0)
        {
            horizontalNum = 1;
        }
        if (verticalNum < 0)
        {
            verticalNum = 1;
        }
        if (number < 0)
        {
            number = 0;
        }

        horizontalX = (number % horizontalNum);
        horizontalY = Division(number, horizontalNum);
        verticalX = (horizontalY % horizontalNum);
        verticalY = Division(horizontalY, horizontalNum);
        dataHorizontal = horizontalX;
        dataVertical = verticalX;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 間のポイントを得る
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float Leap(float start, float goal, float point)
    {
        float ans;
        ans = ((goal - start) * point);
        ans = ans + start;
        return ans;
    }
    public static double Leap(double start, double goal, double point)
    {
        double ans;
        ans = ((goal - start) * point);
        ans = ans + start;
        return ans;
    }
    public static Vector2 Leap(Vector2 start, Vector2 goal, float point)
    {
        Vector2 ansV2;
        ansV2.x = ((goal.x - start.x) * point);
        ansV2.y = ((goal.y - start.y) * point);
        ansV2 = ansV2 + start;
        return ansV2;
    }

    public static Vector3 Leap(Vector3 start, Vector3 goal, float point)
    {
        Vector3 ansV3;
        ansV3.x = ((goal.x - start.x) * point);
        ansV3.y = ((goal.y - start.y) * point);
        ansV3.z = ((goal.z - start.z) * point);
        ansV3 = ansV3 + start;
        return ansV3;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 間のポイントを計算する
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float LeapReverseCalculation(float start, float goal, float point)
    {
        float ans;
        float longPoint = point - start;
        float difStartAndGoal = goal - start;
        ans = MyCalculator.Division(longPoint, difStartAndGoal);
        return ans;
    }
    public static double LeapReverseCalculation(double start, double goal, double point)
    {
        double ans;
        double longPoint = point - start;
        double difStartAndGoal = goal - start;
        ans = MyCalculator.Division(longPoint, difStartAndGoal);
        return ans;
    }


    public static Quaternion Sleap(Quaternion start, Quaternion goal, float point)
    {
        Quaternion ansQuaternion;
        ansQuaternion = Quaternion.Slerp(start, goal, point);
        return ansQuaternion;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゴールに割合で向かって
    // 誤差以下なら同じにする
    // 誤差以下ならTRUEが返る
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float GoParsent(float start, float goal, float parsent, float error)
    {
        float move = (goal - start) * parsent;
        float point = start + move;
        float dif = goal - parsent;
        if (dif < error)
        {
            return goal;
        }
        return point;
    }
    public static float GoParsent100(float start, float goal, float parsent, float error)
    {
        float move = (goal - start) * parsent * 100.0f;
        float point = start + move;
        float dif = goal - parsent;
        if (dif < error)
        {
            return goal;
        }
        return point;
    }
    public static double GoParsent(double start, double goal, double parsent, double error)
    {
        double move = (goal - start) * parsent;
        double point = start + move;
        double dif = goal - parsent;
        if (dif < error)
        {
            return goal;
        }
        return point;
    }
    public static double GoParsent100(double start, double goal, double parsent, double error)
    {
        double move = (goal - start) * parsent * 100.0f;
        double point = start + move;
        double dif = goal - parsent;
        if (dif < error)
        {
            return goal;
        }
        return point;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 誤差以下ならTRUEが返る
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool GoParsent(ref float  getPoint, float start, float goal, float parsent, float error)
    {
        float move = (goal - start) * parsent;
        float point = start + move;
        float dif = goal - parsent;
        if (dif < error)
        {
            getPoint = goal;
            return true;
        }
        getPoint = point;
        return false;
    }
    public static bool GoParsent100(ref float  getPoint, float start, float goal, float parsent, float error)
    {
        float move = (goal - start) * parsent * 100.0f;
        float point = start + move;
        float dif = goal - parsent;
        if (dif < error)
        {
            getPoint = goal;
            return true;
        }
        getPoint = point;
        return false;
    }
    public static bool GoParsent(ref double getPoint, double start, double goal, double parsent, double error)
    {
        double move = (goal - start) * parsent;
        double point = start + move;
        double dif = goal - parsent;
        if (dif < error)
        {
            getPoint = goal;
            return true;
        }
        getPoint = point;
        return false;
    }
    public static bool GoParsent100(ref double getPoint, double start, double goal, double parsent, double error)
    {
        double move = (goal - start) * parsent * 100.0f;
        double point = start + move;
        double dif = goal - parsent;
        if (dif < error)
        {
            getPoint = goal;
            return true;
        }
        getPoint = point;
        return false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Matrix作成
    // point に確実に移動（回転、拡縮の影響を受けない）
    // rotate に回転
    // scale に拡縮
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Matrix4x4 MakeMatrix(Vector3 point, Quaternion rotate, Vector3 scale)
    {
        Matrix4x4 world = Matrix4x4.Scale(scale) *
        Matrix4x4.Rotate(rotate) *
        Matrix4x4.Translate(point);
        return world;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Quaternion作成
    // ヨーピッチロール
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Quaternion MakeQuaternionYawPitchRoll(Vector3 rotation)
    {
        Quaternion rotationQ;
        rotationQ = Quaternion.Euler(rotation.y, rotation.z, rotation.x);
        return rotationQ;
    }
    public static Quaternion MakeQuaternionYawPitchRoll_HL(Vector3 rotation)
    {
        Quaternion rotationQ;
        rotationQ = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        return rotationQ;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 補完する数字を返す
    // Start + X = goal
    // となる
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Quaternion InterpolationQuaternion(Quaternion start, Quaternion goal)
    {
        Quaternion interpolation;
        Quaternion startInverse;
        startInverse = Quaternion.Inverse(start);
        interpolation = goal * startInverse;
        return interpolation;
    }
    public static Quaternion InterpolationQuaternion(Vector3 start, Vector3 goal)
    {
        Quaternion interpolation;
        Vector3 among;
        start.Normalize();
        goal.Normalize();
        among = Vector3.Cross(start, goal);
        float dotS = Vector3.Dot(start, goal);
        if (Mathf.Abs(1 + dotS) <= float.Epsilon)
        {
            interpolation.x = 1.0f;
            interpolation.y = interpolation.z = interpolation.w = 0.0f;
            return interpolation;
        }
        float sprtS = Mathf.Sqrt((1 + dotS) * 2.0f);
        interpolation.x = among.x / sprtS;
        interpolation.y = among.y / sprtS;
        interpolation.z = among.z / sprtS;
        interpolation.w = sprtS / 2.0f;
        return interpolation;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ベクトルの内積
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float InnerProduct(ref Vector2 a, ref Vector2 b)
    {
        return (a.x * b.x + a.y * b.y);//a・b
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ベクトルの内積
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float InnerProduct(ref Vector3 a, ref Vector3 b)
    {
        //Vector3 ans;
        //ans.x = (a.y * b.z + a.z * b.y);
        //ans.y = (a.z * b.x + a.x * b.z);
        //ans.z = (a.x * b.y + a.y * b.x);
        return (a.x * b.x + a.y * b.y + a.z * b.z);
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ベクトルの外積
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float OuterProduct(ref Vector2 a, ref Vector2 b)
    {
        return (a.x * b.y - a.y * b.x);//a×b
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ベクトルの外積
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 OuterProduct(ref Vector3 a, ref Vector3 b)
    {
        Vector3 ans;
        ans.x = (a.y * b.z - a.z * b.y);
        ans.y = (a.z * b.x - a.x * b.z);
        ans.z = (a.x * b.y - a.y * b.x);
        return ans;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //ベクトルの長さの2乗
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float VectorLength2(ref Vector2 v)
    {
        return InnerProduct(ref v, ref v);//v・v=|v|^2
    }

    public static Vector2 CreateVector2(ref Vector2 start, ref Vector2 goal)
    {
        return new Vector2(goal.x - start.x, goal.y - start.y);//start->qベクトル
    }

    public static Vector3 CreateVector3(ref Vector3 start, ref Vector3 goal)
    {
        return new Vector3(goal.x - start.x, goal.y - start.y, goal.z - start.z);//start->goalベクトル
    }

    public static Vector2 SubVector2(ref Vector2 A, ref Vector2 B)
    {
        return new Vector2(A.x - B.x, A.y - B.y);//start->qベクトル
    }

    public static Vector3 SubVector3(ref Vector3 A, ref Vector3 B)
    {
        return new Vector3(A.x - B.x, A.y - B.y, A.z - B.z);//start->goalベクトル
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ２角の差を求める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 DifferenceAngleVector3(ref Vector3 start, ref Vector3 goal)
    {
        Vector3 ansAngle;
        Vector3 trueGoal = ChangeData.SetDeg360(goal);
        Vector3 trueStart = ChangeData.SetDeg360(start);
        ansAngle = trueGoal - trueStart;
        ansAngle = goal - start;
        ansAngle = ChangeData.SetDeg360Signed(ansAngle);
        return ansAngle;
    }
    public static Vector3 DifferenceAngleVector3Short(ref Vector3 start, ref Vector3 goal)
    {
        Vector3 ansAngle;
        Vector3 trueGoal = ChangeData.SetDeg360(goal);
        Vector3 trueStart = ChangeData.SetDeg360(start);
        ansAngle = trueGoal - trueStart;
        ansAngle = goal - start;
        ansAngle = ChangeData.SetDeg360Short(ansAngle);
        return ansAngle;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 2つの角度の世界線
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 DirectionToAngle_PYR(Vector3 start)
    {
        float lowAngle = 0;
        float highAngle = 0;
        return DirectionToAngle_PYR(start, ref highAngle, ref lowAngle);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 2つの角度の世界線
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 DirectionToAngle_PYR(Vector3 start, ref float angleTop, ref float angleLow)
    {
        float x, y, z;
        float lowLong = 0;
        float lowAngle = 0;
        float highAngle = 0;
        start.Normalize();
        x = start.x;
        y = start.y;
        z = start.z;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        lowAngle = ChangeData.Vector2ToAngleRad(new Vector2(x, z));
        Vector3 lowVector = new Vector3(start.x, 0, start.z);
        lowLong = LongVector3(lowVector);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float fl = InnerProduct(ref lowVector, ref start);
        float fl2 = 1 * lowLong;
        if (fl2 == 0.0f)
        {
            highAngle = 0.0f;
        }
        else
        {
            highAngle = Mathf.Acos(fl / fl2);
        }

        //highAngle = ChangeData.Vector2ToAngleRad(new Vector2(y, z));
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        angleLow = lowAngle;
        angleTop = highAngle;
        return DirectionToAngle_PYR(highAngle, lowAngle);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 2つの角度の世界線
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 DirectionToAngle_PYR(float angleTop, float angleLow)
    {
        float x, y, z;
        float lowAngle = 0;
        float highAngle = 0;
        x = 0;
        y = 0;
        z = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        lowAngle = angleLow;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        highAngle = angleTop;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        y = lowAngle;
        x = highAngle;
        z = 0;
        Vector3 angle;
        angle = new Vector3(x, y, z);
        return angle;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 2つの角度の世界線
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 DirectionToAngle_PYRDeg(Vector3 start)
    {
        float lowAngle = 0;
        float highAngle = 0;
        return DirectionToAngle_PYRDeg(start, ref highAngle, ref lowAngle);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 2つの角度の世界線
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 DirectionToAngle_PYRDeg(Vector3 start, ref float angleTop, ref float angleLow)
    {
        float x, y, z;
        float lowLong = 0;
        float lowAngle = 0;
        float highAngle = 0;
        start.Normalize();
        x = start.x;
        y = start.y;
        z = start.z;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        lowAngle = ChangeData.Vector2ToAngleDeg(new Vector2(x, z));
        lowAngle = ChangeData.Vector2ToAngleRad(new Vector2(x, z));
        Vector3 lowVector = new Vector3(start.x, 0, start.z);
        lowLong = LongVector3(lowVector);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float fl = InnerProduct(ref lowVector, ref start);

        float fl2 = 1 * lowLong;
        if (fl2 == 0.0f)
        {
            //highAngle = 1;
            highAngle = ChangeData.GetRadDeg90();
            if (y < 0)
            {
                highAngle *= -1;
            }
        }
        else
        {
            fl = ChangeData.Among(fl, -1.0f, 1.0f);
            highAngle = Mathf.Acos(fl / fl2);
            if (y < 0)
            {
                highAngle *= -1;
            }
        }

        //highAngle = ChangeData.Vector2ToAngleRad(new Vector2(y, z));
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        angleLow = lowAngle;
        angleTop = highAngle;
        return DirectionToAngle_PYRDeg(highAngle, lowAngle);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 2つの角度の世界線
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 DirectionToAngle_PYRDeg(float angleTop, float angleLow)
    {
        float x, y, z;
        float lowAngle = 0;
        float highAngle = 0;
        x = 0;
        y = 0;
        z = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        lowAngle = angleLow;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        highAngle = angleTop;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // Y軸の世界
        //*|***|***|***|***|***|***|***|***|***|***|***|
        y = ChangeData.RadToDeg(lowAngle);
        x = ChangeData.RadToDeg(highAngle);
        z = ChangeData.RadToDeg(0);
        Vector3 angle;
        angle = new Vector3(x, y, z);
        return angle;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 融合する角度の世界線
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 DirectionAndAngle_PYRToDirection(Vector3 direction, Vector3 angle)
    {
        float startLong = 0;
        Vector3 startAngle = new Vector3(0, 0);
        Vector3 totalAngle = new Vector3(0, 0);
        Vector3 v = new Vector3(0, 0);
        startLong = LongVector3(direction);
        startAngle = DirectionToAngle_PYR(direction);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 融合角度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        totalAngle = startAngle + angle;
        v = Angle_PYRToDirection(totalAngle);
        v *= startLong;
        return v;
    }
    public static Vector3 DirectionAndAngle_PYRToDirectionDeg(Vector3 direction, Vector3 angle)
    {
        float startLong = 0;
        Vector3 startAngle = new Vector3(0, 0);
        Vector3 totalAngle = new Vector3(0, 0);
        Vector3 v = new Vector3(0, 0);
        startLong = LongVector3(direction);
        startAngle = DirectionToAngle_PYRDeg(direction);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 融合角度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        totalAngle = startAngle + angle;
        v = Angle_PYRToDirectionDeg(totalAngle);
        v *= startLong;
        return v;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 新しい方向に向かって
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 Angle_PYRToDirection(Vector3 angle)
    {
        Vector3 v = new Vector3(1.0f, 0.0f, 0.0f);
        Quaternion rotZ = Quaternion.Euler(0, 0, ChangeData.RadToDeg(angle.x));
        Quaternion rotY = Quaternion.Euler(0, ChangeData.RadToDeg(-angle.y), 0);
        v = rotZ * v;
        v = rotY * v;
        Vector3 answer = v;
        return answer;
    }
    public static Vector3 Angle_PYRToDirectionDeg(Vector3 angle)
    {

        Vector3 v = new Vector3(1.0f, 0.0f, 0.0f);
        Quaternion rotZ = Quaternion.Euler(0, 0, angle.x);
        Quaternion rotY = Quaternion.Euler(0, -angle.y, 0);
        v = rotZ * v;
        v = rotY * v;
        Vector3 answer = v;
        if (Mathf.Abs(answer.x) <= float.Epsilon)
        {
            answer.x = 0;
        }
        if (Mathf.Abs(answer.y) <= float.Epsilon)
        {
            answer.y = 0;
        }
        //XMConvertToRadians
        return answer;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回転後の場所を求める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector2 RotatedPositionDeg(ref Vector2 startPoint, ref float moveAngle)
    {
        Vector2 normalVector = new Vector2(0, 0);
        Vector2 returnPoint = new Vector2(0, 0);
        float startLong = LongVector2(startPoint);
        float startAngle = ChangeData.Vector2ToAngleDeg(startPoint);
        float goalAngle = startAngle + moveAngle;
        normalVector = ChangeData.AngleDegToVector2(goalAngle);
        returnPoint = normalVector * startLong;
        return returnPoint;
    }
    public static Vector2 RotatedPositionRad(ref Vector2 startPoint, ref float moveAngle)
    {
        Vector2 normalVector = new Vector2(0, 0);
        Vector2 returnPoint = new Vector2(0, 0);
        float startLong = LongVector2(startPoint);
        float startAngle = ChangeData.Vector2ToAngleRad(startPoint);
        float goalAngle = startAngle + moveAngle;
        normalVector = ChangeData.AngleRadToVector2(goalAngle);
        returnPoint = normalVector * startLong;
        return returnPoint;
    }
    public static float MakeFuzzyGrade(ref float value, ref float startPoint, ref float endPoint)
    {
        float ansValue = 0.0f;
        if (value <= startPoint)
        {
            ansValue = 0.0f;
            return ansValue;
        }
        if (value >= endPoint)
        {
            ansValue = 1.0f;
            return ansValue;
        }
        float V = Division(value, endPoint - startPoint);
        float S = Division(startPoint, endPoint - startPoint);
        ansValue = V - S;
        return ansValue;
    }
    public static float MakeFuzzyReverseGrade(ref float value, ref float startPoint, ref float endPoint)
    {
        float ansValue = 0.0f;
        if (value <= startPoint)
        {
            ansValue = 1.0f;
            return ansValue;
        }
        if (value >= endPoint)
        {
            ansValue = 0.0f;
            return ansValue;
        }
        float V = Division(-value, endPoint - startPoint);
        float S = Division(endPoint, endPoint - startPoint);
        ansValue = V + S;
        return ansValue;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 三角形の作成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float MakeFuzzyTriangle(ref float value, ref float startPoint, ref float vertexPoint, ref float endPoint)
    {
        float ansValue = 0.0f;
        if (value == vertexPoint)
        {
            ansValue = 1.0f;
            return ansValue;
        }
        if (value < vertexPoint)
        {
            ansValue = MakeFuzzyGrade(ref value, ref startPoint, ref vertexPoint);
            return ansValue;
        }
        if (value > vertexPoint)
        {
            ansValue = MakeFuzzyReverseGrade(ref value, ref vertexPoint, ref endPoint);
            return ansValue;
        }
        return ansValue;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 台形の作成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float MakeFuzzyTrapezoid(ref float value, ref float startPoint, ref float startVertexPoint, ref float endVertexPoint, ref float endPoint)
    {
        float ansValue = 0.0f;
        if (value >= startVertexPoint && value <= endVertexPoint)
        {
            ansValue = 1.0f;
            return ansValue;
        }
        if (value < startVertexPoint)
        {
            ansValue = MakeFuzzyGrade(ref value, ref startPoint, ref startVertexPoint);
            return ansValue;
        }
        if (value > endVertexPoint)
        {
            ansValue = MakeFuzzyReverseGrade(ref value, ref endVertexPoint, ref endPoint);
            return ansValue;
        }
        return ansValue;
    }

}



