using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentForPlayer : MonoBehaviour
{
    [SerializeField]
    private float shakeSpeed = 0.08f;

    private float distance = 0.0f;

    [SerializeField]
    private float maxDistance = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //最大値まで行ったら反転
        if (distance > maxDistance)
        {
            shakeSpeed = shakeSpeed * -1;
            distance = 0.0f;
        }
        //最低値まで行ったら反転
        if (distance < -maxDistance)
        {
            shakeSpeed = shakeSpeed * -1;
            distance = 0.0f;
        }

        //敵を移動させる
        transform.Translate(shakeSpeed, 0, 0);
        distance += shakeSpeed;
    }
}
