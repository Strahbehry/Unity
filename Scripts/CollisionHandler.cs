using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1.00f;
    [SerializeField] AudioClip succes;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem succesParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;
    bool invulnerability = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C)){
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isTransitioning || collisionDisabled)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    StartFinishSequence();
                    break;
                case "Invulnerability":
                    invulnerability = true;
                    Destroy(other.gameObject);
                    break;
                default:
                    if(!invulnerability)
                        StartCrashSequence();
                    break;
            }
        }
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(crash);

        crashParticle.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    private void StartFinishSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(succes);

        succesParticle.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex <= SceneManager.sceneCount)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
