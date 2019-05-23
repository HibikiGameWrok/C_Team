using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CameraArts : MonoBehaviour
{
    public enum NUM_SHAKE
    {
        SHAKE,
        HEIG_SHAKE,
        WIDTH_SHAKE,
        MAX_NUM,
    };

    [SerializeField]
    private int m_Num_Shake;

    [SerializeField]
    private float m_duration;

    [SerializeField]
    private float m_magnitudeMax;

    [SerializeField]
    private float m_magnitudeMin;

    private bool m_shakeFlag;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 揺れ幅
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private Vector3 m_shakeDif;

    // Start is called before the first frame update
    void Start()
    {
        m_magnitudeMax = 0;
        m_magnitudeMin = 0;
        m_duration = 0;
        m_shakeFlag = false;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 揺れ幅
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateShake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間経過
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_duration -= 1.0f;
        if (m_duration < 0)
        {
            m_duration = 0;
            m_shakeFlag = false;
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // シェイクタイム！
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_shakeFlag)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 番号によって揺れを変える
            //*|***|***|***|***|***|***|***|***|***|***|***|
            switch (m_Num_Shake)
            {
                case 0: // 揺れ
                    DoShake();
                    break;
                case 1: // 縦揺れ
                    DoHeightShake();
                    break;
                case 2: // 横揺れ
                    DoWidthShake();
                    break;
                default:
                    break;
            }
        }

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 揺れ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void Shake(float duration, float magnitude)
    {
        m_shakeFlag = true;
        m_duration = duration;
        m_magnitudeMax = magnitude;
        m_magnitudeMin = 0;
        m_Num_Shake = (int)NUM_SHAKE.SHAKE;
    }
    public void Shake(float duration, float magnitudeMax, float magnitudeMin)
    {
        m_shakeFlag = true;
        m_duration = duration;
        m_magnitudeMax = magnitudeMax;
        m_magnitudeMin = magnitudeMin;
        m_Num_Shake = (int)NUM_SHAKE.SHAKE;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 縦揺れ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void HeightShake(float duration, float magnitude)
    {
        m_shakeFlag = true;
        m_duration = duration;
        m_magnitudeMax = magnitude;
        m_magnitudeMin = 0;
        m_Num_Shake = (int)NUM_SHAKE.HEIG_SHAKE;
    }
    public void HeightShake(float duration, float magnitudeMax, float magnitudeMin)
    {
        m_shakeFlag = true;
        m_duration = duration;
        m_magnitudeMax = magnitudeMax;
        m_magnitudeMin = magnitudeMin;
        m_Num_Shake = (int)NUM_SHAKE.HEIG_SHAKE;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 横揺れ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void WidthShake(float duration, float magnitude)
    {
        m_shakeFlag = true;
        m_duration = duration;
        m_magnitudeMax = magnitude;
        m_magnitudeMin = 0;
        m_Num_Shake = (int)NUM_SHAKE.WIDTH_SHAKE;
    }
    public void WidthShake(float duration, float magnitudeMax, float magnitudeMin)
    {
        m_shakeFlag = true;
        m_duration = duration;
        m_magnitudeMax = magnitudeMax;
        m_magnitudeMin = magnitudeMin;
        m_Num_Shake = (int)NUM_SHAKE.WIDTH_SHAKE;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 今回のシェイク：横縦
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void DoShake()
    {
        Vector3 pos = Vector3.zero;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 今回のシェイク：横
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float x = GetShakePower();
        float y = GetShakePower();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 今回のシェイクの結果
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_shakeDif = new Vector3(x, y, pos.z);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 今回のシェイク：横
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void DoHeightShake()
    {
        Vector3 pos = Vector3.zero;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 今回のシェイク：横
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float x = 0;
        float y = GetShakePower();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 今回のシェイクの結果
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_shakeDif = new Vector3(x, y, pos.z);
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 今回のシェイク：横
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void DoWidthShake()
    {
        Vector3 pos = Vector3.zero;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 今回のシェイク：横
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float x = GetShakePower();
        float y = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 今回のシェイクの結果
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_shakeDif = new Vector3(x, y, pos.z);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 今回のシェイク力
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private float GetShakePower()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 乱数で位置データ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int powerData = XORShiftRand.GetSeedMaxMinRand(1000, 0);
        float power = MyCalculator.Division((float)powerData, 1000.0f);
        power = ChangeData.Among(power, 0.0f, 1.0f);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 位置データでシェイク力
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float shakePower = MyCalculator.Leap(m_magnitudeMin, m_magnitudeMax, power);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 乱数でシェイク方向
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int vec = XORShiftRand.GetSeedMaxMinRand(1, 0);
        if(vec == 1)
        {
            shakePower *= -1;
        }
        return shakePower;
    }
}
