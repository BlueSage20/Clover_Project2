using UnityEngine;
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

public class Effect
{
	EffectType type;
	int amonut;
	bool isFriendly;
}

public class Card{

	public CardType type;
	public string cardName;
	public int atk;
	public int def;
	public string effect1;
	public string effect2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Card(string typ, string nme, string at, string df, string eff1, string value1, string eff2, string value2)
	{
		//type = typ;
		cardName = nme;
		atk = int.Parse (at);
		def = int.Parse (df);
		effect1 = eff1;
		effect2 = eff2;
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
}
