using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelect : MonoBehaviour
{
    [SerializeField] public int Option = 1;
    private int MaxOption = 5;
    private RectTransform rectTransform;
    private Vector2 Position;

    // Start is called before the first frame update
    void Start()
    {
        Position = new Vector2(55, -66);
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            if (Option > 1)
            {
                Option--;
                Position.y += 50;
                rectTransform.anchoredPosition = Position;
            }
        }
        if (Input.GetKeyDown("down"))
        {
            if (Option < MaxOption)
            {
                Option++;
                Position.y -= 50;
                rectTransform.anchoredPosition = Position;
            }
        }
        
    }
}
