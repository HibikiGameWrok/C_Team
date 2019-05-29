using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChangeData
{

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 円周率
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float PI = 3.141592654f;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 3dsmaxの単位 100 = Unityの単位 2.56
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float UnitMax3ds = 100.0f;
    public static float UnitUnityAndMax = 2.56f;
    public static float UnitmaxToUnity3ds = UnitUnityAndMax / UnitMax3ds;
    public static float UnitUnityToMax3ds = UnitMax3ds / UnitUnityAndMax;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Dot（ドット）の単位 2.56 = Unityの単位 1
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float UnitDot = 2.56f;
    public static float UnitUnityAndDot = 1.0f;
    public static float UnitDotToUnity = UnitUnityAndDot / UnitDot;
    public static float UnitUnityToDot = UnitDot / UnitUnityAndDot;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 3dsmaxの単位　１　をUnityの単位　１　にする
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float Max3dsToUnity(float numberFloat)
    {
        return (UnitmaxToUnity3ds * numberFloat);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 3dsmaxの単位　１　をUnityの単位　１　にする
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 Max3dsToUnity(Vector3 numberVector3)
    {
        Vector3 returnVector3 = new Vector3(0, 0, 0);
        returnVector3.x = Max3dsToUnity(numberVector3.x);
        returnVector3.y = Max3dsToUnity(numberVector3.y);
        returnVector3.z = Max3dsToUnity(numberVector3.z);
        return returnVector3;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Unityの単位　１　を3dsmaxの単位　１　にする
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float UnityToMax3ds(float numberFloat)
    {
        return (UnitUnityToMax3ds * numberFloat);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Unityの単位　１　を3dsmaxの単位　１　にする
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 UnityToMax3ds(Vector3 numberVector3)
    {
        Vector3 returnVector3 = new Vector3(0, 0, 0);
        returnVector3.x = UnityToMax3ds(numberVector3.x);
        returnVector3.y = UnityToMax3ds(numberVector3.y);
        returnVector3.z = UnityToMax3ds(numberVector3.z);
        return returnVector3;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ドットの単位　１　をUnityの単位　１　にする
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float DotToUnity(float numberFloat)
    {
        return (UnitmaxToUnity3ds * numberFloat);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ドットの単位　１　をUnityの単位　１　にする
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 DotToUnity(Vector3 numberVector3)
    {
        Vector3 returnVector3 = new Vector3(0, 0, 0);
        returnVector3.x = DotToUnity(numberVector3.x);
        returnVector3.y = DotToUnity(numberVector3.y);
        returnVector3.z = DotToUnity(numberVector3.z);
        return returnVector3;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Unityの単位　１　をドットの単位　１　にする
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float UnityToDot(float numberFloat)
    {
        return (UnitUnityToDot * numberFloat);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Unityの単位　１　をドットの単位　１　にする
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 UnityToDot(Vector3 numberVector3)
    {
        Vector3 returnVector3 = new Vector3(0, 0, 0);
        returnVector3.x = UnityToDot(numberVector3.x);
        returnVector3.y = UnityToDot(numberVector3.y);
        returnVector3.z = UnityToDot(numberVector3.z);
        return returnVector3;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Unityの単位　１　をドットの単位　１　にする
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector3 HorizontalViewToTopViewForVector3(Vector3 numberFloat)
    {
        Vector3 chengeVector3 = new Vector3();
        chengeVector3.x = numberFloat.x;
        chengeVector3.y = numberFloat.z;
        chengeVector3.z = numberFloat.y;
        return chengeVector3;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値の交換
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void Change2Data(ref int number1, ref int number2)
    {
        int t = number1;
        number1 = number2;
        number2 = t;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値の交換
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void Change2Data(ref float number1, ref float number2)
    {
        float t = number1;
        number1 = number2;
        number2 = t;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値の交換
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void Change2Data(ref uint number1, ref uint number2)
    {
        uint t = number1;
        number1 = number2;
        number2 = t;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値の交換
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void Change2Data(ref double number1, ref double number2)
    {
        double t = number1;
        number1 = number2;
        number2 = t;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ディグリーとラジアン
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float DegToRad(float angle)
    {
        return (angle / 180.0f * PI);
    }
    public static float RadToDeg(float angle)
    {
        return (angle * 180.0f / PI);
    }
    public static double DegToRad(double angle)
    {
        return (angle / 180.0f * PI);
    }
    public static double RadToDeg(double angle)
    {
        return (angle * 180.0f / PI);
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ディグリーとラジアン
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector2 DegToRad(Vector2 angle)
    {
        Vector2 radAngle;
        radAngle.x = DegToRad(angle.x);
        radAngle.y = DegToRad(angle.y);
        return radAngle;
    }
    public static Vector2 RadToDeg(Vector2 angle)
    {
        Vector2 degAngle;
        degAngle.x = RadToDeg(angle.x);
        degAngle.y = RadToDeg(angle.y);
        return degAngle;
    }
    public static Vector3 DegToRad(Vector3 angle)
    {
        Vector3 radAngle;
        radAngle.x = DegToRad(angle.x);
        radAngle.y = DegToRad(angle.y);
        radAngle.z = DegToRad(angle.z);
        return radAngle;
    }
    public static Vector3 RadToDeg(Vector3 angle)
    {
        Vector3 degAngle;
        degAngle.x = RadToDeg(angle.x);
        degAngle.y = RadToDeg(angle.y);
        degAngle.z = RadToDeg(angle.z);
        return degAngle;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 角度のオーバーフロー防止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float SetDeg360(float angle)
    {
        float angleR = AntiOverflow(angle, 360.0f);
        return angleR;
    }
    public static Vector2 SetDeg360(Vector2 angle)
    {
        Vector2 angleR = angle;
        angleR.x = SetDeg360(angleR.x);
        angleR.y = SetDeg360(angleR.y);
        return angleR;
    }
    public static Vector3 SetDeg360(Vector3 angle)
    {
        Vector3 angleR = angle;
        angleR.x = SetDeg360(angleR.x);
        angleR.y = SetDeg360(angleR.y);
        angleR.z = SetDeg360(angleR.z);
        return angleR;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 平行線オーバーフロー防止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float SetDeg360Signed(float angle)
    {
        bool singPlus;
        singPlus = (angle >= 0);
        float angleR1 = Mathf.Abs(angle);
        float angleR2 = AntiOverflow(angleR1, 360.0f);
        float angleR3 = angleR2;
        if (!singPlus)
        {
            angleR3 *= -1;
        }
        return angleR3;
    }
    public static Vector2 SetDeg360Signed(Vector2 angle)
    {
        Vector2 angleR = angle;
        angleR.x = SetDeg360Signed(angleR.x);
        angleR.y = SetDeg360Signed(angleR.y);
        return angleR;
    }
    public static Vector3 SetDeg360Signed(Vector3 angle)
    {
        Vector3 angleR = angle;
        angleR.x = SetDeg360Signed(angleR.x);
        angleR.y = SetDeg360Signed(angleR.y);
        angleR.z = SetDeg360Signed(angleR.z);
        return angleR;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 最短オーバーフロー防止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float SetDeg360Short(float angle)
    {
        bool singPlus;
        singPlus = (angle >= 0);
        float angleR1 = Mathf.Abs(angle);
        float angleR2 = AntiOverflow(angleR1, 360.0f);
        float angleR3 = angleR2;
        if (angleR3 > 180.0f)
        {
            angleR3 = angleR3 - 360.0f;
        }
        if (!singPlus)
        {
            angleR3 *= -1;
        }

        return angleR3;
    }
    public static Vector2 SetDeg360Short(Vector2 angle)
    {
        Vector2 angleR = angle;
        angleR.x = SetDeg360Short(angleR.x);
        angleR.y = SetDeg360Short(angleR.y);
        return angleR;
    }
    public static Vector3 SetDeg360Short(Vector3 angle)
    {
        Vector3 angleR = angle;
        angleR.x = SetDeg360Short(angleR.x);
        angleR.y = SetDeg360Short(angleR.y);
        angleR.z = SetDeg360Short(angleR.z);
        return angleR;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 四捨五入してイント型
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int FloatToIntRound(float numberFloat)
    {
        int numberInt = (int)numberFloat;
        float circle = numberFloat - (float)numberInt;
        if (circle >= 0.5f)
        {
            numberInt += 1;
        }
        return numberInt;
        //return Mathf.RoundToInt(numberFloat);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ディグリーのアングルが引数
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector2 AngleDegToVector2(float angle)
    {
        Vector2 returnAngle;
        angle = DegToRad(angle);
        returnAngle.x = Mathf.Cos(angle);
        returnAngle.y = Mathf.Sin(angle);
        return returnAngle;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 返す角度がディグリー
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float Vector2ToAngleDeg(Vector2 vector2)
    {
        float returnAngle = 0;
        if (!(vector2.x == 0.0f && vector2.y == 0))
        {
            returnAngle = Mathf.Atan2(vector2.y, vector2.x);
        }
        return RadToDeg(returnAngle);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ラジアンのアングルが引数
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static Vector2 AngleRadToVector2(float angle)
    {
        Vector2 returnAngle;
        returnAngle.x = Mathf.Cos(angle);
        returnAngle.y = Mathf.Sin(angle);
        return returnAngle;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 返す角度がラジアン
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float Vector2ToAngleRad(Vector2 vector2)
    {
        float returnAngle = 0;
        if (!(vector2.x == 0.0f && vector2.y == 0.0f))
        {
            returnAngle = Mathf.Atan2(vector2.y, vector2.x);
        }
        return returnAngle;
    }
    public static float Reflection(float rushAngle, float normalAngle)
    {
        Vector2 Vector2A = AngleRadToVector2(DegToRad(rushAngle));
        Vector2 Vector2B = AngleRadToVector2(DegToRad(normalAngle));
        Vector2 Vector2AR = Vector2A * -1;

        float naiseki = Vector2AR.x * Vector2B.x + Vector2AR.y * Vector2B.y;
        Vector2 jugmentVector2 = (Vector2A + (Vector2B * naiseki * 2));
        float jugmentAngle = RadToDeg(Vector2ToAngleRad(jugmentVector2));
        return jugmentAngle;
    }

    public static Vector2 Vector2AndAngleToVector2(Vector2 vector, float angle)
    {
        Vector2 returnAngle;
        float vectorLong = MyCalculator.LongVector2(vector);
        float Phi = Vector2ToAngleRad(vector);
        float Theta = DegToRad(angle) + Phi;
        float jugX, jugY;
        //jugX = Mathf.Cos(Phi) * Mathf.Cos(Theta) - Mathf.Sin(Phi) * Mathf.Sin(Theta);
        //jugY = Mathf.Sin(Phi) * Mathf.Cos(Theta) + Mathf.Cos(Phi) * Mathf.Sin(Theta);
        jugX = Mathf.Cos(Theta);
        jugY = Mathf.Sin(Theta);
        returnAngle.x = vectorLong * jugX;
        returnAngle.y = vectorLong * jugY;
        return returnAngle;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // の間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int Among(int number, int min, int max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        if (min > number)
        {
            number = min;
        }
        if (max < number)
        {
            number = max;
        }
        return number;
    }
    public static float Among(float number, float min, float max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        if (min > number)
        {
            number = min;
        }
        if (max < number)
        {
            number = max;
        }
        return number;
    }
    public static int AmongLess(int number, int min, int max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        max = max - 1;
        if (min > number)
        {
            number = min;
        }
        if (max < number)
        {
            number = max;
        }
        return number;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // の間か？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool Between(int number, int min, int max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        if (min > number)
        {
            return false;
        }
        if (max < number)
        {
            return false;
        }
        return true;
    }
    public static bool Between(float number, float min, float max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        if (min > number)
        {
            return false;
        }
        if (max < number)
        {
            return false;
        }
        return true;
    }
    public static bool Between(double number, double min, double max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        if (min > number)
        {
            return false;
        }
        if (max < number)
        {
            return false;
        }
        return true;
    }
    public static bool Between(uint number, uint min, uint max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        if (min > number)
        {
            return false;
        }
        if (max < number)
        {
            return false;
        }
        return true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // の間との間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool Between(int numberMin, int numberMax, int thresholdMin, int thresholdMax)
    {
        if (numberMin > numberMax)
        {
            Change2Data(ref numberMin, ref numberMax);
        }
        if (thresholdMin > thresholdMax)
        {
            Change2Data(ref thresholdMin, ref thresholdMax);
        }
        if (numberMin >= thresholdMin && numberMin <= thresholdMax)
        {
            return true;
        }
        if (numberMax >= thresholdMin && numberMax <= thresholdMax)
        {
            return true;
        }
        return false;
    }
    public static bool Between(float numberMin, float numberMax, float thresholdMin, float thresholdMax)
    {
        if (numberMin > numberMax)
        {
            Change2Data(ref numberMin, ref numberMax);
        }
        if (thresholdMin > thresholdMax)
        {
            Change2Data(ref thresholdMin, ref thresholdMax);
        }
        if (numberMin >= thresholdMin && numberMin <= thresholdMax)
        {
            return true;
        }
        if (numberMax >= thresholdMin && numberMax <= thresholdMax)
        {
            return true;
        }
        return false;
    }
    public static bool Between(double numberMin, double numberMax, double thresholdMin, double thresholdMax)
    {
        if (numberMin > numberMax)
        {
            Change2Data(ref numberMin, ref numberMax);
        }
        if (thresholdMin > thresholdMax)
        {
            Change2Data(ref thresholdMin, ref thresholdMax);
        }
        if (numberMin >= thresholdMin && numberMin <= thresholdMax)
        {
            return true;
        }
        if (numberMax >= thresholdMin && numberMax <= thresholdMax)
        {
            return true;
        }
        return false;
    }
    public static bool Between(uint numberMin, uint numberMax, uint thresholdMin, uint thresholdMax)
    {
        if (numberMin > numberMax)
        {
            Change2Data(ref numberMin, ref numberMax);
        }
        if (thresholdMin > thresholdMax)
        {
            Change2Data(ref thresholdMin, ref thresholdMax);
        }
        if (numberMin >= thresholdMin && numberMin <= thresholdMax)
        {
            return true;
        }
        if (numberMax >= thresholdMin && numberMax <= thresholdMax)
        {
            return true;
        }
        return false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // の間か？
    // 触れるはFALSE
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool BetweenNoTouch(int number, int min, int max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        if (min >= number)
        {
            return false;
        }
        if (max <= number)
        {
            return false;
        }
        return true;
    }
    public static bool BetweenNoTouch(float number, float min, float max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        if (min >= number)
        {
            return false;
        }
        if (max <= number)
        {
            return false;
        }
        return true;
    }
    public static bool BetweenNoTouch(double number, double min, double max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        if (min >= number)
        {
            return false;
        }
        if (max <= number)
        {
            return false;
        }
        return true;
    }
    public static bool BetweenNoTouch(uint number, uint min, uint max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        if (min >= number)
        {
            return false;
        }
        if (max <= number)
        {
            return false;
        }
        return true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // の間との間
    // 触れるはFALSE
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool BetweenNoTouch(int numberMin, int numberMax, int thresholdMin, int thresholdMax)
    {
        if (numberMin > numberMax)
        {
            Change2Data(ref numberMin, ref numberMax);
        }
        if (thresholdMin > thresholdMax)
        {
            Change2Data(ref thresholdMin, ref thresholdMax);
        }
        if (numberMin > thresholdMin && numberMin < thresholdMax)
        {
            return true;
        }
        if (numberMax > thresholdMin && numberMax < thresholdMax)
        {
            return true;
        }
        return false;
    }
    public static bool BetweenNoTouch(float numberMin, float numberMax, float thresholdMin, float thresholdMax)
    {
        if (numberMin > numberMax)
        {
            Change2Data(ref numberMin, ref numberMax);
        }
        if (thresholdMin > thresholdMax)
        {
            Change2Data(ref thresholdMin, ref thresholdMax);
        }
        if (numberMin > thresholdMin && numberMin < thresholdMax)
        {
            return true;
        }
        if (numberMax > thresholdMin && numberMax < thresholdMax)
        {
            return true;
        }
        return false;
    }
    public static bool BetweenNoTouch(double numberMin, double numberMax, double thresholdMin, double thresholdMax)
    {
        if (numberMin > numberMax)
        {
            Change2Data(ref numberMin, ref numberMax);
        }
        if (thresholdMin > thresholdMax)
        {
            Change2Data(ref thresholdMin, ref thresholdMax);
        }
        if (numberMin > thresholdMin && numberMin < thresholdMax)
        {
            return true;
        }
        if (numberMax > thresholdMin && numberMax < thresholdMax)
        {
            return true;
        }
        return false;
    }
    public static bool BetweenNoTouch(uint numberMin, uint numberMax, uint thresholdMin, uint thresholdMax)
    {
        if (numberMin > numberMax)
        {
            Change2Data(ref numberMin, ref numberMax);
        }
        if (thresholdMin > thresholdMax)
        {
            Change2Data(ref thresholdMin, ref thresholdMax);
        }
        if (numberMin > thresholdMin && numberMin < thresholdMax)
        {
            return true;
        }
        if (numberMax > thresholdMin && numberMax < thresholdMax)
        {
            return true;
        }
        return false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ０～１でminからmaxまでの割合を求める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float AmongProportion(float number, float min, float max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        return MyCalculator.Leap(min, max, number);
    }
    public static double AmongProportion(double number, double min, double max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        return MyCalculator.Leap(min, max, number);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ０～１でminからmaxまでの百分率を求める
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float AmongPercentage(float number, float min, float max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        return MyCalculator.Leap(min, max, MyCalculator.Division(number, 100.0f));
    }
    public static double AmongPercentage(double number, double min, double max)
    {
        if (min > max)
        {
            Change2Data(ref min, ref max);
        }
        return MyCalculator.Leap(min, max, MyCalculator.Division(number, 100.0));
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 0以外ならTRUE
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static bool NumberToBool(int number)
    {
        return (number != 0);
    }
    public static bool NumberToBool(float number)
    {
        return (number != 0);
    }
    public static bool CharChackNumber(char letter)
    {
        if (letter >= '0' && letter <= '9')
        {
            return true;
        }
        return false;

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Aを0番目とする数字を獲得
    // startPoint スタート位置ずらし
    // errorPoint 数字以外が来たらこれを返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int CharLetterOfNumber(char letter, int errorPoint, int startPoint)
    {
        int type = errorPoint;
        if (letter >= 'A' && letter <= 'Z')
        {
            type = letter - 'A';
        }
        else if (letter >= 'a' && letter <= 'z')
        {
            type = letter - 'a';
        }
        else
        {
            return errorPoint;
        }
        type += startPoint;
        return type;

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 16進数の数字獲得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static char Char16Number(int number)
    {
        char type;
        number = Among(number, 0, 15);
        if (number < 10)
        {
            type = (char)(number + 0x30);
        }
        else
        {
            type = (char)(number + 0x37);
        }
        return type;

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // オーバーフロー防止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float AntiOverflow(float number, float loopNum)
    {
        float numberR = number;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーバーフローも何もない
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopNum <= 0)
        {
            return 0;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データを正の数にする
        //*|***|***|***|***|***|***|***|***|***|***|***|
        while (numberR < 0)
        {
            numberR += loopNum;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // numberR % loopNum
        //*|***|***|***|***|***|***|***|***|***|***|***|
        while (numberR >= loopNum)
        {
            numberR -= loopNum;
        }
        return numberR;
    }
    public static double AntiOverflow(double number, double loopNum)
    {
        double numberR = number;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーバーフローも何もない
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopNum <= 0)
        {
            return 0;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データを正の数にする
        //*|***|***|***|***|***|***|***|***|***|***|***|
        while (numberR < 0)
        {
            numberR += loopNum;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // numberR % loopNum
        //*|***|***|***|***|***|***|***|***|***|***|***|
        while (numberR >= loopNum)
        {
            numberR -= loopNum;
        }
        return numberR;
    }
    public static uint AntiOverflow(uint number, uint loopNum)
    {
        uint numberR = number;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーバーフローも何もない
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopNum <= 0)
        {
            return 0;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データを正の数にする
        //*|***|***|***|***|***|***|***|***|***|***|***|
        while (numberR < 0)
        {
            numberR += loopNum;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // numberR % loopNum
        //*|***|***|***|***|***|***|***|***|***|***|***|
        while (numberR >= loopNum)
        {
            numberR -= loopNum;
        }
        return numberR;
    }
    public static int AntiOverflow(int number, int loopNum)
    {
        int numberR = number;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーバーフローも何もない
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (loopNum <= 0)
        {
            return 0;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データを正の数にする
        //*|***|***|***|***|***|***|***|***|***|***|***|
        while (numberR < 0)
        {
            numberR += loopNum;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // numberR % loopNum
        //*|***|***|***|***|***|***|***|***|***|***|***|
        while (numberR >= loopNum)
        {
            numberR -= loopNum;
        }
        return numberR;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // オーバーフロー防止しつつInt化
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static int FloatToIntFloor(float number, int loopNum)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データをIntにする
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int numberInt = Mathf.FloorToInt(number);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // loopNum以下に修正
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int numberLoopOver = AntiOverflow(numberInt, loopNum);
        return numberLoopOver;
    }
    public static int FloatToIntFloorParsent(float number, int loopNum)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データをIntにする
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int numberInt = Mathf.FloorToInt(number * loopNum);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // loopNum以下に修正
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int numberLoopOver = AntiOverflow(numberInt, loopNum);
        return numberLoopOver;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 円周率獲得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float GetPI()
    {
        return PI;
    }
    public static float GetRadDeg90()
    {
        return PI / 2.0f;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Vectorのグレードダウン
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Vector2
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float GetX(Vector2 getData)
    {
        return getData.x;
    }
    public static float GetY(Vector2 getData)
    {
        return getData.y;
    }
    public static Vector3 GetVector3(Vector2 getData)
    {
        return new Vector3(getData.x, getData.y, 0);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Vector3
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float GetX(Vector3 getData)
    {
        return getData.x;
    }
    public static float GetY(Vector3 getData)
    {
        return getData.y;
    }
    public static float GetZ(Vector3 getData)
    {
        return getData.z;
    }
    public static Vector2 GetVector2(Vector3 getData)
    {
        return new Vector2(getData.x, getData.y);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Vector4
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float GetX(Vector4 getData)
    {
        return getData.x;
    }
    public static float GetY(Vector4 getData)
    {
        return getData.y;
    }
    public static float GetZ(Vector4 getData)
    {
        return getData.z;
    }
    public static float GetW(Vector4 getData)
    {
        return getData.w;
    }
    public static Vector2 GetVector2(Vector4 getData)
    {
        return new Vector2(getData.x, getData.y);
    }
    public static Vector3 GetVector3(Vector4 getData)
    {
        return new Vector3(getData.x, getData.y, getData.z);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // Rect
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static float GetX(Rect getData)
    {
        return GetVector2(getData).x;
    }
    public static float GetY(Rect getData)
    {
        return GetVector2(getData).y;
    }
    public static Vector2 GetVector2(Rect getData)
    {
        Vector2 size;
        size.x = (float)getData.xMax - (float)getData.xMin;
        size.y = (float)getData.yMax - (float)getData.yMin;
        return size;
    }




}



