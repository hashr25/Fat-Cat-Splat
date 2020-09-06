using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PromoCode
{
    public int promoCodeId;
    public string code;
    public string description;
    public string currencyType;
    public int currencyGiven;
    public bool hasBeenUsed;

    public PromoCode(int promoCodeId, string code, string description, string currencyType, int currencyGiven, bool hasBeenUsed)
    {
        this.promoCodeId = promoCodeId;
        this.code = code;
        this.description = description;
        this.currencyType = currencyType;
        this.currencyGiven = currencyGiven;
        this.hasBeenUsed = hasBeenUsed;
    }
}
