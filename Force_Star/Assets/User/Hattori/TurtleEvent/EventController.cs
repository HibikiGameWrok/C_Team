using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField]
    private GameObject turtle;

    [SerializeField]
    private GameObject rEnemy;

    [SerializeField]
    private GameObject lEnemy;

    private bool changeFlag = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lEnemy.GetComponent<LeftEnemyController>().destroyFlag && rEnemy.GetComponent<RightEnemyController>().destroyFlag)
        {
            changeFlag = true;
            turtle.GetComponent<PresentForPlayer>().savedFlag = true;
        }
    }
}
