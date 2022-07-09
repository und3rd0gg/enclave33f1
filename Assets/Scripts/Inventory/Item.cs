using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Entity/InventoryItem")]
[Serializable]
public class Item : ScriptableObject
{
    private static int _instances;

    [SerializeField] private int _id;

    [SerializeField] private string _title;

    [SerializeField] private bool _stackable;

    [SerializeField] private Sprite _icon;

    public GameObject prefab;

    [SerializeField] [TextArea] private string _description;

    public Item()
    {
        _instances++;
        _id = getActiveInstances() - 1;
    }

    public int ID => _id;

    public string Title => _title;

    public bool Stackable => _stackable;

    public Sprite Icon => _icon;

    public string Description => _description;

    ~Item()
    {
        _instances--;
    }

    private static int getActiveInstances()
    {
        return _instances;
    }
}