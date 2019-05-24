using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsUIOver : MonoBehaviour
{
    bool stop = false;

    void Awake()
    {
        StartCoroutine("InsUI");
        Debug.Log("コルーチン呼ばれた");
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator InsUI()
    {
        yield return new WaitForSeconds(0.5f);
        if (stop != true)
        {
            GameObject okUI = (GameObject)Resources.Load("GameOverString");
            Instantiate(okUI, new Vector3(this.transform.position.x - 40, this.transform.position.y - 1.0f, this.transform.position.z), Quaternion.identity);
            stop = true;
        }
    }
}
