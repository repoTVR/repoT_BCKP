using System.Collections;
using UnityEngine;

public class ColorChangerLvl : MonoBehaviour
{
    //Bool per decidere quando far partire/fermare la funzione
    public bool play;


    //Parametro per decidere da che cubo iniziare a colorare
    public int cuboPartenza = 3;
    ArrayList cubi;
    private readonly Color[] colorArray = new Color[6] { Color.green, Color.red, Color.yellow, Color.cyan, Color.blue, Color.magenta }; //Array di colori per i cubi

    //Gestione del tempo per la chiamata della funzione per cambio colore cubi
    float nextTime;
    float coolDownTime;

    // Start is called before the first frame update
    void Start()
    {
        play = true;
        cubi = new ArrayList();
        cubi = GetComponent<Percorso>().getCuboMaggId(cuboPartenza);

        coolDownTime = 0.5f; //Ogni quanto viene chiamata la funzione per il cambio di colore dei cubi
}

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            ColorChanger(cubi);
        }
    }

    private void ColorChanger(ArrayList cubi)
    {
        int randomNumber = Random.Range(0, 3);

        //Se il tempo è maggiore o uguale dell'azione successiva
        if (Time.time >= nextTime)
        {
            //Il tempo dell'azione successiva è dato dall'inizio dell'azione + cooldowntime
            nextTime = Time.time + coolDownTime;
            foreach (GameObject c in cubi)
            {
                c.GetComponent<Renderer>().material.SetColor("_Color", colorArray[randomNumber]);
            }
        }
    }

}
