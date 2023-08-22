using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]private GameObject treasure;
    // Start is called before the first frame update
    void Start()
    {
        treasure = GameObject.FindGameObjectWithTag("treasure");
        _animator = treasure.GetComponentInChildren<Animator>();
        AnimateChest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AnimateChest()
    {
        _animator.Play("treasure box");
    }
}
