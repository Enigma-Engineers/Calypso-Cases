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
    private List<ItemPickup> items = new List<ItemPickup>();
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
            items = inventory.GetInventory();
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
        inventory.SetInventory(items);

        Debug.Log(items.Count);

        bool onMap;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Evidence"))      
        {
            onMap = true;
            foreach (ItemPickup item in items)
            {
                Debug.Log(item.itemName);
                 if (item.itemName == obj.GetComponent<ItemPickup>().itemName)
                 {
                     onMap = false;
                 }
            }
            obj.SetActive(onMap);
        }
    }

    //Sets board states such as items obtained and connected threads
    public void onBoardEnter()
    {
        bool active;
        items = inventory.GetInventory();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Evidence"))
        {
            active = false;
            foreach(ItemPickup item in items)
            {
                if(item.itemName == obj.GetComponent<CorkboardEvidence>().itemName)
                {
                    active = true;
                } 
            }
            obj.SetActive(active);
        }

        x = 5.5f;
        y = -17.5f;
    }
}
