using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worker : Unit
{
    private RaycastHit rayh;

    public float mineTime = 3f;

    public bool Mining = false;

    private GameObject town;

    private Vector3 minePos;

    private bool inMine,inTown,goingMine = true;

    private Vector3 destination;

    public float distanceFromBuildings;

    private float distance;

    private Mine GoldMine;
    public float gold;
    private ClickOn unit;
    private BuildingPanel panel;


    private void Start()
    {
        destination = transform.position;
        unit = GetComponent<ClickOn>();
        panel = BuildingPanel.instance;
    }
    private void FindMine()
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out rayh))
        {
            if(rayh.collider.tag == "Mine")
            {
                GoldMine = rayh.collider.GetComponent<Mine>();
                town = FindTownHall(rayh.collider.gameObject);
                Bounds b = rayh.collider.GetComponent<MeshFilter>().mesh.bounds;
                minePos = new Vector3(rayh.transform.position.x,b.min.y+transform.position.y,rayh.transform.position.z);
                destination = minePos;
                Mining = true;
            }
        }
    }

    public override void Update()
    {
        if(Input.GetMouseButtonDown(1) &&unit.currentlySelected)
        {
            FindMine();
        }
        if(Mining)
        {
            Trace(town, rayh.collider.gameObject);
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), town.GetComponent<BoxCollider>());
        }
        if(unit.currentlySelected)
        {
            GetButtons();
        }
    }

    private void GetButtons()
    {
        Button[] but = panel.GetButtons();
        but[0].onClick.AddListener();
        but[0].gameObject.SetActive(true);
    }

    private void ResButtons()
    {
        Button[] but = panel.GetButtons();
        but[0].gameObject.SetActive(false);
    }



    private void OnEnable()
    {
        if(Mining)
        {
            gold = GoldMine.GetGold();
            destination = town.transform.position;
            goingMine = false;
        }
    }

    private void Trace(GameObject town, GameObject mine)
    {

        if (goingMine)
        {
            distance = Vector3.Distance(transform.position, minePos);
        }
        else if(!goingMine)
        {
            distance = Vector3.Distance(transform.position, town.transform.position);
        }
        if(transform.position != destination && distance>distanceFromBuildings)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
        }
        else
        {
            if(!goingMine)
            {
                GameManager.instance.gold += gold;
                gold = 0f;
                destination = minePos;
                goingMine = true;
            }
        }
        
    }

    private GameObject FindTownHall(GameObject mine)
    {
        GameObject[] towns = GameObject.FindGameObjectsWithTag("TownHall");
        List<float> distances = new List<float>();
        foreach(GameObject town in towns)
        {
            distances.Add(Vector3.Distance(mine.transform.position, town.transform.position));
        }

        float lowestDistance = Mathf.Infinity;

        foreach(float dist in distances)
        {
            if(lowestDistance>dist)
            {
                lowestDistance = dist;
            }
        }
        return towns[distances.IndexOf(lowestDistance)];
    }
}
