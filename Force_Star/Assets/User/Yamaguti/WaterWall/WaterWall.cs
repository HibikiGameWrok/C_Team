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

    private GameObject WaterWallClone = null;

    float count;

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
        int i = myList.IndexOf(Wobject);
        Destroy(myList[i]);
        myList.RemoveAt(i);
    }
    public void WaterListAt(GameObject Wobject)
    {

        for (int i = 0; i < myList.IndexOf(Wobject)+1; i++)
        {
            Debug.Log(i);
            Destroy(myList[i]);
        }

        myList.RemoveAll(myList => myList == Wobject);
    }
}
