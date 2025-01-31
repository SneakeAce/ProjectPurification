using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider _bar;
    [SerializeField] protected TMP_Text _textMeshPro;

    public abstract void Initialize();
    public abstract void UpdateBar();
}
