using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFadeScene : MonoBehaviour
{
    private GameObject FadePanel;

    private StartFade startFade;

    // Start is called before the first frame update
    void Start()
    {
        FadePanel = GameObject.Find("Panel");
        startFade = FadePanel.GetComponent<StartFade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade.GetFadeInFlag() != true)
            if (startFade.GetFadeMoveFlag() == true)
            {
                //シーン遷移
                SceneManager.LoadScene("PlayScene");
            }
    }
}
