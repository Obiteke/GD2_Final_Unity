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

    [SerializeField]
    private float _timeRemaining = 0;
    private bool _timerIsRunning = false;

    void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _piratePos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        if (!Slap)
        {
            transform.LookAt(_piratePos, Vector3.up);
            _timerIsRunning = true;
        }
        else
        {
            _animator.SetBool("Slap",true);
            StartCoroutine(SlapAnimation(_animator.GetCurrentAnimatorStateInfo(0).length));
        }
    }
    private void Timer()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _timeRemaining = Random.Range(2,10);
                _timerIsRunning = false;
                Slap = true;
            }
        }
    }
    IEnumerator SlapAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        Slap = false;
        _animator.SetBool("Slap", false);
    }
}
