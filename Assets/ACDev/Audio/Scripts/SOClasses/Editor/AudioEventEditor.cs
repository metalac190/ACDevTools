using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
[CustomEditor(typeof(MinAttribute))]
public class AudioEventEditor : Editor
{
    AudioEvent _target;
    float _minVolumeRange = 0;
    float _maxVolumeRange = 1;
    float minPitch = .5f;
    float maxPitch = 1.5f;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _target = (AudioEvent)target;
        DrawVolumeSlider();
    }

    void DrawVolumeSlider()
    {
        EditorGUILayout.MinMaxSlider(ref _minVolumeRange, ref _maxVolumeRange, _target.MinVolume, _target.MaxVolume);
    }
}
*/

