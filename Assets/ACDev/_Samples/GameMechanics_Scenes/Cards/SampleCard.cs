using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Samples
{
    [System.Serializable]
    public class SampleCard
    {
        [SerializeField] string _name = "Sample Card";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [SerializeField] int _value = 0;
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public SampleCard(string name, int value)
        {
            _name = name;
            _value = value;
        }
    }
}

