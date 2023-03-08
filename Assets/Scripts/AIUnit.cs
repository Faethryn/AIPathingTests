using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIUnit : MonoBehaviour
{

    public Transform _destination;

    [SerializeField]
    private Vector3 _ReachableDestination;

    [SerializeField]
    private float _range = 10f;
    [SerializeField]
    private float _reachedRange = 0.2f;

    private Vector3 _formationOffset;


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
            RecalculateReachableDestination();

            if ( ( Vector3.Distance(this.transform.position, _ReachableDestination) <= _reachedRange ))
            {
                _state = UnitState.Idle;
                this.GetComponent<NavMeshAgent>().enabled = false;
                this.GetComponent<NavMeshObstacle>().enabled = true;
               


            }
        }

        if(_state == UnitState.InFormation)
        {

            RecalculateCirclePosition(_formationOffset);
            


             
        }
        
       

    }


    public void SetFollowLocation(Transform newDestination)
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

   
   

    private void RecalculateReachableDestination()
    {
        NavMeshHit hit;

        if(NavMesh.SamplePosition(_destination.position, out hit, _range, NavMesh.AllAreas))
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

    public void SetFormationDestination(Transform newDestination, Vector3 offset)
    {
        _state = UnitState.InFormation;
        _destination = newDestination;
        _formationOffset = offset;
        RecalculateCirclePosition(offset);


    }

    private void RecalculateCirclePosition(Vector3 offset)
    {


        NavMeshHit hit;

        if (NavMesh.SamplePosition(_destination.position + offset, out hit, _range, NavMesh.AllAreas))
        {
            _ReachableDestination = hit.position;
            this.GetComponent<NavMeshAgent>().enabled = true;
            this.GetComponent<NavMeshObstacle>().enabled = false;

            this.GetComponent<NavMeshAgent>().SetDestination(_ReachableDestination);

        }
    }

}
