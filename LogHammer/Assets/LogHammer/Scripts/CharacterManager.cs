using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class creates instances of both player and keeps them here in array
public class CharacterManager : MonoBehaviour {

    //player side
    [SerializeField]
    private bool m_humanPlayerShouldBeOnLeft = false;
    //prefab of character
    [SerializeField]
    private GameObject m_characterPrefab;
    //parent transform of both the characcters
    [SerializeField]
    private Transform m_charactersParent;
    //array which will save all the characters
    private Character[] m_charactersArray;

	// Use this for initialization
	void Awake () {
        //allocate memory to charactersArray
        m_charactersArray = new Character[Constant.TotalNumberOfPlayers];

        //0 is left character and 1 is right character
        //This loop will create characters, positions and it will also assign them parent
        for(int i = 0; i < Constant.TotalNumberOfPlayers; i++)
        {
            Character character = null;
            if(i == (int)Character.CharacterSide.Left)
            {
                character = Instantiate<GameObject>(m_characterPrefab, m_characterPrefab.transform.position, Quaternion.identity,m_charactersParent).GetComponent<Character>();
                if (m_humanPlayerShouldBeOnLeft) { character.TypeOfCharacter = Character.CharacterType.Human; }
                else { character.TypeOfCharacter = Character.CharacterType.AI; }
            }
            else if(i == (int)Character.CharacterSide.Right)
            {
                Vector3 characterPosition = new Vector3(-m_characterPrefab.transform.position.x, m_characterPrefab.transform.position.y, m_characterPrefab.transform.position.z);
                character = Instantiate<GameObject>(m_characterPrefab, characterPosition, Quaternion.identity, m_charactersParent).GetComponent<Character>();
                if (!m_humanPlayerShouldBeOnLeft) { character.TypeOfCharacter = Character.CharacterType.Human; }
                else { character.TypeOfCharacter = Character.CharacterType.AI; }
            }

            //Define which side character will be
            character.SideOfCharacter = (Character.CharacterSide)i;
            //assign gameobject name for differentiation
            character.gameObject.name = "Character" + ((Character.CharacterSide)i).ToString();
            //add character to array
            m_charactersArray[i] = character;
        }
	}
	
}
