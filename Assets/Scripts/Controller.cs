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
		decks[0] = new Deck();
	}


	public void Update()
	{
		int pid;
		switch (state)
		{
			default:
			case GameState.p1turn:
				pid = 0;
				break;
			case GameState.p2turn:
				pid = 1;
				break;
		}

		Debug.Log("Player " + pid + "'s turn.");

		var hand = hands[pid];

		bool gotCard = true;
		int handCount;
		while ((handCount = hand.transform.childCount) < 5 && gotCard)
		{
			Debug.Log("Player " + pid + " has " + handCount + " cards, drawing.");
			gotCard = DrawCard(pid);
		}
	}

	public bool DrawCard(int pid)
	{
		if (decks[pid].cards.Count < 1)
		{
			return false;
		}

		var card = Instantiate(cardPrefab) as GameObject;
		var script = card.GetComponent<UICard>();
		script.card = decks[pid].Draw();

		card.transform.parent = hands[pid].transform;

		return true;
	}

	public void CardClicked(Object cardObject)
	{
		var card = cardObject as GameObject;

		Debug.Log("Clicked card: " + card.GetComponent<UICard>().card.cardName);
	}
}