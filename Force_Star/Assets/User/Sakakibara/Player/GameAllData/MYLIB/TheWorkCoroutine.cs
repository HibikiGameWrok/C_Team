using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class TheWorkCoroutine
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 請け負った仕事
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private IEnumerator m_workIEnumerator;
    private Action m_workVoid;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 終わりフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool endFlag = false;
    private bool endSuccessFlag = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 終わりフラグ取得
    //*|***|***|***|***|***|***|***|***|***|***|***|

    ~TheWorkCoroutine()
    {

    }
    public bool GetEnd()
    {
        return endFlag;
    }
    public bool GetSuccess()
    {
        return endSuccessFlag;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 研修登録する関数
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void TheWorkRecord(Action function)
    {
        m_workVoid = function;
        m_workIEnumerator = TheWorkVoidY();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 研修する関数
    //*|***|***|***|***|***|***|***|***|***|***|***|
    IEnumerator TheWorkVoidY()
    {
        TheWorkVoidX();
        yield return 0;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 研修する関数
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void TheWorkVoidX()
    {
        endFlag = false;
        endSuccessFlag = false;
        try
        {
            m_workVoid();
            endSuccessFlag = true;
        }
        catch (Exception)
        {
            endSuccessFlag = false;
        }
        endFlag = true;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 研修進行する関数
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void TheWorkUpdate()
    {
        m_workIEnumerator.MoveNext();
    }
}
