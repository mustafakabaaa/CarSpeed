using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public float startDelay = 5f;
    private bool loopStarted = false;

    private void Start()
    {
        audioSource.time = startDelay;
        audioSource.Play();

        // MainCar s�n�f�n�n OnCarCrash olay�na abone ol
        MainCar.OnCarCrash += StopAudioOnCarCrash;
        MainCar.OnGamePause += PauseAudio;
        MainCar.OnGameResume += ResumeAudio;
    }

    private void Update()
    {
        if (!loopStarted && audioSource.time >= startDelay)
        {
            loopStarted = true;
            audioSource.loop = true;
        }
    }

    private void StopAudioOnCarCrash()
    {
        audioSource.Stop();
    }

    private void PauseAudio()
    {
        audioSource.Pause();
    }

    private void ResumeAudio()
    {
        audioSource.UnPause();
    }

    private void OnDestroy()
    {
        // OnDestroy'da abonelikleri kald�r
        MainCar.OnCarCrash -= StopAudioOnCarCrash;
        MainCar.OnGamePause -= PauseAudio;
        MainCar.OnGameResume -= ResumeAudio;
    }
}
