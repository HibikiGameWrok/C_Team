using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class TheWork
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 請け負った仕事
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Thread m_workThread;
    private Action m_workVoid;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 終わりフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private bool endFlag = false;
    private bool endSuccessFlag = false;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 終わりフラグ取得
    //*|***|***|***|***|***|***|***|***|***|***|***|

    ~TheWork()
    {
        if(m_workThread.ThreadState != ThreadState.Stopped)
        {
            m_workThread.Join();
        }
        else
        {
            m_workThread.Join();
        }
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
        ThreadStart makeTexWorkStart = new ThreadStart(TheWorkVoidX);
        m_workThread = new Thread(makeTexWorkStart);
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
    // 研修スタートする関数
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void TheWorkStart()
    {
        m_workThread.Start();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 研修終了を待つ関数
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void TheWorkJoin()
    {
        m_workThread.Join();
    }
    public void TheWorkJoin(Int32 time)
    {
        m_workThread.Join(time);
    }
}
