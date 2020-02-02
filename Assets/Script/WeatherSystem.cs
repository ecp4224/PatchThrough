using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeatherSystem : BindableMonoBehavior
{
    [BindComponent()]
    private AudioSource audio;
    private float lastWindChange;
    
    public float windChangeInterval = 20f;
    public FloatVariable windSpeed;

    public float fastWindMin = 15f;
    public AudioSource fastWindSound;
    public AudioSource slowWindSound;
    public float windFadeDuration = 2.3f;

    private AudioSource currentAudio;
    private bool fadeAudio;
    private AudioSource newAudio;
    private float fadeStart;

    public float showerChanceInterval = 10f;

    public FloatVariable showerChance;
    private float lastShowerChance;
    public List<GameObject> Astroids;
    public float showerDuration;
    private float showerStart;
    private bool isShower;
    public float astroidSpawnInterval;
    public int maxSpawn;
    public int minSpawn;
    public Vector3 minSpawnLocation;
    public Vector3 maxSpawnLocation;
    private float lastSpawn;

    public float minScale, maxScale;

    private void Start()
    {
        windSpeed.Value = 8f;
        currentAudio = slowWindSound;
        showerChance.Value = 44;
    }

    void Update()
    {
        if (showerChance.Value < 3f)
        {
            if (!isShower)
            {
                showerStart = Time.time;
                isShower = true;
                SpawnAstroids();
            }

            if (Time.time - lastSpawn >= astroidSpawnInterval)
            {
                SpawnAstroids();
            }

            if (Time.time - showerStart >= showerDuration)
            {
                isShower = false;
                showerChance.Value = Random.Range(44, 64);
            }
        }
        else
        {
            if (Time.time - lastShowerChance >= showerChanceInterval)
            {
                showerChance.Value -= Random.Range(1, 5);
                lastShowerChance = Time.time;
            }
        }
        
        if (fadeAudio)
        {
            float end = fadeStart + windFadeDuration;
            float duration = windFadeDuration - (end - Time.time);
            
            float percentage = duration / windFadeDuration;

            newAudio.volume = percentage;
            currentAudio.volume = 1f - percentage;

            if (percentage >= 1f)
            {
                currentAudio.enabled = false;
                currentAudio = newAudio;
                fadeAudio = false;
            }
        }
        
        if (Time.time - lastWindChange >= windChangeInterval)
        {
            float minWind = Math.Max(windSpeed.Value - 7, 7);
            float maxWind = Math.Min(windSpeed.Value + 7, 22);
            
            windSpeed.Value = Random.Range(minWind, maxWind);

            AudioSource newClip;
            if (windSpeed.Value >= fastWindMin)
                newClip = fastWindSound;
            else
                newClip = slowWindSound;

            if (newClip != currentAudio)
            {
                newAudio = newClip;
                newAudio.enabled = true;
                newAudio.volume = 0f;
                newAudio.Play();
                newAudio.loop = true;

                fadeAudio = true;
                fadeStart = Time.time;
            }

            lastWindChange = Time.time;
        }
    }

    public void SpawnAstroids()
    {
        var playerTransform = GameObject.FindWithTag("Player").transform.position;
        
        int spawnCount = Random.Range(minSpawn, maxSpawn);
        for (var i = 0; i < spawnCount; i++)
        {
            var astroid = Astroids[Random.Range(0, Astroids.Count)];

            var x = Random.Range(playerTransform.x - minSpawnLocation.x, playerTransform.x + maxSpawnLocation.x);
            var y = Random.Range(minSpawnLocation.y, maxSpawnLocation.y);
            var z = Random.Range(playerTransform.z - minSpawnLocation.z, playerTransform.z + maxSpawnLocation.z);

            if (x < 60)
                x = 60;
            if (z < 60)
                z = 60;

            if (x >= 960)
                x = 940;
            if (z >= 960)
                z = 940;

            var obj = Instantiate(astroid, new Vector3(x, y, z), Quaternion.identity);

            var scale = Random.Range(minScale, maxScale);
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }
        
        
        lastSpawn = Time.time;
    }
}
