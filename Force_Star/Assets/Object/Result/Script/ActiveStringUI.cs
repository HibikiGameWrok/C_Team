using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveStringUI : MonoBehaviour
{
    // リザルトシーン管理クラス(クリアしているかを知っている)
    private GameObject activeUI;
    private ResultManager resultManager;

    // ActiveするUIのオブジェクト
    private GameObject panel;
    private ResultFade resulteFade;

    private GameObject Rocket;
    private ResultRocketMove resultRocketMove;

    bool start = false;
    int clearOrOver = 0;

    // Start is called before the first frame update
    void Start()
    {
        activeUI = GameObject.Find("ActiveStringUI");
        resultManager = activeUI.GetComponent<ResultManager>();

        panel = GameObject.Find("Panel");
        resulteFade = panel.GetComponent<ResultFade>();

        if (resulteFade.GetActiveFlag() == true)
        {
            clearOrOver = 1;
            Rocket = GameObject.Find("Rocket");
            resultRocketMove = Rocket.GetComponent<ResultRocketMove>();
        }
        else
        {
            clearOrOver = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(resulteFade.GetActiveFlag() == true)
        {
            StartCoroutine("ActiveCorou");
        }

        if(start == true)
        {
            if(clearOrOver == 1)
            {
                if (resultRocketMove.GetMoveEndFlag() == true)
                {
                    GameObject GameClear = (GameObject)Resources.Load("GameClearString");
                    Instantiate(GameClear, new Vector3(-45, 2, this.transform.position.z), Quaternion.identity);
                    clearOrOver = 0;
                }
            }
            else if(clearOrOver == 2)
            {
                GameObject GameOver = (GameObject)Resources.Load("GameOverString");
                Instantiate(GameOver, new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z), Quaternion.identity);

                clearOrOver = 0;
            }
        }

    }

    private IEnumerator ActiveCorou()
    {
        yield return new WaitForSeconds(1.0f);
        start = true;
    }
}
