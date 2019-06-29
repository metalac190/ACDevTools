using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This script gives Tooltip functionality to a raycast UI object. A tooltip prefab
/// is spawned and positioned on mouse hover.
/// Created by: Adam Chandler
/// Notes: Make sure the object you attach this to has Raycast enabled. Make sure you
/// drag the TooltipBox into the prefab slot.
/// </summary>
namespace ACDev.UI
{
    public class TooltipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] string _hoverText = "";
        [SerializeField] float _hoverDelay = .25f;
        [SerializeField] TooltipBox _tooltipBoxPrefab = null;
        [SerializeField] Transform _newPrefab = null;
        [SerializeField] Vector2 _offset = new Vector3(0, -25);

        Coroutine _delayTimer;
        EventTrigger _eventTrigger;
        TooltipBox _activeTooltipBox;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(_tooltipBoxPrefab == null)
            {
                Debug.LogWarning("No Tooltip prefab assigned: " + gameObject.name);
                return;
            }
            // if we already have a timer, cancel it
            if(_delayTimer != null)
            {
                StopCoroutine(_delayTimer);
            }
            // start a new timer
            _delayTimer = StartCoroutine(WaitForTooltip(_hoverDelay));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // if we already have a timer, cancel it
            if(_delayTimer != null)
            {
                StopCoroutine(_delayTimer);
            }

            DestroyActiveTooltip();
        }

        void OnDisable()
        {
            // disabling gameObject doesn't automatically call a "pointerExit" call, do this to ensure it gets destroyed
            if(_activeTooltipBox != null)
            {
                DestroyActiveTooltip();
            }
        }

        private void SpawnNewTooltip()
        {
            TooltipBox tooltipBox = Instantiate(_tooltipBoxPrefab, gameObject.transform);
            if (_newPrefab == null)
            {
                _newPrefab = gameObject.transform;
            }
            tooltipBox.Initialize(_newPrefab, _hoverText, _offset);
            _activeTooltipBox = tooltipBox;
        }

        void DestroyActiveTooltip()
        {
            if(_activeTooltipBox == null)
            {
                //Debug.LogWarning("Can't destroy tooltip, as one does not exist");
                return;
            }

            Destroy(_activeTooltipBox.gameObject);
        }

        IEnumerator WaitForTooltip(float hoverDelay)
        {
            yield return new WaitForSeconds(hoverDelay);
            SpawnNewTooltip();
        }
    }
}

