using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMillMove : MonoBehaviour
{
    // 星生成用
    private GameObject star;

    //// 今の一秒当たりの回転角度
    //float nowSpeed ;
    //float startSpeed;   // 保存用

    //// 最大回転速度
    //float maxSpeed;
    //float startMaxSpeed;  // 保存用  
    
    //// 回転速度の加減速用 
    //float addSpeed; // 足す用
    //float subSpeed; // 引く用

    //// 加速してからの減速用カウント
    //int countTime;
    //int startCountTime; //  保存用

    GameObject parent; // 一つ上の親のオブジェクト
    GameObject punchObject;


    // Use this for initialization
    void Start()
    {
        parent = transform.parent.gameObject;
        punchObject = parent.GetComponent<WindMillCollisionScript>().punchObject;

    }

    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, -parent.GetComponent<FeatherSetScript>().GetNowSpeed()));
        //Debug.Log(-parent.GetComponent<FeatherSetScript>().GetNowSpeed());
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //if (punchObject.GetComponent<PunchController>().GetPunchFlag())
        //{
        if (!parent.GetComponent<WindMillCollisionScript>().CheckWindMillFlag())
        {
            if (other.gameObject.tag == "AttackBoal")
                {
                //SEの再生
                //sound01.PlayOneShot(sound01.clip);

                parent.GetComponent<WindMillCollisionScript>().SetCheckHitFlag(true);
                }
        }
    }


    //void SetNowSpeed()
    //{
    //    nowSpeed = parent.GetComponent<FeatherSetScript>().GetNowSpeed();
    //    startSpeed = nowSpeed;
    //}
    //void SetMaxSpeed()
    //{
    //    maxSpeed = parent.GetComponent<FeatherSetScript>().GetMaxSpeed();
    //    startMaxSpeed = maxSpeed;
    //}
    //void SetCountTime()
    //{
    //    countTime = parent.GetComponent<FeatherSetScript>().GetCountTime();
    //    startCountTime = countTime;
    //}
    //void SetAddSpeed()
    //{
    //    addSpeed = parent.GetComponent<FeatherSetScript>().GetAddSpeed();
    //}
    //void SetSubSpeed()
    //{
    //    subSpeed = parent.GetComponent<FeatherSetScript>().GetSubSpeed();
    //}
}
