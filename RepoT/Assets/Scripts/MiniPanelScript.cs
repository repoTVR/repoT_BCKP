using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniPanelScript : MonoBehaviour
{
    public GameObject player;
    public static int cont;
    public Button buttonPrefab;
    private Button actualButton;
    private SortedDictionary<int, Button> buttons;
    [SerializeField] private Sprite[] arrImg;
    // Start is called before the first frame update
    void Start()
    {
        buttons = new SortedDictionary<int, Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addButton(int id)
    {
        actualButton = Instantiate(buttonPrefab, transform, false);
        buttons.Add(cont, actualButton);
        cont++;
        actualButton.GetComponentInChildren<Image>().sprite = arrImg[id];
    }

    public Button getButtonById(int id)
    {
        if (buttons.ContainsKey(id))
        {
            return buttons[id];
        }
        else
        {
            Debug.Log("Id non presente");
            return null;
        }

    }
    public void selectButton(int id)
    {
        if(id > 0 && getButtonById(id-1) != null)
        {
            getButtonById(id-1).GetComponentInChildren<RawImage>().enabled = false;
        }

        if(getButtonById(id) != null)
        {
            getButtonById(id).GetComponentInChildren<RawImage>().enabled = true;
        }
        

    }

    public void EliminaUltimoBottone()
    {
        if (buttons.ContainsKey(cont-1))
        {
            buttons.Remove(cont-1);
            Destroy(transform.GetChild(cont-1).gameObject);
            cont--;
        }
    }

    public void EliminaTutteLeAzioni()
    {
        buttons.Clear();
        foreach(Transform tr in transform)
        {
            Destroy(tr.gameObject);
        }
        cont = 0;
    }
}
