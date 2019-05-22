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
    bool morto;
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

        //if (RandomPoint(transform.position, range, out point) && !inMovimento)
        //{
        //    Debug.DrawRay(point, Vector3.up, Color.blue, 5.0f);
        //    navmeshagent.SetDestination(point);
        //    inMovimento = true;

        //}
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;
        NavMeshHit hit;
        //Debug.Log("In movimento = " + inMovimento);
        if(!morto)
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
        if(player.GetComponent<Movimento>().getMorto())
        {
            morto = true;
            navmeshagent.stoppingDistance = 3f;
            navmeshagent.speed = 3f;
            navmeshagent.SetDestination(player.transform.position);
        }
    }

    //bool RandomPoint(Vector3 center, float range, out Vector3 result)
    //{
    //    for (int i = 0; i < 30; i++)
    //    {
    //        Vector3 randomPoint = center + Random.insideUnitSphere * range;
    //        NavMeshHit hit;
    //        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, areaNavMesh))
    //        {
    //            result = hit.position;
    //            return true;
    //        }
    //    }
    //    result = Vector3.zero;
    //    return false;
    //}

}
