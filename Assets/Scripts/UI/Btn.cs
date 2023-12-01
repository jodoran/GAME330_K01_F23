using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour
{
    Animator anim;

    public MYType.Unit Type;
    public GameManager gm;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnBtnClick()
    {
        switch (Type)
        {
            case MYType.Unit.Sword:
                Debug.Log("Sword");
                gm.Index = 0;
                gm.SpawnUnit();
                break;
            case MYType.Unit.Guard:
                Debug.Log("Guard");
                gm.Index = 1;
                gm.SpawnUnit();
                break;
            case MYType.Unit.Range:
                Debug.Log("Range");
                gm.Index = 2;
                gm.SpawnUnit();
                break;
            case MYType.Unit.Wizard:
                Debug.Log("Wizard");
                gm.Index = 3;
                gm.SpawnUnit();
                break;
            case MYType.Unit.Castle:
                //Debug.Log("Castle");
                gm.Index = 4;
                gm.SpawnUnit();
                break;
        }
    }

    public void NotEnoughCost()
    {
        anim.SetTrigger("NotEnoughCost");
    }
}
