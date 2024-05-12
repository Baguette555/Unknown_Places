using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hold2Skip : MonoBehaviour
{
    [SerializeField] Animator textAnimator;
    [SerializeField] Image holdToSkipImage;
    [SerializeField] Image progressCircleImage;
    [SerializeField] float holdTime = 2f; // Temps en secondes pour atteindre 100%
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
            float progress = Mathf.Clamp01(holdDuration / holdTime); // Progression entre 0 et 100%
            progressCircleImage.fillAmount = progress;

            SetAlphaAndFill(progress);

            if (holdDuration >= holdTime)
            {
                LevelLoader.LoadNextLevel();
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

        if (Input.GetKeyDown(KeyCode.F))
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

    void SetAlphaAndFill(float alpha)   // Modifie l'alpha et le remplissage du cercle et du texte
    {
        Color textColor = holdToSkipImage.color;
        textColor.a = alpha;
        holdToSkipImage.color = textColor;

        float newFillAmount = Mathf.Clamp01(progressCircleImage.fillAmount + fillSpeed * Time.deltaTime);
        progressCircleImage.fillAmount = newFillAmount;
        progressCircleImage.color = new Color(progressCircleImage.color.r, progressCircleImage.color.g, progressCircleImage.color.b, alpha);
    }
}