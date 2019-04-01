using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject starDirec; //星
    StarDirector starCreate;

    float vecX;
    float vecY1;
    float vecY2;
    public float a;

    // Start is called before the first frame update
    void Start()
    {
       // starDirec= GameObject.Find("Star");
        starCreate = starDirec.GetComponent<StarDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, 0f);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "weel")
        {
            vecY1 = this.transform.position.y + this.GetComponent<Renderer>().bounds.size.y / 2 + a;
            vecY2 = this.transform.position.y - this.GetComponent<Renderer>().bounds.size.y / 2 - a;
            vecX = this.transform.position.x - this.GetComponent<Renderer>().bounds.size.x / 2;
            starCreate.CreateStar(new Vector3(vecX,vecY1,0.0f),new Vector3(vecX, vecY2, 0.0f),5,false, 0.5f,0.0f);
        }

        if (other.gameObject.tag == "Floor")
        {
            starCreate.CreateStar();
        }
        //if (other.gameObject.name == "New Sprite")
        //{
        //    starCreate.CreateStar();
        //}
    }
}
