using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameParameters gameParameters;
    public DayAndNightCyclers dndCycles;

    private void Awake()
    {
        // ...
        dndCycles.enabled = gameParameters.enableDayAndNightCycle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
