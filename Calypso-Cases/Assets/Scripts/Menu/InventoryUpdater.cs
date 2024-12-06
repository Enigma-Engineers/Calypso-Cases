using Ink.Parsed;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpdater : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField]
    private GameObject _inventoryUIContent;
    [SerializeField]
    private TextMeshProUGUI _inventoryItemName;
    [SerializeField]
    private TextMeshProUGUI _inventoryItemDescription;
    [SerializeField]
    private GameObject _buttonPrefab;

    public void RefreshUI()
    {
        for (ushort i = 0; i < _inventoryUIContent.transform.childCount; ++i)
        {
            Destroy(_inventoryUIContent.transform.GetChild(i).gameObject);
        }

        List<ItemPickup> inventory = _inventory.GetInventory();

        for (ushort i = 0; i < inventory.Count; ++i)
        {
            ushort itemPos = i;
            GameObject buttonObject = Instantiate(_buttonPrefab);

            buttonObject.GetComponent<Image>().sprite = inventory[i].gameObject.GetComponent<Image>().sprite;
            buttonObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                ItemPickup item = inventory[itemPos];
                _inventoryItemName.text = item.itemName;
                _inventoryItemDescription.text = item.description;
            });

            buttonObject.transform.localScale = Vector3.one;
            buttonObject.transform.SetParent(_inventoryUIContent.transform);
            buttonObject.SetActive(true);
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
