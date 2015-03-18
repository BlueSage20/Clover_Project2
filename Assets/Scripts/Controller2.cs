using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

[Flags]
public enum GameState
{
	None = 0x0,
	Player1Deciding = 0x1,
	Player2Deciding = 0x2,
	Player1Chosen = 0x4,
	Player2Chosen = 0x8
}

public class Controller2 : MonoBehaviour {
	private Player p1;
	private Player p2;
	private Card c1, c2;
	private GameObject go1, go2;

	[HideInInspector]
	public GameState state = GameState.Player1Deciding | GameState.Player2Deciding;

	public GameObject cardPrototype;
	public GameObject textPrototype;
	public RectTransform logWindow;

	public Transform hand1, hand2;

	// Use this for initialization
	void Start () {
		// initialize a name for the player. (Player class takes at least a name)
		p1 = new Player ("James", hand1, cardPrototype, BattleLog);
		p2 = new Player ("Sung", hand2, cardPrototype, BattleLog);
	}
	
	// Update is called once per frame
	void Update () {
	}

	/// <summary>
	/// Print a message to the battle log, resizing it as necessary.
	/// </summary>
	/// <param name="message"></param>
	void BattleLog(string message)
	{
		Debug.Log("BattleLog: " + message);
		var txt = Instantiate(textPrototype) as GameObject;

		txt.GetComponent<Text>().text = message;

		txt.transform.SetParent(logWindow, false);
	}

	public void CardClicked(GameObject card)
	{
		var uiCard = card.GetComponent<UICard>();
		Debug.Log(uiCard.card.cardName + " was clicked");

		var owner = uiCard.owner;

		if ((state & GameState.Player1Deciding) == GameState.Player1Deciding)
			if (owner == p1)
			{
				c1 = uiCard.card;
				go1 = card;
				BattleLog("Player " + p1.Name + " picked " + uiCard.card.ToString());
				state = state | GameState.Player1Chosen ^ GameState.Player1Deciding;
			}

		if ((state & GameState.Player2Deciding) == GameState.Player2Deciding)
			if (owner == p2)
			{
				c2 = uiCard.card;
				go2 = card;
				BattleLog("Player " + p2.Name + " picked " + uiCard.card.ToString());
				state = state | GameState.Player2Chosen ^ GameState.Player2Deciding;
			}

		if ((state & GameState.Player1Chosen) == GameState.Player1Chosen)
			if ((state & GameState.Player2Chosen) == GameState.Player2Chosen)
			{
				CardChecks(p1, p2, c1, c2);
				Destroy(go1);
				Destroy(go2);
				p1.discard(c1);
				p2.discard(c2);
				p1.draw();
				p2.draw();
				state = GameState.Player1Deciding | GameState.Player2Deciding;
			}
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

		BattleLog("Player " + p1.Name + " now has " + p1.Health + "HP");
		BattleLog("Player " + p2.Name + " now has " + p2.Health + "HP");
	}
}
