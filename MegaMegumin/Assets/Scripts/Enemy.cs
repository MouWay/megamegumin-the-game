using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void MoveToPlayer();

    public abstract void ReactToPlayer(); //Detect player inside reaction area

    public abstract void AttackPlayer(); //Behavior if player is inside attack area


}
