using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float gold = 100;
    public float wood = 100;
    public float meat = 20;
    public float maxMeat = 30;

    public static GameManager instance;

    public Text goldDisplay,woodDisplay,meatDisplay;

    private void Awake()
    {
        if(instance!=null && instance!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        goldDisplay.text = gold.ToString();
        woodDisplay.text = wood.ToString();
        meatDisplay.text = meat.ToString();
    }
}
