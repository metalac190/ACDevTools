using UnityEngine;
using ACDev.Audio;

namespace ACDev.Audio.Sample
{
    public class Tests : MonoBehaviour
    {
        [SerializeField] SoundEvent _explode = null;
        [SerializeField] AudioClip _music1 = null;
        [SerializeField] AudioClip _music2 = null;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _explode.Play2D();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _explode.Play3D(new Vector3(0, 0, 0));
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

