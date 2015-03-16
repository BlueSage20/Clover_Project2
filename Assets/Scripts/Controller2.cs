using UnityEngine;
using System.Collections;

// To keep track of battle turns
public enum BoardState 
{
	standby,
	BattleP1,
	BattleP2
}

// check for card choice
public enum CardChoice
{
	card1,
	card2,
	card3
}

public class Controller2 : MonoBehaviour {
	// the players are instantiated with a set number
	public Player[] players = new Player[2];
	// set enum on standby for now
	public BoardState gameState = BoardState.standby;

	// Cards are kept as defaults for the moment until player choice can be checked.
	// Enum CardChoice = CardChoice.card1;
	public CardChoice p1CardChoice = CardChoice.card1;
	public CardChoice p2CardChoice = CardChoice.card1;

	// Use this for initialization
	void Start () {
		// initialize a name for the player. (Player class takes at least a name)
		players [0] = new Player ("James");
		players [1] = new Player ("Sung");
		Debug.Log("Please press space to initiate battle");
	}
	
	// Update is called once per frame
	void Update () {
		// Do a switch statement for gamestate or "Turns"
		switch (gameState){
		case BoardState.standby:
			// if the player presses Space, the game will begin
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				Battle(players[0],players[1]);
			}
			break;
		case BoardState.BattleP1:
			// get player key input, between ASD, and set the cards from hand. (Will later be changed with User Interface cards)
			if (Input.GetKeyDown (KeyCode.A)) {
				p1CardChoice = CardChoice.card1;
				Debug.Log ("Battle Mode Part 2!");
				gameState = BoardState.BattleP2;
			} 
			else if (Input.GetKeyDown (KeyCode.S)) {
				p1CardChoice = CardChoice.card2;
				Debug.Log ("Battle Mode Part 2!");
				gameState = BoardState.BattleP2;
			} 
			else if (Input.GetKeyDown (KeyCode.D)) {
				p1CardChoice = CardChoice.card3;
				Debug.Log ("Battle Mode Part 2!");
				gameState = BoardState.BattleP2;
			}
			break;
		case BoardState.BattleP2:
			// Same as above, now for player 2
			if (Input.GetKeyDown (KeyCode.J)) {
				p2CardChoice = CardChoice.card1;
				BattleResolve (players[0], players[1]);
			} 
			else if (Input.GetKeyDown (KeyCode.K)) {
				p2CardChoice = CardChoice.card2;
				BattleResolve (players[0], players[1]);
			} 
			else if (Input.GetKeyDown (KeyCode.L)) {
				p2CardChoice = CardChoice.card3;
				BattleResolve (players[0], players[1]);
			}
			break;
		}
	}
	
	// Begin Battle Phase
	public void Battle(Player p1, Player p2)
	{
		Debug.Log ("Battle Mode!");
		gameState = BoardState.BattleP1;
	}

	public void BattleResolve(Player p1, Player p2)
	{
		// call cards
		Card p1Card = null;
		Card p2Card = null;

		switch (p1CardChoice) {
		case CardChoice.card1:
			p1Card = p1.playCard (1);
			break;
		case CardChoice.card2:
			p1Card = p1.playCard (2);
			break;
		case CardChoice.card3:
			p1Card = p1.playCard (3);
			break;
		}

		switch (p2CardChoice) {
		case CardChoice.card1:
			p2Card = p2.playCard (1);
			break;
		case CardChoice.card2:
			p2Card = p2.playCard (2);
			break;
		case CardChoice.card3:
			p2Card = p2.playCard (3);
			break;
		}

		CardChecks (p1, p2, p1Card, p2Card);

		// check for player death
		if (p1.Health <= 0) {
			Debug.Log("Player " + p1.Name + " has fallen!");
		}
		if (p2.Health <= 0) {
			Debug.Log("Player " + p2.Name + " has fallen!");
		}
	
		Debug.Log ("Standby Mode!");

		gameState = BoardState.standby;
	}

	public void CardChecks(Player p1, Player p2, Card p1Card, Card p2Card)
	{
		// Defense Checks
		if (p1Card.type == CardType.defense) {
			p1.Defend(p1Card.def);
		}
		if (p2Card.type == CardType.defense) {
			p2.Defend(p2Card.def);
		}

		// Attack Checks
		if (p1Card.type == CardType.attack) {
			if(p1Card.effect1 == EffectType.pierce)
			{
				p2.TakeDamage(p1Card.atk, p1Card.effectStrength1);
			}
			else if(p1Card.effect2 == EffectType.pierce)
			{
				p2.TakeDamage(p1Card.atk, p1Card.effectStrength2);
			}
			else if(p1Card.effect1 == EffectType.counter)
			{
				p1.TakeDamage(p2Card.atk,0);
				p2.TakeDamage(p1Card.atk,0);
			}
			else if(p1Card.effect2 == EffectType.counter)
			{
				p1.TakeDamage(p2Card.atk,0);
				p2.TakeDamage(p1Card.atk,0);
			}
			else{
				p2.TakeDamage(p1Card.atk, 0);
			}
		}
		if (p2Card.type == CardType.attack) {
			if(p2Card.effect1 == EffectType.pierce)
			{
				p1.TakeDamage(p2Card.atk, p2Card.effectStrength1);
			}
			else if(p2Card.effect2 == EffectType.pierce)
			{
				p1.TakeDamage(p2Card.atk, p2Card.effectStrength2);
			}
			else if(p2Card.effect1 == EffectType.counter)
			{
				p1.TakeDamage(p2Card.atk,0);
				p2.TakeDamage(p1Card.atk,0);
			}
			else if(p2Card.effect2 == EffectType.counter)
			{
				p1.TakeDamage(p2Card.atk,0);
				p2.TakeDamage(p1Card.atk,0);
			}
			else{
				p1.TakeDamage(p2Card.atk, 0);
			}
		}

		// Effect Checks
		if (p1Card.type == CardType.utility) {
			if(p1Card.effect1 == EffectType.heal || p1Card.effect2 == EffectType.heal || p1Card.effect1 == EffectType.cure || p1Card.effect2 == EffectType.cure)
			{
				p1.AddEffect(p1Card);
			}
			else{
				p2.AddEffect(p1Card);
			}
		}
		if(p2Card.type == CardType.utility){
			if(p2Card.effect1 == EffectType.heal || p2Card.effect2 == EffectType.heal || p2Card.effect1 == EffectType.cure || p2Card.effect2 == EffectType.cure)
			{
				p2.AddEffect(p2Card);
			}
			else{
				p1.AddEffect(p2Card);
			}
		}

	}

}
