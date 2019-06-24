using UnityEngine;
using ACDev.Audio;

namespace ACDev.Audio.Sample
{
    public class Tests : MonoBehaviour
    {
        [SerializeField] SoundEvent2D _explode;
        [SerializeField] SoundEvent3D _explode3D;
        [SerializeField] AudioClip _music1;
        [SerializeField] AudioClip _music2;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _explode.Play();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _explode3D.Play(new Vector3(0, 0, 0));
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                AudioManager.Instance.PlayMusic(_music1);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.Instance.PlayMusicWithFade(_music2);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                AudioManager.Instance.PlayMusicWithCrossFade(_music1);
            }
        }
    }
}

