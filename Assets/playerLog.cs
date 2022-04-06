 using UnityEngine;
 using System.Collections.Generic;
 using System.Text;
 using UnityEngine.UI; 


public class playerLog : MonoBehaviour
{

    public Canvas canvas;  
    public Text debugText; 
    // Adjust via the Inspector
    public int maxLines = 8;
    private Queue<string> queue = new Queue<string>();
    private string currentText = "";



    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }
    void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Delete oldest message
        if (queue.Count >= maxLines) queue.Dequeue();

        queue.Enqueue(logString);

        var builder = new StringBuilder();
        foreach (string st in queue)
        {
            builder.AppendLine(st); 
        }

        currentText = builder.ToString();

        debugText.text = currentText; 
        
    }

    
}