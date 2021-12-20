using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TentacleScript : MonoBehaviour
{
    private Transform _piratePos;
    private Animator _animator;
    [SerializeField]
    private bool Slap = false;

    void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _piratePos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Slap)
        {
            transform.LookAt(_piratePos, Vector3.up);
        }
        else
        {
            _animator.SetBool("Slap",true);
            StartCoroutine(SlapAnimation(_animator.GetCurrentAnimatorStateInfo(0).length));
        }
    }
    IEnumerator SlapAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        Slap = false;
        _animator.SetBool("Slap", false);
    }
}
