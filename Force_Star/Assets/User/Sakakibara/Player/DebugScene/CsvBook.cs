using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Text;

public class CsvBook
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private List<string> dataBook;
    private int dataBookPaze;


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コンストラクタ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public CsvBook(List<string> fileData)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // fileData取得中
        //*|***|***|***|***|***|***|***|***|***|***|***|
        dataBookPaze = fileData.Count;
        dataBook = fileData;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // デストラクタ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    ~CsvBook()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // dataBook解放
        //*|***|***|***|***|***|***|***|***|***|***|***|
        dataBook.Clear();
    }


	//*|***|***|***|***|***|***|***|***|***|***|***|
	// 本の名前を付ける
	//*|***|***|***|***|***|***|***|***|***|***|***|
	public void SetBookName(string bookName)
    {
        this.bookName = bookName;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 本の名前は同じか
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool MatchBookName(string bookName)
    {
        if (this.bookName == bookName)
        {
            return true;
        }
        return false;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 本のジャンルを付ける
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetBookGenre(string bookGenre)
    {
        this.bookGenre = bookGenre;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 本のジャンルは同じか
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool MatchBookGenre(string bookGenre)
    {
        if (this.bookGenre == bookGenre)
        {
            return true;
        }
        return false;
    }
	//*|***|***|***|***|***|***|***|***|***|***|***|
	// 本の名前
	//*|***|***|***|***|***|***|***|***|***|***|***|
	string bookName;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 本のジャンル
    //*|***|***|***|***|***|***|***|***|***|***|***|
    string bookGenre;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ取り出し部分
    //*|***|***|***|***|***|***|***|***|***|***|***|
	private static int FLOATPAZE = 1;
    private static int VECTOR2PAZE = 2;
    private static int VECTOR3PAZE = 3;
    private static int VECTOR4PAZE = 4;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 本のページ数獲得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public int GetBookPaze()
	{
		return (int)dataBookPaze;
	}

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値獲得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public int GetValueInt(int rowLong, int dataRow, int dataColumn)
    {
        float numberF = GetValueFloat(rowLong, dataRow, dataColumn);
        int number = (int)numberF;
        return number;
    }
    public bool GetValueBool(int rowLong, int dataRow, int dataColumn)
    {
        float numberF = GetValueFloat(rowLong, dataRow, dataColumn);
        bool flag = ChangeData.NumberToBool((int)numberF);
        return flag;
    }
    public float GetValueFloat(int rowLong, int dataRow, int dataColumn)
    {
        float returnData = 0.0f;
        int dataIndex = 0;
        dataIndex = dataColumn * rowLong + dataRow;
        if (dataBookPaze >= FLOATPAZE)
        {
            returnData = GetValueFloat(dataIndex);
        }
        return returnData;
    }
    public string GetStringData(int rowLong, int dataRow, int dataColumn)
    {
        string returnData = "";
        int dataIndex = 0;
        dataIndex = dataColumn * rowLong + dataRow;
        if (dataBookPaze >= FLOATPAZE)
        {
            returnData = GetStringData(dataIndex);
        }
        return returnData;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 値獲得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector2 GetValueVector2(int rowLong, int dataRow, int dataColumn)
    {
        Vector2 returnData = Vector2.zero;
        int dataIndex = 0;
        dataIndex = dataColumn * rowLong + dataRow;
        if (dataBookPaze >= VECTOR2PAZE)
        {
            returnData = GetValueVector2(dataIndex);
        }
        return returnData;
    }
    public Vector3 GetValueVector3(int rowLong, int dataRow, int dataColumn)
    {
        Vector3 returnData = Vector3.zero;
        int dataIndex = 0;
        dataIndex = dataColumn * rowLong + dataRow;
        if (dataBookPaze >= VECTOR3PAZE)
        {
            returnData = GetValueVector3(dataIndex);
        }
        return returnData;
    }

    public Vector4 GetValueVector4(int rowLong, int dataRow, int dataColumn)
    {
        Vector4 returnData = Vector4.zero;
        int dataIndex = 0;
        dataIndex = dataColumn * rowLong + dataRow;
        if (dataBookPaze >= VECTOR4PAZE)
        {
            returnData = GetValueVector4(dataIndex);
        }
        return returnData;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 変換
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private string GetStringData(int dataNumber)
    {
        string returnData = "";
        int dataIndex = dataNumber;
        dataIndex = ChangeData.AmongLess(dataIndex, 0, (int)dataBookPaze - (FLOATPAZE - 1));

        returnData = dataBook[dataIndex];
        return returnData;
    }
    private float GetValueFloat(int dataNumber)
    {
        float returnData = 0.0f;
        int dataIndex = dataNumber;
        dataIndex = ChangeData.AmongLess(dataIndex, 0, (int)dataBookPaze - (FLOATPAZE - 1));
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 事故多発地点
        // 文字を数字にできません！
        // 事故発生なら０を返す。
        //*|***|***|***|***|***|***|***|***|***|***|***|
        try
        {
            returnData = float.Parse(dataBook[dataIndex]);
        }
        catch (Exception)
        {
            returnData = 0.0f;
        }
        return returnData;
    }
    private Vector2 GetValueVector2(int dataNumber)
    {
        Vector2 returnData = Vector2.zero;
        int dataIndex = dataNumber;
        dataIndex = ChangeData.AmongLess(dataIndex, 0, (int)dataBookPaze - (VECTOR2PAZE - 1));
        returnData.x = GetValueFloat(dataIndex);
        dataIndex++;
        returnData.y = GetValueFloat(dataIndex);
        return returnData;
    }
    private Vector3 GetValueVector3(int dataNumber)
    {
        Vector3 returnData = Vector3.zero;
        int dataIndex = dataNumber;
        dataIndex = ChangeData.AmongLess(dataIndex, 0, (int)dataBookPaze - (VECTOR3PAZE - 1));
        returnData.x = GetValueFloat(dataIndex);
        dataIndex++;
        returnData.y = GetValueFloat(dataIndex);
        dataIndex++;
        returnData.z = GetValueFloat(dataIndex);
        return returnData;
    }
    private Vector4 GetValueVector4(int dataNumber)
    {
        Vector4 returnData = Vector4.zero;
        int dataIndex = dataNumber;
        dataIndex = ChangeData.AmongLess(dataIndex, 0, (int)dataBookPaze - (VECTOR4PAZE - 1));
        returnData.x = GetValueFloat(dataIndex);
        dataIndex++;
        returnData.y = GetValueFloat(dataIndex);
        dataIndex++;
        returnData.z = GetValueFloat(dataIndex);
        dataIndex++;
        returnData.w = GetValueFloat(dataIndex);
        return returnData;
    }
}
