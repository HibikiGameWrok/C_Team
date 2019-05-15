using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsMove : MonoBehaviour
{
    [SerializeField]
    private float thrust;

    [SerializeField]
    private float xthrust;

    [SerializeField]
    private float ythrust;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // コルーチンを実行  
        StartCoroutine("Delete");
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(this.transform.position.x * thrust + xthrust, this.transform.position.y * thrust + ythrust));
    }

    private IEnumerator Delete()
    {
        // 1秒待つ  
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
