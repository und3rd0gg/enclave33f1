using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Item emptyItem;

    public List<Item> inventoryItems = new List<Item>();

    private void Awake()
    {
        for (var i = 0; i < inventoryItems.Count; i++)
            if (inventoryItems[i] == null)
                inventoryItems[i] = emptyItem;
    }

    private void Start()
    {
        GameManager.Instance.InventoryContainer.Add(gameObject, this);
    }
}