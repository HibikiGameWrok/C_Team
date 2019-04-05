﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Text;

public class FileConnection
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // エリア名
    //*|***|***|***|***|***|***|***|***|***|***|***|
    string m_areaName;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // エリア名設定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetAreaName(string name)
    {
        m_areaName = name;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // エリア名同じか？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool BoolAreaName(string name)
    {
        bool flag;
        if (name == m_areaName)
        {
            flag = true;
        }
        else
        {
            flag = false;
        }
        return flag;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル名
    //*|***|***|***|***|***|***|***|***|***|***|***|
    string m_fileName;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル名設定
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetFileName(string name)
    {
        m_fileName = name;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル名同じか？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool BoolFileName(string name)
    {
        bool flag;
        if(name == m_fileName)
        {
            flag = true;
        }
        else
        {
            flag = false;
        }
        return flag;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データの名前
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private List<string> m_haveListData;
    private int m_haveListCount;
    bool m_normal;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイルデータ登録
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetListData(List<string> listData)
    {
        m_haveListData = listData;
        m_haveListCount = listData.Count;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイルデータ取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public CsvBook GetBook()
    {
        CsvBook book = new CsvBook(m_haveListData);
        return book;
    }
    public bool GetBool()
    {
        return m_normal;
    }





    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コンストラクタ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public FileConnection()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データの名前
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveListData = new List<string>();
        m_haveListCount = 0;
        m_normal = false;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル読み込む
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ReaderFile()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 仮の名前
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 入力ヘッダーデータの名前
        //*|***|***|***|***|***|***|***|***|***|***|***|
        List<string> IhaveListData = new List<string>();
        int IhaveListCount = 0;
        bool IhaveListGet = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 検査用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_normal = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイル名作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string trueFileName = m_areaName + m_fileName;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイルを読み込む
        //*|***|***|***|***|***|***|***|***|***|***|***|
        using (FileStream fs = new FileStream(
            trueFileName, FileMode.Open))
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            StreamReader reader = new StreamReader(fs, sjisEnc);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ファイルの読み込みを先頭まで戻す
            //*|***|***|***|***|***|***|***|***|***|***|***|
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ファイルが終わるまで
            //*|***|***|***|***|***|***|***|***|***|***|***|
            while (reader.EndOfStream == false)
            {
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 一行読み込む
                //*|***|***|***|***|***|***|***|***|***|***|***|
                string str = reader.ReadLine();
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // それはインプットのヘッダーですか？
                //*|***|***|***|***|***|***|***|***|***|***|***|
                IhaveListData.Add(str);
                IhaveListCount += 1;

                IhaveListGet = true;
            }
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ファイル取得できたか？
            //*|***|***|***|***|***|***|***|***|***|***|***|
            if (IhaveListGet)
            {
                m_haveListCount = IhaveListCount;
                m_haveListData = IhaveListData;
                //*|***|***|***|***|***|***|***|***|***|***|***|
                // 検査用
                //*|***|***|***|***|***|***|***|***|***|***|***|
                m_normal = true;
            }
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ファイル書き込む
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void WriterFile()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 検査用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_normal = false;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイル名作成
        //*|***|***|***|***|***|***|***|***|***|***|***|
        string trueFileName = m_areaName + m_fileName;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ファイル書き込む
        //*|***|***|***|***|***|***|***|***|***|***|***|
        using (FileStream fs = new FileStream(trueFileName,
        FileMode.Create, FileAccess.Write))
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            StreamWriter writer = new StreamWriter(fs, sjisEnc);
            string str = "";

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // データ書き込む
            //*|***|***|***|***|***|***|***|***|***|***|***|
            for (int column = 0; column < m_haveListCount; column++)
            {
                str = "";
                str = m_haveListData[column];
                writer.WriteLine(str);
            }

            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 検査用
            //*|***|***|***|***|***|***|***|***|***|***|***|
            m_normal = true;
            writer.Close();
        }
    }
}
