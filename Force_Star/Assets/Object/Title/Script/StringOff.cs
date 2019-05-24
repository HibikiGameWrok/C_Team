using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringOff : MonoBehaviour
{
    // 非アクティブにしたいオブジェクト
    [SerializeField]
    private GameObject[] activeOffObject;

    private GameObject meteorite;

    private RocketTracking roketTrack;

    // Start is called before the first frame update
    void Start()
    {
        meteorite = GameObject.Find("Meteorite");
        roketTrack = meteorite.GetComponent<RocketTracking>();
    }

    // Update is called once per frame
    void Update()
    {
        if(roketTrack.GetMoveFlag() == true)
        {
            for (int i = 0; i < activeOffObject.Length; i++)
            {
                activeOffObject[i].gameObject.SetActive(false);
            }
        }
    }
}
