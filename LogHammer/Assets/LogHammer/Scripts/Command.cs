using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command {
    public abstract void Execute(Character character);
}


public class MoveToCommand : Command
{  
    public override void Execute(Character character)
    {
        character.moveToPosition();
    }
}

public class MoveUpCommand : Command
{
    public override void Execute(Character character)
    {
        character.moveUp();
    }
}

public class MoveDownCommand : Command
{
    public override void Execute(Character character)
    {
        character.moveDown();
    }
}

public class MoveLeftCommand : Command
{
    public override void Execute(Character character)
    {
        character.moveLeft();
    }
}

public class MoveRightCommand : Command
{
    public override void Execute(Character character)
    {
        character.moveRight();
    }
}

public class StopMovementCommand : Command
{
    public override void Execute(Character character)
    {
        character.stopMovement();
    }
}