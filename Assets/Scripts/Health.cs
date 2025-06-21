using System.Collections;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100f;
    [SerializeField]
    private float _currentHealth;

    public TextMeshProUGUI healthUI;
    AudioSource audioSource;
    private void Start()
    {
        _currentHealth = MaxHealth;
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    public void TakeDamage(float amount)
    {
        amount = Mathf.Round(amount * 100f) / 100f; // zaokr¹glenie do 2 miejsc po przecinku
        _currentHealth -= amount;
        _currentHealth = Mathf.Max(_currentHealth, 0); // zapobiegaj ujemnemu zdrowiu

        Debug.Log($"Player took {amount} fall damage. Health: {_currentHealth}");

        if (healthUI != null)
        {
            healthUI.text = "Health: " + _currentHealth.ToString("F2");
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, MaxHealth);
        healthUI.text = "Health: " + _currentHealth.ToString("F2");
        Debug.Log($"Player healed by {amount}. Current health: {_currentHealth:F2}");
    }
    public AudioClip deathClip;
    private void Die()
    {
        audioSource.clip = deathClip;
        audioSource.PlayOneShot(deathClip);
        Debug.Log("Player died from fall.");
        var ragdoll = GetComponentInChildren<RagdollController>();
        ragdoll.ActivateRagdoll();
        StartCoroutine(OpenGameOverScreen());
        // logika œmierci
    }
    IEnumerator OpenGameOverScreen()
    {
        yield return new WaitForSeconds(3f);
        UIManager.Instance.ShowDeathScreen();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
