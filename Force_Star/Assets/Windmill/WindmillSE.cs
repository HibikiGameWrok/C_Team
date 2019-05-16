using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillSE : MonoBehaviour
{
    //SE
    private AudioSource sound01;

    // Start is called before the first frame update
    void Start()
    {
        //SE再生データ
        sound01 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "AttackBoal")
        {
             //SEの再生
             sound01.PlayOneShot(sound01.clip);
        }
     }
    
}
