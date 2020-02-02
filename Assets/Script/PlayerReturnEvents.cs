using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerReturnEvents : BindableMonoBehavior
{
    [BindComponent()]
    private PlayerControl controller;

    [BindComponent()]
    private PlayerHealth health;

    public GameObject winScreen;
    public GameObject loseScreen;
    public Text loseReason;
    public HomeBase HomeBase;
    
    public List<Status> Statuses;

    private void Start()
    {
        StatusByName("Transmission").onReturn.AddListener(OnTransmissionReturned);
        StatusByName("Signal to Home").onReturn.AddListener(OnHomeSignalReturned);
        StatusByName("Hard Drive").onReturn.AddListener(OnHardDriveReturned);
        StatusByName("Wires").onReturn.AddListener(OnWiresReturned);
        StatusByName("Generator").onReturn.AddListener(OnGeneratorReturned);
        StatusByName("Antenna").onReturn.AddListener(OnAntennaReturned);
        
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void Update()
    {
        if (HomeBase.BaseHealth <= 0)
        {
            loseScreen.SetActive(true);
            controller.freeze = true;
            loseReason.text = "Base Destroyed";
        }

        if (health.Health <= 0)
        {
            loseScreen.SetActive(true);
            controller.freeze = true;
            loseReason.text = "You Died";
        }
    }

    private Status StatusByName(string name)
    {
        foreach (var s in Statuses)
        {
            if (s.Name.ToLower() == name.ToLower())
            {
                if (s.onReturn == null)
                    s.onReturn = new StatusEvent();
                s.IsDisconnected = true;
                
                return s;
            }
        }

        return null;
    }

    public void OnTransmissionReturned(Status obj)
    {
        Debug.Log("Finished Repair For " + obj.Name);
        Debug.Log("Running inside " + gameObject.name);

        obj.IsDisconnected = false;
        StartCoroutine(ConnectTransmission(obj));
        CheckForWin();
    }

    public void OnAntennaReturned(Status obj)
    {
        Debug.Log("Returned Antenna");
        obj.IsDisconnected = false;
        StartCoroutine(ConnectAntenna(obj));
        CheckForWin();
    }

    public void OnWiresReturned(Status obj)
    {
        Debug.Log("Returned Wires");
        obj.IsDisconnected = false;
        StartCoroutine(ConnectWires(obj));
        CheckForWin();
    }

    public void OnGeneratorReturned(Status obj)
    {
        Debug.Log("Returned Generator");
        obj.IsDisconnected = false;
        StartCoroutine(ConnectGenerator(obj));
        CheckForWin();
    }

    public void OnHomeSignalReturned(Status obj)
    {
        Debug.Log("Returned HomeSignal");
        obj.IsDisconnected = false;
        StartCoroutine(ConnectHomeSignal(obj));
        CheckForWin();
    }

    public void OnHardDriveReturned(Status obj)
    {
        Debug.Log("Returned Hard drive");
        obj.IsDisconnected = false;
        StartCoroutine(ConnectHardDrive(obj));
    }

    private IEnumerator ConnectTransmission(Status obj)
    {
        yield return new WaitForSeconds(2);
        var variable = (StringVariable) obj.Value;
        variable.Value = "Connecting...";
        yield return new WaitForSeconds(4);
        variable.Value = "Connected";
    }
    
    private IEnumerator ConnectHardDrive(Status obj)
    {
        yield return new WaitForSeconds(2);
        var variable = (StringVariable) obj.Value;
        variable.Value = "Booting...";
        yield return new WaitForSeconds(4);
        variable.Value = "Functional";
    }
    
    private IEnumerator ConnectGenerator(Status obj)
    {
        yield return new WaitForSeconds(2);
        var variable = (StringVariable) obj.Value;
        variable.Value = "Checking...";
        yield return new WaitForSeconds(4);
        variable.Value = "Powered Up";
    }
    
    private IEnumerator ConnectHomeSignal(Status obj)
    {
        yield return new WaitForSeconds(2);
        var variable = (StringVariable) obj.Value;
        variable.Value = "Connecting...";
        yield return new WaitForSeconds(4);
        variable.Value = "Connected";
    }
    
    private IEnumerator ConnectWires(Status obj)
    {
        yield return new WaitForSeconds(2);
        var variable = (StringVariable) obj.Value;
        variable.Value = "Checking...";
        yield return new WaitForSeconds(4);
        variable.Value = "Functional";
    }
    
    private IEnumerator ConnectAntenna(Status obj)
    {
        yield return new WaitForSeconds(2);
        var variable = (StringVariable) obj.Value;
        variable.Value = "Connecting...";
        yield return new WaitForSeconds(4);
        variable.Value = "Online";
    }

    private void CheckForWin()
    {
        foreach (Status s in Statuses)
        {
            if (s.IsDisconnected)
                return;
        }
        
        //TODO Show Win
        controller.freeze = true;
        winScreen.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
