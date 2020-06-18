using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Uses template pattern to simplify the registering of methods through code
/// for Unity UI buttons. Inherit from this and just add whatever you want to
/// happen on Button Click
/// </summary>
public abstract class ButtonClickAction : MonoBehaviour
{
    public abstract void OnClick();

    Button _button = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}
