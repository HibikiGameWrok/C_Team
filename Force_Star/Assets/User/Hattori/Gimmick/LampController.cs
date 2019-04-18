using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    public GameObject starDirec;

    private StarDirector starCreate;

    Rigidbody2D rigid2D;

    [SerializeField]
    private GameObject fire;

    private bool lightFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();

        starCreate = starDirec.GetComponent<StarDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackBoal")
        {
            if (lightFlag == false)
            {
                fire.gameObject.SetActive(true);

                float posX1;
                float posY;
                posX1 = this.transform.position.x;
                posY = this.transform.position.y;

                // 
                starCreate.CreateOneStar(new Vector2(posX1, posY), 5, false,0.5f);
                lightFlag = true;
            }
        }
    }
}
