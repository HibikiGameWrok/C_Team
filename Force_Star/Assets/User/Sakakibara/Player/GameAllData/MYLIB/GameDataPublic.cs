using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataPublic
{


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 伝説の画像用システム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [Serializable]
    public class TexImageData
    {
        public Texture2D image;
        public Vector2 size;
        public Rect rextParsent;
        public void Reset()
        {
            image = null;
            size = Vector2.one;
            rextParsent = Rect.zero;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 伝説の描画用システム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [Serializable]
    public class RenderImageData
    {
        public int depth;
        public void Reset()
        {
            depth = 0;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星形の当たり判定作成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    static public List<Vector2> GetStarPartsPosition(int partsNumber, float size = 1.0f)
    {
        int starCorner = 5;
        int starAllConner = starCorner * 2;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 挿入データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        List<Vector2> pointList = new List<Vector2>();
        pointList.Clear();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 挿入データ作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float lengthLong = 1.0f * size;
        float lengthShote = 0.5f * size;
        float angle = 0;
        Vector2 vectorDirection = Vector2.zero;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        {
            int partsNow = ChangeData.AntiOverflow(partsNumber, starAllConner);
            int partsNext = ChangeData.AntiOverflow(partsNumber + 1, starAllConner);
            float getLong = 0.0f;
            Vector2 getVector = Vector2.zero;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // partsNow
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 方向作成
                //*|***|***|***|***|***|***|***|***|***|***|***|
                angle = (360.0f / 10.0f) * partsNow + 90.0f;
                angle = ChangeData.SetDeg360(angle);
                vectorDirection = ChangeData.AngleDegToVector2(angle);
                if (partsNow % 2 == 0)
                {
                    getLong = lengthLong;
                }
                else
                {
                    getLong = lengthShote;
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 地点獲得
                //*|***|***|***|***|***|***|***|***|***|***|***|
                getVector = vectorDirection * getLong;
                pointList.Add(getVector);
                getVector = vectorDirection * (getLong * 0.5f);
                pointList.Add(getVector);
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // partsNext
            //*|***|***|***|***|***|***|***|***|***|***|***|
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 方向作成
                //*|***|***|***|***|***|***|***|***|***|***|***|
                angle = (360.0f / 10.0f) * partsNext + 90.0f;
                angle = ChangeData.SetDeg360(angle);
                vectorDirection = ChangeData.AngleDegToVector2(angle);
                if (partsNext % 2 == 0)
                {
                    getLong = lengthLong;
                }
                else
                {
                    getLong = lengthShote;
                }
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 地点獲得
                //*|***|***|***|***|***|***|***|***|***|***|***|
                getVector = vectorDirection * (getLong * 0.5f);
                pointList.Add(getVector);
                getVector = vectorDirection * getLong;
                pointList.Add(getVector);
            }
        }
        return pointList;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 指定した三角形の中にその地点はありますか？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    static public bool TriangleHitPoint(List<Vector2> getPoint, Vector2 jugPoint)
    {
        bool returnJudgment = false;
        Vector2 point = jugPoint;
        Vector2 triangleA;
        Vector2 triangleB;
        Vector2 triangleC;
        if (getPoint.Count >= 3)
        {
            triangleA = getPoint[0];
            triangleB = getPoint[1];
            triangleC = getPoint[2];
            if (MyCalculator.TriangleAndPoint(point, triangleA, triangleB, triangleC))
            {
                returnJudgment = true;
            }
            if (getPoint.Count >= 4)
            {
                triangleA = getPoint[0];
                triangleB = getPoint[2];
                triangleC = getPoint[3];
                if (MyCalculator.TriangleAndPoint(point, triangleA, triangleB, triangleC))
                {
                    returnJudgment = true;
                }
            }
        }
        return returnJudgment;
    }
}
