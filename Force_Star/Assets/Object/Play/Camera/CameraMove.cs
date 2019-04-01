using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }


    public void HeightShake(float duration, float magnitude)
    {
        StartCoroutine(DoHeightShake(duration, magnitude));
    }

    public void WidthShake(float duration, float magnitude)
    {
        StartCoroutine(DoWidthShake(duration, magnitude));
    }

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
