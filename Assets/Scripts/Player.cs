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

// check if player got any status ailments
public enum PlayerState
{
	safe,
	pierced,
	blocked,
	healed,
	countered,
	poisoned,
	burned,
	boosted,
	bound
}

public class Player {
    // CONSTANTS
    public const int MAX_HEALTH = 20;
    public const int BASE_DEFENSE = 1;
    // public const int MAX_MOVEMENT_POSSIBLE = 0;

    // Player Global Variables
    public int Health;
    public int defense;
	public int oldDef;
	public string Name;
	public int turns;
	public Controller2 contrl;
	public Card cardChoice = null;
	public Deck deck;
	public Card[] hand = new Card[3];
	public bool tookDamage = false;

	public PlayerState p1State = PlayerState.safe;

	// Use this for initialization
	public Player (string nme)
	{
		deck = new Deck ();
		Name = nme;
		
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
			hand[i] = deck.Draw();
			Debug.Log("Player " + nme + " draws " + hand[i].cardName);
		}
		

	}

    // meant to handle when the player is struck by enemy card. 
    public void TakeDamage(int i, int pierceValue)
    {
		var p = this;
		// Some effects are handled in take damage
		if (p1State == PlayerState.pierced) 
		{
			p.defense -= pierceValue;
			i -= p.defense;
			if(i < 0){i=0;}
			p.Health -= i;
		}
        else if (p.defense > 0)
        {
            // check for pierce
            i -= p.defense;
			if(i < 0){i = 0;}
			p.Health -= i;
        }
        else
        {
            p.Health -= i; // Subtracts health depending on card

        }

		defense = oldDef;
		Debug.Log ("Player " + Name + " Takes " + i + " damage!");
        p.Health -= i;
    }

    // Defend class. Happens when player throws up a defense card. 
    public void Defend(int i)
    {
		Debug.Log ("Player " + Name + " Gains " + i + " defense!");
		var p = this;
		p.defense = i; 
    }

    // Adds random event based on card usage. (LOTS OF HANDLERS)
    public void AddEffect(Card effectCard) // accept int enum
    {
		var p = this;
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
			//Debug.Log("Player " + Name + " is now affected with pierce!");
			p1State = PlayerState.pierced;
			break;
		case EffectType.block:
			//Debug.Log("Player " + Name + " is now affected by a card block!");

			// I can't figure out how to do this one. 
			p1State = PlayerState.blocked;
			break;
		case EffectType.heal: 
			//Debug.Log("Player " + Name + " is Healed!");
			p1State = PlayerState.healed;
			
			p.Health += cardChoice.effectStrength1;
			break;
		case EffectType.counter:
			//Debug.Log("Player " + Name + " is now ready to counter!");

			// handled in the controller 
			p1State = PlayerState.countered;
			break;
		case EffectType.poison:
			//Debug.Log("Player " + Name + " is now affected with poison!");
			p1State = PlayerState.poisoned;
			if(p1State == PlayerState.poisoned && contrl.gameState == BoardState.standby && cardChoice.effect1 == EffectType.poison)
			{
				turns++;
				p.Health -= cardChoice.effectStrength1;
				if(turns >= 3)
				{
					p1State = PlayerState.safe;
				}
			}
			if(p1State == PlayerState.poisoned && contrl.gameState == BoardState.standby && cardChoice.effect2 == EffectType.poison)
			{
				turns++;
				p.Health -= cardChoice.effectStrength2;
				if(turns >= 3)
				{
					p1State = PlayerState.safe;
				}
			}
			break;
		case EffectType.cure:
			//Debug.Log("Player " + Name + " is cured!");
			p1State = PlayerState.safe;
			break;
		case EffectType.burn:
			//Debug.Log("Player " + Name + " is now affected with burn!");

			// right now I just have burn hurting the player's defense
			p1State = PlayerState.burned;
			if(p1State == PlayerState.burned && contrl.gameState == BoardState.standby && cardChoice.effect1 == EffectType.burn)
			{
				turns++;
				p.defense -= cardChoice.effectStrength1;
				if(turns >= 3)
				{
					p1State = PlayerState.safe;
				}
			}
			if(p1State == PlayerState.burned && contrl.gameState == BoardState.standby && cardChoice.effect2 == EffectType.burn)
			{
				turns++;
				p.defense -= cardChoice.effectStrength2;
				if(turns >= 3)
				{
					p1State = PlayerState.safe;
				}
			}
			break;
		case EffectType.boost:
			//Debug.Log("Player " + Name + "'s attack is boosted!");
			// should be handled in controller. Not implemented atm
			p1State = PlayerState.boosted;
			break;
		case EffectType.bind:
			//Debug.Log("Player " + Name + " is now affected with bind!");
			p1State = PlayerState.bound;
			if(p1State == PlayerState.bound && contrl.gameState == BoardState.BattleP1)
			{
				contrl.gameState = BoardState.BattleP2;
			}
			break;
        }

		// What we want to do here is figure out a way to implement effects on the player based on effect cards played by the opposing factor.
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
			//Debug.Log("Player " + Name + " is now affected with pierce!");
			p1State = PlayerState.pierced;
			break;
		case EffectType.block:
			//Debug.Log("Player " + Name + " is now affected by a card block!");
			
			// I can't figure out how to do this one. 
			p1State = PlayerState.blocked;
			break;
		case EffectType.heal: 
			//Debug.Log("Player " + Name + " is Healed!");
			p1State = PlayerState.healed;
			
			p.Health += cardChoice.effectStrength1;
			break;
		case EffectType.counter:
			//Debug.Log("Player " + Name + " is now ready to counter!");
			
			// handled in the controller 
			p1State = PlayerState.countered;
			break;
		case EffectType.poison:
			//Debug.Log("Player " + Name + " is now affected with poison!");
			p1State = PlayerState.poisoned;
			if(p1State == PlayerState.poisoned && contrl.gameState == BoardState.standby && cardChoice.effect1 == EffectType.poison)
			{
				turns++;
				p.Health -= cardChoice.effectStrength1;
				if(turns >= 3)
				{
					p1State = PlayerState.safe;
				}
			}
			if(p1State == PlayerState.poisoned && contrl.gameState == BoardState.standby && cardChoice.effect2 == EffectType.poison)
			{
				turns++;
				p.Health -= cardChoice.effectStrength2;
				if(turns >= 3)
				{
					p1State = PlayerState.safe;
				}
			}
			break;
		case EffectType.cure:
			//Debug.Log("Player " + Name + " is cured!");
			p1State = PlayerState.safe;
			break;
		case EffectType.burn:
			//Debug.Log("Player " + Name + " is now affected with burn!");
			
			// right now I just have burn hurting the player's defense
			p1State = PlayerState.burned;
			if(p1State == PlayerState.burned && contrl.gameState == BoardState.standby && cardChoice.effect1 == EffectType.burn)
			{
				turns++;
				p.defense -= cardChoice.effectStrength1;
				if(turns >= 3)
				{
					p1State = PlayerState.safe;
				}
			}
			if(p1State == PlayerState.burned && contrl.gameState == BoardState.standby && cardChoice.effect2 == EffectType.burn)
			{
				turns++;
				p.defense -= cardChoice.effectStrength2;
				if(turns >= 3)
				{
					p1State = PlayerState.safe;
				}
			}
			break;
		case EffectType.boost:
			//Debug.Log("Player " + Name + "'s attack is boosted!");
			// should be handled in controller. Not implemented atm
			p1State = PlayerState.boosted;
			break;
		case EffectType.bind:
			//Debug.Log("Player " + Name + " is now affected with bind!");
			p1State = PlayerState.bound;
			if(p1State == PlayerState.bound && contrl.gameState == BoardState.BattleP1)
			{
				contrl.gameState = BoardState.BattleP2;
			}
			break;
		}
    }
	public Card playCard(int cardToPlay)
	{
		// Player will choose a card from their hand.
		// This will eventually be associated with a button press on the card. 

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