using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWallMove : MonoBehaviour
{
    bool[] playerHF = { false, false };
    bool waterHF = false;

    LayerMask playerlayer;


    LayerMask waterlayer;

    public ParticleSystem HitEffect;
    GameObject starManeger = null;

    private const float RAY_DISPLAY_TIME = 3;
    // Start is called before the first frame update
    void Start()
    {
        starManeger = GameObject.Find("StarManeger");
        playerlayer = LayerMask.GetMask(LayerMask.LayerToName(8));
        waterlayer = LayerMask.GetMask(LayerMask.LayerToName(14));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y+-0.1f, transform.position.z);
        HitCheck();
    }

    void HitCheck()
    {
        Vector3 pos=new Vector3(transform.position.x - (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.y, transform.position.z);
        playerHF[0] = Physics2D.Linecast(pos, pos - transform.up * 7.0f, playerlayer);

        pos = new Vector3(transform.position.x + (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.y, transform.position.z);
        playerHF[1] = Physics2D.Linecast(pos, pos - transform.up * 7.0f, playerlayer);

        pos = new Vector3(transform.position.x, transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2, transform.position.z);
        waterHF = Physics2D.Linecast(new Vector3(transform.position.x, transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2, transform.position.z), pos - transform.up * 1.0f, waterlayer);

        if (!playerHF[0] && !playerHF[1] && !waterHF && this.transform.parent.GetComponent<WaterWall>().GetHitCheck())
        {
            Debug.Log(1);
            this.transform.parent.GetComponent<WaterWall>().SetSList(this.gameObject);
        }


    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "WaterWall")
        {
            this.transform.parent.GetComponent<WaterWall>().WaterList(gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            Vector3 raypos = new Vector3(transform.position.x - (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.y - (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.z);
            playerHF[0] = Physics2D.Linecast(raypos, raypos - transform.up * 7.0f, playerlayer);

            raypos = new Vector3(transform.position.x + (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.y - (transform.GetComponent<Renderer>().bounds.size.x / 2.0f), transform.position.z);
            playerHF[1] = Physics2D.Linecast(raypos, raypos - transform.up * 7.0f, playerlayer);

           if (playerHF[0] || playerHF[1])
            {
                // this.transform.parent.GetComponent<WaterWall>().SetHitflag(true);
                if (HitEffect != null)
                {
                    starManeger.GetComponent<StarManeger>().CreateStarPisce(this.transform.position, 20);
                    Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2, this.transform.position.z);
                    ParticleSystem par = Instantiate(HitEffect, pos, Quaternion.identity) as ParticleSystem;
                    par.Play();
                }
            }          
            this.transform.parent.GetComponent<WaterWall>().WaterListAt(gameObject);
        }
    }
    void OnTriggerSTay2D(Collider2D other)
    {
        if (other.gameObject.tag == "WaterWall")
        {
            this.transform.parent.GetComponent<WaterWall>().WaterList(gameObject);
        }
    }
}
