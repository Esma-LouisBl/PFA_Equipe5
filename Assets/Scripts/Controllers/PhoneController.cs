using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour
{
    public List<PhoneContact> contactList;
    private int _index;

    [SerializeField]
    private TextMeshProUGUI _name;
    void Start()
    {
        
    }

    void Update()
    {
        _name.text = contactList[_index].name;
    }
}
