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
        //(Instantiate(buttonPrefab)).transform.SetParent(transform, false);
        //(Debug.Log("Posizione bottone" + ((cont * 40.0F) + panelWidth / 2));
        //Instantiate(buttonPrefab, new Vector3((panelWidth/2) - (cont * 40f), 0, 0), Quaternion.identity).transform.SetParent(transform,false);
        //Instantiate(buttonPrefab).transform.SetParent(transform, false);
        //Instantiate(buttonPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
        actualButton = Instantiate(buttonPrefab, transform, false);
        //actualButton.GetComponent<buttonNumber>().index = cont;
        buttons.Add(cont, actualButton);
        cont++;
        Debug.Log("Bottone " + actualButton.GetComponentInChildren<TextMeshProUGUI>().text);

        switch (id)
        {
            case 0:
                actualButton.GetComponentInChildren<TextMeshProUGUI>().text = "Cammina";
                break;
            case 1:
                actualButton.GetComponentInChildren<TextMeshProUGUI>().text = "Gira destra";
                break;
            case 2:
                actualButton.GetComponentInChildren<TextMeshProUGUI>().text = "Gira sinistra";
                break;
            case 3:
                actualButton.GetComponentInChildren<TextMeshProUGUI>().text = "Salta";
                break;
        }
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
        getButtonById(id).Select();

    }
}
