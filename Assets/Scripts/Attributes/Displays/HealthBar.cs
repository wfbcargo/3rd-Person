using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HealthBar : MonoBehaviour
{

    private Canvas canvas;
    private Slider slider;

    private Health health;
    // Start is called before the first frame update
    void Start()
    {
        canvas = Canvas.Instantiate(Resources.Load<Canvas>("Displays/HealthBarCanvas"), this.transform);
        canvas.transform.localPosition = new Vector3(0, 1.5f, 0);
        slider = canvas.GetComponentInChildren<Slider>();

        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health.CurrentHealth / health.MaxHealth;
    }
}
