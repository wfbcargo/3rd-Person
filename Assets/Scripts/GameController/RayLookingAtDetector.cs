using Assets.Scripts.GameController.LookDetectors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLookingAtDetector : MonoBehaviour
{
    public float maxDistance = 100f;

    private GameObject lastLookedAtObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            var gameobject = hit.collider.gameObject;

            if (gameObject != lastLookedAtObject)
            {
                var lookedAtComponent = gameobject.GetComponent<LookedAtHandler>();
                if (lookedAtComponent != null)
                {
                    lookedAtComponent.OnLookAt();
                }
                lastLookedAtObject = gameobject;
            }
        }
        else
        {
            if(lastLookedAtObject != null)
            {
                var lookedAtComponent = lastLookedAtObject.GetComponent<LookedAtHandler>();
                if (lookedAtComponent != null)
                {
                    lookedAtComponent.OnLookAway();
                }
                lastLookedAtObject = null;
            }
        }
    }
}
