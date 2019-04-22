using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentForPlayer : MonoBehaviour
{
    public GameObject starDirec;

    private StarDirector starCreate;

    SpriteRenderer turtleSprite;

    [SerializeField]
    private float shakeSpeed = 0.08f;

    private float distance = 0.0f;

    [SerializeField]
    private float maxDistance = 0.2f;

    public bool savedFlag = false;

    [SerializeField]
    private float destroyColor = 1.0f;

    private bool changeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        turtleSprite = gameObject.GetComponent<SpriteRenderer>();

        starCreate = starDirec.GetComponent<StarDirector>();
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

        // 透明化させる
        if(savedFlag == true)
        {
            if (changeFlag == false)
            {
                // 透明度を下げる
                destroyColor -= 0.01f;
                // オブジェクトに反映させる
                turtleSprite.color = new Color(1.0f, 1.0f, 1.0f, destroyColor);
            }
        }

        // 透明度が0よりも小さい時
        if (destroyColor < 0.0f)
        {
            // 透明度を0に
            destroyColor = 0.0f;
            // 星に出現場所を与えて生成
            float posX = this.transform.position.x;
            float posY = this.transform.position.y;
            starCreate.CreateOneStar(new Vector2(posX, posY), 10, false, 0.5f);
            // 連続で星を出すのを止める
            changeFlag = true;
        }

    }
}
