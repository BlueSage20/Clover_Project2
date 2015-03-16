using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
	p1turn,
	p2turn,
	gameover
}

public class Controller : MonoBehaviour
{
	public GameState state = GameState.p1turn;

	public GameState nextState = GameState.p1turn;

	public Player[] players;

	public Deck[] decks;

	public GameObject[] hands;

	public Transform cardPrefab;

	public GameObject deckPrefab;

	public void Start()
	{
		players [0] = new Player ("James");
		players [1] = new Player ("Sung");
		decks[0] = new Deck();
	}


	public void Update()
	{
		int playerID;
		switch (state)
		{
			default:
			case GameState.p1turn:
				playerID = 0;
				break;
			case GameState.p2turn:
				playerID = 1;
				break;
		}

		Debug.Log("Player " + playerID + "'s turn.");

		var hand = hands[playerID];

		bool gotCard = true;
		int handCount;
		while ((handCount = hand.transform.childCount) < 5 && gotCard)
		{
			Debug.Log("Player " + playerID + " has " + handCount + " cards, drawing.");
			gotCard = DrawCard(playerID);
		}

		if (Input.GetKeyDown ("space")) {

			Battle(players[0],players[1]);
		}
	}

	public bool DrawCard(int playerID)
	{
		if (decks[playerID].cards.Count < 1)
		{
			return false;
		}

		var card = Instantiate(cardPrefab) as GameObject;
		var script = card.GetComponent<UICard>();
		script.card = decks[playerID].Draw();

		card.transform.parent = hands[playerID].transform;

		return true;
	}

	// Battle Handler
	public void Battle(Player p1, Player p2)
	{
	}

	public void CardClicked(Object cardObject)
	{
		var card = cardObject as GameObject;

		Debug.Log("Clicked card: " + card.GetComponent<UICard>().card.cardName);
	}
}