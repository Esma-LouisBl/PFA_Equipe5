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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _name.text = contactList[_index].name;
    }
}
