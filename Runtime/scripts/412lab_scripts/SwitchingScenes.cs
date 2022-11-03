using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SwitchingScenes : MonoBehaviour
{
    public int index = 0;
    int sceneCount; 

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.N))
        {
                 index++;
                 if(index >= sceneCount)
                     {
                         index = 0;
                     }
                    SceneManager.LoadScene(index);


        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            index--;
            if (index < 0)
            {
                index = 0;
            }
            SceneManager.LoadScene(index);
        }

    }
}
