using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoCodeLibrary : MonoBehaviour
{
    public static PromoCodeLibrary promoCodeLibrary;

    public List<PromoCode> promoCodes = new List<PromoCode>();
    public Dictionary<string, PromoCode> promoCodeDictionary = new Dictionary<string, PromoCode>();

    


    public void Start()
    {
        

        if (promoCodeLibrary == null)
        {
            DontDestroyOnLoad(gameObject);
            SetupDictionary();
            promoCodeLibrary = this;
        }
        else if (promoCodeLibrary != this)
        {
            Destroy(gameObject);
        }
    }


    public void Save()
    {
        PromoCodeSaveData data = new PromoCodeSaveData(promoCodes);
        DataIO.Save<PromoCodeSaveData>("PromoCodeSaveData", data);
    }

    public void Load()
    {
        PromoCodeSaveData data = DataIO.Load<PromoCodeSaveData>("PromoCodeSaveData");

        if (data != null)
        {
            promoCodes = data.promoCodes;

            foreach (var code in promoCodes)
            {
                promoCodeDictionary.Add(code.promoCode, code);
            }
        }

        
    }

    private void SetupDictionary()
    {
        Load();

        if(promoCodes.Count == 0 || promoCodeDictionary.Count == 0) // No promo codes?! Create a test one!
        {
            PromoCode hashPackPromo = new PromoCode("HASHPACK", "LuLaGirls Promo", () =>
            {
                GameController.gameController.coins += 1000;
                GameController.gameController.Save();
            }, false);

            promoCodes.Add(hashPackPromo);
            promoCodeDictionary.Add(hashPackPromo.promoCode, hashPackPromo);

            PromoCode robeyTech = new PromoCode("ROBEYROCKS", "Robeytech Appreciation", () =>
            {
                GameController.gameController.coins += 500;
                GameController.gameController.Save();
            }, false);

            promoCodes.Add(robeyTech);
            promoCodeDictionary.Add(robeyTech.promoCode, robeyTech);

            PromoCode welcomeBackPromo = new PromoCode("WELCOME", "Welcome Back", () =>
            {
                GameController.gameController.coins += 500;
                GameController.gameController.Save();
            }, false);

            promoCodes.Add(welcomeBackPromo);
            promoCodeDictionary.Add(welcomeBackPromo.promoCode, welcomeBackPromo);

            PromoCode newbiePromo = new PromoCode("NEWBIE", "Welcome Pack", () =>
            {
                GameController.gameController.coins += 500;
                GameController.gameController.Save();
            }, false);

            promoCodes.Add(newbiePromo);
            promoCodeDictionary.Add(newbiePromo.promoCode, newbiePromo);

            PromoCode myLovePromo = new PromoCode("ILOVEYOUSEXY", "I love you beautiful!", () =>
            {
                GameController.gameController.coins += 500;
                GameController.gameController.Save();
            }, false);

            promoCodes.Add(myLovePromo);
            promoCodeDictionary.Add(myLovePromo.promoCode, myLovePromo);

            Save();
        }
    }

    public bool IsValidPromoCode(string promoCode)
    {
        bool isValidPromoCode = false;

        PromoCode code;
        promoCodeDictionary.TryGetValue(promoCode, out code);

        if (code != null)
        {
            isValidPromoCode = true;
        }

        return isValidPromoCode;
    }

    public bool HasUserUsedPromoCode(string promoCode)
    {
        bool hasUserUsedPromoCode = false;

        PromoCode code;
        promoCodeDictionary.TryGetValue(promoCode, out code);

        if(code != null)
        {
            hasUserUsedPromoCode = code.hasBeenUsed;
        }

        return hasUserUsedPromoCode;
    }

    public void UsePromoCode(string promoCode)
    {
        promoCode = promoCode.ToUpper();

        if (IsValidPromoCode(promoCode))
        {
            if (!HasUserUsedPromoCode(promoCode))
            {
                PromoCode code;
                promoCodeDictionary.TryGetValue(promoCode, out code);

                promoCodes.Remove(code);
                promoCodeDictionary.Remove(code.promoCode);

                code.action.Invoke();
                code.hasBeenUsed = true;


                promoCodes.Add(code);
                promoCodeDictionary.Add(code.promoCode, code);

                Save();

                DialogSpawner.dialogSpawner.SpawnErrorDialog("The " + code.displayName + " Promo code has been used!");
            }
            else
            {
                DialogSpawner.dialogSpawner.SpawnErrorDialog("That promo code has already been used.");
            }
        } else
        {
            DialogSpawner.dialogSpawner.SpawnErrorDialog("That is not a valid promo code");
        }
    }

    


    [Serializable]
    public class PromoCode
    {
        public string promoCode;
        public string displayName;
        public Action action;
        public bool hasBeenUsed;
        

        public PromoCode(string promoCode, string displayName, Action action, bool hasBeenUsed)
        {
            this.promoCode = promoCode;
            this.displayName = displayName;
            this.action = action;
            this.hasBeenUsed = hasBeenUsed;
        }
    }

    [Serializable]
    class PromoCodeSaveData
    {
        public List<PromoCode> promoCodes;

        public PromoCodeSaveData(List<PromoCode> promoCodes)
        {
            this.promoCodes = promoCodes;
        }
    }

    //  ======================================

    //public static Dictionary<string, PromoCode> promoCodes = new Dictionary<string, PromoCode>()
    //{
    //    { "12345", new PromoCode() { promoCode = "12345", displayName = "Test Code", action = () => { GameController.gameController.coins += 500; } } }
    //}


}
