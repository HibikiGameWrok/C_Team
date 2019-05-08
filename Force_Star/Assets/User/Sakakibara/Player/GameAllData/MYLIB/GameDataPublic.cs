using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤーナンバー言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
using PlayerDataNum = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number;
using PlayerData_Number_List = WarehouseData.PlayerData.WarehousePlayer.PlayerData_Number_List;

public class GameDataPublic
{


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 伝説の画像用システム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [Serializable]
    public class TexImageData
    {
        [SerializeField]
        public Texture2D image;
        [SerializeField]
        public Vector2 size;
        [SerializeField]
        public Rect rextParsent;
        [SerializeField]
        public Vector2 pibot;
        public void Reset()
        {
            image = null;
            size = Vector2.one;
            rextParsent = Rect.zero;
            pibot = new Vector2(0.5f, 0.5f);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 隠された描画システム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public class TexImageHidden
    {
        public Vector2 textureSize;
        public Vector2 spriteSize;
        public void Reset()
        {
            textureSize = Vector2.one;
            spriteSize = Vector2.one;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 伝説の描画用システム
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シリアライズ可能
    // エディター取得可能
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [Serializable]
    public class RenderImageData
    {
        [SerializeField]
        public int depth;
        public void Reset()
        {
            depth = 0;
        }
    } 
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 伝説の描画用システム FOR UI
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シリアライズ可能
    // エディター取得可能
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [Serializable]
    public class RenderUIData
    {
        [SerializeField]
        public int depth;
        [SerializeField]
        public Vector2 pibot;
        [SerializeField]
        public float alpha;
        public void Reset()
        {
            depth = 0;
            pibot = new Vector2(0.5f, 0.5f);
            alpha = 1.0f;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーのデータリスト
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [Serializable]
    public class PartsData
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 親のパーツを継承する回転、場所、拡大縮小
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Vector3 localPos;
        public Vector3 localAngle;
        public Vector2 localScale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 親のパーツが関係ない回転、場所、拡大縮小
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Vector3 imagePos;
        public Vector3 imageAngle;
        public Vector2 imageScale;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 親のパーツに調和できないもの
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Vector3 difAngle;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // イメージ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public Vector2 size;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public PlayerDataNum dataNum;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 描画
        //*|***|***|***|***|***|***|***|***|***|***|***|
        public int depth;

        public PartsData()
        {

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親のパーツを継承する回転、場所、拡大縮小
            //*|***|***|***|***|***|***|***|***|***|***|***|
            imagePos = Vector3.zero;
            imageAngle = Vector3.zero;
            imageScale = Vector2.zero;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 親のパーツが関係ない回転、場所、拡大縮小
            //*|***|***|***|***|***|***|***|***|***|***|***|
            localPos = Vector3.zero;
            localAngle = Vector3.zero;
            localScale = Vector2.zero;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            difAngle = Vector3.zero;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // イメージ
            //*|***|***|***|***|***|***|***|***|***|***|***|
            size = Vector2.one;
            depth = 0;
            dataNum = PlayerDataNum.BLANK;
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


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // nameと同じCHILDを返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    static public GameObject SearchChild(GameObject parent, string searchName)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 子どもを探して
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject getData = null;
        GameObject returnData = null;
        for (int number = 0; number < parent.transform.childCount; number++)
        {
            getData = parent.transform.GetChild(number).gameObject;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 同じ名前のものを検索する
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (getData.name == searchName)
            {
                returnData = getData;
                return returnData;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 見つかりませんでした。なら作る。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return MakeChild(parent, searchName);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // nameと同じCHILDを返す階層すべて探す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    static public GameObject SearchChildAllHierarchy(GameObject parent, string searchName)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回帰処理に任せた。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject returnData = SearchChildNowHierarchy(parent, searchName);
        if (returnData != null)
        {
            return returnData;
        }
        else
        {
            return MakeChild(parent, searchName);
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // nameと同じCHILDを返す階層すべて探す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    static private GameObject SearchChildNowHierarchy(GameObject parent, string searchName)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 子どもを探して
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject getData = null;
        GameObject returnData = null;
        for (int number = 0; number < parent.transform.childCount; number++)
        {
            getData = parent.transform.GetChild(number).gameObject;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 獲得データの階層を探す
            //*|***|***|***|***|***|***|***|***|***|***|***|
            returnData = SearchChildNowHierarchy(getData, searchName);
            if (returnData != null)
            {
                return returnData;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 同じ名前のものを検索する
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (getData.name == searchName)
            {
                returnData = getData;
                return returnData;
            }
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 見つかりませんでした。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return returnData;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // nameと同じCHILDを返す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    static private GameObject MakeChild(GameObject parent, string name)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ここのプレイヤー親子
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GameObject returnData = new GameObject(name);
        returnData.transform.parent = parent.transform;
        return returnData;
    }
}
