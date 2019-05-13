using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWallMove : MonoBehaviour
{
    bool grounded = false;

    LayerMask playerlayer;

    public ParticleSystem HitEffect;
    GameObject starManeger = null;
    // Start is called before the first frame update
    void Start()
    {
        starManeger = GameObject.Find("StarManeger");
        playerlayer = LayerMask.GetMask(LayerMask.LayerToName(9));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y+-0.1f, transform.position.z);
    }

    void HitCheck()
    {
        grounded = Physics2D.Linecast(transform.position - transform.up * 1.7f, transform.position - transform.up * 3.0f, playerlayer);


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ok");
        if (other.gameObject.tag == "WaterWall")
        {
            this.transform.parent.GetComponent<WaterWall>().WaterList(gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            starManeger.GetComponent<StarManeger>().CreateStarPisce(this.transform.position, 20);
            if (HitEffect != null)
            {
                Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2, this.transform.position.z);
                ParticleSystem par = Instantiate(HitEffect, pos, Quaternion.identity) as ParticleSystem;
                par.Play();
            }
            this.transform.parent.GetComponent<WaterWall>().WaterListAt(gameObject);
        }
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    Debug.Log("ok");
    //    if (other.gameObject.tag == "WaterWall")
    //    {
    //        this.transform.parent.GetComponent<WaterWall>().WaterList(gameObject);
    //    }
    //    if (other.gameObject.tag == "Player")
    //    {
    //        starManeger.GetComponent<StarManeger>().CreateStarPisce(this.transform.position, 20);
    //        if (HitEffect != null)
    //        {
    //            ParticleSystem par = Instantiate(HitEffect, this.transform.position, Quaternion.identity) as ParticleSystem;
    //            par.Play();
    //        }
    //        this.transform.parent.GetComponent<WaterWall>().WaterListAt(gameObject);
    //    }

    //}
}
