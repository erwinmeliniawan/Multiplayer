using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerName : MonoBehaviour
{
    public InputField createName;
    public Button NameBtn;

    public void OnChange()
    {
        if (createName.text.Length > 2)
        {
            NameBtn.interactable = true;
        }
        else
            NameBtn.interactable = false;
    }

    public void OnClick_SetName()
    {
        PhotonNetwork.NickName = createName.text; 
    }
}
