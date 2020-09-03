using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFSM : MonoBehaviour
{
    public Dictionary<EnemyStates, Action> dic;

    public enum EnemyStates
    {
        Idle,
        Walk
    }

    public EnemyStates EnemyState;

    // Start is called before the first frame update
    void Start()
    {
        dic = new Dictionary<EnemyStates, Action>
        {
            {EnemyStates.Idle, HandleIdle }
        };

        EnemyState = EnemyStates.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        dic[EnemyState].Invoke();
    }

    private void HandleIdle()
    {
        // Idle code here
        Debug.Log("Currently in the idle state.");
    }
}
