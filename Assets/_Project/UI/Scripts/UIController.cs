using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("MENUS")]
    [SerializeField] private GameObject virusNeutralizedMenu;
    [SerializeField] private GameObject virusOutOfControlMenu;
    [SerializeField] private GameObject playerInfectedMenu;
    [SerializeField] private GameObject ragePeopleMenu;

    private void Start()
    {
        GameEvents.OnRageOutOfControl += ShowRagePeopleMenu;
        GameEvents.OnVirusOutOfControl += ShowVirusOutOfControlMenu;
        GameEvents.OnPlayerInfected += ShowPlayerInfectedMenu;
        GameEvents.OnPlayerWin += ShowVirusNeutralizedMenu;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerWin -= ShowVirusNeutralizedMenu;
        GameEvents.OnRageOutOfControl -= ShowRagePeopleMenu;
        GameEvents.OnVirusOutOfControl -= ShowVirusOutOfControlMenu;
        GameEvents.OnPlayerInfected -= ShowPlayerInfectedMenu;
    }
    public void LoadLevel(string nameLevel)
    {
        SceneManager.LoadScene(nameLevel);
    }
    private void ShowPlayerInfectedMenu()
    {
        virusOutOfControlMenu.SetActive(false);
        virusNeutralizedMenu.SetActive(false);
        ragePeopleMenu.SetActive(false);

        playerInfectedMenu.SetActive(true);
    }
    private void ShowRagePeopleMenu()
    {
        virusOutOfControlMenu.SetActive(false);
        virusNeutralizedMenu.SetActive(false);
        playerInfectedMenu.SetActive(false);

        ragePeopleMenu.SetActive(true);
    }
    private void ShowVirusOutOfControlMenu()
    {
        virusNeutralizedMenu.SetActive(false);
        playerInfectedMenu.SetActive(false);
        ragePeopleMenu.SetActive(false);

        virusOutOfControlMenu.SetActive(true);
    }
    private void ShowVirusNeutralizedMenu()
    {
        virusOutOfControlMenu.SetActive(false);
        playerInfectedMenu.SetActive(false);
        ragePeopleMenu.SetActive(false);

        virusNeutralizedMenu.SetActive(true);
    }



}
