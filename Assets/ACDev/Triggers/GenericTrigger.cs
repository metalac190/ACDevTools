using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent()]
public class GenericTrigger : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("This is the gameobject which will trigger the director to play.  For example, the player.")]
    [SerializeField]
    private GameObject _specificTriggerObject = null;
    [SerializeField]
    private LayerMask _layersToDetect = -1;     // default to 'Everything'
    [SerializeField]
    private bool _onlyEnterOnce = false;

    [Space(10)]
    public UnityEvent<Collider> OnEnterTrigger;
    public UnityEvent<Collider> OnExitTrigger;

    private Collider _collider;

    [Header("Gizmo Settings")]
    [SerializeField]
    private bool _displayGizmos;
    [SerializeField]
    private bool _showOnlyWhileSelected = true;
    [SerializeField]
    private Color _gizmoColor = Color.green;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsObjectValid(other.gameObject) == false)
            return;

        Debug.Log("Entered");
        OnEnterTrigger.Invoke(other);

        // if we only use once, disable component
        if (_onlyEnterOnce)
            _collider.enabled = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (IsObjectValid(other.gameObject) == false)
            return;

        Debug.Log("Exited");
        OnExitTrigger.Invoke(other);
    }

    private void OnDrawGizmos()
    {
        if (_displayGizmos == false)
            return;
        if (_showOnlyWhileSelected == true)
            return;
        // ensure collider is filled
        if (_collider == null)
            _collider = GetComponent<Collider>();
        //
        Gizmos.color = _gizmoColor;
        Gizmos.DrawCube(transform.position, _collider.bounds.size);
    }

    private void OnDrawGizmosSelected()
    {
        if (_displayGizmos == false)
            return;
        if (_showOnlyWhileSelected == false)
            return;
        // ensure collider is filled
        if (_collider == null)
            _collider = GetComponent<Collider>();
        //
        Gizmos.color = _gizmoColor;
        Gizmos.DrawCube(transform.position, _collider.bounds.size);
    }

    private bool IsObjectValid(GameObject objectToTest)
    {
        if (_specificTriggerObject != null
            && objectToTest != _specificTriggerObject)
            return false;
        // if gameObject is not in any of the layers specified to detect, return
        if (_layersToDetect != (_layersToDetect | (1 << objectToTest.layer)))
            return false;
        else
            return true;
    }
}
