using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : BindableMonoBehavior
{
    public bool freeze;
    
    [BindComponent()]
    private Rigidbody _rigidbody;

    [BindComponent()]
    Animator anim;    
    public float rotateSpeed;
    public float moveSpeed;

    public FloatVariable windSpeed;

    public StringVariable actionText;
    private bool _displayedText;
    private bool repairing;
    private float repairStarted;
    public GameObject sparksPrefab;
    public Transform sparkOffset;
    private GameObject _sparks;
    public FloatVariable repairBar;
    public GameObject repairBarUI;
    public bool completedRepair;
    public float idleTimeout;
    private float timeComplete;

    public Vector3 globalGravity;

    private float lastMove;
    private static readonly int IdleLook = Animator.StringToHash("IdleLook");

    public bool IsFlipped
    {
        get
        {
            var dot = Vector3.Dot(transform.up, Vector3.down);

            return dot > -0.4f;
        }
    }

    private void Start()
    {
        anim.SetTrigger(IdleLook);

        Physics.gravity = globalGravity;
    }

    void Update()
    {
        if (freeze)
            return;
        
        if (Time.time - lastMove >= idleTimeout)
        {
            lastMove = Time.time + 10f;
            anim.SetTrigger(IdleLook);
        }
    }

    void FixedUpdate()
    {
        if (freeze)
            return;
        
        if (!IsFlipped)
        {
            if (_displayedText)
            {
                actionText.Value = "";
                _displayedText = false;
            }
            
            var rotate = Input.GetAxis("Horizontal");
            var moveForward = Input.GetAxis("Vertical");

            var currentMoveSpeed = moveSpeed;
            if (windSpeed.Value >= 15f)
            {
                currentMoveSpeed /= 1.2f;
            }

            _rigidbody.MovePosition(_rigidbody.position + (transform.forward * currentMoveSpeed * moveForward));

            if (Math.Abs(moveForward) < 1f)
            {
                transform.Rotate(Vector3.down * rotateSpeed * rotate * Time.deltaTime);
            }
            anim.SetFloat("Turn", rotate);

            if (!(Math.Abs(moveForward) < 0.01f) || !(Math.Abs(rotate) < 0.01f))
            {
                lastMove = Time.time;
            }
        }
        else
        {
            actionText.Value = "Hold E to flip yourself over";
            _displayedText = true;

            if (Input.GetKey(KeyCode.E))
            {
                actionText.Value = "Repairing..";
                repairBarUI.SetActive(true);
                if (!repairing)
                {
                    repairStarted = Time.time;
                    repairing = true;
                    _sparks = Instantiate(sparksPrefab, sparkOffset);
                }
                else
                {
                    float repairTime = 9f;
                    float end = repairStarted + repairTime;
                    float duration = repairTime - (end - Time.time);

                    float percentage = duration / repairTime;

                    if (percentage >= 1f)
                    {
                        transform.rotation = Quaternion.Euler(transform.up);

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
        }
    }
}
