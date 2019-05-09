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
            for(int i=0;i<MaxWater;i++)
            {
                WaterWallClone = Instantiate(WaterWallObject, StartPosition, Quaternion.identity) as GameObject;
                WaterWallClone.transform.localScale = transform.localScale;
                WaterWallClone.transform.position=new Vector3(WaterWallClone.transform.position.x, WaterWallClone.transform.position.y-WaterWallClone.GetComponent<Renderer>().bounds.size.y, WaterWallClone.transform.position.z); 
                StartPosition = WaterWallClone.transform.position;
                WaterWallClone.transform.parent = transform;
                myList.Add(WaterWallClone.gameObject);
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
            myList.Add(WaterWallClone.gameObject);
            count = 0.0f;
        }
    }

    public void WaterList(GameObject Wobject)
    {
        Debug.Log(myList.BinarySearch(Wobject));
        Destroy(Wobject);
        myList.Remove(Wobject);
       // Debug.Log(myList[2]);
    }
    public void WaterListAt(GameObject Wobject)
    {
        int i = myList.IndexOf(Wobject);
        //for (int i = myList.IndexOf(Wobject); i>0;i--)
        //{
        //    Debug.Log(i);
           // Destroy(myList[i]);
        //}
        Debug.Log(i);
        myList.RemoveAll(myList => myList == Wobject);
    }
}
