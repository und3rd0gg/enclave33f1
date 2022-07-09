using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    //? прикрутить получение ссылки через гейм манагер
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Text maxClipAmmo;
    [SerializeField] private Text currentClipAmmo;

    private void Update()
    {
        currentClipAmmo.text = currentWeapon.CurrentClipAmmo.ToString();
        maxClipAmmo.text = "/ " + currentWeapon.MaxClipAmmo;
    }
}
