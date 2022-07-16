using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public int num;
    public AudioClip onStep;
    private FloorControl floorControl;
    private Animator animator;
    private AudioSource _audioSource;
    private void Start()
    {
        animator = GetComponent<Animator>();
        floorControl = this.transform.parent.GetComponent<FloorControl>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.activeSelf)
            {
                animator.Play("Sag", 0, 0);
                _audioSource.PlayOneShot(onStep);
            }
            floorControl.AddNum(num);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.activeSelf)
            {
                animator.Play("Up", 0, 0);
                _audioSource.PlayOneShot(onStep);
            }
        }
    }
}