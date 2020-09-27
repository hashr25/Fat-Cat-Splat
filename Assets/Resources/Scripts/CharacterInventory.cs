using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterInventory : MonoBehaviour
{
    public static CharacterInventory characterInventory;

    public string activeCharacter = "Fat Cat";
    public List<string> charactersUnlocked;

    private RuntimeAnimatorController[] characterAnimators;
    private Dictionary<string, RuntimeAnimatorController> characterAnimatorDictionary;

    private Animator animator;


    public void Start()
    {
        if(charactersUnlocked.Count == 0) { charactersUnlocked.Add(activeCharacter); }
        animator = GameObject.Find("Player").GetComponent<Animator>();

        SetupDictionary();

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (characterInventory == null)
        {
            DontDestroyOnLoad(gameObject);
            Load();
            characterInventory = this;
            ChangeCharacter(activeCharacter);
        }
        else if (characterInventory != this)
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GrassChapter" || scene.name == "MistChapter")
        {
            ChangeCharacter(activeCharacter);
            GameObject player = GameObject.Find("Player");
            RuntimeAnimatorController thisAnim;

            characterAnimatorDictionary.TryGetValue(activeCharacter, out thisAnim);
            player.GetComponent<RunningPlayer>().UpdateSprite(thisAnim);
        }
        
    }

    public void Save()
    {
        CharacterInventorySaveData data = new CharacterInventorySaveData(activeCharacter, charactersUnlocked);
        DataIO.Save<CharacterInventorySaveData>("CharacterInventory", data);
    }

    public void Load()
    {
        CharacterInventorySaveData data = DataIO.Load<CharacterInventorySaveData>("CharacterInventory");

        if (data != null)
        {
            activeCharacter = data.activeCharacter;
            charactersUnlocked = data.charactersUnlocked;
        }
    }

    private void SetupDictionary()
    {
        //Gets all animators
        characterAnimators = Resources.LoadAll<RuntimeAnimatorController>("Animators");
        characterAnimatorDictionary = new Dictionary<string, RuntimeAnimatorController>();
        //Add to dictionary for easy lookup
        foreach (RuntimeAnimatorController animatorController in characterAnimators)
        {
            characterAnimatorDictionary.Add(animatorController.name, animatorController);
        }
    }

    public bool UserOwnsCharacter(string characterName)
    {
        return charactersUnlocked.Contains(characterName);
    }

    public void ChangeCharacter(string characterName)
    {
        if(animator == null)
        {
            animator = GameObject.Find("Player").GetComponent<Animator>();
        }

        if (charactersUnlocked.Contains(characterName) || SceneManager.GetActiveScene().name == "Character Store" )
        {
            try
            {
                activeCharacter = characterName;
                animator.runtimeAnimatorController = characterAnimatorDictionary[characterName];
                Save();
            }
            catch
            {
                Debug.Log("Unable to find Character Animator in dictionary: " + characterName);
            }
        }
        else
        {
            Debug.Log("Tried to apply character sprite that the user does not own.");
        }
        
    }

    public bool PurchaseCharacter(string characterName)
    {
        bool characterPurchased = false;

        if (charactersUnlocked.Contains(characterName))
        {
            //Character already unlocked.
            characterPurchased = false;
            DialogSpawner.dialogSpawner.SpawnConfirmationDialog("You already have this character!");
        }
        else
        {
            if (characterAnimatorDictionary.ContainsKey(characterName))
            {
                //Character Does exist
                if(GameController.gameController.coins >= 200)
                {
                    //They have enough money
                    GameController.gameController.coins -= 200;
                    charactersUnlocked.Add(characterName);
                    DialogSpawner.dialogSpawner.SpawnErrorDialog("Character purchased! Congratulations! You are now using " + characterName + "!");
                    characterPurchased = true;
                    ChangeCharacter(characterName);
                    Save();
                    GameController.gameController.Save();
                }
                else
                {
                    DialogSpawner.dialogSpawner.SpawnErrorDialog("Error! You can't purchase this character! You don't have enough coins! Characters cost 200 coins!");
                    characterPurchased = false;
                }
            }

        }

        return characterPurchased;
    }


    [Serializable]
    class CharacterInventorySaveData
    {
        public string activeCharacter;
        public List<string> charactersUnlocked;

        public CharacterInventorySaveData(string activeCharacter, List<string> charactersUnlocked)
        {
            this.activeCharacter = activeCharacter;
            this.charactersUnlocked = charactersUnlocked;
        }
    }
}
