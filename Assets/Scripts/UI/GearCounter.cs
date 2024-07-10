using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearCounter : MonoBehaviour
{
    private int currentGears;
    public int CurrentGears // Property. If value changes - sets text to number of the value
    {
        get { return currentGears; }
        set { currentGears = value;
              text.text = currentGears + "";
        }
    }
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }
    public void AddGears(int amount)
    {
        CurrentGears += amount;
    }
    public void SubtractGears(int amount)
    {
        CurrentGears -= amount;
    }
}
