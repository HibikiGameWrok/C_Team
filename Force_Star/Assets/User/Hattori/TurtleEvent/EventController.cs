using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private GameObject turtle;
    private PresentForPlayer presentForPlayer;

    private GameObject rEnemy;
    private GameObject lEnemy;
    private LeftEnemyController leftEnemyController;
    private RightEnemyController rightEnemyController;


    private bool[] destroyFlag = new bool[2];

    private bool changeFlag = false; 

    // Start is called before the first frame update
    void Start()
    {
        turtle = GameObject.Find("Turtle");
        presentForPlayer = turtle.GetComponent<PresentForPlayer>();

        lEnemy = GameObject.Find("lEnemy");
        rEnemy = GameObject.Find("rEnemy");
        leftEnemyController = lEnemy.GetComponent<LeftEnemyController>();
        rightEnemyController = rEnemy.GetComponent<RightEnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームオブジェクトが生きている時、敵が生きているか毎フレーム確認
        destroyFlag[0] = leftEnemyController.destroyFlag;
        destroyFlag[1] = rightEnemyController.destroyFlag;

        // 両側の敵を倒されていたら
        if (destroyFlag[0] == true && destroyFlag[1] == true)
        {
            changeFlag = true;
            // 星を出す為の動作
            presentForPlayer.savedFlag = true;
        }    
    }
}
