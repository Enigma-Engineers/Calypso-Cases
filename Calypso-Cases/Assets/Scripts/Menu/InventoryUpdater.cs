using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpdater : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField]
    private GameObject _inventoryUIContent;
    [SerializeField]
    private GameObject _buttonPrefab;

    public void RefreshUI()
    {
        Debug.Log("Refreshing UI");

        for (ushort i = 0; i < _inventoryUIContent.transform.childCount; ++i)
        {
            Destroy(_inventoryUIContent.transform.GetChild(i).gameObject);
        }

        List<ItemPickup> inventory = _inventory.GetInventory();

        for (ushort i = 0; i < inventory.Count; ++i)
        {
            GameObject button = Instantiate(_buttonPrefab);
            Image image = button.GetComponent<Image>();
            image.sprite = inventory[i].gameObject.GetComponent<Image>().sprite;

            button.transform.localScale = Vector3.one;
            button.transform.SetParent(_inventoryUIContent.transform);
            button.SetActive(true);
        }
    }

    private void Start()
    {
        _inventory = FindAnyObjectByType<Inventory>();
        _inventory.ItemAdded += RefreshUI;
    }

    private void OnDestroy()
    {
        _inventory.ItemAdded -= RefreshUI;
    }
}
