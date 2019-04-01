using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    // 速度
    public Vector2 speed = new Vector2(0.05f, 0.05f);
    public GameObject player;
    ParticleSystem Particle;
    // ラジアン変数
    private float rad;
    // 現在位置を代入する為の変数
    private Vector2 Position;
    int a = 0;

    // Start is called before the first frame update
    void Start()
    {
        Particle = this.GetComponent<ParticleSystem>();
  
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null)
        {
            if (Particle.isPlaying)
            {
                rad = Mathf.Atan2(player.transform.position.y - Particle.transform.position.y, player.transform.position.x - Particle.transform.position.x);
                a++;
                if(a>=2)
                {
                   if (Particle.IsAlive())
                        Particle.Stop();
                    // 現在位置をPositionに代入
                    Position = Particle.transform.position;
                    // x += SPEED * cos(ラジアン)
                    // y += SPEED * sin(ラジアン)
                    // これで特定の方向へ向かって進んでいく。
                    Position.x += speed.x * Mathf.Cos(rad);
                    Position.y += speed.y * Mathf.Sin(rad);
                    // 現在の位置に加算減算を行ったPositionを代入する
                    Particle.transform.position=Position;
                   

                }
                   
            }
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
           // Particle.collision.lifetimeLoss = 1;
        }
    }
    public void SetGameObject(GameObject gameobject)
    {
        player = gameobject;

    }
}
