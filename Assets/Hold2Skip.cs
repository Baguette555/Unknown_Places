using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hold2Skip : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image progressCircle;
    [SerializeField] float holdTime = 2f; // Temps en secondes pour atteindre 100%
    [SerializeField] bool isHolding = false;
    [SerializeField] float holdStartTime;

    public LevelLoader LevelLoader;

    void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    void Update()
    {
        if (isHolding)
        {
            float holdDuration = Time.time - holdStartTime;
            float progress = Mathf.Clamp01(holdDuration / holdTime); // Progression entre 0 et 100%

            progressCircle.fillAmount = progress;

            if (holdDuration >= holdTime)
            {
                LevelLoader.LoadNextLevel();
            }
        }
    }

    void OnButtonClicked()
    {
        Debug.Log("Button clicked!");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdStartTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        progressCircle.fillAmount = 0f; // Réinitialise la progression du cercle
    }
}
