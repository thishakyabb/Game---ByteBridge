using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager: MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] public AudioSource soundFXObject;
    [SerializeField] public AudioSource BGSoundFXObject;
    [SerializeField] public AudioClip[] waveMusic;
    [SerializeField] public AudioClip[] menuMusic;
    private AudioSource currentAudioSource = null;
    public void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform,float vol)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = vol;
        audioSource.Play();
        float cliplength = audioSource.clip.length;
        Destroy(audioSource.gameObject,cliplength);
    }
    public IEnumerator PlaySoundClipDelayed(float delaySec, AudioClip audioClip, Transform spawnTransform,float vol)
    {
        yield return new WaitForSeconds(delaySec);
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = vol;
        audioSource.Play();
        float cliplength = audioSource.clip.length;
        Destroy(audioSource.gameObject,cliplength);
    }

    public void PlayBgWaveMusic(float vol)
    {
        if (currentAudioSource && currentAudioSource.gameObject != null)
        {
           Destroy(currentAudioSource.gameObject); 
        }
        currentAudioSource = Instantiate(BGSoundFXObject, transform.position, Quaternion.identity);
        currentAudioSource.clip = waveMusic[Random.Range(0,waveMusic.Length - 1)];
        currentAudioSource.volume = vol;
        currentAudioSource.loop = true;
        currentAudioSource.Play();
        float cliplength = currentAudioSource.clip.length;
    }

    public void StopBgMusic()
    {
        if (currentAudioSource && currentAudioSource.gameObject != null)
        {
            
            Destroy(currentAudioSource.gameObject);
        }
    }

    public void PlayBgMenuMusic(float vol)
    {
        if (currentAudioSource && currentAudioSource.gameObject != null)
        {
           Destroy(currentAudioSource.gameObject); 
        }
        currentAudioSource = Instantiate(BGSoundFXObject, transform.position, Quaternion.identity);
        currentAudioSource.clip = menuMusic[Random.Range(0,waveMusic.Length - 1)];
        currentAudioSource.volume = vol;
        currentAudioSource.loop = true;
        currentAudioSource.Play();
        float cliplength = currentAudioSource.clip.length;
    }
}