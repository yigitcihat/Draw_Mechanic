using UnityEngine;
using UnityEngine.AI;

public class TeamMemberNavMeshController1 : MonoBehaviour
{
    public Camera cam;
    [SerializeField] GameObject target;
    public NavMeshAgent player;
   
    private void Update()
    {
        player.SetDestination(target.transform.position);
       

        
    }

    
}
