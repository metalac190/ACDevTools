using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Samples
{
    public partial class SampleGameController : MonoBehaviour
    {
        State SampleActionState
        {
            get
            {
                if(_sampleActionState == null)
                {
                    _sampleActionState = new State(OnEnterSampleActionState, null, "SampleAction");
                }
                return _sampleActionState;
            }
        }
        State _sampleActionState;

        void OnEnterSampleActionState()
        {
            Debug.Log("Sample Action State was entered!");
            // do stuff, or change back to a different state.
            // since this is a partial class we have access to the fields of the main Controller class
            _stateMachine.ChangeState(SampleIntroState);
        }
    }
}

