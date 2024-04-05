using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayClickEffect : MonoBehaviour
{
    private void Awake()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(delegate
            {
                AudioManager.Instance.PlayHitEffect();
            });
        }
    }
}

