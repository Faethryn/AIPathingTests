using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIUnit : MonoBehaviour
{

    public Vector3 _destination;

    [SerializeField]
    private Vector3 _ReachableDestination;

    [SerializeField]
    private float _range = 10f;
    [SerializeField]
    private float _reachedRange = 0.2f;


    enum UnitState
    {
        Idle,
        Moving,
        InFormation
    }

    private UnitState _state;

    // Start is called before the first frame update
    void Start()
    {
        UnitState _state = UnitState.Idle;
        this.GetComponent<NavMeshAgent>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if( _state == UnitState.Moving )
        {
          if( ( Vector3.Distance(this.transform.position, _ReachableDestination) <= _reachedRange ))
            {
                _state = UnitState.Idle;
                this.GetComponent<NavMeshAgent>().enabled = false;
                this.GetComponent<NavMeshObstacle>().enabled = true;


            }
        }
        

    }


    public void SetFollowLocation(Vector3 newDestination)
    {
        _state = UnitState.Moving;
        _destination = newDestination;
        RecalculateReachableDestination();
    }

    public void StopFormation()
    {
        _state = UnitState.Idle;
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<NavMeshObstacle>().enabled = true;

    }

    public void SetFormationLocation(Vector3 newDestination)
    {
        _state = UnitState.InFormation;
        _destination = newDestination;
        RecalculateReachableDestination();
    }

    private void RecalculateReachableDestination()
    {
        NavMeshHit hit;

        if(NavMesh.SamplePosition(_destination, out hit, _range, NavMesh.AllAreas))
        {
            _ReachableDestination = hit.position;
            this.GetComponent<NavMeshAgent>().enabled = true;
            this.GetComponent<NavMeshObstacle>().enabled = false;

            this.GetComponent<NavMeshAgent>().SetDestination(_ReachableDestination);

        }


    }

    public void SetRange(float newRange)
    {
        _range = newRange;
    }

    public void SetFormationDestination(Vector3 newDestination)
    {
        _state = UnitState.InFormation;
        _destination = newDestination;

        RecalculateReachableDestination();


    }


}
