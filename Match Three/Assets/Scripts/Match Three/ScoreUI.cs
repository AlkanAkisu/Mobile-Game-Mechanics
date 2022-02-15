using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private IntEvent UpdateUI;

    [SerializeField] private TMP_Text text;
    [SerializeField] private int score;

    private void Awake()
    {
        score = 0;
        text.text = $"{score}";
        UpdateUI.RegisterListener(IncreaseScore);
    }

    private void IncreaseScore(int add)
    {
        score += add;
        text.text = $"{score}";
    }
}