using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public float m_backSpeed = 0.2f;
    public float m_scale = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //周波数（振動数）の公式
        float f = 1.0f / m_backSpeed;

        float sin = Mathf.Sin(2 * Mathf.PI * f * Time.time);

        transform.localPosition = new Vector3(0.0f, sin * m_scale, 0.0f);  
    }
}
