using UnityEngine;

public class CrystalButton : MonoBehaviour
{
    [SerializeField] private PuzzleElement pe;

    private SpriteRenderer buttonSprite;

    [SerializeField] private Sprite active;
    [SerializeField] private Sprite inactive;


    private void Start()
    {
        pe = new PuzzleElement(gameObject);
        buttonSprite = GetComponent<SpriteRenderer>();
        buttonSprite.sprite = active;

        pe.isActive = true;
        pe.isTriggered = false;
        pe.light = transform.GetChild(0).gameObject;
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BoomerangObj>() != null)
        {
            pe.isTriggered = true;
            pe.HandleTriggered();
            buttonSprite.sprite = inactive;
            pe.light.SetActive(false);
        }
    }
}
