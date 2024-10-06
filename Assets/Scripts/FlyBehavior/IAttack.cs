using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{

    public abstract float AttackTime {get; set;}
    public abstract float AttackRange {get; set;}
    public abstract float AttackAnimTime {get; set;}
    public abstract bool Ready {get;}

    public abstract bool TryAttack(GameObject enemyFly);
};
