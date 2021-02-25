using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboFollow : MonoBehaviour
{

    public ComboAttack ComboAttack;

    public void ChainComboPossible()
    {
        ComboAttack.ComboPossible();
    }

    public void ChainCombo()
    {
        ComboAttack.Combo();
    }

    public void ChainComboReset()
    {
        ComboAttack.ComboReset();
    }
}
