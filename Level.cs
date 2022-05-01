using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks;

    LoadSceneManage sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<LoadSceneManage>();
    }
    public void CountBreakBlocks()
    {
        breakableBlocks++;
    }
    public void BlockDestory()
    {
        breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
