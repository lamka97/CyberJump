using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------Audio Clip------")]
    public AudioClip background; //zvuk v pozadí
    public AudioClip death; //zvuk pro smrt
    public AudioClip checkpoint; //zvuk po dosažení checkpointu
    public AudioClip finish; //zvuk po dosažení cíle

    public static AudioManager instance; // Statická promìnná pro uchování instance AudioManageru
    private void Awake()
    {
        if (instance == null) // Kontroluje, zda instance existuje
        {
            instance = this; // Nastaví tuto instanci jako jedinou existující
            DontDestroyOnLoad(gameObject); // Zabrání tomu aby se to nezastavilo pøi pøechodu do jiné scény
        }
        else
        {
            Destroy(gameObject); // Pokud již instance existuje, novì vytvoøený objekt se odstaní aby nevznikly duplicity
        }
    }

    private void Start()
    {
        musicSource.clip = background; // Nastaví hudební zdroj na klip hudby na pozadí
        musicSource.Play(); // Spustí pøehrávání hudby
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip); // Pøehrává zadaný zvukový klip jednou bez toho aniž by byl pøerušen ostatními zvuky
    }
}
