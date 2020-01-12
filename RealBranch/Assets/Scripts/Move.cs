using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Threading;
using System.Collections;
using System.Globalization;
public class Move : MonoBehaviour
{
 
    SerialPort sp = new SerialPort("COM5", 9600); // set port of your arduino connected to computer
    public GameObject SensorsPanel;
    bool appeare;
    public GameObject TextTemp;
    public GameObject TextHum;
    public GameObject TextLight;
    public GameObject TextSmog;

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
            sp.ReadTimeout = 1;

            try
            {

             
                if (sp.ReadByte() == 1)
                {
                   
                    transform.Translate(Vector3.left * Time.deltaTime * 3);
                }
                if (sp.ReadByte() == 2)
                {
                  
                    transform.Translate(Vector3.right * Time.deltaTime * 3);
                    Thread.Sleep(30 * 1000);
                
                }
                if (sp.ReadByte() == 3)
                {
                    sp.ReadTimeout = 3;
                    try
                    {
                        string value = sp.ReadExisting();
                        string[] vec3 = value.Split(',');

                        string X = vec3[3];
                        float Xint= float.Parse(X, CultureInfo.InvariantCulture.NumberFormat);
                        string Y = vec3[2];
                        string Z = vec3[0];
                        string Q = vec3[1]; 
                        //TextTemp.GetComponent<UnityEngine.UI.Text>().text = X + "*C" + " too cold";
                        if (Xint <= 22.00)
                        {
                            TextTemp.GetComponent<UnityEngine.UI.Text>().text = X + "*C" + " too cold";
                        }
                        else if (Xint >= 42)
                        {
                            TextTemp.GetComponent<UnityEngine.UI.Text>().text = X + "*C" + " too hot";
                        }
                        else
                        {
                            TextTemp.GetComponent<UnityEngine.UI.Text>().text = X + "*C" + " normal";
                        }
                        // TextTemp.GetComponent<UnityEngine.UI.Text>().text = X+ "fine";
                        TextHum.GetComponent<UnityEngine.UI.Text>().text = Y;
                        TextSmog.GetComponent<UnityEngine.UI.Text>().text = Z;
                        TextLight.GetComponent<UnityEngine.UI.Text>().text = Q;
                    }
                    catch (TimeoutException)
                    {
                        print(null);
                    }

                    if (appeare)
                    {
                       
                        SensorsPanel.gameObject.SetActive(false);
                   
                        appeare = false;
                    }
                    else
                    {
                      
                        SensorsPanel.gameObject.SetActive(true);
                       
                        
                        appeare = true;
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
    }

}
