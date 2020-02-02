using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : BindableMonoBehavior
{
    [BindComponent()]
    private AudioSource audio;
    
    private bool landed;

    private void Start()
    {
        audio.Stop();
    }

    private void Update()
    {
        if (transform.position.y < -5)
            StartCoroutine(DelayDestory(1f));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (landed)
            return;
        
        audio.Play();
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().Health -= Random.Range(1, 4);
        } 
        else if (other.gameObject.CompareTag("Base"))
        {
            other.gameObject.GetComponent<HomeBase>().baseHealth.Value -= Random.Range(1, 4);
        }

        StartCoroutine(DelayDestory(Random.Range(4f, 10f)));
    }

    private IEnumerator DelayDestory(float duration)
    {
        landed = true;
        
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
