using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VideoManager : SingletonMonobehaviour<VideoManager>
{
    public List<GameObject> videos = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void PlayVideo(int index)
    {
        videos[index].gameObject.SetActive(true);
    }
}
