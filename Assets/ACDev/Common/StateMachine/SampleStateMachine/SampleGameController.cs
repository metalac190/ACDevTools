using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an example of how to implement a StateMachine inside of a Game Controller.
/// First, we need to create our Controller as a partial class and attach it to a GameObject
/// Next, we need to instantiate and keep track of our State Machine.
/// Then we will add new states as Partial classes to add to this class, but in more bite-sized chunks of code.
/// We can also add other controllers to this class so that the states can access them.
/// </summary>
namespace ACDev.Samples
{
    public partial class SampleGameController : MonoBehaviour
    {
        StateMachine _stateMachine = new StateMachine();

        // put other public controller references here so that the states can access them

        void Start()
        {
            _stateMachine.ChangeState(SampleIntroState);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _stateMachine.ChangeState(SampleActionState);
            }
        }
    }
}

