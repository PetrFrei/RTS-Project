using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{
    [SerializeField]
    private Material red;
    [SerializeField]
    private Material green;

    private MeshRenderer meshRender;

    [HideInInspector]
    public bool currentlySelected = false;

    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this.gameObject);
        OnClick();
    }

    public void OnClick()
    {
        if(currentlySelected == false)
        {
            meshRender.material = red;
        }
        else
        {
            meshRender.material = green;
        }
    }

}
