using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;

    [SerializeField]
    private LayerMask _layerMask;

    private void Awake()
    {
      

    }


    // Update is called once per frame
    void Update()
    {
      
      Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
       
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        if(Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            this.gameObject.transform.position = hit.point;
        }

    }
}
