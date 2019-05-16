using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    [SerializeField]
    Text Text;

    void Update()
    {
        Text.text = DateTime.Now.ToString("HH:mm:ss");
    }
}
