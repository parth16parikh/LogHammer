using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command {

    public abstract void Execute(CharacterMovement characterMovement);
}


public class MoveToCommand : Command
{
    
    public override void Execute(CharacterMovement characterMovement)
    {
        characterMovement.moveToPosition();
    }
} 