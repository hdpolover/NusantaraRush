using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler
{
    private GameObject player;
    

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void OnPointerDown(PointerEventData data)
    {
        player.GetComponent<PlayerController>().Moving();
    }
    /*
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnPointerDownDelegate(PointerEventData data)
    {
        player.GetComponent<PlayerController>().Moving();
    }
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        et = GetComponent<EventTrigger>();
        entry = GetComponent<EventTrigger.Entry>();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = GetComponent<EventTrigger.TriggerEvent>();
        UnityAction<BaseEventData> l_callback = new UnityAction<BaseEventData>(OnSelectOption);
        entry.callback.AddListener(l_callback);
        et.triggers.Add(entry);
    }

    private void OnSelectOption(BaseEventData eventData)
    {
        player.GetComponent<PlayerController>().Moving();
    }

    IPointerDownHandler pointerDown 
    */
}
