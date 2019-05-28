using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectErrorPanel : MonoBehaviour
{

    void Start()
    {
        if (!this.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            Debug.Log(this.transform.GetChild(0).name);
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
