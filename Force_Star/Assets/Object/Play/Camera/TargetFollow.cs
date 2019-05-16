using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null; // 追従する目標

    [SerializeField]
    private GameObject point = null; // 超える目標

    [SerializeField]
    private float MinimumLimit = 0.0f;  // 最低の範囲

    [SerializeField]
    private float HighLimit = 0.0f;  // 最高の範囲

    [SerializeField]
    private float count = 0.05f;  // 最高の範囲

    private GameObject Player = null;  // プレイヤーの管理オブジェクト 

    private void Awake()
    {
        Player = GameObject.Find("Player");
        target = Player;
    }

    // Update is called once per frame
    void Update()
    {


        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);

        //if (transform.position.y < MinimumLimit)
        //{
        //    transform.position = new Vector3(target.transform.position.x, MinimumLimit, this.transform.position.z);
        //}

        //if (transform.position.x >= point.transform.position.x)
        //{
        //    if(this.transform.position.z - count>=-30)
        //    {
        //        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z - count);
        //    }
        //}
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 狙われたい人を受け付けています
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
