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

public class MoveUpCommand : Command
{

    public override void Execute(CharacterMovement characterMovement)
    {
        characterMovement.moveUp();
    }
}

public class MoveDownCommand : Command
{

    public override void Execute(CharacterMovement characterMovement)
    {
        characterMovement.moveDown();
    }
}

public class MoveLeftCommand : Command
{

    public override void Execute(CharacterMovement characterMovement)
    {
        characterMovement.moveLeft();
    }
}

public class MoveRightCommand : Command
{

    public override void Execute(CharacterMovement characterMovement)
    {
        characterMovement.moveRight();
    }
}