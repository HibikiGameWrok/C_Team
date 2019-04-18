using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentForPlayer : MonoBehaviour
{
    SpriteRenderer turtleSprite;

    [SerializeField]
    private float shakeSpeed = 0.08f;

    private float distance = 0.0f;

    [SerializeField]
    private float maxDistance = 0.2f;

    private bool savedFlag = true;

    [SerializeField]
    private float destroyColor = 255.0f;

    // Start is called before the first frame update
    void Start()
    {
        turtleSprite = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (savedFlag != true)
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

        if(savedFlag == true)
        {
            destroyColor -= 1.0f;
            turtleSprite.color = new Color(255.0f, 255.0f, 255.0f, destroyColor);
        }

        if (destroyColor < 0.0f)
        {
            destroyColor = 0.0f;
        }

    }
}
