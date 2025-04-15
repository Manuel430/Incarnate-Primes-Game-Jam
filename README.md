# World of Squares
Play the game here: [World of Squares](https://primalknight430.itch.io/world-of-squares)

## About
World of Squares is a Game Jam project done for the University of the Incarnate Word. Working with a small team of modelers and animators, the theme of this project was "Make Me Laugh", so we went with the gameplay of a jester character collecting three items for three people within the time limit.

### Credits
Animation - Cat Weaver, Jackie Ransom, Josh Chavis

Rigging - Angelina Martinez, Jackie Ransom, Cat Weaver

Modeling - Jesica Rios Orrantia, Angelina Martinez, Ryeesa Edge, Mikael Santiago

Enivroment - Ryeesa Edge

Programming - Manuel Rodriguez

Concept - Manuel Rodriguez, Cat Weaver


## Coding
### Collecting Gameplay
With collecting different items for the NPCs is the main purpose of the game, it is important to make sure that the player is able to pick up an item, store a data that they have the data, then delete that data once they have given it to the right NPC.\

#### Item Interact
Starting the game, this checks the icon and interaction UI off, as the icon's activation is what is used for the data that the player has the item.
```cs
    private void Awake()
    {
        interactionUI.SetActive(false);
        playerControl = new PlayerControlsScript();
        playerControl.Player.Enable();
        itemIcon.SetActive(false);
    }
```

Once the player is close to the item, they get the ability to collect the item, destroying the item while also acitviating the item, telling the game that they now have the item to give to the NPCs.
```cs
    public void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) { return; }

        player.CanGrab(true);
        interactionUI.SetActive(true);
        playerControl.Player.Interact.performed += CollectItem;
    }
```

```cs
    private void CollectItem(InputAction.CallbackContext context)
    {
        interactionUI.SetActive(false);
        itemIcon.SetActive(true);
        Destroy(parentObject);
    }
```

### Enemies
### Misc Code
#### World of the day
Simple code done for fun where whenever the player pauses, a random text that was typed out by someone will be displayed. With how the code was set up, this can take in a plethora of random quotes as long as all of them have been typed out.
```cs
public class WordOfTheDay : MonoBehaviour
{
    [SerializeField] TMP_Text wordOfTheDay;
    [SerializeField] string[] textOptions;

    public void SummonWord()
    {
        string wordToDisplay = RandomWord();

        wordOfTheDay.text = wordToDisplay;
    }

    private string RandomWord()
    {
        string randomWord = textOptions[UnityEngine.Random.Range(0, textOptions.Length)];
        return randomWord;
    }
}
```
