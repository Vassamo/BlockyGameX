using UnityEngine;
using UnityEngine.Audio;

public class Abilities : MonoBehaviour
{
    private bool isSlowMotionActive = false;
    private float originalTimeScale;
    private float targetTimeScale = 0.3f;
    public float slowMotionDuration = 3f; // Okreœla czas trwania faktycznego spowolnienia
    public float transitionDuration = 1f; // Czas przejœcia do i z spowolnienia
    private float transitionTimer = 0f;
    private bool isEndingSlowMotion = false;

    public AudioMixer MasterMixer;

    private void Start()
    {
        originalTimeScale = Time.timeScale;
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
                MasterMixer.SetFloat("MasterFilter", 2000);
                Time.timeScale = Mathf.Lerp(originalTimeScale, targetTimeScale, transitionTimer / transitionDuration);
                transitionTimer += Time.unscaledDeltaTime;
            }
            else if (transitionTimer >= transitionDuration && transitionTimer < transitionDuration + slowMotionDuration)
            {
                // Utrzymywanie spowolnienia
                Time.timeScale = targetTimeScale;
                transitionTimer += Time.unscaledDeltaTime;
            }
            else
            {
                // Rozpoczêcie zakoñczenia spowolnienia
                MasterMixer.SetFloat("MasterFilter", 20000);
                isSlowMotionActive = false;
                isEndingSlowMotion = true;
                transitionTimer = 0f; // Resetujemy timer dla fazy koñcowej
            }
        }

        if (isEndingSlowMotion)
        {
            if (transitionTimer < transitionDuration)
            {
                // Interpolacja powrotu do normalnej prêdkoœci
                Time.timeScale = Mathf.Lerp(targetTimeScale, originalTimeScale, transitionTimer / transitionDuration);
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
    }

    private void EndSlowMotion()
    {
        
        Time.timeScale = originalTimeScale; // Powrót do oryginalnej skali czasu
        isEndingSlowMotion = false;
        transitionTimer = 0f; // Resetowanie timera
    }
}
