using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainUnit : Process
{
    public float goldCost = 10;
    public float woodCost = 20;
    public float meatCost = 1;
    public GameObject prefab;
    private int number = 0;

    private GameManager manag;
    private void Start()
    {
        timeToProcess = 2f;
        manag = GameManager.instance;
    }
    public void AddToQueue()
    {
        if(manag.gold>goldCost && manag.wood>woodCost && manag.meat<manag.maxMeat)
        {
            manag.gold -= goldCost;
            manag.wood -= woodCost;
            manag.meat++;   
            quantity++;
        }
    }

    public override void Result()
    {
        timeToProcess = 2f;
        GameObject unit = Instantiate(prefab);
        unit.transform.position = transform.position;
        unit.name = "Peasant " + number;
        number++;
    }
}
