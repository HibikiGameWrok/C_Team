using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

public class WaterWallMusicScript : MonoBehaviour
{
    AudioSource audioSource;
    PlayerDirectorIndex m_playerIndex;

    // Start is called before the first frame update
    void Start()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();

        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource.isPlaying)
        {
            float leave = this.transform.position.x - m_playerIndex.GetPlayerPosition().x;
            float Volume = MyCalculator.Division(leave, 100.0f);
            Volume = ChangeData.Among(Volume, 0.0f, 1.0f);
            Volume = MyCalculator.InversionOfProportion(Volume);
            audioSource.volume = Volume;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

}
