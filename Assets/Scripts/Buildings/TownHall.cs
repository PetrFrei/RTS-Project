using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownHall : Building
{
    private BuildingPanel panel;

    private TrainUnit train;

    private RaycastHit rayh;

    public GameObject selectWindow;

    private void Start()
    {
        panel = BuildingPanel.instance;
        train = GetComponent<TrainUnit>();
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out rayh))
            {
                if (rayh.collider.tag == "TownHall")
                {
                    selectWindow.SetActive(true);
                    GetButtons();
                }
            }
        }
        base.Update();
    }

    private void GetButtons()
    {
        Button[] but = panel.GetButtons();
        but[0].onClick.AddListener(Train);
        but[0].gameObject.SetActive(true);
    }

    private void ResButtons()
    {
        Button[] but = panel.GetButtons();
        but[0].gameObject.SetActive(false);
    }

    public void Train()
    {
        train.AddToQueue();
    }



}
