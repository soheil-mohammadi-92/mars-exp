using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float mainRotation = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void StartThrusting(){
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(mainEngine);
            }
            if(!mainBooster.isPlaying){
                mainBooster.Play();
            }
    }
    void StopThrusting(){
        audioSource.Stop();
        mainBooster.Stop();
    }
    void ProcessRotation()
    {
         if (Input.GetKey(KeyCode.D)) {
           ApplyRotation(mainRotation);
            if(!rightBooster.isPlaying){
                rightBooster.Play();
            }
        }
        else if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(-mainRotation);
             if(!leftBooster.isPlaying){
                leftBooster.Play();
            }
        }
        else {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame){
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
