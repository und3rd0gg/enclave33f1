using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Bar : MonoBehaviour
{
    private Image _filler;
    [SerializeReference] private IObservableCharacterCharacteristic iobs;
    [SerializeField] private IObservableCharacterCharacteristic ios;

    private void Awake()
    {
        _filler = GetComponent<Image>();
    }
    
    
}
