using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    // Groups 
    [SerializeField]
    public GameObject[] groups;

    // Spawns one group from the list of groups
    public void spawnNext()
    {
        // random index
        int i = Random.Range(0, groups.Length);
    
        // Spawn group at current position
        Instantiate(groups[i],
            transform.position,
            Quaternion.identity);
    }

    // Spawns the first group at beginning of game
    private void Start()
    {
        spawnNext();
    }
}
