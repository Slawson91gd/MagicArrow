using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private PlayerControllerData PlayerData { get; set; }

    public GameObject CurrentRang { get; private set; }
    public GameObject NormalRang { get; private set; }
    public GameObject FireRang { get; private set; }
    public GameObject IceRang { get; private set; }
    //public GameObject WindRang { get; private set; }
    public GameObject LightningRang { get; private set; }
    public GameObject ObsidianRang { get; private set; }

    public Image HealthBar { get; private set; }
    public Image PotionBar { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        PlayerData = FindObjectOfType<PlayerController>().PlayerData;

        NormalRang = gameObject.transform.GetChild(0).gameObject;
        FireRang = NormalRang.transform.Find("Rang_Fire").gameObject;
        IceRang = NormalRang.transform.Find("Rang_Ice").gameObject;
        // Wind boomerang ref here
        LightningRang = NormalRang.transform.Find("Rang_Lightning").gameObject;
        ObsidianRang = NormalRang.transform.Find("Rang_Obsidian").gameObject;

        HealthBar = GameObject.Find("Healthbar_fill").GetComponent<Image>();
        PotionBar = GameObject.Find("Potion_fill").GetComponent<Image>();

        LightningRang.SetActive(false);
        IceRang.SetActive(false);
        ObsidianRang.SetActive(false);
        FireRang.SetActive(false);
        CurrentRang = NormalRang;

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

    public void UpdateBoomerangType(Boomerang boomerang)
    {
        switch (boomerang.Type)
        {
            case Boomerang.BoomerangTypes.NORMAL:
                SetBoomerang(NormalRang);
                break;

            case Boomerang.BoomerangTypes.FIRE:
                SetBoomerang(FireRang);
                break;

            case Boomerang.BoomerangTypes.ICE:
                SetBoomerang(IceRang);
                break;

            case Boomerang.BoomerangTypes.SHOCK:
                SetBoomerang(LightningRang);
                break;

            case Boomerang.BoomerangTypes.OBSIDIAN:
                SetBoomerang(ObsidianRang);
                break;
        }
    }

    private void SetBoomerang(GameObject boomerang)
    {
        if(CurrentRang != boomerang)
        {
            if(CurrentRang != NormalRang)
            {
                CurrentRang.SetActive(false);
            }
            CurrentRang = boomerang;
            if (CurrentRang != NormalRang)
            {
                CurrentRang.SetActive(true);
            }
        }
    }
}
