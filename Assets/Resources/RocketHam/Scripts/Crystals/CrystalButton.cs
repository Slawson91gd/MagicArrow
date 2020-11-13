using UnityEngine;

public class CrystalButton : MonoBehaviour
{
    [SerializeField] private PuzzleElement pe;

    private SpriteRenderer buttonSprite;

    [SerializeField] private Sprite active;
    [SerializeField] private Sprite inactive;


    private void Start()
    {
        buttonSprite = GetComponent<SpriteRenderer>();
        buttonSprite.sprite = active;
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
        {
            pe.isTriggered = true;
            pe.HandleTriggered();
            buttonSprite.sprite = inactive;
            pe.light.SetActive(false);
        }
    }
}
