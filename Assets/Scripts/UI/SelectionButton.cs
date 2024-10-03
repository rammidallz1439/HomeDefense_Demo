using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

public class SelectionButton : MonoBehaviour
{
    [SerializeField] private GameObject SelectionPanel;
    public void OnSelectionButtonClicked(GameObject turret)
    {
        EventManager.Instance.TriggerEvent(new SpawnTurretEvent(turret));
        SelectionPanel.SetActive(false);
    }
}
