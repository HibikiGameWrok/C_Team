using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera2D : MonoBehaviour
{
    [SerializeField] // 対象物
    Transform target1 = null, target2 = null;

    [SerializeField]
    Vector2 offset = new Vector2(1, 1);

    // 画面比
    private float screenAspect = 0;

    // カメラ要素
    private Camera _camera = null;

    void Awake()
    {
        // 画面比の設定
        screenAspect = (float)Screen.height / Screen.width;

        // カメラの要素を取得
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        // カメラの座標を更新
        UpdateCameraPosition();

        // 
        UpdateOrthographicSize();

    }

    void UpdateCameraPosition()
    {
        // 2点間の中心点からカメラの位置を更新
        Vector3 center = Vector3.Lerp(target1.position, target2.position, 0.5f);

        transform.position = center + Vector3.forward * -10;
    }

    void UpdateOrthographicSize()
    {
        // ２点間のベクトルを取得
        Vector3 targetsVector = AbsPositionDiff(target1, target2) + (Vector3)offset;

        // アスペクト比が縦長ならyの半分、横長ならxとアスペクト比でカメラのサイズを更新
        float targetsAspect = targetsVector.y / targetsVector.x;

        float targetOrthographicSize = 0;

        if (screenAspect < targetsAspect)
        {
            targetOrthographicSize = targetsVector.y * 0.5f;
        }
        else
        {
            targetOrthographicSize = targetsVector.x * (1 / _camera.aspect) * 0.5f;
        }

        _camera.orthographicSize = targetOrthographicSize;
    }

    Vector3 AbsPositionDiff(Transform target1, Transform target2)
    {
        Vector3 targetsDiff = target1.position - target2.position;
 
        return new Vector3(Mathf.Abs(targetsDiff.x), Mathf.Abs(targetsDiff.y));
    }
    
    //カメラ揺れ
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    // 縦振り関数
    public void HeightShake(float duration, float magnitude)
    {
        StartCoroutine(DoHeightShake(duration, magnitude));
    }

    // 幅振り関数
    public void WidthShake(float duration, float magnitude)
    {
        StartCoroutine(DoWidthShake(duration, magnitude));
    }

    // 全振りコルーチン関数
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

    // 縦振りコルーチン関数
    private IEnumerator DoHeightShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(pos.x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }


        transform.localPosition = pos;
    }

    // 横振りコルーチン関数
    private IEnumerator DoWidthShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, pos.y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // ローカル座標へ代入
        transform.localPosition = pos;
    }
}
