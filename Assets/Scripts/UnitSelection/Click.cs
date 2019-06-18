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

                if(Input.GetKey("left ctrl"))
                {
                    if(!click.currentlySelected)
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
                }
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            MoveObjects();
        }
        if(Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if(mousePos1!=mousePos2)
            {
                SelectObjects();
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
        if(remObjects.Count>0)
        {
            foreach(GameObject rem in remObjects)
            {
                selectableObjects.Remove(rem);
            }

            remObjects.Clear();
        }
    }


    void ClearSelection()
    {
        if (selectedObjects.Count > 0)
        {
            foreach (GameObject obj in selectedObjects)
            {
                obj.GetComponent<ClickOn>().currentlySelected = false;
                obj.GetComponent<ClickOn>().OnClick();
            }
            selectedObjects.Clear();
        }
    }
}
