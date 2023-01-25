using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGull : MonoBehaviour
{
    [SerializeField] private Transform target;
    private bool moveGull = true;
    public bool soundGull = true;
    private Vector3 initialGull;
    public float speed;

    private void Start()
    {
        initialGull = transform.position;
    }

    private void Update()
    {
        if(transform.position != target.position && moveGull == true)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        else if(moveGull == true)
        {
            moveGull = false;
            Invoke("RestartGull", 4f);
        }

        if(soundGull == true)
        {
            Invoke("PlaySoundGull", 8f);
            soundGull = false;
        }
    }

    private void RestartGull()
    {
        transform.position = initialGull;
        moveGull = true;
    }

    private void PlaySoundGull()
    {
        AudioManager.instance.PlayGullSfx();
        soundGull = true;
    }
}
