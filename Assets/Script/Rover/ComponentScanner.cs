using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentScanner : MonoBehaviour
{

    public GameObject Player;
    GameObject target;

    public float speed;

    public GameObject Scanner;

    public float ScanTimer;
    public float ScanDuration;
    float scannerTimer;
    float scanningTimer;
    bool Scanning;

    // Start is called before the first frame update
    void Start()
    {
        Scan();

    }

    // Update is called once per frame
    void Update()
    {
        if(Scanning){
            Scanner.transform.GetChild(0).gameObject.SetActive(true);
        }else{
            Scanner.transform.GetChild(0).gameObject.SetActive(false);
        }
        
        transform.position = Player.transform.position;

        if (scannerTimer <= ScanTimer)
        {
            scannerTimer += Time.deltaTime;
        }
        else
        {
            Scanning = true;
            Scan();
        }


        if (Scanning)
        {
            if(scanningTimer <= ScanDuration){
                scanningTimer += Time.deltaTime;
            }else{
                Scanning = false;
                scannerTimer = 0;
                scanningTimer = 0;
            }
            Vector3 targetDirection = target.transform.position - transform.position;
            float singleStep = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            Scanner.transform.rotation = Quaternion.LookRotation(newDirection);
        }


    }


    public void Scan()
    {

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Component"))
        {
            if (target == null)
            {
                target = g;
            }
            if (Vector3.Distance(transform.position, g.transform.position) < Vector3.Distance(transform.position, target.transform.position))
            {
                target = g;
            }
        }
    }

}
