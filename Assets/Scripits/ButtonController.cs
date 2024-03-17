using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class ButtonController : MonoBehaviour
{
    public void OpenScene(string t)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(t);
    }
}
