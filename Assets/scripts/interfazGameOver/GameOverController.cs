using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverController : MonoBehaviour
{
    public RectTransform textoGameOver;
    public CanvasGroup canvasGroup;
    public Button botonReintentar;
    public Button botonMenu;

    private Vector3 escalaOriginalReintentar;
    private Vector3 escalaOriginalMenu;

    void Start()
    {
        botonReintentar.onClick.AddListener(ReintentarJuego);
        botonMenu.onClick.AddListener(VolverAlMenu);

        escalaOriginalReintentar = botonReintentar.transform.localScale;
        escalaOriginalMenu = botonMenu.transform.localScale;

        // A�adir efectos a botones
        AddButtonEvents(botonReintentar, escalaOriginalReintentar);
        AddButtonEvents(botonMenu, escalaOriginalMenu);

        StartCoroutine(AnimarTextoGameOver());
    }

    void ReintentarJuego()
    {
        SceneManager.LoadScene("mainMenuScene"); // Cambia por tu escena
    }

    void VolverAlMenu()
    {
        SceneManager.LoadScene("instruccionesEnlaces"); // Cambia por tu escena
    }

    IEnumerator AnimarTextoGameOver()
    {
        textoGameOver.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            textoGameOver.localScale = Vector3.Lerp(Vector3.zero, new Vector3(1.2f, 1.2f, 1f), t);
            canvasGroup.alpha = t;
            yield return null;
        }

        textoGameOver.localScale = Vector3.one;

        // Comienza parpadeo
        StartCoroutine(BlinkTextoGameOver());
    }

    IEnumerator BlinkTextoGameOver()
    {
        while (true)
        {
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 1f;
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
                yield return null;
            }

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 1f;
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }
        }
    }

    void AddButtonEvents(Button button, Vector3 escalaOriginal)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // OnPointerEnter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) =>
        {
            button.transform.localScale = escalaOriginal * 1.1f;
        });
        trigger.triggers.Add(entryEnter);

        // OnPointerExit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) =>
        {
            button.transform.localScale = escalaOriginal;
        });
        trigger.triggers.Add(entryExit);

        // OnPointerDown (click)
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) =>
        {
            button.transform.localScale = escalaOriginal * 0.95f;
        });
        trigger.triggers.Add(entryDown);

        // OnPointerUp
        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) =>
        {
            button.transform.localScale = escalaOriginal * 1.1f;
        });
        trigger.triggers.Add(entryUp);
    }
}
