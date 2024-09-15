using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rock_interact : MonoBehaviour, IPointerClickHandler
{
    private bool isClicked = false;
    public int index;
    public JournalClass t;
    public GameObject[] eols;
    public GameObject eol_cab_link;
    public AkSwitch akswitch;

    // Start is called before the first frame update
    void Start()
    {
        
        index = int.Parse(name.Substring(name.Length - 2)); 
        eols =  GameObject.FindGameObjectsWithTag("eol");
        


        clearHalo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void clearHalo() {
        for (int i = 0; i < eols.Length; i++)
        {
            eols[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
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
        clearHalo();
        if (!isClicked)
        {
            
            // desactive toutes les isClicked de tous les objets
            for(int i = 0; i < eols.Length; i++)
            {
                
                eols[i].GetComponent<rock_interact>().isClicked = false;
            }
            int y = gameObject.GetComponent<rock_interact>().index;
         
            for (int i = 0; i < eols.Length; i++)
            {
                if (eols[i].gameObject.GetComponent<rock_interact>().index == y)
                {
                    eols[i].gameObject.GetComponent<rock_interact>().isClicked = true;
                    eols[i].gameObject.GetComponent<rock_interact>().gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
            }


        }
        else if (isClicked)
        {
            
            //page is true is it wasnt
            t.page_journal[index] = true;
            // jouer son
            //stop all sounds to play the new one

            //activer journal
            t.gameObject.SetActive(true);
            // fonction goToPage()
            t.GoToPage(this.index, gameObject.GetComponent<AkAmbient>());
            isClicked = false;
            
            
            eol_cab_link.SetActive(true);
            // remove aura
            clearHalo();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandleClickOrTouch();
    }

}
