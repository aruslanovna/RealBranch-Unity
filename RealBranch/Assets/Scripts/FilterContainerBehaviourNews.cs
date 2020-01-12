using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterContainerBehaviourNews : MonoBehaviour
{
    public GameObject CategoryButton;
  

    public GameObject CategoryPanel;
   

    //Opens panel based on button pressed.
    public void ButtonPressed(GameObject Pressed)
    {
        if (Pressed == CategoryButton)
        {
            CategoryPanel.SetActive(true);
        }
       
    }

    //If a panel is open hide the open panel
    //Otherwise close the filter view
    public void BackPressed()
    {
        if (IsPanelsActivated())
        {
            CategoryPanel.SetActive(false);
           
        }
        else
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }

    }

    //Checks if any of the type panels are visible
    public bool IsPanelsActivated()
    {
        return CategoryPanel.activeSelf ;
    }
}
