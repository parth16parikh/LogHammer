using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base class for every command that needs to be executed on character
/// </summary>
public abstract class Command {
    /// <summary>
    /// This method should overriden compulsorily in all the class who inherits Command class.
    /// </summary>
    public abstract void Execute(Character character);
}

/// <summary>
/// This command is used to Move character to the perticular position on the screen location acccording to touch position
/// </summary>
public class MoveToCommand : Command
{  
    //it will execute the command
    public override void Execute(Character character)
    {
        character.MoveToPosition();
    }
}

/// <summary>
/// This command will move character to up
/// </summary>
public class MoveUpCommand : Command
{
    //it will execute the command
    public override void Execute(Character character)
    {
        character.MoveUp();
    }
}

/// <summary>
/// This command will move character to down
/// </summary>
public class MoveDownCommand : Command
{
    //it will execute the command
    public override void Execute(Character character)
    {
        character.MoveDown();
    }
}

/// <summary>
/// This command will move character to user's left side
/// </summary>
public class MoveLeftCommand : Command
{
    //it will execute the command
    public override void Execute(Character character)
    {
        character.MoveLeft();
    }
}

/// <summary>
/// This command will move character to user's right side
/// </summary>
public class MoveRightCommand : Command
{
    //it will execute the command
    public override void Execute(Character character)
    {
        character.MoveRight();
    }
}

/// <summary>
/// This command will stop character movement
/// </summary>
public class StopMovementCommand : Command
{
    //it will execute the command
    public override void Execute(Character character)
    {
        character.StopMovement();
    }
}