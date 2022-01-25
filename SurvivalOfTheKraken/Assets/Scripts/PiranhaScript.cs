using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PiranhaScript : MonoBehaviour
{
    private Transform _pirate;
    public float moveSpeed = 2;
    private NavMeshAgent _piranha;

    // Start is called before the first frame update
    void Start()
    {
        _pirate = FindObjectOfType<CharacterController>().transform;
        _piranha = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var pirateDistance = Vector3.Distance(_pirate.position, gameObject.transform.position);
        if (pirateDistance <= 5)
        {
            transform.LookAt(_pirate);
            _piranha.SetDestination(_pirate.position);
        }
    }
}
