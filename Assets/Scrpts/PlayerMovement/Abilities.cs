using UnityEngine;

public class Abilities : MonoBehaviour
{
    private bool isSlowMotion = false;
    private float MainTime;
    private float targetTimeScale = 3f;
    private float slowMotionDuration = 3f;
    private float slowMotionTimer = 0f;

    private void Start()
    {
        MainTime = Time.timeScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isSlowMotion)
        {
            StartSlowMotion();
        }

        if (isSlowMotion)
        {
            slowMotionTimer += Time.unscaledDeltaTime;
            if (slowMotionTimer >= slowMotionDuration)
            {
                EndSlowMotion();
            }
            else
            {
                // Interpolacja liniowa dla p³ynnego przejœcia miêdzy wartoœciami czasu skali
                Time.timeScale = Mathf.Lerp(MainTime, targetTimeScale, slowMotionTimer / slowMotionDuration);
            }
        }
    }

    private void StartSlowMotion()
    {
        isSlowMotion = true;
        targetTimeScale = 0.3f; // Celowa wartoœæ czasu skali podczas spowolnienia
        slowMotionTimer = 0f;
    }

    private void EndSlowMotion()
    {
        isSlowMotion = false;
        targetTimeScale = MainTime; // Celowa wartoœæ czasu skali po zakoñczeniu spowolnienia
        slowMotionTimer = 0f;
    }
}
