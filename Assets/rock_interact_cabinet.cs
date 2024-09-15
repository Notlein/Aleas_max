using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rock_interact_cabinet : MonoBehaviour, IPointerClickHandler
{
    private bool isClicked = false;
    public int index;
    public JournalClass t;
    public GameObject[] eols_cab;
    public AkSwitch akswitch;

    // Start is called before the first frame update
    void Start()
    {
        index = int.Parse(name.Substring(name.Length - 2));
        eols_cab =  GameObject.FindGameObjectsWithTag("eol_cabinet");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void clearHalo() {
        for (int i = 0; i < eols_cab.Length; i++)
        {
            //
            //eols_cab[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // This method is called when the GameObject is clicked with the mouse
    private void OnMouseDown()
    {
        //HandleClickOrTouch();
    }

    // This method is called when the GameObject is touched (released) on a touch device
    private void OnMouseUp()
    {
        //HandleClickOrTouch();
    }

    private void HandleClickOrTouch()
    {
        // dbl click
        
        if (!isClicked)
        {
            
            isClicked = true;
            
        }
        else if (isClicked)
        {
            //stop all sounds to play the new one
            AkSoundEngine.StopAll();
            //page is true is it wasnt
            t.page_journal[index] = true;
            // jouer son
            //gameObject.GetComponent<AkAmbient>().enabled = true;
            //activer journal
            t.gameObject.SetActive(true);
            // fonction goToPage()
            t.GoToPage(this.index, gameObject.GetComponent<AkAmbient>());
            isClicked = false;
            //gameObject.GetComponent<AkAmbient>().enabled = false;
            
            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandleClickOrTouch();
    }

}
