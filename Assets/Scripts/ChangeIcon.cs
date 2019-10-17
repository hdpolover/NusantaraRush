using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon : MonoBehaviour
{
    public Sprite forward;
    public Sprite stop;

    Image toChange;

    private void Start()
    {
        toChange = GetComponent<Image>();
    }

    public void ChangeButtonIcon()
    {
        if (toChange.sprite == forward)
        {
            toChange.sprite = stop;
        } else
        {
            toChange.sprite = forward;
        }
    }
}
