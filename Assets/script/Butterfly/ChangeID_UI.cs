using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeID_UI : MonoBehaviour
{
    [SerializeField] TMP_InputField field;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        field.text = CheckID.playerID;
    }
}
