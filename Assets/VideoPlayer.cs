using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VP : MonoBehaviour
{
    public VideoPlayer player;
    public GameObject Canvas;
    public GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(false);
        pauseUI.SetActive(false);
        player = GetComponent<VideoPlayer>();

        player.Play();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            disappear(player);
        }
        player.loopPointReached += disappear;
    }

    void disappear(VideoPlayer vp)
    {
       
        Canvas.SetActive(true);
        pauseUI.SetActive(true);
    }

}
