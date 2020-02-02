using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : BindableMonoBehavior
{
    public GameObject radius_object;
    public GameObject homebase_hud;
    public GameObject player;
    public FloatVariable radius;
    public float radiusOffset;
    public StringVariable actionText;

    public InventoryData playerInventory;

    public IntVariable baseHealth;

    private bool _displayedText;
    private bool repairing;
    private float repairStarted;
    public GameObject sparksPrefab;
    public Transform sparkOffset;
    private GameObject _sparks;
    public FloatVariable repairBar;
    public GameObject repairBarUI;
    public bool completedRepair;
    private float timeComplete;
    public float repairTimeout = 3f;

    private PlayerHealth health;

    public bool IsInRange
    {
        get { return (player.transform.position - transform.position).magnitude <= ((radius.Value + radiusOffset) / 2f); }
    }

    public bool IsPlayerNear
    {
        get { return (player.transform.position - transform.position).magnitude <= (8.5f + radiusOffset); }
    }

    public int BaseHealth
    {
        get { return baseHealth.Value; }
        set { baseHealth.Value = value; }
    }

    private void Start()
    {
        actionText.Value = "";

        health = player.GetComponent<PlayerHealth>();
        
        playerInventory.items.Clear();

        health.Health = 100;
        BaseHealth = 100;
    }

    void Update()
    {
        radius_object.transform.localScale = new Vector3(radius.Value + radiusOffset, radius_object.transform.localScale.y, radius.Value + radiusOffset);

        homebase_hud.SetActive(IsInRange);

        if (IsPlayerNear)
        {
            if ((playerInventory.items.Count > 0 || health.Health < 100 || BaseHealth < 100) && !completedRepair)
            {
                string itemName;
                if (playerInventory.items.Count > 0)
                    itemName = "the " + playerInventory.items[0].fixesStatus.Name;
                else if (health.Health < 100)
                    itemName = "Patches";
                else
                    itemName = "the HomeBase";

                actionText.Value = "Hold E to Repair " + itemName;
                _displayedText = true;

                if (Input.GetKey(KeyCode.E))
                {
                    actionText.Value = "Repairing..";
                    repairBarUI.SetActive(true);
                    if (!repairing)
                    {
                        repairStarted = Time.time;
                        repairing = true;
                        var offset = sparkOffset;
                        if (playerInventory.items.Count == 0 && health.Health < 100)
                            offset = player.transform;
                        _sparks = Instantiate(sparksPrefab, offset);
                    }
                    else
                    {
                        float repairTime;
                        if (playerInventory.items.Count > 0)
                            repairTime = playerInventory.items[0].repairTime;
                        else if (health.Health < 100)
                            repairTime = 10f;
                        else
                            repairTime = 15f;
                        
                        float end = repairStarted + repairTime;
                        float duration = repairTime - (end - Time.time);

                        float percentage = duration / repairTime;

                        if (percentage >= 1f)
                        {
                            if (playerInventory.items.Count > 0)
                            {
                                playerInventory.items[0].fixesStatus.onReturn.Invoke(playerInventory.items[0].fixesStatus);
                                playerInventory.items.RemoveAt(0);
                            }
                            else if (health.Health < 100)
                                health.Health = 100;
                            else
                                BaseHealth = 100;
                            
                            repairBarUI.SetActive(false);
                            actionText.Value = "Repair Complete!";
                            Destroy(_sparks);
                            completedRepair = true;
                            timeComplete = Time.time;
                            repairing = false;
                        }
                        else
                        {
                            repairBar.Value = percentage;
                        }
                    }
                }
                else
                {
                    repairBarUI.SetActive(false);
                    repairing = false;
                    if (_sparks != null)
                        Destroy(_sparks);
                }
            } 
            else if (completedRepair)
            {
                if (Time.time - timeComplete >= repairTimeout)
                {
                    completedRepair = false;
                    actionText.Value = "";
                }
            }
        } 
        else if (_displayedText)
        {
            actionText.Value = "";
            _displayedText = false;
            repairing = false;
            completedRepair = false;
            repairBarUI.SetActive(false);
            
            if (_sparks != null)
                Destroy(_sparks);
        }
    }
}
