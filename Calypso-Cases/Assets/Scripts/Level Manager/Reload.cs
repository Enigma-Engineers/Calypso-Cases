using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    private Inventory inventory;
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
            inventory = GetComponent<Inventory>();
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1 || scene.buildIndex == 3 || scene.buildIndex == 4)
        {
            onBoardExit();
        } 
        else if(scene.buildIndex == 2)
        {
            onBoardEnter();
        }
    }

    //Sets room states such as player position, and items collected
    public void onBoardExit()
    {
        bool onMap;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Evidence"))      
        {
            onMap = true;
            foreach (ItemPickup item in inventory.items)
            {
                Debug.Log(item.itemName);
                 if (item.itemName == obj.GetComponent<ItemPickup>().itemName)
                 {
                     onMap = false;
                 }
            }
            obj.SetActive(onMap);
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Startup"))
        {
            obj.SetActive(false);
        }
    }

    //Sets board states such as items obtained and connected threads
    public void onBoardEnter()
    {
        bool active;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Evidence"))
        {
            active = false;
            foreach (ItemPickup item in inventory.items)
            {
                if (item.itemName == obj.transform.GetChild(0).GetComponent<CorkboardEvidence>().itemName)
                {
                    active = true;
                }
            }
            obj.SetActive(active);
        }
    }
}
