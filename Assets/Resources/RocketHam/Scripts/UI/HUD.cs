using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private PlayerControllerData PlayerData { get; set; }

    public GameObject LightningRang { get; private set; }
    public GameObject IceRang { get; private set; }
    public GameObject ObsidianRang { get; private set; }
    public GameObject FireRang { get; private set; }

    public Image HealthBar { get; private set; }
    public Image PotionBar { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        PlayerData = FindObjectOfType<PlayerController>().PlayerData;

        LightningRang = GameObject.Find("Rang_Lightning");
        IceRang = GameObject.Find("Rang_Ice");
        ObsidianRang = GameObject.Find("Rang_Obsidian");
        FireRang = GameObject.Find("Rang_Fire");

        HealthBar = GameObject.Find("Healthbar_fill").GetComponent<Image>();
        PotionBar = GameObject.Find("Potion_fill").GetComponent<Image>();

        LightningRang.SetActive(false);
        IceRang.SetActive(false);
        ObsidianRang.SetActive(false);
        FireRang.SetActive(false);

        HealthBar.fillAmount = PlayerData.PlayerHealth / PlayerData.MaxPlayerHealth;
        PotionBar.fillAmount = PlayerData.PlayerPotion / PlayerData.MaxPlayerPotion;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealth(float playerHealth, float maxPlayerHealth)
    {
        HealthBar.fillAmount = playerHealth / maxPlayerHealth;
    }

    public void UpdatePotion(float playerPotion, float maxPlayerPotion)
    {
        PotionBar.fillAmount = playerPotion / maxPlayerPotion;
    }
}
