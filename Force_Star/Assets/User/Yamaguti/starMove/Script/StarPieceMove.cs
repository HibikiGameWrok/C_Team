using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPieceMove : MonoBehaviour
{
    [SerializeField]
    private int count = 20; // 広がる時間

    // 速度
    public float m_speed;
    public float m_attenuation;
    private Vector3 m_velocity;

    bool flag = false;      // 当たった時のフラグ

    // 動き用

    private float vecX;
    private float vecY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count>0)
        {
            count--;
            this.transform.position = new Vector3(this.transform.position.x + vecX, this.transform.position.y + vecY, this.transform.position.z); // 拡散
        }
        else
        {
            //　追従
            m_velocity += (transform.root.GetComponent<StarManeger>().GetPlayer().transform.position - transform.position) * m_speed;
            m_velocity *= m_attenuation;
            transform.position += m_velocity *= Time.deltaTime;
        }
    }

    public void SetVec(float x,float y)
    {
        vecX = x;
        vecY = y;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!flag)
        {
            //  プレイヤーとの当たり判定
            if (other.gameObject.tag == "Player")
            {
                flag = true;
                GameObject.Find("StarCount").GetComponent<StarCount>().AddCount(1); // 足してるのはここ
                Destroy(this.gameObject);
            }
        }
      
    }
}
