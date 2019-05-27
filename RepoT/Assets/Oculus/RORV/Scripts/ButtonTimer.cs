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





    // Use this for initialization
    void Start () {
        //azioniListTmp = new ArrayList();
        player = GameObject.FindGameObjectWithTag("Player");
        _button = GetComponent<Button>();
        isButtonLaterale = gameObject.tag.Equals("ButtonLaterali");
        lvlManager = GameObject.FindGameObjectWithTag("GameController");
   
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
        HideImage(0);
        Debug.Log("Play Dentro");
        if (player.GetComponent<Movimento>().azioniList.Count > 0)
        {
            Debug.Log("Play conta azioni " + player.GetComponent<Movimento>().azioniList.Count);
            player.GetComponent<Movimento>().play = true;
        }
    }

    public void Clicked() {
        HideImage(0);
        player.GetComponent<Movimento>().azioniList.Add(idAzione);
        miniPanel.GetComponent<MiniPanelScript>().addButton(idAzione);
    }

    public void SpecialiClicked(int id)
    {
        
        ShowPanelPopUp(id);
    }

    public void HideImage(int id)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("RawImageBack"))
        {
            g.GetComponent<RawImage>().enabled = false;
        }
    }

    public void ShowImage(int id)
    {
        HideImage(id);

        gameObject.GetComponentInChildren<RawImage>().enabled = true;

        
    }

    public void HidePanelPopUp(int id)
    {
        GameObject panelSpeciale = GameObject.FindGameObjectWithTag("PanelSpecialAction");

        foreach (Transform tr in panelSpeciale.transform)
        {
            tr.gameObject.SetActive(false);
        }

        HideImage(id);
    }

    public void ShowPanelPopUp(int id)
    {
        GameObject panelSpeciale = GameObject.FindGameObjectWithTag("PanelSpecialAction");

        foreach (Transform tr in panelSpeciale.transform)
        {
            tr.gameObject.SetActive(true);
        }

        ShowImage(id);

    }

    public void BottoneLateraleClicked()
    {
        HideImage(0);
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
        HideImage(0);
        if (player.GetComponent<Movimento>().azioniList.Count > 0)
        {
            player.GetComponent<Movimento>().EliminaUltimaAzione();
            miniPanel.GetComponent<MiniPanelScript>().EliminaUltimoBottone();
        }
            
    }

    public void DelTutto()
    {
        HideImage(0);
        if (player.GetComponent<Movimento>().azioniList.Count > 0)
        {
            player.GetComponent<Movimento>().EliminaTutteLeAzioni();
            miniPanel.GetComponent<MiniPanelScript>().EliminaTutteLeAzioni();
        }
            
    }

    public void Riavvia()
    {
        HideImage(0);
        lvlManager.GetComponent<InizioLivello>().Riavvia();
    }





}
