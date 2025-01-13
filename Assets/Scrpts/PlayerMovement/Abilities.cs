using UnityEngine;
using UnityEngine.Audio;

public class Abilities : MonoBehaviour
{
    private bool isSlowMotionActive = false;
    private float originalTimeScale;
    private float targetTimeScale = 0.3f;
    public float slowMotionDuration = 3f; // Okreœla czas trwania faktycznego spowolnienia
    public float transitionDuration = 1f; // Czas przejœcia do i z spowolnienia
    public int FilterValue = 1000;
    private float transitionTimer = 0f;
    private bool isEndingSlowMotion = false;

    public AudioMixer MasterMixer;

    public AudioSource StopIn;
    public AudioSource StopOut;
    

    private void Start()
    {
        originalTimeScale = Time.timeScale;
        MasterMixer.SetFloat("MasterFilter", 20000); // Ustawienie pocz¹tkowej wartoœci filtra na 20000
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isSlowMotionActive && !isEndingSlowMotion)
        {
            StartSlowMotion();
        }

        if (isSlowMotionActive)
        {
            if (transitionTimer < transitionDuration)
            {
                // Interpolacja do spowolnienia
                float t = transitionTimer / transitionDuration;
                MasterMixer.SetFloat("MasterFilter", Mathf.Lerp(20000, FilterValue, t));
                Time.timeScale = Mathf.Lerp(originalTimeScale, targetTimeScale, t);
                transitionTimer += Time.unscaledDeltaTime;
            }
            else if (transitionTimer >= transitionDuration && transitionTimer < transitionDuration + slowMotionDuration)
            {
                // Utrzymywanie spowolnienia
                Time.timeScale = targetTimeScale;
                MasterMixer.SetFloat("MasterFilter", FilterValue);
                transitionTimer += Time.unscaledDeltaTime;
            }
            else
            {
                // Rozpoczêcie zakoñczenia spowolnienia
                isSlowMotionActive = false;
                isEndingSlowMotion = true;
                StopOut.Play();
                transitionTimer = 0f; // Resetujemy timer dla fazy koñcowej
            }
        }

        if (isEndingSlowMotion)
        {
            if (transitionTimer < transitionDuration)
            {
                // Interpolacja powrotu do normalnej prêdkoœci
                float t = transitionTimer / transitionDuration;
                MasterMixer.SetFloat("MasterFilter", Mathf.Lerp(FilterValue, 20000, t));
                Time.timeScale = Mathf.Lerp(targetTimeScale, originalTimeScale, t);
                transitionTimer += Time.unscaledDeltaTime;
            }
            else
            {
                EndSlowMotion();
            }
        }
    }

    private void StartSlowMotion()
    {
        isSlowMotionActive = true;
        transitionTimer = 0f; // Resetujemy timer na start
        StopIn.Play();
    }

    private void EndSlowMotion()
    {
        Time.timeScale = originalTimeScale; // Powrót do oryginalnej skali czasu
        MasterMixer.SetFloat("MasterFilter", 20000); // Powrót do oryginalnej wartoœci filtra
        isEndingSlowMotion = false;
        transitionTimer = 0f; // Resetowanie timera
    }
}
