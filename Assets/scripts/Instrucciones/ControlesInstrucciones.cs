using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ControlesInstrucciones : MonoBehaviour
{
    public Button botonSiguiente;
    public Button botonMenu;

    private Vector3 escalaOriginalSiguiente;
    private Vector3 escalaOriginalMenu;

    void Start()
    {
        // Guardar escalas originales
        escalaOriginalSiguiente = botonSiguiente.transform.localScale;
        escalaOriginalMenu = botonMenu.transform.localScale;

        // Agregar listeners
        botonSiguiente.onClick.AddListener(IrAlJuego);
        botonMenu.onClick.AddListener(VolverAlMenu);

        // Agregar efectos de hover/click
        AddButtonEffects(botonSiguiente, escalaOriginalSiguiente);
        AddButtonEffects(botonMenu, escalaOriginalMenu);
    }

    void IrAlJuego()
    {
        SceneManager.LoadScene("NIVEL_UNO");
    }

    void VolverAlMenu()
    {
        SceneManager.LoadScene("mainMenuScene");
    }

    void AddButtonEffects(Button button, Vector3 escalaOriginal)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
        trigger.triggers = new List<EventTrigger.Entry>();

        // Hover - PointerEnter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) =>
        {
            button.transform.localScale = escalaOriginal * 1.1f;
        });
        trigger.triggers.Add(entryEnter);

        // Hover out - PointerExit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) =>
        {
            button.transform.localScale = escalaOriginal;
        });
        trigger.triggers.Add(entryExit);

        // Click down - PointerDown
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) =>
        {
            button.transform.localScale = escalaOriginal * 0.95f;
        });
        trigger.triggers.Add(entryDown);

        // Click up - PointerUp
        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) =>
        {
            button.transform.localScale = escalaOriginal * 1.1f;
        });
        trigger.triggers.Add(entryUp);
    }
}

