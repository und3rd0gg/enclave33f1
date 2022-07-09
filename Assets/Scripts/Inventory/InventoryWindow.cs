using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private GameObject inventoryWindow; //* ссылка на полноценный инвентарь

    [SerializeField] private RectTransform rectTransform;

    private Animator _animator;
    private GameManager _gameManager;

    private Inventory _inventory; //* ссылка на инвентарь игрока

    private List<GameObject> _inventoryCell;

    private List<GameObject> _miniInventoryCell;

    private Action<GameObject, List<GameObject>> _onInventoryChange;

    // [SerializeField]
    // private Text currentItemText;


    private void Start()
    {
        _gameManager = GameManager.Instance;
        _inventory = _gameManager.InventoryContainer[PlayerController.Instance.gameObject];
        _onInventoryChange += DrawInventory; // подписка на событие
        _animator = inventoryWindow.GetComponent<Animator>();
        _miniInventoryCell = new List<GameObject>();
        for (var i = 0; i < gameObject.transform.childCount; i++)
            if (gameObject.transform.GetChild(i).gameObject.GetComponent<Button>())
                _miniInventoryCell.Add(gameObject.transform.GetChild(i).gameObject);
        _inventoryCell = new List<GameObject>();
        for (var i = 0; i < inventoryWindow.transform.childCount; i++)
            if (inventoryWindow.transform.GetChild(i).gameObject.GetComponent<Button>())
                _inventoryCell.Add(inventoryWindow.transform.GetChild(i).gameObject);
        _onInventoryChange(gameObject, _miniInventoryCell);
        inventoryWindow.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Tab)) return;
        if (inventoryWindow.activeInHierarchy)
            CloseInventory();
        else
            OpenInventory();
        //currentItemText = currentItem.Title;
    }

    private void DrawInventory(GameObject go, List<GameObject> cell)
    {
        for (var i = 0; i < go.transform.childCount; i++)
        {
            cell[i].GetComponent<Image>().sprite = _inventory.inventoryItems[i].Icon;
            cell[i].GetComponentInChildren<Text>().text = _inventory.inventoryItems[i].Title;
        }
    }

    private void OpenInventory()
    {
        inventoryWindow.SetActive(true);
        _onInventoryChange(inventoryWindow, _inventoryCell);
        _animator.SetBool("isOpen", true);
    }

    private void CloseInventory()
    {
        _animator.SetBool("isOpen", false);
        StartCoroutine(DisableInventory());
    }

    private IEnumerator DisableInventory()
    {
        yield return new WaitForSeconds(0.9f);
        inventoryWindow.SetActive(false);
    }
}