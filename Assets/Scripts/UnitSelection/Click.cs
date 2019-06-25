using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField]
    private LayerMask Selectables;
    [SerializeField]
    private LayerMask Ground;

    private List<GameObject> selectedObjects;

    private List<GameObject> selectedMoveObjects;

    private List<UnitCollection> collection;

    private List<UnitCollection> deleteObjects;

    private List<Vector3> points;

    private int index = -1;

    [HideInInspector]
    public List<GameObject> selectableObjects;

    private Vector3 mousePos1;
    private Vector3 mousePos2;
    private bool move;
    private RaycastHit rayh;
    private float elapsedTime;
    private Rigidbody rb;
    public float speed = 20f;

    private void Awake()
    {
        selectedObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
        selectedMoveObjects = new List<GameObject>();
        collection = new List<UnitCollection>();
        points = new List<Vector3>();
        deleteObjects = new List<UnitCollection>();
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            Move();
        }
        if(Input.GetMouseButtonDown(0))
        {
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);


            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,Mathf.Infinity,Selectables))
            {
                ClickOn click = hit.collider.GetComponent<ClickOn>();

                if (Input.GetKey("left ctrl"))
                {
                    if (!click.currentlySelected)
                    {
                        selectedObjects.Add(hit.collider.gameObject);
                        click.currentlySelected = true;
                        click.OnClick();
                    }
                    else
                    {
                        selectedObjects.Remove(hit.collider.gameObject);
                        click.currentlySelected = false;
                        click.OnClick();
                    }
                }
                else
                {
                    ClearSelection();
                    selectedObjects.Add(hit.collider.gameObject);
                    click.currentlySelected = true;
                    click.OnClick();
                    Debug.Log(selectedObjects[0]);
                }
            }
            else
            {
                ClearSelection();
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            if(selectedObjects.Count>0)
            {
                MoveObjects();
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if(mousePos1!=mousePos2)
            {
                SelectObjects();
                rayh.point = Vector3.zero;
            }
        }
    }

    void MoveObjects()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mousePos,out rayh,Mathf.Infinity,Ground))
            {
                foreach (GameObject obj in selectedObjects)
                {
                    obj.GetComponent<OperatingUnit>().isOnPlace = false;
                }
                move = true;
                elapsedTime = 0f;
            }
            
        }
    }

    void Move()
    {
        foreach (GameObject obj in selectedMoveObjects)
        {
            OperatingUnit unit = obj.GetComponent<OperatingUnit>();
            rb = obj.GetComponent<Rigidbody>();
            elapsedTime += Time.deltaTime;

            float velocity = (obj.transform.position - unit.lastPos).magnitude / elapsedTime;
            float distance = Vector3.Distance(obj.transform.position, rayh.point);
            unit.lastPos = obj.transform.position;
            if (!unit.isOnPlace)
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, rayh.point, Time.deltaTime);
            }

            if (velocity * 1000 <= 0.25 && elapsedTime>5f || distance<1f)
            {
                unit.isOnPlace = true;
            }
        }
        foreach (UnitCollection coll in collection)
        {
            if (coll.units.Count > 0)
            {
                bool areOnPlace = true;
                int i = collection.IndexOf(coll);
                foreach (GameObject objct in coll.units)
                {
                    OperatingUnit unit = objct.GetComponent<OperatingUnit>();
                    rb = objct.GetComponent<Rigidbody>();
                    elapsedTime += Time.deltaTime;
                    float distance = Vector3.Distance(objct.transform.position, points[i]);
                    unit.lastPos = objct.transform.position;
                    if (!unit.isOnPlace)
                    {
                        objct.transform.position = Vector3.MoveTowards(objct.transform.position, points[i], Time.deltaTime);
                    }

                    if (distance < 1f)
                    {
                        unit.isOnPlace = true;
                    }
                    if(!unit.isOnPlace)
                    {
                        areOnPlace = false;
                    }

                }
                if(areOnPlace)
                {
                    deleteObjects.Add(coll);
                }

            }
        }
        foreach(UnitCollection col in deleteObjects)
        {
            collection.Remove(col);
            index--;
        }
        deleteObjects.Clear();
    }

    void SelectObjects()
    {
        List<GameObject> remObjects = new List<GameObject>();

        if(!Input.GetKey("left ctrl"))
        {
            ClearSelection();

        }

        Rect selectRect = new Rect(mousePos1.x,mousePos1.y,mousePos2.x-mousePos1.x,mousePos2.y-mousePos1.y);
        foreach(GameObject selectObj in selectableObjects)
        {
            if(selectObj!=null)
            {
                if(selectRect.Contains(Camera.main.WorldToViewportPoint(selectObj.transform.position),true))
                {
                    if(!selectedObjects.Contains(selectObj))
                    {
                        selectedObjects.Add(selectObj);
                        selectObj.GetComponent<ClickOn>().currentlySelected = true;
                        selectObj.GetComponent<ClickOn>().OnClick();
                    } 
                    if(!selectedMoveObjects.Contains(selectObj))
                    {
                        selectedMoveObjects.Add(selectObj);
                    }
                }
            }
            else
            {
                remObjects.Add(selectObj);
            }

        }
        
        if (remObjects.Count>0)
        {
            foreach(GameObject rem in remObjects)
            {
                selectableObjects.Remove(rem);
            }

            remObjects.Clear();
        }
        foreach (UnitCollection col in collection)
        {
            foreach (GameObject obj in col.units)
            {
                foreach (GameObject checkObj in selectedMoveObjects)
                {
                    if (obj == checkObj)
                    {
                        remObjects.Add(checkObj);
                    }
                }
            }
        }
        foreach(UnitCollection collect in collection)
        {
            foreach (GameObject remO in remObjects)
            {
                collect.units.Remove(remO);
            }
        }
        
    }


    private void ClearSelection()
    {
        bool canAdd = false;
        if (selectedObjects.Count > 0)
        {
            foreach (GameObject obj in selectedObjects)
            {
                obj.GetComponent<ClickOn>().currentlySelected = false;
                obj.GetComponent<ClickOn>().OnClick();
            }
            selectedObjects.Clear();
            foreach(GameObject obj in selectedMoveObjects)
            {
                OperatingUnit unit = obj.GetComponent<OperatingUnit>();
                if (!unit.isOnPlace)
                {
                    collection.Add(new UnitCollection());
                    canAdd = true;
                    index++;
                    break;
                }
            }

            foreach(GameObject addObj in selectedMoveObjects)
            {
                if(canAdd)
                {
                    collection[index].units.Add(addObj);
                }
            }
            points.Add(rayh.point);
            selectedMoveObjects.Clear();
        }
    }
}
