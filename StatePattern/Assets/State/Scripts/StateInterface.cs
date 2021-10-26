using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StateInterface
{
    void Enter();

    void Tick();

    void FixedTick();

    void Exit();
}
