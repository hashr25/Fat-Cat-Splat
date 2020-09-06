using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoCodeLibrary : MonoBehaviour
{
    public static PromoCodeLibrary promoCodeLibrary;

    //public List<PromoCode> promoCodes = new List<PromoCode>();
    public Dictionary<string, PromoCode> promoCodeDictionary = new Dictionary<string, PromoCode>();




    public void Start()
    {
        if (promoCodeLibrary == null)
        {
            DontDestroyOnLoad(gameObject);
            Load();
            promoCodeLibrary = this;
        }
        else if (promoCodeLibrary != this)
        {
            Destroy(gameObject);
        }
    }

    private void SetupDictionary(List<PromoCode> userPromoCodes)
    {
        promoCodeDictionary.Clear();

        foreach (PromoCode code in userPromoCodes)
        {
            promoCodeDictionary.Add(code.code, code);
        }
    }

    private void GetPromoCodesForUser(User user)
    {
        PromoCodeWebService service = new PromoCodeWebService();
        StartCoroutine(service.GetPromoCodesForUser(user.userName, SetupDictionary));
    }

    private void DecideAddUser(bool userExists)
    {
        PromoCodeWebService service = new PromoCodeWebService();
        User user = new User();
        if (!userExists)
        {
            StartCoroutine(service.AddUser(user, GetPromoCodesForUser));
        }
        else
        {
            GetPromoCodesForUser(user);
        }
    }



    public void Load()
    {
        //Load from Web API
        User user = new User();
        PromoCodeWebService service = new PromoCodeWebService();
        StartCoroutine(service.DoesUserExist(user.userName, DecideAddUser));
    }

    public void UsePromoCode(string code)
    {
        code = code.ToUpper();
        PromoCode promoCode;
        User user = new User();
        promoCodeDictionary.TryGetValue(code, out promoCode);

        if (promoCode != null)
        {
            if (!promoCode.hasBeenUsed)
            {
                bool promoCodeHasBeenUsed = false;

                if (promoCode.currencyType == "Coin")
                {

                    PromoCodeWebService service = new PromoCodeWebService();
                    StartCoroutine
                    (
                        service.UserUsePromoCode(user.userName, code, () =>
                        {
                            promoCodeHasBeenUsed = true;
                            GameController.gameController.coins += promoCode.currencyGiven;
                            GameController.gameController.Save();
                            Load();

                            if (promoCodeHasBeenUsed)
                            {
                                DialogSpawner.dialogSpawner.SpawnErrorDialog("The " + promoCode.description + " promo code has been used!");
                            }
                        })
                    );


                }
                else if (promoCode.currencyType == "Star") // Add as many of these as you want
                {
                    //No star yet, but who knows?!
                }
                else
                {
                    //Maybe throw an error here later?
                }




            }
            else
            {
                DialogSpawner.dialogSpawner.SpawnErrorDialog("That promo code has already been used.");
            }
        }
        else
        {
            DialogSpawner.dialogSpawner.SpawnErrorDialog("That is not a valid promo code");
        }
    }




    //[Serializable]
    //public class PromoCode
    //{
    //    public int promoCodeId { get; set; }
    //    public string code { get; set; }
    //    public string description { get; set; }
    //    public string currencyType { get; set; }
    //    public int currencyGiven { get; set; }
    //    public bool hasBeenUsed { get; set; }


    //    public PromoCode(int promoCodeId, string code, string description, string currencyType, int currencyGiven, bool hasBeenUsed)
    //    {
    //        this.promoCodeId = promoCodeId;
    //        this.code = code;
    //        this.description = description;
    //        this.currencyType = currencyType;
    //        this.currencyGiven = currencyGiven;
    //        this.hasBeenUsed = hasBeenUsed;
    //    }
    //}

    //[Serializable]
    //class PromoCodeSaveData
    //{
    //    public List<PromoCode> promoCodes;

    //    public PromoCodeSaveData(List<PromoCode> promoCodes)
    //    {
    //        this.promoCodes = promoCodes;
    //    }
    //}

    //  ======================================

    //public static Dictionary<string, PromoCode> promoCodes = new Dictionary<string, PromoCode>()
    //{
    //    { "12345", new PromoCode() { promoCode = "12345", displayName = "Test Code", action = () => { GameController.gameController.coins += 500; } } }
    //}


}
