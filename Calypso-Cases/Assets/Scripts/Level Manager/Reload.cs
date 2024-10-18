using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class Reload : MonoBehaviour
{
    private List<string> items;
    private List<string> threads;
    private float x;
    private float y;
    private static Reload instance = null;

    public static Reload Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)
        {
            onBoardExit();
        } 
        else if(scene.buildIndex == 1)
        {
            onBoardEnter();
        }
    }

    //Sets room states such as player position, and items collected
    public void onBoardExit()
    {
        GameObject.Find("Player").transform.position = new Vector3(x, y, 0);

        bool onMap;

        for (int i = 1; i < 3; i++)
        {
            onMap = true;
            foreach (string item in items)
            {
                if (item.Equals($"Evidence{i}"))
                {
                    onMap = false;
                }
            }
            GameObject.Find($"Evidence{i}").SetActive(onMap);
        }
    }

    //Sets board states such as items obtained and connected threads
    public void onBoardEnter()
    {
        items = this.GetComponent<Inventory>().GetInventory();
        bool active;
        for(int i = 1; i < 3; i++)
        {
            active = false;
            foreach(string item in items)
            {
                if (item.Equals($"Evidence{i}"))
                {
                    active = true;
                } 
            }
            GameObject.Find($"Pin{i}").SetActive(active);
        }

        this.GetComponent<Inventory>().SetInventory(items);

        x = 5.5f;
        y = -17.5f;
    }
}
