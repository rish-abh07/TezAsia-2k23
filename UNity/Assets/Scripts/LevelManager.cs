using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject spawnPos;
    [SerializeField]private GameObject player;


    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        spawnPos = GameObject.FindGameObjectWithTag("Respawn");
        

    }
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
       
        player.transform.position = spawnPos.transform.position;
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
