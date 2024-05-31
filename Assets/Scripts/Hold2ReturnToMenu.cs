using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hold2ReturnToMenu : MonoBehaviour
{
    [SerializeField] Animator textAnimator;
    [SerializeField] Image holdToSkipImage;
    [SerializeField] Image progressCircleImage;
    [SerializeField] float holdTime = 2f; // Time in seconds to be at 100%
    [SerializeField] float fillSpeed = 1f;
    [SerializeField] float alphaSpeed = 1f;
    [SerializeField] bool isHolding = false;
    [SerializeField] float holdStartTime;
    private float circleFillState = 1f;

    public LevelLoader LevelLoader;

    private void Start()
    {
        textAnimator.Play("HoldToSkip");
    }

    void Update()
    {
        if (isHolding)
        {
            float holdDuration = Time.time - holdStartTime;
            float progress = Mathf.Clamp01(holdDuration / holdTime); // Progress between 0 and 100%
            progressCircleImage.fillAmount = progress;

            SetAlphaAndFill(progress);

            if (holdDuration >= holdTime)
            {
                SceneManager.LoadScene("SCN_MainMenu");
            }
        }
        else
        {
            circleFillState = Mathf.Clamp01(circleFillState - fillSpeed * Time.deltaTime);
            progressCircleImage.fillAmount = circleFillState;

            float newAlpha = Mathf.Clamp01(holdToSkipImage.color.a - alphaSpeed * Time.deltaTime);
            SetAlphaAndFill(newAlpha);

            if (holdToSkipImage.color.a == 0)
            {
                textAnimator.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))            // Works well with keyboard...
        {
            textAnimator.enabled = false;
            isHolding = true;
            holdStartTime = Time.time;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            isHolding = false;
        }
    }

    public void HoldToSkip(InputAction.CallbackContext context) // ... Cannot hold with the controller for some reason
    {
        if(context.performed)
        {
            textAnimator.enabled = false;
            isHolding = true;
            holdStartTime = Time.time;
        }
        else if(context.canceled)
        {
            isHolding = false;
        }
    }

    void SetAlphaAndFill(float alpha)   // Fill circle and its alpha with its text
    {
        Color textColor = holdToSkipImage.color;
        textColor.a = alpha;
        holdToSkipImage.color = textColor;

        float newFillAmount = Mathf.Clamp01(progressCircleImage.fillAmount + fillSpeed * Time.deltaTime);
        progressCircleImage.fillAmount = newFillAmount;
        progressCircleImage.color = new Color(progressCircleImage.color.r, progressCircleImage.color.g, progressCircleImage.color.b, alpha);
    }

    public void LoadMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SceneManager.LoadScene("SCN_MainMenu");
        }
    }
}