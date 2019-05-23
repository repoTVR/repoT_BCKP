using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshController : MonoBehaviour
{
    NavMeshAgent navmeshagent;
    GameObject player;
    float range;
    int areaNavMesh;
    bool inMovimento;
    bool caduto;
    Vector3 result;
    // Start is called before the first frame update
    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        range = 200f;
        areaNavMesh = 1 << NavMesh.GetAreaFromName("Recinto");
        result = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        MortePersonaggio();
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;
        NavMeshHit hit;
        //Debug.Log("In movimento = " + inMovimento);
        if(!caduto)
        {
            if (!inMovimento)
            {
                //Debug.Log("Sample position : " + (NavMesh.SamplePosition(randomPoint, out hit, 50f, areaNavMesh)));
                if (NavMesh.SamplePosition(randomPoint, out hit, 3000f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    navmeshagent.SetDestination(result);
                    inMovimento = true;
                }
            }
            else
            {
                inMovimento = Mathf.Abs(transform.position.x - result.x) > 0.2 && Mathf.Abs(transform.position.z - result.z) > 0.2;
            }
        }
        
        
    }

    public void MortePersonaggio()
    {
        if(player.GetComponent<Movimento>().getCaduto())
        {
            caduto = true;
            navmeshagent.stoppingDistance = 3f;
            navmeshagent.speed = 3f;
            navmeshagent.SetDestination(player.transform.position);
        }
    }

}
