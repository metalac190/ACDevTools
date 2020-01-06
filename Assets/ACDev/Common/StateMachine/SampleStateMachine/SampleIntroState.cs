using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an example of how to add a State to our StateMachine system.
/// Create a partial class of our Controller that holds our StateMachine. Name this script "IntroState.cs" or whatever.
///     Note that the script name is for the state, but the class name is still a partial class for the controller.
/// Create a new State that subscribes to OnEnter and OnExit functions.
/// Do stuff OnEnter or OnExit.
/// </summary>
namespace ACDev.Samples
{
    public partial class SampleGameController : MonoBehaviour
    {
        State SampleIntroState
        {
            get
            {
                if(_sampleIntroState == null)
                {
                    // note: you can pass 'null' into the Enter/Exit fields if you don't want to use them
                    _sampleIntroState = new State(OnEnterSampleIntroState, OnExitSampleIntroState, "SampleIntro");
                }
                return _sampleIntroState;
            }
        }
        State _sampleIntroState;

        void OnEnterSampleIntroState()
        {
            Debug.Log("Entered Sample Intro State!");
        }

        void OnExitSampleIntroState()
        {
            Debug.Log("Exit Sample Intro State!");
        }
    }
}

