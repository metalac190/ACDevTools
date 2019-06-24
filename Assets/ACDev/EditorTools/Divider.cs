using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script gives a GameObject Divider functionality, quickly turning it into
/// a hierarchy separator. Primarily used for simpler scene organization
/// Created by: Adam Chandler
/// How to Use: Just apply to an empty GameObject and give it a name
/// </summary>
namespace ACDev.EditorTools
{
    public class Divider : MonoBehaviour
    {
        [SerializeField] string _name = "Divider";
        public string Name { get { return _name; } }

        private void Reset()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}


