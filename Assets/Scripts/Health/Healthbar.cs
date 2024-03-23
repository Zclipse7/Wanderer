using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image TotalHealthbar;
    [SerializeField] private Image CurrentHealthbar;

    private void Start()
    {
        TotalHealthbar.fillAmount = playerHealth.CurrentHealth / 10;
    }

    private void Update()
    {
        CurrentHealthbar.fillAmount = playerHealth.CurrentHealth / 10;


    }
}
