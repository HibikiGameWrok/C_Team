using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWall : MonoBehaviour
{
    [SerializeField]
    private GameObject WaterWallObject=null;

    [SerializeField]
    private int MaxWater = 1;

    private List<GameObject> myList = new List<GameObject>();

    private List<GameObject> sMyList = new List<GameObject>();

    private GameObject WaterWallClone = null;

    float count;

    bool HitPW = false;

    Vector3 StartPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        if (WaterWallObject!=null)
        {
            //StartPosition.y = StartPosition.y - WaterWallObject.GetComponent<Renderer>().bounds.size.y * (MaxWater + 1);
            for (int i=0;i<MaxWater;i++)
            {
                WaterWallClone = Instantiate(WaterWallObject, StartPosition, Quaternion.identity) as GameObject;
                WaterWallClone.transform.localScale = transform.localScale;
                WaterWallClone.transform.position=new Vector3(WaterWallClone.transform.position.x, WaterWallClone.transform.position.y-WaterWallClone.GetComponent<Renderer>().bounds.size.y*(MaxWater-i), WaterWallClone.transform.position.z); 
               // StartPosition.y = StartPosition.y - WaterWallObject.GetComponent<Renderer>().bounds.size.y * (MaxWater - i);
                WaterWallClone.transform.parent = transform;
                myList.Add(WaterWallClone);
            }
        }
        count = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        count+=0.1f;
        if(count>3.0f)
        {
            WaterWallClone = Instantiate(WaterWallObject, transform.position, Quaternion.identity) as GameObject;
            WaterWallClone.transform.localScale = transform.localScale;
            WaterWallClone.transform.position = new Vector3(WaterWallClone.transform.position.x, WaterWallClone.transform.position.y - WaterWallClone.GetComponent<Renderer>().bounds.size.y, WaterWallClone.transform.position.z);
            WaterWallClone.transform.parent = transform;
            myList.Add(WaterWallClone);
            count = 0.0f;
        }
    }

    public void WaterList(GameObject Wobject)
    {
        if(myList.IndexOf(Wobject)>-1)
        {
            int i = myList.IndexOf(Wobject);
            Destroy(myList[i]);
            myList.RemoveAt(i);
        }
        else
        {
            int i = sMyList.IndexOf(Wobject);
            Destroy(sMyList[i]);
            sMyList.RemoveAt(i);
        }

    }
    public void WaterListAt(GameObject Wobject)
    {

        if (myList.IndexOf(Wobject) > -1)
        {
            for (int i = 0; i < myList.IndexOf(Wobject) + 1; i++)
            {
                Destroy(myList[i]);
            }
            if (sMyList.Count > 0)
            {
                for (int i = 0; i < sMyList.Count; i++)
                {
                    Debug.Log(sMyList.Count);
                    Debug.Log(i);
                    Destroy(sMyList[i]);
                    Debug.Log(i);
                }
                for (int i = 0; i < sMyList.Count; i++)
                {
                    Debug.Log(i);
                    sMyList.RemoveAt(i);
                }
                Debug.Log("徹");
                //   sMyList.RemoveAll(sMyList => sMyList == sMyList);
                //sMyList.RemoveAt(sMyList.Count);
            }
            myList.RemoveAll(myList => myList == Wobject);
            Debug.Log("徹");
            
        }
        else
        {
            for (int i = 0; i < sMyList.IndexOf(Wobject) + 1; i++)
            {
                Destroy(sMyList[i]);
            }
            //for (int i = 0; i < sMyList.IndexOf(Wobject) + 1; i++)
            //{
            //    sMyList.RemoveAt(i);
            //}
            sMyList.RemoveAll(sMyList => sMyList == Wobject);
        }
        HitPW = true;

    }

    public void SetSList(GameObject obj)
    {
        StartPosition = obj.transform.position;
        for (int i = 0; i < MaxWater; i++)
        {
            WaterWallClone = Instantiate(WaterWallObject, StartPosition, Quaternion.identity) as GameObject;
            WaterWallClone.transform.localScale = transform.localScale;
            WaterWallClone.transform.position = new Vector3(WaterWallClone.transform.position.x, WaterWallClone.transform.position.y - WaterWallClone.GetComponent<Renderer>().bounds.size.y * (i + 1), WaterWallClone.transform.position.z);
            // StartPosition.y = StartPosition.y - WaterWallObject.GetComponent<Renderer>().bounds.size.y * (MaxWater - i);
            WaterWallClone.transform.parent = transform;
            sMyList.Add(WaterWallClone);
        }
        HitPW = false;
        sMyList.Reverse();
    }
    public bool CheckListNum(GameObject obj)
    {
        if (myList.IndexOf(obj) > -1)
        {
            int i = myList.IndexOf(obj);
            if (i == 0)
            {
                return true;
            }
        }
        else
        {
            int i = sMyList.IndexOf(obj);
            if (i == 0)
            {
                return false;
            }
        }

        //int i = myList.IndexOf(obj);
        return false;
    }
    public bool GetHitCheck()
    {
        return HitPW;
    }
    public void SetHitflag(bool flag)
    {
        HitPW = flag;
    }
}
