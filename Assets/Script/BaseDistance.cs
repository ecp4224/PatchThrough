using UnityEngine;
using UnityEngine.UI;

public class BaseDistance : MonoBehaviour
{
    [SerializeField]
    private Transform homebase;
    [SerializeField]
    private Text distanceText;
    private float distance;

    public FloatVariable windSpeed;

    void Update()
    {
        distance = (homebase.transform.position - transform.position).magnitude;
        distanceText.text = "Distance from Base: " + distance.ToString("F1") + " meters\nWind Speed: " + windSpeed.Value.ToString("F1") + " mph";
    }
}
