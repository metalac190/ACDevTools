using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// This script applies Camera shake (when specified) to all flagged Cinemachine cameras.
/// Repurposed from Unity's 3D GameKit assets.
/// </summary>
namespace ACDev.Cinemachine
{
    [RequireComponent(typeof(CinemachineVirtualCameraBase))]
    public class CameraShakeCinemachine : MonoBehaviour
    {
        // list of all cameras with this script, to apply shake to all
        static protected List<CameraShakeCinemachine> Cameras = new List<CameraShakeCinemachine>();

        public const float PlayerHitShakeAmount = 0.05f;
        public const float PlayerHitShakeTime = 0.4f;

        protected float _shakeAmount = 0;
        protected float _remainingShakeTime;

        protected CinemachineVirtualCameraBase _cinemachineCam;
        protected bool _isShaking = false;
        protected Vector3 _originalLocalPosition;

        private void Awake()
        {
            _cinemachineCam = GetComponent<CinemachineVirtualCameraBase>();
        }

        private void OnEnable()
        {
            Cameras.Add(this);
        }

        private void OnDisable()
        {
            Cameras.Remove(this);
        }

        private void LateUpdate()
        {
            if (_isShaking)
            {
                _cinemachineCam.LookAt.localPosition = _originalLocalPosition + Random.insideUnitSphere * _shakeAmount;

                _remainingShakeTime -= Time.deltaTime;
                if (_remainingShakeTime <= 0)
                {
                    _isShaking = false;
                    _cinemachineCam.LookAt.localPosition = _originalLocalPosition;
                }
            }
        }

        private void StartShake(float amount, float time)
        {
            if (!_isShaking)
            {
                _originalLocalPosition = _cinemachineCam.LookAt.localPosition;
            }

            _isShaking = true;
            _shakeAmount = amount;
            _remainingShakeTime = time;
        }

        static public void Shake(float amount, float time)
        {
            for (int i = 0; i < Cameras.Count; ++i)
            {
                Cameras[i].StartShake(amount, time);

            }
        }

        void StopShake()
        {
            _originalLocalPosition = _cinemachineCam.LookAt.localPosition;
            _isShaking = false;
            _shakeAmount = 0f;
            _remainingShakeTime = 0f;
        }

        public static void Stop()
        {
            for (int i = 0; i < Cameras.Count; i++)
            {
                Cameras[i].StopShake();
            }
        }
    }
}
