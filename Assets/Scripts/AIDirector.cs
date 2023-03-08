using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDirector : MonoBehaviour
{

    [SerializeField]
    private List<AIUnit> _unitList = new List<AIUnit>();

    [SerializeField]
    private Transform _cursorTransform;

    [SerializeField]
    private float _rangeIncrementPerRow = 0.7f;

    [SerializeField]
    private float _lengthOfOneSide = 2;

    enum FormationState
    {
        Waiting,
        SimpleFollow,
        Circle,
        Rectangle,
        Cone
    }

    private FormationState _state;


    // Start is called before the first frame update
    void Start()
    {
        _state = FormationState.Waiting;
        _unitList.AddRange(FindObjectsOfType<AIUnit>());
       

    }

    private void CalculateRanges()
    {
        int currentAmountPerRow = 1;
        int currentRowIndex = 0;
        int currentRow = 0;
        float currentRange = _rangeIncrementPerRow;
        for(int i = 0; i < _unitList.Count; i++)
        {
            if(currentRowIndex == currentAmountPerRow)
            {
                currentRow += 1;
                currentRange += _rangeIncrementPerRow;
                currentRowIndex = 0;
                currentAmountPerRow *= 2;
            }
            else
            {
                currentRowIndex += 1;
                _unitList[i].SetRange(currentRange);
            }

            Debug.Log(currentRange);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        StateChecks();
        StateUpdates();
        CoroutineRotation();

    }

    private void StateChecks()
    {
        if (Input.GetMouseButtonDown(0))
        {

            StartFollow();
        }

        if (Input.GetMouseButtonDown(1))
        {

            _state = FormationState.Waiting;
            SetWaiting();
            StopCoroutines();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _state = FormationState.SimpleFollow;

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _state = FormationState.Circle;
            StopCoroutines();

            SetCircleLocation();

        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            _state = FormationState.Cone;

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _state = FormationState.Rectangle;

        }
    }


    private void StateUpdates()
    {
        if(_state == FormationState.Rectangle)
        {

        }

        if (_state == FormationState.Circle)
        {
           
        }

        if (_state == FormationState.Cone)
        {

        }

        if (_state == FormationState.SimpleFollow)
        {
            UpdateFollowLocation();
        }

        

       
    }

    private void StartFollow()
    {
        
        for (int currentID = 0; currentID < _unitList.Count; currentID++)
        {
            _unitList[currentID].SetFollowLocation(_cursorTransform);
        }
    }

    private void UpdateFollowLocation()
    {
        for (int currentID = 0; currentID < _unitList.Count; currentID++)
        {
            _unitList[currentID].SetFollowLocation(_cursorTransform);
        }
    }

    private void SetCircleLocation()
    {
        float radius = (_lengthOfOneSide) / (2f * (Mathf.Sin((2f  )/ _unitList.Count)));

        float radianIncrementPerPoint = ((2f * Mathf.PI) / _unitList.Count);
       

        for(int i = 0; i < _unitList.Count; i++)
        {

            if (runningCoroutine == null)
            {
                runningCoroutine = CalculateCirclePosition(i, radius, radianIncrementPerPoint);
                StartCoroutine(runningCoroutine);
            }
            else
                _calculationQueue.Enqueue(CalculateCirclePosition(i, radius, radianIncrementPerPoint));

           
        }


    }

    private void SetWaiting()
    {
        for (int i = 0; i < _unitList.Count; i++)
        {


            _unitList[i].StopFormation();

        }
    }

    private void SetRectangleLocation(bool xAxis)
    {

    }

    private void SetConeLocation()
    {

    }

    IEnumerator CalculateCirclePosition(int index, float radius, float radianIncrementPerPoint)
    {

        float xOffset = radius * Mathf.Cos(index * radianIncrementPerPoint);
        float yOffset = radius * Mathf.Sin(index * radianIncrementPerPoint);

        //Debug.Log(xOffset);
        //Debug.Log(yOffset);

        Vector3 newPosition = (new Vector3(xOffset, 0, yOffset));
        // Debug.Log(newPosition);

        
          
            

        _unitList[index].SetFormationDestination(_cursorTransform, newPosition);

        runningCoroutine = null;
        if (_calculationQueue.Count > 0)
        {
            runningCoroutine = _calculationQueue.Dequeue();
            StartCoroutine(runningCoroutine);
        }
     

        yield return null;
    }


    IEnumerator runningCoroutine = null;
    private Queue<IEnumerator> _calculationQueue = new Queue<IEnumerator>();

    

    private void CoroutineRotation()
    {
      
    }

    private void StopCoroutines()
    {
        _calculationQueue.Clear();
        if (runningCoroutine != null)
            StopCoroutine(runningCoroutine);
    }
}
