using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    GameObject player;
    ParticleSystem Particle;
    ParticleSet paricleSet;
    ParticleSystem ps;
    // 速度
    public float m_speed;
    public float m_attenuation;

    private Vector3 m_velocity;
    private Vector3 m_velocity2;
    // ラジアン変数
    private float rad;

    int count = 0;
    int startMax;
    public int waitTime;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Particle.collision.SetPlane(0, player.transform);
            Particle.trigger.SetCollider(0, player.GetComponent<Collider2D>());
            ParticleSystem[] psArray = GetComponentsInChildren<ParticleSystem>();
            if (Particle.isPlaying)
            {
                rad = Mathf.Atan2(player.transform.position.y - Particle.transform.position.y, player.transform.position.x - Particle.transform.position.x);
                count++;
                if (count == waitTime)
                    paricleSet.ResetSpeed();

                if (Particle.particleCount == 0 && startMax == 0)
                {
                    SetMaxParticle();
                }
                else if (startMax != Particle.particleCount)
                {
                    //Debug.Log("最大" + startMax);
                    //Debug.Log("今の最大" + Particle.particleCount);
                    //Debug.Log("差分" + (startMax - Particle.particleCount));
                    if (startMax != 0)
                        GameObject.Find("StarCount").GetComponent<StarCount>().AddCount(startMax - Particle.particleCount);
                    if (count >= waitTime)
                    {
                        if (Particle.particleCount==0)
                        {
                            //Debug.Log("最大" + startMax);
                            //Debug.Log("今の最大" + Particle.particleCount);
                            //Debug.Log("最後" + (startMax - Particle.particleCount));
                            //GameObject.Find("StarCount").GetComponent<StarCount>().AddCount(startMax - Particle.particleCount);
                            Destroy(transform.root.gameObject);
                        }
                    }
                    startMax = Particle.particleCount;

                }

                if (count >= waitTime)
                {
                    m_velocity.x += (player.transform.position.x - transform.position.x) * m_speed;
                    m_velocity *= m_attenuation;
                   // m_velocity.x = player.transform.position.x;
                    //m_velocity2 += (player.transform.position - psArray[i].transform.position) * m_speed;
                    //m_velocity2 *= m_attenuation;
                    //m_velocity2.x = player.transform.position.x - (transform.position.x-psArray[i].transform.position.x);
                    if (player.GetComponent<PlayerController>().GetJumpFlag())
                    {
                        m_velocity.y = player.transform.position.y;
                    }
                    //// m_velocity *= m_attenuation;
                    //psArray[i].transform.position = m_velocity2;
                    transform.position += m_velocity *= Time.deltaTime;
                    //if (player.transform.position.x < transform.position.x)
                    //{
                    //    transform.position += new Vector3(m_velocity.x, 0.0f, 0.0f) * Time.deltaTime;
                    //}
                    //else if (player.transform.position.x > transform.position.x)
                    //{
                    //    transform.position -= new Vector3(m_velocity.x, 0.0f, 0.0f) * Time.deltaTime;
                    //}
                    //else if(player.transform.position.x == transform.position.x)
                    //{
                    //    transform.position = new Vector3(m_velocity.x, transform.position.y, transform.position.z) ;
                    //}

                }



            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag=="Player")
        {
            //transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        }
    }
    public void SetGameObject(GameObject gameobject)
    {
        player = gameobject;
        Particle = this.GetComponent<ParticleSystem>();
        paricleSet = transform.root.gameObject.GetComponent<ParticleSet>();
        startMax = 0;
    }

    public void SetGameObject(GameObject gameobject,GameObject b)
    {
        player = gameobject;
        Particle = this.GetComponent<ParticleSystem>();
        paricleSet = transform.root.gameObject.GetComponent<ParticleSet>();
        startMax = 0;
    }

    public void SetMaxParticle()
    {
        startMax = Particle.main.maxParticles;
    }


    // これらのリストは、各フレームでトリガーの条件に
    // 一致するパーティクルを格納します
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        // このフレームのトリガーの条件に一致するパーティクルを取得します
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //Debug.Log("入った");
        // トリガーに侵入したパーティクルを走査し、赤にします
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            enter[i] = p;
        }

        // 変更したパーティクルをパーティクルシステムに再割り当てします
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    
    }
}

