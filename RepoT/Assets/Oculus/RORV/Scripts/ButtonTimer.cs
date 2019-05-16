using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;



public class ButtonTimer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject player;
    public int timeremain = 1; // tiempo restante
    Button _button;
    public int idAzione;
    private ArrayList azioniList;
    private ArrayList azioniListTmp;




    
    // Use this for initialization
    void Start () {
        //azioniListTmp = new ArrayList();
        _button = GetComponent<Button>();
   
    }

    void Update()
    { 
 
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //do your stuff when highlighted
        NotificationCenter.DefaultCenter().PostNotification(this, "EnBoton");
        //print("en boton");
        InvokeRepeating("countDown", 1, 1);//llama al cursor
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //do your stuff when highlighted
        NotificationCenter.DefaultCenter().PostNotification(this, "EnNada");
        print("Cancela");
        CancelInvoke("countDown");
        timeremain = 3;
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

    public void Clicked() {
        switch (idAzione)
        {
            case 0:
                player.GetComponent<Movimento>().azioniList.Add(idAzione);
                break;
            case 1:
                player.GetComponent<Movimento>().azioniList.Add(idAzione);
                break;
            case 2:
                player.GetComponent<Movimento>().azioniList.Add(idAzione);
                break;
            case 3:
                player.GetComponent<Movimento>().azioniList.Add(idAzione);
                break;
            case 4:
                //player.GetComponent<Movimento>().enabled = true;
                if (player.GetComponent<Movimento>().azioniList == null)
                {
                    Debug.Log("null arr list");
                }
                //player.GetComponent<Movimento>().azioniList.Add(azioniListTmp.Clone());
                player.GetComponent<Movimento>().play = true;
                
                break;
        }
        Debug.Log("Azione " + idAzione + "aggiunta alla lista");
    }





}
