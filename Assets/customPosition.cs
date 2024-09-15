using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class customPosition : MonoBehaviour
{
    static public float R_b = -10505.0f;
    static public float L_b = -35.0f;

    static float new_R = -10480.00f;
    static float new_L = -70.00f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //if (this.gameObject.transform.localPosition.x < R_b) {
        //    Debug.Log("right reached");
        //    //right boundary
        //    // - 5635
        //    gameObject.transform.localPosition = new Vector3(new_L, transform.localPosition.y, transform.position.z);
            
        //}

        //if (this.gameObject.transform.localPosition.x > L_b) // left boundary
        //{
        //    Debug.Log("left reached");
        //    // -7950
        //    gameObject.transform.localPosition = new Vector3(new_R, transform.localPosition.y, transform.position.z);

        //}

    }
    static public void BoundCheck() {
        GameObject g = GameObject.FindGameObjectWithTag("backg-eol");
        if (g.transform.localPosition.x < R_b)
        {
            Debug.Log("right reached");
            //right boundary
            // - 5635
            g.transform.localPosition = new Vector3(new_L, g.transform.localPosition.y, g.transform.position.z);

        }

        if (g.transform.localPosition.x > L_b) // left boundary
        {
            Debug.Log("left reached");
            // -7950
            g.transform.localPosition = new Vector3(new_R, g.transform.localPosition.y, g.transform.position.z);

        }
    }
}
