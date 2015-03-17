using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum CardType 
{
	attack,
	defense,
	utility
}

public enum EffectType 
{
	pierce,
	block,
	heal,
	counter,
	poison,
	cure,
	burn,
	boost,
	bind
}

public class Card{

	public CardType type;
	public string cardName;
	public int atk;
	public int def;
	public EffectType effect1;
	public int effectStrength1;
	public EffectType effect2;
	public int effectStrength2;
	public Player owner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Card(string typ, string nme, string at, string df, string eff1, string value1, string eff2, string value2)
	{
		switch(typ)
		{
		case "attack":
			type = CardType.attack;
			break;
		case "defense":
			type = CardType.defense;
			break;
		case "utility":
			type = CardType.utility;
			break;
		}
		cardName = nme;
		atk = int.Parse (at);
		def = int.Parse (df);
		//effect1 = eff1;
		//effect2 = eff2;
		switch(eff1)
		{
		case "Pierce":
			effect1 = EffectType.pierce;
			break;
		case "Block":
			effect1 = EffectType.block;
			break;
		case "Heal":
			effect1 = EffectType.heal;
			break;
		case "Counter":
			effect1 = EffectType.counter;
			break;
		case "Poison":
			effect1 = EffectType.poison;
			break;
		case "Cure":
			effect1 = EffectType.cure;
			break;
		case "Burn":
			effect1 = EffectType.burn;
			break;
		case "Boost":
			effect1 = EffectType.boost;
			break;
		case "Bind":
			effect1 = EffectType.bind;
			break;
		}

		if (value1 != null) {
			effectStrength1 = int.Parse (value1);
		}
		
		switch(eff2)
		{
		case "Pierce":
			effect2 = EffectType.pierce;
			break;
		case "Block":
			effect2 = EffectType.block;
			break;
		case "Heal":
			effect2 = EffectType.heal;
			break;
		case "Counter":
			effect2 = EffectType.counter;
			break;
		case "Poison":
			effect2 = EffectType.poison;
			break;
		case "Cure":
			effect2 = EffectType.cure;
			break;
		case "Burn":
			effect2 = EffectType.burn;
			break;
		case "Boost":
			effect2 = EffectType.boost;
			break;
		case "Bind":
			effect2 = EffectType.bind;
			break;
		}

		if (value2 != null) {
			effectStrength2 = int.Parse (value2);
		}
	}

	/// <summary>
	/// Create the UI representation of the card, place it in the proper area,
	/// and link up event handlers, information, etc.
	/// </summary>
	/// <param name="cardPrototype"></param>
	/// <param name="uiHand"></param>
	public void setupUICard(GameObject cardPrototype, Transform uiHand, Player owner)
	{
		this.owner = owner;

		var card = GameObject.Instantiate(cardPrototype) as GameObject;
		card.name = this.ToString();

		var text = card.GetComponentInChildren<Text>();
		text.text = this.ToString();

		var uiCard = card.GetComponent<UICard>();
		uiCard.card = this;
		uiCard.owner = owner;

		//GameObject.Find("GameManager").GetComponent<Controller2>().CardClicked
		card.transform.SetParent(uiHand, false);
	}

	public override string ToString()
	{
		return string.Format("{0} ({1}) {2}/{3} {4} ({5}) {6} ({7}",
		cardName, type, atk, def, effect1, effectStrength1, effect2, effectStrength2);
	}

	/*
	public static Card ReadFromCSV(string typ)
	{
		switch (typ) {
		case "attack":
			Card.type = CardType.attack;
	*/

	public CardType ReturnType()
	{
		return type;
	}

	public void AffectPlayer(/*Player user, Player target*/)
	{
		// Deal damage to the other player
		// target.takeDamage(atk);

		// Raise Shield, protect player
		// user.defend(def);

		// Use effect on either user or target
		// if(effect1 effects user){user.applyEffect(effect1}
		// if(effect2 effects target){target.applyEffect(effect1}
		// repeat for effect 2
	}
	// for the moment let's call this the theoretical Affect Player method.
}
