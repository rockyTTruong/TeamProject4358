using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Video;

public class TriggerCutscene : MonoBehaviour
{
    public GameObject collisionBox;

    private GameObject cutscene;

    
    // Start is called before the first frame update
    void Start()
    {
        cutscene = GameObject.Find("/Cutscenes/ExplosionCS");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cutscene.SetActive(true);
        }
        
    }
}
