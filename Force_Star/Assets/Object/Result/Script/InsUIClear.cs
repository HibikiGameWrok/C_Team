using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsUIClear : MonoBehaviour
{
    bool stop = false;

    GameObject Roket;
    ResultRocketMove resultRocket;


    void Awake()
    {
        Roket = GameObject.Find("Rocket");
        resultRocket = Roket.GetComponent<ResultRocketMove>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(resultRocket.GetMoveEndFlag() == true)
        {
            StartCoroutine("InsUI");
        }
    }


    private IEnumerator InsUI()
    {
        yield return new WaitForSeconds(1.0f);
        if (stop != true)
        {
            GameObject okUI = (GameObject)Resources.Load("GameClearString");
            Instantiate(okUI, new Vector3(this.transform.position.x + 43, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity);
            stop = true;
        }
    }
}
