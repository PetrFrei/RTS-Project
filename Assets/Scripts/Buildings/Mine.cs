using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mine : Building
{
    private float Gold = 1000;
    private bool isDepleted;
    public Text textDisplayTemp;
    private float mineTime = 3f;
    private GameObject worker;
    private bool Mining;
    private Queue<GameObject> workerQueue;

    private void Start()
    {
        workerQueue = new Queue<GameObject>();
    }

    public int GetGold()
    {

        if (!isDepleted)
        {
            Gold -= 10;
            if(Gold<=0)
            {
                isDepleted = true;
            }
            return 10;
        }
        else
        {
            return 3;
        }
    }

    public void MineGold()
    {
        mineTime = mineTime - 1 * Time.deltaTime;
        if (mineTime <= 0)
        {
            mineTime = 3f;
            if(workerQueue.Count==0)
            {
                Mining = false;
            }
            GameObject worker = workerQueue.Dequeue();
            worker.SetActive(true);
        }
    }

    public override void Update()
    {
        textDisplayTemp.text = Gold.ToString();
        if(Mining)
        {
            MineGold();
        }
        base.Update();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Worker") && other.gameObject.GetComponent<Worker>().Mining && other.gameObject.GetComponent<Worker>().gold==0)
        {
            worker = other.gameObject;
            worker.SetActive(false);
            workerQueue.Enqueue(worker);
            Mining = true;
        }
    }



}
