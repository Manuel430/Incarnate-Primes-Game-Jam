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
With collecting different items for the NPCs is the main purpose of the game, it is important to make sure that the player is able to pick up an item, store a data that they have the data, and then delete that data once they have given it to the right NPC.\

#### Item Interact
Starting the game, this checks the icon and interaction UI off, as the icon's activation is what is used for the data that the player has for the item.
```cs
    private void Awake()
    {
        interactionUI.SetActive(false);
        playerControl = new PlayerControlsScript();
        playerControl.Player.Enable();
        itemIcon.SetActive(false);
    }
```

Once the player is close to the item, they can collect the item, destroying the item while also acitviating the item, telling the game that they now have the item to give to the NPCs.
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

Finally returning to the NPCs, determining which item the player has in order, they could either say the player has the wrong or right item, playing out different dialogue depending on the situation and taking the item if it is the correct one.
```cs
    private void Interact(InputAction.CallbackContext context)
    {
        if (pause.GetPaused())
        {
            return;
        }

        if (interactionUI.gameObject == true && inCutscene == false)
        {
            inCutscene = true;
            interactionUI.SetActive(false);

            if (!isHappy)
            {
                decision.ItemDecider();
            }

            if (wrongItem_01 == true && rightItem == false)
            {
                dialogueTemplate.text = wrongItemDialogue_01;
                wrongTalk.Play();
                sadInteract.Play();
                timer.LossOfTime(timeLoss);
            }
            else if(wrongItem_02 == true && rightItem == false)
            {
                dialogueTemplate.text = wrongItemDialogue_02;
                wrongTalk.Play();
                sadInteract.Play();
                timer.LossOfTime(timeLoss);
            }
            else if (rightItem == true)
            {

                dialogueTemplate.text = rightItemDialogue;
                happyInteract.Play();

                if (isHappy == false)
                {
                    rightTalk.Play();
                    npcAnimation.Play("Laugh");
                    npcAnimation.PlayQueued("Idle_Happy");
                    isHappy = true;
                    pointOff.SubtractAmount(1);
                }
            }
            else
            {
                dialogueTemplate.text = customDialogue;
                sadInteract.Play();
            }

            DialoguePanel.SetActive(true);
        }
    }
}
```

### Enemies
The enemies, also known as the Fun Police in the game, are swarming around the city, ready to stop Chester from collecting the needed items. Due to the time limit for the Game Jam, their AI is a simple, but still functional.

The enemies have three simple movements that they have: Patrolling, Chasing, and Attacking. Patrolling lets the enemy wander around wherever they please, as long as it is within a range that they are able to walk to.
```cs
    private void Patrol()
    {
        range = UnityEngine.Random.Range(1,3);
        
        Move(patrolSpeed);
        
        if(funPolice.remainingDistance <= funPolice.stoppingDistance)
        {
            if(RandomPoint(centerPoint.position, range, out rayPoint))
            {
                Debug.DrawRay(rayPoint, Vector3.up, Color.yellow);
                funPolice.SetDestination(rayPoint);
            }
        }
    }
```

```cs
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
```

Once the player gets close to the cops, they swap from Patrolling to Chasing, speeding up their way towards the enemy so they could get ready to attack.
```cs
    private void Chase()
    {

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        Move(chaseSpeed);

        funPolice.SetDestination(playerPosition);

    }
```

Lastly, when the player is in the smallest range, the enemy enters Attacking, stopping in place so that an animation can play and allow them to hit the player, slowing them down from their progress.
```cs
    private void Attack()
    {
        Stop();
        funPolice.speed = 0;
        policeANIM.Play("Attack");
        return;
    }

    private void Stop()
    {
        policeANIM.Play("Attack");

        if(funPolice.isActiveAndEnabled)
        {
            funPolice.isStopped = true;
            funPolice.speed = 0;
        }
    }
```

### Misc Code

#### Game Timer
The risk of the game, the player has to complete the main goal before the time ends, because once it hits 0, its game over. The timer only stops when the player completes the game.
```cs
    private void Awake()
    {
        gameOverUI.SetActive(false);
        countDown = true;
        remainingTime = maxTime;
        policeArmy.SetActive(false);
    }

    private void Update()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);

        if (countDown)
        {
            if (remainingTime > 1)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 1)
            {
                remainingTime = 0;
                policeArmy.SetActive(true);
                player.SetCutscene(true);
                player.GameOver();
                gameOverUI.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public bool SetCountDown(bool setActive)
    {
        return countDown = setActive;
    }
```

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
