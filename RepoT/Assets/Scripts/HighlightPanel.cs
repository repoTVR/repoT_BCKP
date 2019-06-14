using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightPanel : MonoBehaviour, IPointerEnterHandler
{

    public GameObject PanelDaAttivare;
    // Start is called before the first frame update


    public void OnPointerEnter(PointerEventData eventData)
    {
        PanelDaAttivare.GetComponent<Button>().OnPointerEnter(eventData);
    }
    public void OnSelect(BaseEventData eventData)
    {
        //PanelDaAttivare.GetComponent<Button>().Select();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        PanelDaAttivare.GetComponent<Button>().OnPointerExit(eventData);
    }
}
