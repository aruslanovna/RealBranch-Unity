using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;

public class Sensors : MonoBehaviour
{
    public GameObject TextTemp;
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

     print("1 read");
                    SensorsPanel.gameObject.SetActive(false);
               
                }
               if (sp.ReadByte() == 2)
                {
                    print("2 read");
                    SensorsPanel.gameObject.SetActive(true);
                   
                }
                //else 
                //{
                //    TextTemp.GetComponent<UnityEngine.UI.Text>().text = " ";
                //    TextTemp.GetComponent<UnityEngine.UI.Text>().text = sp.ReadByte().ToString();
                //    print(sp.ReadByte().ToString());
                //}
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


