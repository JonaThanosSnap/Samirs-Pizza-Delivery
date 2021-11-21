using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeInput : MonoBehaviour
{
    Dictionary<int, InputMode> inputCorrespondence = new Dictionary<int, InputMode>() { {0, InputMode.controller}, { 1, InputMode.keyboard }, { 2, InputMode.arduino } };

    public void changeInput(TMP_Dropdown dd)
    {
        GameLoadParameters.inputMode = inputCorrespondence[dd.value];
    }
}
