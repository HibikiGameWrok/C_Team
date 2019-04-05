using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingImage : MonoBehaviour
{
    public GameObject clearImage;

    [SerializeField]
    float time = 0.0f;

    [SerializeField]
    int next = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += 0.03f;

        next = (int)time % 2;

        if (next == 0)
        {

            clearImage.SetActive(true);
        }
        else
        {

            clearImage.SetActive(false);
        }
    }
}
