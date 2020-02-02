using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBaseSystemStatus : MonoBehaviour
{
    public List<Status> statuses = new List<Status>();
    public GameObject statusHUDPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var s in statuses)
        {
            var obj = Instantiate(statusHUDPrefab, transform);
            obj.GetComponent<HomeBaseStatusText>().status = s;
        }
    }
}
