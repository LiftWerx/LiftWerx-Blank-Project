using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodexControls : MonoBehaviour
{

    private string codexObjectName = GameManager.codexObjectName;
    private Transform codex;
    [SerializeField]
    private bool canOpenCodex = true;
    
    void Start()
    {
        codex = transform.parent.Find(codexObjectName);
        Debug.Log(codex.name);
        
    }

    public void SetCanOpenCodex(bool canOpen)
    {
        canOpenCodex = canOpen;
        if (!canOpenCodex && codex != null) codex.gameObject.SetActive(false);
    }

    public void EnableCodex()
    {
        if (canOpenCodex && codex != null)
        {
            GameManager.Instance.HideAllCodexes();

            codex.gameObject.SetActive(true);
        }
    }

}
