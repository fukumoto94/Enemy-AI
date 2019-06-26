using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Drone : MonoBehaviour
{
    [SerializeField] private Team _team;
    [SerializeField] private GameObject _laserVisual;

    public Transform Target { get; private set; }

    public Team Team => _team;
    public StateMachine StateMachine => GetComponent<StateMachine>();

    private void Awake() 
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(WanderState), new WanderState(drone: this) },
            { typeof(ChaseState), new ChaseState(drone:this) },
            { typeof(AttackState), new AttackState(drone:this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(Transform target){
        Target = target;
    }

    public void FireWeapon()
    {
        _laserVisual.transform.position = (Target.position + transform.position) / 2f;

        float distance = Vector3.Distance(a: Target.position, b: transform.position);
        _laserVisual.transform.localScale = new Vector3(x: 0.1f, y: 0.1f, z: distance);
        _laserVisual.SetActive(true);

        StartCoroutine(TurnOffLaser());
    }

    private IEnumerator TurnOffLaser()
    {
        yield return new WaitForSeconds(seconds: 0.25f);
        _laserVisual.SetActive(value: false);

        if(Target != null){
            GameObject.Destroy(Target.gameObject);
        }
    }
}

public enum Team
{
    Red,
    Blue,
}
