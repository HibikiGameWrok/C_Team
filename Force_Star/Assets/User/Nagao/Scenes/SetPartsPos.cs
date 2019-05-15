using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPartsPos : MonoBehaviour
{
    //最小値
    private int min = 1;
    //最大値
    [SerializeField]
    private int max = 5;

    // 配置するゲームオブジェクト
    [SerializeField]
    private GameObject[] point;

    //生成するゲームオブジェクト
    //public GameObject parts;
    //public GameObject parts2;
    public GameObject[] parts = null;

    //ランダムの結果を代入する変数
    private int[] ransu = null;

    //private int[] ransu;

    int start = 1;
    int end = 5;

    List<int> numbers = new List<int>();


    void Start()
    {

        for (int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }

        while (numbers.Count > 0)
        {

            int index = Random.Range(0, numbers.Count);

            int ransu = numbers[index];
            Debug.Log(ransu);

            numbers.RemoveAt(index);

            SetPos(ransu);

        }

        //ransu2 = Random.Range(min, max + 1);


        //while (ransu2 == ransu)
        //{
        //    ransu2 = Random.Range(min, max + 1);
        //}

        // Debug.Log(ransu);
        //Debug.Log(ransu2);

    }


    void Update()
    {


    }

    void SetPos(int i)
    {
        // parts.transform.position = ransu;

        switch (ransu[i])
        {
            case 1:
                //Instantiate( 生成するオブジェクト,  場所, 回転 ); 
                Instantiate(parts[0], point[0].transform.position, Quaternion.identity);
                break;

            case 2:
                Instantiate(parts[0], point[1].transform.position, Quaternion.identity);
                break;

            case 3:
                Instantiate(parts[0], point[2].transform.position, Quaternion.identity);
                break;

            case 4:
                Instantiate(parts[0], point[3].transform.position, Quaternion.identity);
                break;

            case 5:
                Instantiate(parts[0], point[4].transform.position, Quaternion.identity);
                break;
        }

        //switch (ransu[i])
        //{
        //    case 1:
        //        //Instantiate( 生成するオブジェクト,  場所, 回転 ); 
        //        Instantiate(parts[0], new Vector3(-10.0f, 0.0f, 0.0f), Quaternion.identity);
        //        break;

        //    case 2:
        //        Instantiate(parts[0], new Vector3(-7.0f, 0.0f, 0.0f), Quaternion.identity);
        //        break;

        //    case 3:
        //        Instantiate(parts[0], new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);
        //        break;

        //    case 4:
        //        Instantiate(parts[0], new Vector3(4.0f, 0.0f, 0.0f), Quaternion.identity);
        //        break;

        //    case 5:
        //        Instantiate(parts[0], new Vector3(9.0f, 0.0f, 0.0f), Quaternion.identity);
        //        break;

        //}

        //switch (ransu2)
        //{
        //    case 1:
        //        //Instantiate( 生成するオブジェクト,  場所, 回転 ); 
        //        Instantiate(parts[1], new Vector3(-10.0f, 0.0f, 0.0f), Quaternion.identity);
        //        break;

        //    case 2:
        //        Instantiate(parts[1], new Vector3(-7.0f, 0.0f, 0.0f), Quaternion.identity);
        //        break;

        //    case 3:
        //        Instantiate(parts[1], new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);
        //        break;

        //    case 4:
        //        Instantiate(parts[1], new Vector3(4.0f, 0.0f, 0.0f), Quaternion.identity);
        //        break;

        //    case 5:
        //        Instantiate(parts[1], new Vector3(9.0f, 0.0f, 0.0f), Quaternion.identity);
        //        break;

        //}
    }
}
