using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//TODO not fully functional

public class ModalConfirmPanel : MonoBehaviour
{
    [SerializeField] Text _displayTextUI = null;
    [SerializeField] Button _confirmButtonUI = null;
    [SerializeField] Text _confirmTextUI = null;
    [SerializeField] Button _cancelButtonUI = null;
    [SerializeField] Text _cancelTextUI = null;

    public event Action OnConfirmPress = delegate { };
    public event Action OnCancelPress = delegate { };

    public void InitializePanel(string displayData, string confirmData, string cancelData)
    {
        AddLocalEventListeners();
        // pass in data
        _displayTextUI.text = displayData;
        _confirmTextUI.text = confirmData;
        _cancelTextUI.text = cancelData;
    }

    public void InitializePanel(Transform newParent, string displayData, string confirmData, string cancelData)
    {
        //ClearDisplayText();
        //ClearButtonText();
        AddLocalEventListeners();
        // resize
        transform.SetParent(newParent);
        // pass in data
        _displayTextUI.text = displayData;
        _confirmTextUI.text = confirmData;
        _cancelTextUI.text = cancelData;
    }

    void AddLocalEventListeners()
    {
        _confirmButtonUI.onClick.AddListener(ConfirmButtonPress);
        _cancelButtonUI.onClick.AddListener(CancelButtonPress);
    }

    void ConfirmButtonPress()
    {
        OnConfirmPress.Invoke();

        Destroy(gameObject);
    }

    void CancelButtonPress()
    {
        OnCancelPress.Invoke();

        Destroy(gameObject);
    }
}

