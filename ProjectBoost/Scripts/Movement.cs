using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrustMultiplier = 1000.0f;
    [SerializeField] float rotationThrustMultiplier = 1000.0f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;
    Rigidbody myRigidbody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            NewMethod();
        }
        else
        {
            mainBoosterParticle.Stop();
            audioSource.Stop();
        }
    }

    void NewMethod()
    {
        myRigidbody.AddRelativeForce(Vector3.up * mainThrustMultiplier * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!leftBoosterParticle.isPlaying)
            {
                leftBoosterParticle.Play();
            }
            ApplyRotation(rotationThrustMultiplier); // Move forward
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!rightBoosterParticle.isPlaying)
            {
                rightBoosterParticle.Play();
            }
            ApplyRotation(-rotationThrustMultiplier); // Negative rotationThrust is move the other way
        }
        else
        {
            leftBoosterParticle.Stop();
            rightBoosterParticle.Stop();
        }
    }

    private void ApplyRotation(float rotationDirection)
    {
        myRigidbody.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationDirection * Time.deltaTime);
        myRigidbody.freezeRotation = false; // Unfreeze
    }
}
