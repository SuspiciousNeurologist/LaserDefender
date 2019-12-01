using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints; 
    int waypointIndex = 0;

    void Start(){
        waypoints = waveConfig.getWaypoints();
        transform.position =waypoints[waypointIndex].transform.position;
    }

    void Update(){
        MoveEnemy();
    }

    public void setWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void MoveEnemy(){
        if (waypointIndex <= waypoints.Count -1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.getMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,targetPosition,movementThisFrame);
            if(transform.position==targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
