using UnityEngine;

/// <summary>
/// This class disables all Debug.Log statement calls from Unity, and controls whether or not we're in
/// debug state. Just attach it to a GameObject and it should work.
/// Created by: Adam Chandler
/// </summary>
namespace ACDev.EditorTools
{
    public class DisableDebugInExecutable : MonoBehaviour
    {
        [SerializeField] bool _debugMode = true;
        public bool DebugMode
        {
            get { return _debugMode; }
            private set { _debugMode = value; }
        }

        private void Awake()
        {
            // this saves us from unnecessary performance hits in the final build
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }
    }
}

