using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuilding : MonoBehaviour
{
    public List<GameObject> prefab = new List<GameObject>();

    private GameObject currentPrefab;

    private Vector3 mOffset;

    private void Start()
    {

    }

    public void SelectBuilding(int pos)
    {
        switch(pos)
        {
            case 0:
                currentPrefab = Instantiate(prefab[0]);
                break;
        }
    }

    private void Update()
    {
        if(currentPrefab!=null)
        {
            ObjectOnMouse();
            if(Input.GetMouseButtonDown(0))
            {
                Buildings buildingsList = new Buildings();
                GameObject building = Instantiate(currentPrefab);
                Building build = building.GetComponent<Building>();
                build.build= true;
                buildingsList.buildings.Add(building);
                Destroy(currentPrefab);
            }
        }

    }

    private void ObjectOnMouse()
    {
        Vector3 mousePos = MousePos();
        currentPrefab.transform.position = mousePos;
    }

    private Vector3 MousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }








































    //private Vector3 mOffset;

    //private float mZCoord;


    //private void OnMouseDown()
    //{
    //    mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

    //    mOffset = gameObject.transform.position - GetMouseWorldPos();
    //}

    //private Vector3 GetMouseWorldPos()
    //{
    //    Vector3 mousePoint = Input.mousePosition;

    //    mousePoint.z = mZCoord;

    //    return Camera.main.ScreenToWorldPoint(mousePoint);
    //}

    //private void OnMouseDrag()
    //{
    //    transform.position = GetMouseWorldPos() + mOffset;
    //}


}
