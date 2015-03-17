/*
 * Sung Won Choi's Player Class
 * Developed on 2015 for GDD 2 Group Project
 * For Academic Purposes
 * Team Name: Clover
 * Team Members: Kevin Granger, James Sasson, Hanna Doerr. Sung Won Choi
 * Contributors to Class: James Sasson, Kevin Granger
*/

using UnityEngine;
using System.Collections;

public class Player {
    // CONSTANTS
    public const int MAX_HEALTH = 20;
    public const int BASE_DEFENSE = 1;
    // public const int MAX_MOVEMENT_POSSIBLE = 0;

    // Player Global Variables
    public int Health;
    public int defense;
	public string Name;

	public Deck deck;
	public Card[] hand = new Card[3];
	public Transform uiHand;

	public delegate void logger(string msg);

	logger Log;

	public GameObject cardPrototype;

	// Use this for initialization
	public Player (string nme, Transform uiHand, GameObject cardPrototype, logger log) {
		deck = new Deck ();
		Name = nme;
		this.uiHand = uiHand;
		this.cardPrototype = cardPrototype;
		this.Log = log;
		
		// if player has no health initially, start player with max health.
		if(Health <= 0)
		{
			Health = MAX_HEALTH;
		}
		// give the player a starting defense value if they start with none.
		if (defense <= 0)
		{
			defense = BASE_DEFENSE;
		}
		
		for(int i = 0; i < 3; i++)
		{
			var card = deck.Draw();
			hand[i] = card;
			card.setupUICard(cardPrototype, uiHand, this);

			Log("Player " + nme + " draws " + hand[i].cardName);
		}
		

	}

    // meant to handle when the player is struck by enemy card. 
    public void TakeDamage(int i, int pierceValue)
    {
		var p = this;
        if (p.defense > 0)
        {
            // check for pierce
            i -= p.defense;
			if(i < 0){i = 0;}
        }
        else
        {
            p.Health -= i; // Subtracts health depending on card

			/* Migrated to Controller Class
            if (p.Health <= 0)
            {
                // Code here to handle death. 
            }
            */
        }
		Log ("Player " + Name + " Takes " + i + " damage!");
        p.Health -= i;
    }
    // Defend class. Happens when player throws up a defense card. 
    public void Defend(int i)
    {
		Log ("Player " + Name + " Gains " + i + " defense!");
		var p = this;
		p.defense = i; 
    }
    // Adds random event based on card usage. (LOTS OF HANDLERS)
    public void AddEffect(Card effectCard) // accept int enum
    {
        // What we want to do here is figure out a way to implement effects on the player based on effect cards played by the opposing factor.
        switch (effectCard.effect1)
        {
            // Just for note, We may want to seperate these effects into seperate methods later.
            // It may end up being easier to implement this way in the long run. Gonna need to test this to be sure.
            // add code here to handle enum
            /*
             * Pierce - ignore defense
             * block - stops use of certain cards
             * heal - heals user
             * counter - if attacked, does counter damage
             * poison - poisons target (intensity level)
             * cure - heals all status ailments
             * burn - burns target (deals damage each turn and lowers atk damage)
             * boost - raises power of next attack by 5
             * bind - Opponent can't move next turn
             * */
            // Actual code will come next playtest. For now, we'll just let the player know something is happening.
		case EffectType.pierce:
			Log("Player " + Name + " is now affected with pierce!");
			break;
		case EffectType.block:
			Log("Player " + Name + " is now affected by a card block!");
			break;
		case EffectType.heal: 
			Log("Player " + Name + " is Healed!");
			break;
		case EffectType.counter:
			Log("Player " + Name + " is now ready to counter!");
			break;
		case EffectType.poison:
			Log("Player " + Name + " is now affected with poison!");
			break;
		case EffectType.cure:
			Log("Player " + Name + " is cured!");
			break;
		case EffectType.burn:
			Log("Player " + Name + " is now affected with burn!");
			break;
		case EffectType.boost:
			Log("Player " + Name + "'s attack is boosted!");
			break;
		case EffectType.bind:
			Log("Player " + Name + " is now affected with bind!");
			break;
        }
		switch (effectCard.effect2)
		{
			// Just for note, We may want to seperate these effects into seperate methods later.
			// It may end up being easier to implement this way in the long run. Gonna need to test this to be sure.
			// add code here to handle enum
			/*
             * Pierce - ignore defense
             * block - stops use of certain cards
             * heal - heals user
             * counter - if attacked, does counter damage
             * poison - poisons target (intensity level)
             * cure - heals all status ailments
             * burn - burns target (deals damage each turn and lowers atk damage)
             * boost - raises power of next attack by 5
             * bind - Opponent can't move next turn
             * */
			// Actual code will come next playtest. For now, we'll just let the player know something is happening.
		case EffectType.pierce:
			Log("Player " + Name + " is now affected with pierce!");
			break;
		case EffectType.block:
			Log("Player " + Name + " is now affected by a card block!");
			break;
		case EffectType.heal: 
			Log("Player " + Name + " is Healed!");
			break;
		case EffectType.counter:
			Log("Player " + Name + " is now ready to counter!");
			break;
		case EffectType.poison:
			Log("Player " + Name + " is now affected with poison!");
			break;
		case EffectType.cure:
			Log("Player " + Name + " is cured!");
			break;
		case EffectType.burn:
			Log("Player " + Name + " is now affected with burn!");
			break;
		case EffectType.boost:
			Log("Player " + Name + "'s attack is boosted!");
			break;
		case EffectType.bind:
			Log("Player " + Name + " is now affected with bind!");
			break;
		}
    }
	public Card playCard(int cardToPlay)
	{
		// Player will choose a card from their hand.
		// This will eventually be associated with a button press on the card. 
		Card cardChoice = null;

		switch (cardToPlay) {
		case 1:
			cardChoice = hand[0];
			break;
		case 2:
			cardChoice = hand[1];
			break;
		case 3:
			cardChoice = hand[2];
			break;
		}
		return cardChoice;
	}

}