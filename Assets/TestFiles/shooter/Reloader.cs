using UnityEngine;

public class Reloader : MonoBehaviour
{
    [SerializeField]
    private Weapon activeWeapon;
    private Animator _animator;
    private static readonly int StartReload = Animator.StringToHash("StartReload");
    [SerializeField]
    private GameObject reloadReminder;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown($"Reload"))
        {
            _animator.SetTrigger(StartReload);
        }
    }

    private void LateUpdate()
    {
        CheckLoaded();
    }

    private void Reload()//запускается в анимации
    {
        activeWeapon.Reload();
    }

    private void CheckLoaded()
    {
        if (!activeWeapon.IsLoaded)
        {
            reloadReminder.SetActive(true);
        }
    }
}
