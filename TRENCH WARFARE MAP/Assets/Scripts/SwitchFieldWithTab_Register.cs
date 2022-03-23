using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchFieldWithTab_Register : MonoBehaviour
{
  // Start is called before the first frame update

  public TMP_InputField usernameInput; // 0
  public TMP_InputField emailInput; // 1
  public TMP_InputField passwordInput; // 2
  public TMP_InputField confirmPasswordInput; // 3

  public int currentInput;

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Tab))
    {
      currentInput++;
      if (currentInput > 3) currentInput = 0;
      SelectInputField();
    }

    void SelectInputField()
    {
      switch (currentInput)
      {
        case 0:
          usernameInput.Select();
          break;
        case 1:
          emailInput.Select();
          break;
        case 2:
          passwordInput.Select();
          break;
        case 3:
          confirmPasswordInput.Select();
          break;
      }
    }
  }

  public void SelectUsernameInput() => currentInput = 0;
  public void SelectEmailInput() => currentInput = 1;
  public void SelectPasswordInput() => currentInput = 2;
  public void SelectConfirmPasswordInput() => currentInput = 3;
}
