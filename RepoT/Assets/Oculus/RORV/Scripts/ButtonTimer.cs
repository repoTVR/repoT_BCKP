using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;



public class ButtonTimer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject miniPanel;
    private GameObject player;
    public int timeremain = 1; // tiempo restante
    Button _button;
    public int idAzione;
    private ArrayList azioniList;
    private ArrayList azioniListTmp;
    private bool isButtonLaterale;
    public int tempoSelezione;
    private GameObject lvlManager;
    private int idPanelNum = 2;
    private int idPanelIf = 3;
    private int counterMaxAzioni = 12;

    private GameObject lvlController;





    // Use this for initialization
    void Start () {
        //azioniListTmp = new ArrayList();
        player = GameObject.FindGameObjectWithTag("Player");
        _button = GetComponent<Button>();
        isButtonLaterale = gameObject.tag.Equals("ButtonLaterali");
        lvlManager = GameObject.FindGameObjectWithTag("GameController");
        lvlController = GameObject.FindGameObjectWithTag("LvlChanger");

    }

    void Update()
    { 
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //do your stuff when highlighted
        NotificationCenter.DefaultCenter().PostNotification(this, "EnBoton");
        //print("en boton");
        InvokeRepeating("countDown", 0f, 1);//llama al cursor
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //do your stuff when highlighted
        NotificationCenter.DefaultCenter().PostNotification(this, "EnNada");
        CancelInvoke("countDown");
        timeremain = tempoSelezione;
    }

    void countDown()
    {
       
        //print(timeremain);
        
        
        if (timeremain <= 0)
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "EnNada");
            _button.onClick.Invoke();
            //reset time
            CancelInvoke("countDown");
            timeremain = 1;
            //print("reset time");
            
        }

        timeremain--;
    }

    public void Play()
    {
        ActivateImageFor();
        HideImage();
        HidePanelPopUp();
        if (player.GetComponent<Movimento>().azioniList.Count > 0)
        {
            player.GetComponent<Movimento>().play = true;
        }
    }

    public void Clicked() {
        ActivateImageFor();
        HideImage();
        HidePanelPopUp();
        if(miniPanel.GetComponent<MiniPanelScript>().GetCont() < counterMaxAzioni)
        {
            player.GetComponent<Movimento>().azioniList.Add(idAzione);
            miniPanel.GetComponent<MiniPanelScript>().addButton(idAzione);
        }
        
    }

    public void IfButtonClicked()
    {
        Movimento movimento = player.GetComponent<Movimento>();
        HideImage();
        //HidePanelPopUp();
        if (miniPanel.GetComponent<MiniPanelScript>().GetCont() < counterMaxAzioni)
        {
            movimento.azioniList.Add(idAzione);
            miniPanel.GetComponent<MiniPanelScript>().addButton(idAzione);
        }
            
        
    }

    public void ActivateImageFor()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("ImageHide"))
        {
            g.GetComponent<Image>().enabled = true;
            g.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void DeactivateImageFor()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("ImageHide"))
        {
            g.GetComponent<Image>().enabled = false;
            g.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void ShowPanelNumber()
    {
        HideImage();
        HidePanelCentrale();
        GameObject.FindGameObjectWithTag("PanelSpeciali").transform.GetChild(idPanelNum).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("PanelEsecuzione").GetComponent<OperazioniFor>().SetIdAzione(GetIdAzioneByForPanel(idAzione));
        DeactivateImageFor();
    }

    public void ShowPanelIfActions()
    {
        HideImage();
        HidePanelCentrale();
        GameObject.FindGameObjectWithTag("PanelSpeciali").transform.GetChild(idPanelIf).gameObject.SetActive(true);
        gameObject.GetComponent<BoolArrayList>().GetBoolArrayList().Add(idAzione!=0);
        Debug.Log("Ho aggiunto " + (idAzione != 0) + "alla lista");

        //GameObject.FindGameObjectWithTag("PanelEsecuzione").GetComponent<OperazioniFor>().SetIdAzione(GetIdAzioneByForPanel(idAzione));
        DeactivateImageFor();
    }

    public void SelectNumTimesAction()
    {
        GameObject.FindGameObjectWithTag("PanelEsecuzione").GetComponent<OperazioniFor>().SetNumVolte(idAzione + 1);
    }

    public int GetIdAzioneByForPanel(int id)
    {
        int azione = id;

        switch(id)
        {
            case 0:
                {
                    azione = 4;
                    break;
                }
            case 1:
                {
                    azione = 5;
                    break;
                }
        }

        return azione;
    }

    


    public void SpecialiClicked()
    {
        ActivateImageFor();
        ShowPanelPopUp(idAzione);
        ShowImage();
        
    }


    public void HideImage()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("RawImageBack"))
        {
            g.GetComponent<RawImage>().enabled = false;
        }
    }

    public void ShowImage()
    {
        HideImage();

        gameObject.GetComponentInChildren<RawImage>().enabled = true;


    }

    public void HidePanelPopUp()
    {
        GameObject panelSpeciale = GameObject.FindGameObjectWithTag("PanelSpeciali");

        foreach (Transform tr in panelSpeciale.transform)
        {
            tr.gameObject.SetActive(false);
        }

        HideImage();
    }

    public void ShowPanelPopUp(int id)
    {
        HidePanelPopUp();

        GameObject.FindGameObjectWithTag("PanelSpeciali").transform.GetChild(id).gameObject.SetActive(true);

    }

    public void HidePanelCentrale()
    {
        GameObject emptyCentrale = GameObject.FindGameObjectWithTag("PanelCentrale");

        foreach (Transform tr in emptyCentrale.transform)
        {

            tr.gameObject.SetActive(false);

        }
    }

    public void BottoneLateraleClicked()
    {
        ActivateImageFor();
        HideImage();
        HidePanelPopUp();
        GameObject[] arrBottoniLaterali = GameObject.FindGameObjectsWithTag("ButtonLaterali");
        GameObject emptyCentrale = GameObject.FindGameObjectWithTag("PanelCentrale");
        

        

        foreach(GameObject btn in arrBottoniLaterali)
        {
            btn.GetComponentInChildren<RawImage>().enabled = false;
        }

        gameObject.GetComponentInChildren<RawImage>().enabled = isButtonLaterale;

        foreach (Transform tr in emptyCentrale.transform)
        {

            tr.gameObject.SetActive(tr.gameObject.GetComponentInChildren<IdPanelCentrale>().IsPanelCentrale(idAzione));
            
        }

        
    }

    public void DelLastClicked()
    {
        ActivateImageFor();
        HideImage();
        HidePanelPopUp();
        if (player.GetComponent<Movimento>().azioniList.Count > 0)
        {
            player.GetComponent<Movimento>().EliminaUltimaAzione();
            miniPanel.GetComponent<MiniPanelScript>().EliminaUltimoBottone();
        }
            
    }

    public void DelTutto()
    {
        ActivateImageFor();
        HideImage();
        HidePanelPopUp();
        if (player.GetComponent<Movimento>().azioniList.Count > 0)
        {
            player.GetComponent<Movimento>().EliminaTutteLeAzioni();
            miniPanel.GetComponent<MiniPanelScript>().EliminaTutteLeAzioni();
        }
            
    }

    public void Riavvia()
    {
        lvlController.GetComponent<SceneSetup>().Riavvia();
    }





}
