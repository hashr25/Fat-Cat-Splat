using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterStoreUI : MonoBehaviour
{
    public GameObject player;
    public Text characterName;
    public Text purchaseButton;
    public Text coinCount;
    public Dictionary<string, RuntimeAnimatorController> sprites;

    int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        LoadSprites();

        //This resets the character to the first in the store.
        currentIndex = 0;

        //This sets the character to the current one selected in the inventory.
        string currentCharacterName = CharacterInventory.characterInventory.activeCharacter;
        if(currentCharacterName != null && sprites.Keys.ToList().Contains(currentCharacterName))
        {
            currentCharacterName = "Fat Cat";
        }
        currentIndex = sprites.Keys.ToList().IndexOf(currentCharacterName);


        UpdateSprite();
        UpdatePurchaseButton();
    }

    // Update is called once per frame
    void Update()
    {
        coinCount.text = GameController.gameController.coins.ToString();
    }

    void PrintFiles(string filename)
    {
        string[] filePaths = Directory.GetFiles(filename);
        for (int i = 0; i < filePaths.Length; ++i)
        {
            string path = filePaths[i];
            Debug.Log(System.IO.Path.GetFileName(path));
        }
    }

    private void UpdateSprite()
    {
        RuntimeAnimatorController runtimeAnimatorController = sprites.ElementAt(currentIndex).Value;
        characterName.text = runtimeAnimatorController.name;
        
        player.GetComponent<RunningPlayer>().UpdateSprite(runtimeAnimatorController);

        
    }

    private void UpdatePurchaseButton()
    {
        RuntimeAnimatorController runtimeAnimatorController = sprites.ElementAt(currentIndex).Value;
        bool characterOwned = CharacterInventory.characterInventory.UserOwnsCharacter(runtimeAnimatorController.name);

        //Character already owned
        if (characterOwned)
        {
            //Character already selected
            if(CharacterInventory.characterInventory.activeCharacter == runtimeAnimatorController.name)
            {
                purchaseButton.text = "Current";
            }
            else //Character not selected
            {
                purchaseButton.text = "Select";
            }
        }
        else //Character not owned
        {
            purchaseButton.text = "Purchase";
        }
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void OnLeftArrowClicked()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        else if (currentIndex == 0)
        {
            currentIndex = sprites.Count - 1;
        }

        UpdateSprite();
        UpdatePurchaseButton();
    }

    public void OnRightArrowClicked()
    {
        currentIndex = (currentIndex + 1) % (sprites.Count);

        UpdateSprite();
        UpdatePurchaseButton();
    }

    public void OnPurchaseButtonClicked()
    {
        switch (purchaseButton.text)
        {
            case "Purchase":
                CharacterInventory.characterInventory.PurchaseCharacter(characterName.text);
                UpdatePurchaseButton();
                break;
            case "Select":
                CharacterInventory.characterInventory.ChangeCharacter(characterName.text);
                UpdatePurchaseButton();
                break;
            default:
                //Do nothing
                break;

        }
    }

    public void OnDonateButtonClicked()
    {
        if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
        string donateUrl = "https://bit.ly/HotHashGames";

        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            //Find some other way to open URLS so that it 
            Application.OpenURL(donateUrl);
        }
        else
        {
            Application.OpenURL(donateUrl);
        }
    }

    private void LoadSprites()
    {
        sprites = new Dictionary<string, RuntimeAnimatorController>();

        //PrintFiles("Resources/Assets/Animators");
        List<RuntimeAnimatorController> spriteArray = Resources.LoadAll<RuntimeAnimatorController>("Animators").ToList();

        foreach(RuntimeAnimatorController rac in spriteArray)
        {
            sprites.Add(rac.name, rac);
        }
    }
}
