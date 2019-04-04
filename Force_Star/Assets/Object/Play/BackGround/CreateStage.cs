using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStage : MonoBehaviour
{
    //Inspectorに表示される
    [SerializeField]
    private int createFloorCount = 12;   // 生成する床の数

    [SerializeField]
    private GameObject[] valuePrefab = null;  // 生成するプレハブオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < createFloorCount; i++)
        {
            Vector2 pos = new Vector2(i * 3, gameObject.transform.position.y);
            // プレハブからインスタンスを生成
            GameObject obj = (GameObject)Instantiate(valuePrefab[0], new Vector3(pos.x - 18, pos.y,transform.position.z), Quaternion.identity);
            // 作成したオブジェクトを子として登録
            obj.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
