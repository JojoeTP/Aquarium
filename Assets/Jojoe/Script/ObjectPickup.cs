using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ObjectPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag ("Player"))
        {
            Debug.Log("Picked");
            string objectName = this.name;
            // Debug.Log(this.transform.position);

            //Dictionary -- 
            // AnalyticsResult analyticsResult = Analytics.CustomEvent("PickupDictionary" + 
            //     new Dictionary<string, object>{
            //         {"count",1},
            //         {"position",other.transform.position}
            //     });


            AnalyticsResult analyticsResult = Analytics.CustomEvent("PickupCount" + 1);

            AnalyticsResult analyticsString = Analytics.CustomEvent("TestPickupString : " + objectName);

            AnalyticsResult analyticsFloat = Analytics.CustomEvent("TestPickupFloat : " + 1.01);

            AnalyticsResult analyticsPosition = Analytics.CustomEvent("TestPickupPosition : " + this.transform.position);
            // Debug.Log(objectName);
            // Debug.Log(this.transform.position);
            Debug.Log("analyticsResult: " + analyticsResult);
            Debug.Log("analyticsResult: " + analyticsString);
            Debug.Log("analyticsResult: " + analyticsFloat);
            Debug.Log("analyticsResult: " + analyticsPosition);
            Destroy(this.gameObject);
        }
        
    }
}
