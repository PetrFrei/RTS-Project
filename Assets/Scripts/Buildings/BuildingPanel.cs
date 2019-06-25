using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanel : MonoBehaviour
{
    public static BuildingPanel instance;
    public Button but;
    private Button[] buttons;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        buttons = new Button[8];
        CreateButtons();
    }
    public Button[] GetButtons()
    {
        return buttons;
    }

    private void CreateButtons()
    {
        for (int i = 0; i < 8; i++)
        {
            Button button = Instantiate(but);
            RectTransform transform = button.GetComponent<RectTransform>();
            transform.SetParent(gameObject.transform);
            button.name = "Button " + i;
            button.gameObject.SetActive(false);
            buttons[i] = button;
        }
    }

    public void SetButtons(Image[] img)
    {
        Button[] buttons = new Button[8];
        buttons = GetComponentsInChildren<Button>();
        int i = 0;
        foreach(Button but in buttons)
        {
            but.image = img[i];
            i++;
        }
    }


}
