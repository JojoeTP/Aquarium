using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDripper : MonoBehaviour
{

    private Renderer rend;
    private float bloodDryAmount = 0.0f; // automatic drying after the yOffset is complete (ofter the drips)

    [Tooltip("How fast does it dry?")]
    public float autoDrySpeed = 0.1f; // the drying speed 
    [Tooltip("How fast does it move?.")]
    public float dripSpeed = 1.0f; // the dripping speed
    [Tooltip("For these values, go to the material and alter the YOffset slider then copy your start and end values in here.")]
    public float yOffsetStartValue = -1.0f; //the initial value of the drip effect, see the material if you want to change these values in the editor
    public float yOffsetEndValue = -0.05f;
    [Tooltip("Smaller amount, longer the fade, object parent destoryed when fully faded.")]
    public float fadeSpeed =0.01f; // rate of fade

    private bool makeBloodDry = false; // internal bool
    private float speedArc = 0.0f; // internal float
    private float fadeAmount = 0.0f; // the fade value (maybe this can be fetched if you want to keep your fade settings)

    void Start()
    {
        rend = GetComponent<Renderer>();
        dripSpeed = Random.Range(dripSpeed - 0.01f, dripSpeed + 0.01f); // alter the drip speed for a little randomness
    }
    
    void Update()
    {
        //Debug.Log(Mathf.Sin(speedArc * Mathf.Deg2Rad) + " : "+speedArc); // internal debug of speed arc
        if (speedArc < 90.0f) { speedArc += dripSpeed; } // update speed based on some genius maths.


        if (!makeBloodDry) // if not making it dry yet
            {
            // make it move
            rend.material.SetFloat("_YOffset", Mathf.Lerp(yOffsetStartValue, yOffsetEndValue, Mathf.Sin(speedArc * Mathf.Deg2Rad))); // update it in the shader
            }

        if (speedArc >= 90.0f) // met limit for drying?
        {
            makeBloodDry = true;
        }

        // Blood Drying
        if (makeBloodDry)
        {
            if (bloodDryAmount < 1.0f) // let get drying...
            {
                bloodDryAmount += autoDrySpeed * Time.deltaTime; // dry it
                rend.material.SetFloat("_BloodDrying", bloodDryAmount); // update it in the shader
            }
            //limits for drying
            if (bloodDryAmount > 1.0f) { bloodDryAmount = 1.0f; } // limit blood dry level
            if (bloodDryAmount < 0.0f) { bloodDryAmount = 0.0f; } // limit blood dry level


            if (bloodDryAmount > 0.2f) // start fade..
            // make it fade
            {
                rend.material.SetFloat("_Fade", fadeAmount); // update it in the shader
                if (fadeAmount<1.0f) { fadeAmount += fadeSpeed * Time.deltaTime; }
                    else // no more fading, time to die.
                    {
                        Destroy(transform.parent.gameObject);
                    }
            }
            
        }

    }

}
