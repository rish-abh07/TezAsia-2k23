using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalAppear : MonoBehaviour
{
    public GameObject door;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(false);
        anim = door.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            door.SetActive(true);
            anim.Play("PortalAnimation");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Level2");
    }
}