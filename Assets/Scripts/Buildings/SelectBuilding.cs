using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuilding : MonoBehaviour
{
    RaycastHit rayh;
    public GameObject selectWindow;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out rayh))
            {
                if(rayh.collider.tag == "Building")
                {
                    selectWindow.SetActive(true);
                }
            }
        }
    }
}
