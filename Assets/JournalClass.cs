using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class JournalClass : MonoBehaviour
{

    public int current_page;
    public int current_eol_page;

    public GameObject journal;
    public GameObject journal_sections;
    public GameObject[] pages;
    //public GameObject[] eol_pages;
    public Transform parentTransform;
    public GameObject[] eols_cabinet;
    public AkAmbient secret_sound; // deactivated because of Wwise limit to 200 assets

    public GameObject back;
    public GameObject fwd;
    public bool muted;

    private bool all_pages;

    const int MAX_PAGE = 3; // adjust to number of pages -1 in full version
    const int MIN_PAGE = 0; // adjust to 0 in full version (cover = 0, scene1 =1, ...), instead of (cover = 1, scene2 = 2, max =3)
    const int MIN_EOL_PAGE = 0; 

    const int EOL_PAGE = 2;
    const int NB_EOL = 38;
    const int STARTING_PAGE = 0; 

    public bool[] page_journal;
    public bool secret_sound_played;


    // Start is called before the first frame update
    void Start()
    {
        secret_sound_played = false;
        journal_sections = GameObject.FindGameObjectWithTag("journal_section");
        journal = GameObject.FindGameObjectWithTag("journal");
        all_pages = false;
        page_journal = new bool[NB_EOL + 1];
        eols_cabinet = GameObject.FindGameObjectsWithTag("eol_cabinet");
        parentTransform = transform.GetChild(0).GetChild(0); // Assuming this script is attached to the parent GameObject
        page_journal[0] = true; // cabinet par defaut
        current_page = STARTING_PAGE;
        current_eol_page = 0;
        this.gameObject.SetActive(false);

        for (int i = 0; i < eols_cabinet.Length; i++)
        {
            eols_cabinet[i].gameObject.SetActive(false);
        }
 //verifier ici si les autres composantes sont cachées
        pages[2].gameObject.SetActive(false); // clear page 2

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.activeInHierarchy)
        {
            all_pages = true;
            
            for (int i = 0; i < NB_EOL ; i++) {
                if (page_journal[i] == false) {
                    all_pages = false;
                }
            }

            if (!secret_sound_played && all_pages && current_page == 2 && current_eol_page == 0) {
                // play secret sound here
                loadSceneAndSound();

            }
        }


 
    }


    async public void loadSceneAndSound() {
        Debug.Log("Secret Sound!");
        AkSoundEngine.StopAll();
        secret_sound_played = true;
        secret_sound.enabled = true;
        await Task.Delay(7000);
        GameObject.FindWithTag("fade").GetComponent<AlphaTransition>().FadeIn();
        await Task.Delay(7000);
        SceneManager.LoadScene("4_Scene2");



    }

    public void SwitchPageNext()
    {
        back.SetActive(true);
        fwd.SetActive(true);
        if (current_page == MAX_PAGE)
        {
            fwd.SetActive(false);
            // Do nothing if the current_page is max_page
            return;
        }

        if (current_page == EOL_PAGE)
        {
            if (current_eol_page != NB_EOL) { DeactivateAllChildren(parentTransform.GetChild(2)); }

            if (current_eol_page < NB_EOL) // Adjusted the condition
            {
                current_eol_page++;
                if (!page_journal[current_eol_page])
                {
                    SwitchPageNext();
                }
                else {
                    ActivateChild(parentTransform.GetChild(2), current_eol_page);
                    AkAmbient amb = GameObject.Find("Page_eol_" + current_eol_page.ToString("D2")).GetComponent<AkAmbient>();
                    AkSoundEngine.StopAll();
                    amb.enabled = true;
                    amb.enabled = false;

                }
            }
            else
            {
                parentTransform.GetChild(2).gameObject.SetActive(false);
                current_page++;
                ActivateChild(parentTransform, current_page);
                
            }
        }
        else
        {
            DeactivateAllChildren(parentTransform);
            current_page++;

            ActivateChild(parentTransform, current_page);
        }

        if (current_page == MAX_PAGE)
        {
            fwd.SetActive(false);

        }
    }


  
    public void SwitchPagePrevious()
    {
        fwd.SetActive(true);
        back.SetActive(true);
        if (current_page == MIN_PAGE && current_eol_page == MIN_EOL_PAGE)
        {
            // Do nothing if both variables are already at their minimum values (0)
            return;
        }

        

        if (current_page == EOL_PAGE)
        {
            if (current_eol_page != MIN_EOL_PAGE) { DeactivateAllChildren(parentTransform.GetChild(2)); }

            if (current_eol_page > MIN_EOL_PAGE)
            {
                current_eol_page--;
                if (!page_journal[current_eol_page])
                {
                    SwitchPagePrevious();
                }
                else if (current_eol_page == 0) {
                    Debug.Log(current_eol_page);
                    ActivateChild(parentTransform.GetChild(2), current_eol_page);
                    AkSoundEngine.StopAll();

                }
                else
                {
                    ActivateChild(parentTransform.GetChild(2), current_eol_page);
                    if (current_eol_page > 0 && current_eol_page < 38)
                    {
                        AkAmbient amb = GameObject.Find("Page_eol_" + current_eol_page.ToString("D2")).GetComponent<AkAmbient>();
                        AkSoundEngine.StopAll();
                        amb.enabled = true;
                        amb.enabled = false;
                    }


                }


            }
            else if (current_page > MIN_PAGE)
            {
                parentTransform.GetChild(2).gameObject.SetActive(false);
                current_page--;
                ActivateChild(parentTransform, current_page);

            }
        }
        else if (current_page > MIN_PAGE )
        {
            DeactivateAllChildren(parentTransform);
            current_page--;
            ActivateChild(parentTransform, current_page);
            if (current_page == 2 && current_eol_page == 38 && !page_journal[current_eol_page])
            {
                SwitchPagePrevious();
            }
        }
        if (current_page == MIN_PAGE)
        {
            back.SetActive(false);

        }
    }

    void DeactivateAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(false);
        }
    }

    void ActivateChild(Transform parent, int index)
    {
        if (index >= 0 && index < parent.childCount)
        {
            parent.GetChild(index).gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid child index: " + index);
        }
    }


    // verifier la séquence de  playback 
    public void GoToPage(int index, AkAmbient AK_amb) {
        AkSoundEngine.StopAll();

        while (current_eol_page != index) {
        
            if (current_eol_page < index || current_page < 2)
            {
                SwitchPageNext();
            } else if (current_eol_page > index || current_page > 2)
            {
                SwitchPagePrevious();
            }

        }
        if (index != 0) {
            AK_amb.enabled = true;
            AK_amb.enabled = false;
        }
            
      
        // play the sound
        
    }
 
    public void GoToPage(int index)
    {
        AkSoundEngine.StopAll();

        while (current_eol_page != index)
        {

            if (current_eol_page < index || current_page < 2)
            {
                SwitchPageNext();
            }
            else if (current_eol_page > index || current_page > 2)
            {
                SwitchPagePrevious();
            }

        }

    }
}
