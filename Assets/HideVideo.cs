using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HideVideo : MonoBehaviour
{
    public VideoPlayer Player;
    private bool isPlayerStarted = false;

    public GameObject PauseUI;
    public GameObject CharUI;
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        CharUI.SetActive(false);
        PauseUI.SetActive(false);
    }
    void Update()
    {
        if (isPlayerStarted == false && Player.isPlaying == true)
        {
            // When the player is started, set this information
            isPlayerStarted = true;
            
        }
        if (isPlayerStarted == true && Player.isPlaying == false)
        {
            // Wehen the player stopped playing, hide it
            Player.gameObject.SetActive(false);
            CharUI.SetActive(true);
            PauseUI.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Player.gameObject.SetActive(false);
            CharUI.SetActive(true);
            PauseUI.SetActive(true);
        }
    }
}