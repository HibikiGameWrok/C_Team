using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
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
    private float m_magnitude;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(m_Num_Shake >= (int)NUM_SHAKE.MAX_NUM)
        {
            Debug.Log("現在の揺れは" + (int)NUM_SHAKE.MAX_NUM + "項目しかありません");
            Debug.Log("揺らす場合" +((int)NUM_SHAKE.MAX_NUM - 1) + "以内の数字を入れてください");
            return;
        }

        // 番号によって揺れを変える
        switch (m_Num_Shake)
        {
            case 0: // 揺れ
                Shake(m_duration, m_magnitude);
                break;
            case 1: // 縦揺れ
                HeightShake(m_duration, m_magnitude);
                break;
            case 2: // 横揺れ
                WidthShake(m_duration, m_magnitude);
                break;
            default:
                break;
        }
    }

    // 揺れ
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    // 縦揺れ
    public void HeightShake(float duration, float magnitude)
    {
        StartCoroutine(DoHeightShake(duration, magnitude));
    }

    // 横揺れ
    public void WidthShake(float duration, float magnitude)
    {
        StartCoroutine(DoWidthShake(duration, magnitude));
    }

    // 揺らすコルーチン
    private IEnumerator DoShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            //縦揺れ
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            //横揺れ
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = pos;
    }

    // 縦揺らしコルーチン
    private IEnumerator DoHeightShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            //  var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(pos.x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }


        transform.localPosition = pos;
    }

    // 横揺らしコルーチン
    private IEnumerator DoWidthShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            //var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, pos.y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }


        transform.localPosition = pos;
    }
}
