using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------Audio Clip------")]
    public AudioClip background; //zvuk v pozad�
    public AudioClip death; //zvuk pro smrt
    public AudioClip checkpoint; //zvuk po dosa�en� checkpointu
    public AudioClip finish; //zvuk po dosa�en� c�le

    public static AudioManager instance; // Statick� prom�nn� pro uchov�n� instance AudioManageru
    private void Awake()
    {
        if (instance == null) // Kontroluje, zda instance existuje
        {
            instance = this; // Nastav� tuto instanci jako jedinou existuj�c�
            DontDestroyOnLoad(gameObject); // Zabr�n� tomu aby se to nezastavilo p�i p�echodu do jin� sc�ny
        }
        else
        {
            Destroy(gameObject); // Pokud ji� instance existuje, nov� vytvo�en� objekt se odstan� aby nevznikly duplicity
        }
    }

    private void Start()
    {
        musicSource.clip = background; // Nastav� hudebn� zdroj na klip hudby na pozad�
        musicSource.Play(); // Spust� p�ehr�v�n� hudby
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip); // P�ehr�v� zadan� zvukov� klip jednou bez toho ani� by byl p�eru�en ostatn�mi zvuky
    }
}
