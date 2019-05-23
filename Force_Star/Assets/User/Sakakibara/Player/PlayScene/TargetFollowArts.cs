using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CameraArts : MonoBehaviour
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


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 中心点
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private Vector3 m_centerPoint;  

    private GameObject Player = null;  // プレイヤーの管理オブジェクト 

    void Awake()
    {
        Player = GameObject.Find("Player");
        target = Player;

    }

    // Update is called once per frame
    void Update()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 狙う
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateTarget();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 揺れ幅
        //*|***|***|***|***|***|***|***|***|***|***|***|
        UpdateShake();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 場所
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 pos = m_centerPoint + m_shakeDif;
        gameObject.transform.position = pos;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 狙う
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateTarget()
    {
        Vector3 positionTarget = target.transform.position;
        Vector3 positionDif = Vector3.zero;
        positionDif.y = 7;
        positionDif.z = this.transform.position.z;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 中心点設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_centerPoint = positionTarget + positionDif;
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 狙われたい人を受け付けています
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
