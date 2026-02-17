using System.Collections;
using UnityEngine;

public enum NPCHealthStatus { Healthy, Sick, Inmune }
public class NPCHealthState : MonoBehaviour
{
    public NPCHealthStatus currentHealthStatus = NPCHealthStatus.Healthy;
    [SerializeField] private float rageAmount = 27; // 27 buen valor.
    [SerializeField] private float virusAmount = 1; // 1 buen valor.
    private int maxHealth = 100;
    private int health = 100; //Si quiero enfermarlo, bajo este valor a 0.

    [Header("COLOR SETTINGS")]
    [SerializeField]private SpriteRenderer spriteRenderer;
    private Color originalColor = Color.white;
    private Color sickColor = Color.green;
    private Color rageColor = Color.red;

    //private void Start()
    //{
    //    Invoke("Sick", Random.Range(7f, 20f));
    //}
    //private void Sick()
    //{
    //    health = 0;
    //}

    private void OnTriggerEnter2D(Collider2D tri)
    {
        if (tri.CompareTag("Syringe"))
        {
            switch(currentHealthStatus)
            {
                case NPCHealthStatus.Healthy:
                    StartCoroutine(ColorEffectCoroutine(originalColor, rageColor));
                    RageUp();
                    break;
                case NPCHealthStatus.Sick:
                    StartCoroutine(ColorEffectCoroutine(sickColor, originalColor));
                    int healAmount = tri.GetComponent<Syringe>().healAmount;
                    Healing(healAmount);
                    break;
            }
        }
    }


    private void Update()
    {
        switch(currentHealthStatus)
        {
            case NPCHealthStatus.Healthy:
                HandleHealthy();
                break;
            case NPCHealthStatus.Sick:
                HandleSick();
                break;
            case NPCHealthStatus.Inmune:
                HandleInmune();
                break;
        }
    }
    private void HandleHealthy() //Lógica para cuando el NPC está saludable
    {
        if(health <= 0)
        {
            currentHealthStatus = NPCHealthStatus.Sick;
            MakeSick();
        }
    }
    private void HandleSick() //Lógica para cuando el NPC está enfermo
    {
        if(health >= maxHealth)
        {
            currentHealthStatus = NPCHealthStatus.Healthy;
            MakeHealthy();
        }
    }
    private void HandleInmune() //Lógica para cuando el NPC es inmune
    {
        // Aquí puedes agregar lógica específica para el estado inmune
    }
    public void MakeSick()
    {
        health = 0;
        spriteRenderer.color = sickColor;
        GameEvents.VirusUpdate(virusAmount);
    }
    public void MakeHealthy()
    {
        StartCoroutine(ColorEffectCoroutine(originalColor, sickColor));
        GameEvents.VirusUpdate(-virusAmount); // Disminuye el nivel de virus al curar al NPC
    }
    private void Healing(int healAmount)
    {
        health = Mathf.Clamp(health + healAmount, 0, maxHealth);
    }
    private void RageUp()
    {
        GameEvents.RageUp(rageAmount);
    }
    private IEnumerator ColorEffectCoroutine(Color currentColor, Color bilkColor)
    { 
        spriteRenderer.color = bilkColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = currentColor;
    }
}
