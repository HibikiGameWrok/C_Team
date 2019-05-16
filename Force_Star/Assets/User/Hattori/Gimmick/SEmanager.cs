using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEmanager : MonoBehaviour
{
    //
    public static string music = "music/";
    public static string se = "SE/";
    [SerializeField]
    AudioClip[] m_sounds;

    enum soundID
    {
        NONE,
        ENEMY,
        SAND_BLOCK,
        STAR,
        MAXNUM
    }
    [SerializeField]
    soundID m_soundId;
    ////敵を倒した時のSE
    //public AudioClip m_sound_Enemy;

    ////砂ブロックを破壊した時のSE
    //public AudioClip m_sound_SandBlock;

    ////星を取った時のSE
    //public AudioClip m_sound_Star;

    [SerializeField]
    bool soundFlag = false;

    AudioSource m_audiosource;

    void Awake()
    {

        m_sounds = new AudioClip[(int)soundID.MAXNUM];
        ReadData();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_audiosource = GetComponent<AudioSource>();
        m_soundId = soundID.NONE;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データの入れどころ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void ReadData()
    {
        string filepass;

        filepass = music + se;

        m_sounds[(int)soundID.NONE] = Resources.Load<AudioClip>(filepass + "bomb");

        m_sounds[(int)soundID.ENEMY] = Resources.Load<AudioClip>(filepass + "bomb");

        m_sounds[(int)soundID.SAND_BLOCK] = Resources.Load<AudioClip>(filepass + "impact");

        m_sounds[(int)soundID.STAR] = Resources.Load<AudioClip>(filepass + "magic");
    }

    private AudioClip GetGameSE(soundID textureNum)
    {
        int type = ChangeData.AmongLess((int)textureNum, 0, (int)soundID.MAXNUM);
        return m_sounds[type];
    }

    // Update is called once per frame
    void Update()
    {
        //switch (m_soundId)
        //{
        //    case soundID.ENEMY:
        //        m_audiosource.PlayOneShot(GetGameSE(m_soundId));
        //        break;
        //    case soundID.SAND_BLOCK:
        //        m_audiosource.PlayOneShot(m_sound_SandBlock);
        //        break;
        //    case soundID.STAR:
        //        m_audiosource.PlayOneShot(m_sound_Star);
        //        break;
        //}



        //if (m_soundId != soundID.NONE)
        //    m_soundId = soundID.NONE;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_soundId = soundID.ENEMY;

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_soundId = soundID.SAND_BLOCK;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            m_soundId = soundID.STAR;
        }

        if (soundFlag)
        {
            m_audiosource.PlayOneShot(GetGameSE(m_soundId));
            soundFlag = false;
        }
    }
}
