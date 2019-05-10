using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherSetScript : MonoBehaviour
{
    [SerializeField]
    private GameObject phantomStar;

    // 今の一秒当たりの回転角度
    public float nowSpeed;
    float startSpeed;   // 保存用

    // 最大回転速度
    public float maxSpeed;
    float startMaxSpeed;  // 保存用  

    // 回転速度の加減速用 
    public float addSpeed; // 足す用
    public float subSpeed; // 引く用

    // 加速してからの減速用カウント
    public int countTime;
    int startCountTime; //  保存用

    bool gone = false;

    // Start is called before the first frame update
    void Start()
    {
        startSpeed = nowSpeed;
        startMaxSpeed = maxSpeed;
        countTime *= 60;
        startCountTime = countTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.GetComponent<WindMillCollisionScript>().CheckHitFlag())
        {
            if (nowSpeed <= maxSpeed)
            {
                nowSpeed += addSpeed;
                if ((nowSpeed > maxSpeed - 20)&&(gone == false))
                {
                    phantomStar.gameObject.SetActive(true);
                }
            }
            countTime--;

            if (countTime <= 0 && !this.GetComponent<WindMillCollisionScript>().CheckWindMillFlag())
            {
                phantomStar.gameObject.SetActive(false);
                if (gone == false)
                {
                    gone = true;
                    this.GetComponent<FeatherCreate>().StarCreate();
                }
                this.GetComponent<WindMillCollisionScript>().SetCheckHitFlag(false);
                this.GetComponent<WindMillCollisionScript>().SetCheckWindMillFlag(true);
            }
        }
        if (countTime <= 0 && !this.GetComponent<WindMillCollisionScript>().CheckHitFlag())
        {
            if (nowSpeed > startSpeed)
                nowSpeed += -subSpeed;
            else
            { 
                nowSpeed = startSpeed;
                countTime = startCountTime;
                this.GetComponent<WindMillCollisionScript>().SetCheckWindMillFlag(false);
            }
        }
    }
    public float GetNowSpeed()
    {
        return nowSpeed;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
    public float GetAddSpeed()
    {
        return addSpeed;
    }
    public float GetSubSpeed()
    {
        return subSpeed;
    }

    public int GetCountTime()
    {
        return countTime * 60;
    }
}
