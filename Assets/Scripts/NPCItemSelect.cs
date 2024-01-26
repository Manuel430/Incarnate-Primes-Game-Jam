using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCItemSelect : MonoBehaviour
{
    [SerializeField] GameObject correctItem;
    [SerializeField] GameObject wrongItem_01;
    [SerializeField] GameObject wrongItem_02;
    [SerializeField] GameObject voidObject;

    [SerializeField] NPCInteract Decider;
    bool itemDeactive;

    public void Update()
    {
        if(itemDeactive)
        {
            correctItem.SetActive(false);
        }
    }

    public void ItemDecider()
    {
        if(wrongItem_01 == null)
        {
           wrongItem_01 = new GameObject ("Void");
           wrongItem_01.SetActive(false);
        }
        else if (wrongItem_02 == null)
        {
            wrongItem_02 = new GameObject ("Void");
            wrongItem_02.SetActive(false);
        }

        if(!correctItem.activeInHierarchy && !wrongItem_01.activeInHierarchy && !wrongItem_02.activeInHierarchy)
        { return; }

        if (correctItem.activeInHierarchy)
        {
            Decider.SetRightItem(correctItem.activeInHierarchy);
            //correctItem.SetActive(false);
            Destroy(correctItem.gameObject);
        }
        else if (wrongItem_01.activeInHierarchy)
        {
            Decider.SetWrongItem_01(wrongItem_01.activeInHierarchy);
        }
        else if (wrongItem_02.activeInHierarchy)
        {
            Decider.SetWrongItem_02(wrongItem_02.activeInHierarchy);
        }
    }
}
