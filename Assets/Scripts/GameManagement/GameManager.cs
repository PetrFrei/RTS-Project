using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gold = 100;
    public float wood = 100;
    public float meat = 20;
    public float maxMeat = 30;

    public static GameManager instance;

    private void Awake()
    {
        if(instance!=null && instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
