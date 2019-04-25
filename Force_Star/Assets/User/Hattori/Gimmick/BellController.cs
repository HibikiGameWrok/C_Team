using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellController : MonoBehaviour
{
    public GameObject starDirec;

    private StarDirector starCreate;

    //動く速さの値
    [SerializeField]
    private float shakeSpeed = 0.5f;

    //動く幅の値
    private float shakeRote;

    //動く幅の最大値
    [SerializeField]
    private float maxShakeRote = 0.3f;

    //当たったら動くフラグ
    private bool shakeFlag = false;

    private bool moveFlag = false;

    //振れ幅の初期値記録用の値
    private float defoltShakeSpeed;

    //最大振れ幅の初期値記録用の値
    private float defoltMaxShakeRote;

    private bool starCreateFlag = false;

    private bool onceStarCreateFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        starCreate = starDirec.GetComponent<StarDirector>();

        //初期化
        shakeRote = this.transform.localRotation.z;

        //値登録
        defoltShakeSpeed = shakeSpeed;

        //値登録
        defoltMaxShakeRote = maxShakeRote;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeFlag)
        {
            //角度代入
            shakeRote = shakeSpeed;

            //最大値に到達しかつ動いていいフラグがfalseなら
            if (this.transform.localRotation.z < -maxShakeRote&& !moveFlag)
            {
                //動け
                moveFlag = true;

                //向きを反転
                shakeSpeed *= -1;

                //スピードを遅くする
                shakeSpeed -= 0.1f;

                //最大値を減らす
                maxShakeRote -= 0.01f;
            }

            //最大値に到達しかつ動いていいフラグがfalseなら
            if (this.transform.localRotation.z > maxShakeRote&& !moveFlag)
            {
                //動け
                moveFlag = true;

                //向きを反転
                shakeSpeed *= -1;

                //スピードを遅くする
                shakeSpeed += 0.1f;

                //最大値を減らす
                maxShakeRote -= 0.01f;
            }

            //最大値と最小値の範囲内でかつフラグがtrueなら
            if (this.transform.localRotation.z > -maxShakeRote&& this.transform.localRotation.z < maxShakeRote&& moveFlag)
            {
                //反転処理止める
                moveFlag = false;
            }

            //もう動かなくなったら
            if (maxShakeRote < 0.01f)
            {
                //フラグを止める
                shakeFlag = false;

                //初期化
                shakeSpeed = defoltShakeSpeed;

                //初期化
                maxShakeRote = defoltMaxShakeRote;
            }
            transform.Rotate(new Vector3(0.0f, 0.0f, shakeRote));
        }
        else
        {
            //falseなら動かない
            transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }

        if(starCreateFlag)
        {
            float posX1;
            float posY;
            posX1 = this.transform.position.x;
            posY = this.transform.position.y;

            starCreate.CreateOneStar(new Vector2(posX1, posY), 5, false, 0.5f);

            //もう星出さない
            starCreateFlag = false;

            onceStarCreateFlag = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackBoal")
        {
            //当たったらフラグを渡し実行させる
            shakeFlag = true;

            //一度だけ星出す処理のフラグがtrueなら星出す
            if(onceStarCreateFlag)
            {
                starCreateFlag = true;
            }

        }
    }
}
