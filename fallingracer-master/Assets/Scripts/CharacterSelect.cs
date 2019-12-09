using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] Toggle selectFemale;
    [SerializeField] Toggle selectMale;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject virtualCamera2;

    private void Awake()
    {
        if (PlayerPrefs.GetFloat("Character Selected", -999) == -999)
            PlayerPrefs.SetFloat("Character Selected", 0f);
    }

    public void OnSelectFemale()
    {
        if (selectFemale.isOn)
        {
            selectMale.isOn = false;
        }
        else
            selectMale.isOn = true;
    }

    public void OnSelectMale()
    {
        if (selectMale.isOn)
        {
            selectFemale.isOn = false;
        }
        else
            selectFemale.isOn = true;
    }

    public void OnClickDone()
    {
        if(selectFemale.isOn)
            PlayerPrefs.SetFloat("Character Selected", 0f);
        else
            PlayerPrefs.SetFloat("Character Selected", 1f);

        mainMenuCanvas.SetActive(true);
        virtualCamera2.SetActive(false);
        gameObject.SetActive(false);

        Debug.Log(PlayerPrefs.GetFloat("Character Selected"));
    }

}
