using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class creates instances of both player and keeps them here in array
public class CharacterManager : MonoBehaviour {

    //player side
    public bool myPlayerShouldBeOnLeft = false;
    //prefab of character
    public GameObject characterPrefab;
    //parent transform of both the characcters
    public Transform charactersParent;
    //array which will save all the characters
    private Character[] charactersArray;

	// Use this for initialization
	void Awake () {
        //allocate memory to charactersArray
        charactersArray = new Character[Constant.PLAYER_NUMBER];

        //0 is left character and 1 is right character
        //This loop will create characters, positions and it will also assign them parent
        for(int i = 0; i < Constant.PLAYER_NUMBER; i++)
        {
            Character character = null;
            if(i == (int)Character.CharacterSide.Left)
            {
                character = Instantiate<GameObject>(characterPrefab, characterPrefab.transform.position, Quaternion.identity,charactersParent).GetComponent<Character>();
                if (myPlayerShouldBeOnLeft) { character.TypeOfCharacter = Character.CharacterType.Human; }
                else { character.TypeOfCharacter = Character.CharacterType.AI; }
            }
            else if(i == (int)Character.CharacterSide.Right)
            {
                character = Instantiate<GameObject>(characterPrefab, new Vector3(-characterPrefab.transform.position.x, characterPrefab.transform.position.y, characterPrefab.transform.position.z), Quaternion.identity, charactersParent).GetComponent<Character>();
                if (!myPlayerShouldBeOnLeft) { character.TypeOfCharacter = Character.CharacterType.Human; }
                else { character.TypeOfCharacter = Character.CharacterType.AI; }
            }

            
            //Define which side character will be
            character.SideOfCharacter = (Character.CharacterSide)i;
            //assign gameobject name for differentiation
            character.gameObject.name = "Character" + ((Character.CharacterSide)i).ToString();
            //add character to array
            charactersArray[i] = character;
        }
	}
	
}
