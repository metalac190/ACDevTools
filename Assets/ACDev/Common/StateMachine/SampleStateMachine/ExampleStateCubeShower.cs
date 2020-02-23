using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Samples
{
    public class ExampleStateCubeShower : MonoBehaviour
    {
        [SerializeField] GameController _gameController;

        [SerializeField] MeshRenderer _meshRenderer;

        private void OnEnable()
        {
            _gameController.PlayerTurnState.PlayerTurnStart += ShowObject;
            _gameController.PlayerTurnState.PlayerTurnEnd += HideObject;
        }

        private void OnDisable()
        {
            _gameController.PlayerTurnState.PlayerTurnStart -= ShowObject;
            _gameController.PlayerTurnState.PlayerTurnEnd -= HideObject;
        }

        void ShowObject()
        {
            _meshRenderer.enabled = true;
        }

        void HideObject()
        {
            _meshRenderer.enabled = false;
        }
    }
}

