using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Start is called before the first frame update
using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour
{
 


    public GameObject Plant1;
    public GameObject Plant2;
    public GameObject Plant3;
    public GameObject Plant4;
    public GameObject Plant5;
    public GameObject Plant6;
   
    public int BallSelected;

    // Use this for initialization
    void Start()
    {
        Plant2.SetActive(false);
        Plant3.SetActive(false);
        Plant4.SetActive(false);
        Plant5.SetActive(false);
        Plant6.SetActive(false);
        Plant1.SetActive(true);

    }

    public void LoadPlant1()
    {
        Plant2.SetActive(false);
        Plant3.SetActive(false);
        Plant4.SetActive(false);
        Plant5.SetActive(false);
        Plant6.SetActive(false);
        Plant1.SetActive(true);
    }

    public void LoadPlant2()
    {
        Plant2.SetActive(true);
        Plant3.SetActive(false);
        Plant4.SetActive(false);
        Plant5.SetActive(false);
        Plant6.SetActive(false);
        Plant1.SetActive(false);
    }
    public void LoadPlant3()
    {
        Plant2.SetActive(false);
        Plant3.SetActive(true);
        Plant4.SetActive(false);
        Plant5.SetActive(false);
        Plant6.SetActive(false);
        Plant1.SetActive(false);
    }
    public void LoadPlant4()
    {
        Plant2.SetActive(false);
        Plant3.SetActive(false);
        Plant4.SetActive(true);
        Plant5.SetActive(false);
        Plant6.SetActive(false);
        Plant1.SetActive(false);
    }
    public void LoadPlant5()
    {
        Plant2.SetActive(false);
        Plant3.SetActive(false);
        Plant4.SetActive(false);
        Plant5.SetActive(true);
        Plant6.SetActive(false);
        Plant1.SetActive(false);
    }
    public void LoadPlant6()
    {
        Plant2.SetActive(false);
        Plant3.SetActive(false);
        Plant4.SetActive(false);
        Plant5.SetActive(false);
        Plant6.SetActive(true);
        Plant1.SetActive(false);
    }
   

}

