using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for every command that needs to be executed on character
public abstract class Command {
    //This method should overriden compulsorily in all the class who inherits Command class.
    public abstract void Execute(Character character);
}

//This command is used to Move character to the perticular position on the screen location acccording to touch position
public class MoveToCommand : Command
{  
    //it will execute the command
    public override void Execute(Character character)
    {
        character.moveToPosition();
    }
}
//This command will move character to up
public class MoveUpCommand : Command
{
    //it will execute the command
    public override void Execute(Character character)
    {
        character.moveUp();
    }
}
//This command will move character to down
public class MoveDownCommand : Command
{
    //it will execute the command
    public override void Execute(Character character)
    {
        character.moveDown();
    }
}
//This command will move character to user's left side
public class MoveLeftCommand : Command
{
    //it will execute the command
    public override void Execute(Character character)
    {
        character.moveLeft();
    }
}
//This command will move character to user's right side
public class MoveRightCommand : Command
{
    //it will execute the command
    public override void Execute(Character character)
    {
        character.moveRight();
    }
}
//This command will stop character movement
public class StopMovementCommand : Command
{
    //it will execute the command
    public override void Execute(Character character)
    {
        character.stopMovement();
    }
}