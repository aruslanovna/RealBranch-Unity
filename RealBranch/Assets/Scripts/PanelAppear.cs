using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class PanelAppear : MonoBehaviour
{

    public GameObject SensorsPanel;
    SerialPort sp = new SerialPort("COM6", 9600); // set port of your arduino connected to computer
    private int Temperature;

    void Start()
    {

        sp.Open();

        sp.ReadTimeout = 1;

    }

    void Update()
    {
        if (sp.IsOpen)
        {

            try
            {


                if (sp.ReadByte() == 1)
                {
                    LoadPanel();
                }
                if (sp.ReadByte() == 2)
                {
                    UnLoadPanel();
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
    public void LoadPanel()
    {
        SensorsPanel.SetActive(true);

    }
    public void UnLoadPanel()
    {
        SensorsPanel.SetActive(false);

    }
}


