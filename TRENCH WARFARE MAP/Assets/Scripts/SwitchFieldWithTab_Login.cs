using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchFieldWithTab_Login : MonoBehaviour
{
  public TMP_InputField emailInput; // 0
  public TMP_InputField passwordInput; // 1

  public int currentInput;

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Tab))
    {
      currentInput++;
      if (currentInput > 1) currentInput = 0;
      SelectInputField();
    }

    void SelectInputField()
    {
      switch (currentInput)
      {
        case 0:
          emailInput.Select();
          break;
        case 1:
          passwordInput.Select();
          break;
      }
    }
  }

  public void SelectEmailInput() => currentInput = 0;
  public void SelectPasswordInput() => currentInput = 1;
}
