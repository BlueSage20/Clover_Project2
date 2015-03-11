/*
 * Sung Won Choi's Player Class
 * Developed on 2015 for GDD 2 Group Project
 * For Academic Purposes
 * Team Name: Clover
 * Team Members: Kevin Granger, James Sasson, Hanna Doerr. Sung Won Choi
 * Contributors to Class: James Sasson
*/

using UnityEngine;
using System.Collections;

public class Player {
    // CONSTANTS
    public const int MAX_HEALTH = 5;
    public const int BASE_DEFENSE = 1;
    // public const int MAX_MOVEMENT_POSSIBLE = 0;

    // Player Global Variables
    public int Health;
    public int defense;

	// Use this for initialization
	void Start () {

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

	}

    // meant to handle when the player is struck by enemy card. 
    public void TakeDamage(int i)
    {
		var p = this;
        if (defense > 0)
        {
            // check for pierce
            i -= p.defense;
        }
        else
        {
            p.Health -= i; // Subtracts health depending on card

            if (p.Health <= 0)
            {
                // Code here to handle death. 
            }
        }
        p.Health -= i;
    }
    // Defend class. Happens when player throws up a defense card. 
    public void Defend(int i)
    {
		var p = this;
        if (p.defense < i) // check if safe
        {
            // defend what you can and then subtract the rest from health.
            i -= p.defense;
            p.Health -= i;
        }
        else // completely safe, lose armor
        {
            i -= p.defense;
        }
    }
    // Adds random event based on card usage. (LOTS OF HANDLERS)
    public static void AddEffect(int i) // accept int enum
    {
        // What we want to do here is figure out a way to implement effects on the player based on effect cards played by the opposing factor.
        switch (i)
        {
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
            case 0:
                Debug.Log(string.Format("Pierce - Ignores defense during next attack."));
                break;
            case 1:
                Debug.Log(string.Format("Block - You are unable to use certain cards (TBD)."));
                break;
            case 2: 
                Debug.Log(string.Format("Heal - Heals the user by a given numeric value."));
                break;
            case 3:
                Debug.Log(string.Format("Counter - If attacked, you can deal damage to the enemy. (Ignores Defense?)"));
                break;
            case 4:
                Debug.Log(string.Format("Poison - Poisons target. Your daily dosage of poison may vary."));
                break;
            case 5:
                Debug.Log(string.Format("Cure - Cures all status ailments."));
                break;
            case 6:
                Debug.Log(string.Format("Burn - Burns targets (Deals damage each turn and lowers atk damage)."));
                break;
            case 7:
                Debug.Log(string.Format("Boost - Raises power of next attack."));
                break;
            case 8:
                Debug.Log(string.Format("Bind - Opponent is prevented from moving next turn."));
                break;
        }
    }
}