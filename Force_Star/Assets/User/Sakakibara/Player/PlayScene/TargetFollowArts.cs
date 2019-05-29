using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CameraArts : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // カメラのターゲット
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private GameObject target = null; // 追従する目標
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // カメラ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private Camera m_camera;
    private int m_size;
    private int m_difY;
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // カメラ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_camera = gameObject.GetComponent<Camera>();
        if (!m_camera)
        {
            m_camera = gameObject.AddComponent<Camera>();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // カメラのサイズ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_size = 10;
        m_difY = 4;
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // サイズ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_camera.orthographicSize = m_size;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 狙う
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdateTarget()
    {
        Vector3 positionTarget = target.transform.position;
        Vector3 positionDif = Vector3.zero;
        positionDif.y = m_difY;
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
