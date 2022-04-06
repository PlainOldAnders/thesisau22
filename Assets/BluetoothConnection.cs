
using UnityEngine;
using System; 
using System.Collections;
using System.IO.Ports;

public class BluetoothConnection : MonoBehaviour {
 
    string[] ports = SerialPort.GetPortNames();
 
    void Start()
    {
        Debug.Log("The following serial ports were found:");
 
        foreach(string port in ports)
        {
            Debug.Log(port); 
        }
    }
}
