using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

	public List<Card> cards = new List<Card>();
	public TextAsset cardStuff;
	private static int numCards;

	// Use this for initialization
	void Start () 
	{
		numCards = 30;
		GenerateDeck ();
		Shuffle();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void Shuffle()
	{
		for (int i = 0; i < cards.Count; i++) {
			Card temp = cards[i];
			int randomIndex = Random.Range(i, cards.Count);
			cards[i] = cards[randomIndex];
			cards[randomIndex] = temp;
		}
	}

	public Card Draw()
	{
		Card drawnCard = cards[0];
		cards.RemoveAt (0);
		return drawnCard;
	}

	private void GenerateDeck()
	{
		string[] cardInfo = ReadCards ();

		for(int i = 0; i < numCards; i++)
		{
			string[] info = cardInfo[i].Split(',');
			cards.Add(new Card(info[0], info[1], info[2], info[3], info[4], info[5], info[6], info[7]));
		}
	}

	private string[] ReadCards()
	{
		string fileContents = cardStuff.text;
		string [] cardContents = fileContents.Split ("\n"[0]);
		return cardContents;
	}
}
