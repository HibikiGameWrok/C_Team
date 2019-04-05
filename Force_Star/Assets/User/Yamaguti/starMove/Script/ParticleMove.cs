using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    // 速度
    public Vector2 speed;
    public GameObject player;
    ParticleSystem Particle;
    ParticleSet paricleSet;
    // ラジアン変数
    private float rad;
    // 現在位置を代入する為の変数
    private Vector2 Position;
    int a = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            for (int i = 0; i >= Particle.main.maxParticles; i++)
            {
                Particle.trigger.SetCollider(i, player.GetComponent<Collider2D>());
                Particle.collision.SetPlane(i, player.transform);
            }
            if (Particle.isPlaying)
            {
                rad = Mathf.Atan2(player.transform.position.y - Particle.transform.position.y, player.transform.position.x - Particle.transform.position.x);
                a++;
                if (a == 10)
                    paricleSet.ResetSpeed();
                if (a >= 10)
                {
                    // 現在位置をPositionに代入
                    Position = Particle.transform.position;
                    // x += SPEED * cos(ラジアン)
                    // y += SPEED * sin(ラジアン)
                    // これで特定の方向へ向かって進んでいく。
                    Position.x += speed.x * Mathf.Cos(rad);
                    Position.y += speed.y * Mathf.Sin(rad);
                    // 現在の位置に加算減算を行ったPositionを代入する
                    Particle.transform.position = Position;

                }
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("最大"+Particle.main.maxParticles );
            Debug.Log("今の最大" + Particle.particleCount);
            Debug.Log("差分" + (Particle.main.maxParticles-Particle.particleCount));
            GameObject.Find("StarCount").GetComponent<StarCount>().AddCount(Particle.main.maxParticles - Particle.particleCount);
            if (Particle.main.maxParticles - Particle.particleCount==0)
                Particle.Stop();
        }
    }
    public void SetGameObject(GameObject gameobject)
    {
        player = gameobject;
        Particle = this.GetComponent<ParticleSystem>();
        paricleSet = transform.root.gameObject.GetComponent<ParticleSet>();

    }

    void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        //粒子
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        // 取得する
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        //繰り返します
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            Debug.Log("hit");
            enter[i] = p;
        }
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            exit[i] = p;
        }

        //セット
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
}

