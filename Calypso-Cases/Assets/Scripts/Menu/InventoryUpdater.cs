using UnityEngine;

public class InventoryUpdater : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private GameObject _inventoryUIContent;

    public void RefreshUI()
    {
        Debug.Log("Refreshing UI");

        for (ushort i = 0; i < _inventoryUIContent.transform.childCount; ++i)
        {
            Destroy(_inventoryUIContent.transform.GetChild(i).gameObject);
        }

        for (ushort i = 0; i < _inventory.GetInventory().Count; ++i)
        {
            GameObject item = Instantiate(_inventory.GetInventory()[i].gameObject);
            item.transform.SetParent(_inventoryUIContent.transform);
        }
    }

    private void Start()
    {
        _inventory.ItemAdded += RefreshUI;
    }

    private void OnDestroy()
    {
        _inventory.ItemAdded -= RefreshUI;
    }
}
