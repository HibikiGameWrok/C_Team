using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    public GameObject starDirec;

    private StarDirector starCreate;

    Rigidbody2D rigid2D;

    private int crystalCount = 0;

    [SerializeField]
    private int maxCrystalCount = 5;

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
            if (crystalCount < maxCrystalCount)
            {
                float posX1;
                float posY;
                posX1 = this.transform.position.x;
                posY = this.transform.position.y;

                starCreate.CreateOneStar(new Vector2(posX1, posY), 5, false, 0.5f);
                crystalCount += 1;
            }
         }
    }
}
