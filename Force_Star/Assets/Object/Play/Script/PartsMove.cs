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
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(this.transform.position.x * thrust + xthrust, this.transform.position.y * thrust + ythrust));
    }
}
