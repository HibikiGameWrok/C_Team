using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherCreate : MonoBehaviour
{
    public GameObject feather;
    private GameObject featherObj;
    public int featherNum;
    public GameObject sterDi;
    bool starCreateFlag;
    // Start is called before the first frame update
    void Start()
    {
        if (featherNum >= 5)
            featherNum = 4;
        else if (featherNum <= 0)
            featherNum = 1;
        CreateFeather();
        starCreateFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    public void CreateFeather()
    {
        if (featherNum % 2 == 0)  // 偶数
        {
            for (int i = 0; i < featherNum; i++)
            {
                featherObj = Instantiate(feather, transform.position, Quaternion.identity) as GameObject;
                featherObj.transform.parent = transform;
                featherObj.transform.localScale = new Vector3(2.85285f, 3.395778f, 0);//希望する値
                switch (i)
                {
                    case 1:
                        featherObj.transform.Rotate(new Vector3(0.0f, 0.0f, 180));
                        break;
                    case 2:
                        featherObj.transform.Rotate(new Vector3(0.0f, 0.0f, -90));
                        break;
                    case 3:
                        featherObj.transform.Rotate(new Vector3(0.0f, 0.0f, 90));
                        break;
                }
            }
        }
        else                      // 奇数
        {
            for (int i = 0; i < featherNum; i++)
            {
                featherObj = Instantiate(feather, transform.position, Quaternion.identity) as GameObject;
                featherObj.transform.parent = transform;
                featherObj.transform.localScale = new Vector3(2.85285f, 3.395778f, 0);//希望する値
                switch (i)
                {
                    case 1:
                        featherObj.transform.Rotate(new Vector3(0.0f, 0.0f, 125));
                        break;
                    case 2:
                        featherObj.transform.Rotate(new Vector3(0.0f, 0.0f, -125));
                        break;
                    case 3:
                        featherObj.transform.Rotate(new Vector3(0.0f, 0.0f, 45));
                        break;
                    //case 4:
                    //    featherObj.transform.Rotate(new Vector3(0.0f, 0.0f, -45));
                    //    break;
                }
            }
        }
    }
    public void SetStarCreateFlag(bool flag)
    {
        starCreateFlag = flag;
    }
    public void StarCreate()
    {
        if(!starCreateFlag)
        {
            sterDi.GetComponent<StarDirector>().CreateOneStar(this.transform.position, 10, false, sterDi.GetComponent<StarDirector>().GetStarJump(),true);
            SetStarCreateFlag(true);
        }
       
    }
}
