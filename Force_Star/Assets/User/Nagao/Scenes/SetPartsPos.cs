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

    //生成するゲームオブジェクト
    //public GameObject parts;
    //public GameObject parts2;
    public GameObject[] parts;

    //ランダムの結果を代入する変数
    private int ransu = 0;
    private int ransu2 = 0;
    //private int[] ransu;


    void Start()
    {
        //ランダム生成
        ransu = Random.Range(min, max + 1);
        ransu2 = Random.Range(min, max + 1);


        while (ransu2 == ransu)
        {
            ransu2 = Random.Range(min, max + 1);
        }

        // Debug.Log(ransu);
        //Debug.Log(ransu2);
        SetPos();
    }


    void Update()
    {


    }

    void SetPos()
    {
        // parts.transform.position = ransu;

        switch (ransu)
        {
            case 1:
                //Instantiate( 生成するオブジェクト,  場所, 回転 ); 
                Instantiate(parts[0], new Vector3(-10.0f, 0.0f, 0.0f), Quaternion.identity);
                break;

            case 2:
                Instantiate(parts[0], new Vector3(-7.0f, 0.0f, 0.0f), Quaternion.identity);
                break;

            case 3:
                Instantiate(parts[0], new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);
                break;

            case 4:
                Instantiate(parts[0], new Vector3(4.0f, 0.0f, 0.0f), Quaternion.identity);
                break;

            case 5:
                Instantiate(parts[0], new Vector3(9.0f, 0.0f, 0.0f), Quaternion.identity);
                break;

        }

        switch (ransu2)
        {
            case 1:
                //Instantiate( 生成するオブジェクト,  場所, 回転 ); 
                Instantiate(parts[1], new Vector3(-10.0f, 0.0f, 0.0f), Quaternion.identity);
                break;

            case 2:
                Instantiate(parts[1], new Vector3(-7.0f, 0.0f, 0.0f), Quaternion.identity);
                break;

            case 3:
                Instantiate(parts[1], new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);
                break;

            case 4:
                Instantiate(parts[1], new Vector3(4.0f, 0.0f, 0.0f), Quaternion.identity);
                break;

            case 5:
                Instantiate(parts[1], new Vector3(9.0f, 0.0f, 0.0f), Quaternion.identity);
                break;

        }
    }
}
