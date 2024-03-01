using System.Collections;
using UnityEngine;

public class TileGameScript : MonoBehaviour
{
    [SerializeField] private GameObject interactibleobject;
    [SerializeField] private ToggleFlashlight flashlight;
    [SerializeField] private GameObject tint;
    [SerializeField] private AudioSource JumpscareSound;
    [SerializeField] private GameObject drop;
    [SerializeField] private Transform EmptyTileSpace = null;
    [SerializeField] private Transform TileParent;
    [SerializeField] private SpriteRenderer EmptySpriteRenderer = null;
    [SerializeField] private LayerMask TileGame;
    [SerializeField] private TileScript[] Tiles;
    private Camera GameMainCamera;
    
    private Vector2 LastEmptySpacePosition;
    private TileScript MovingTileScript;
    int EmptyTileSpaceIndex = 14;
    int TileIndex;
    private bool win;
    private bool colourswap;
    private bool done;
    private Vector3 TargetScale;


    float timer = 2f;
    float counter = 0;

    private void Start()
    {
        GameMainCamera = Camera.main;
        TargetScale = new Vector3(4, 4, 1);
        shuffle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (var tileitem in Tiles)
            {
                if (tileitem != null)
                {
                    tileitem.TargetPosition = tileitem.CorrectPosition;
                }
            }
        }
        if (!win)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = GameMainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, TileGame);
                if (hit)
                {
                    //Debug.Log(hit.transform.name);
                    if (Vector2.Distance(EmptyTileSpace.position, hit.transform.position) < 1.1f)
                    {
                        LastEmptySpacePosition = EmptyTileSpace.position;
                        MovingTileScript = hit.transform.GetComponent<TileScript>();
                        EmptyTileSpace.position = MovingTileScript.TargetPosition;
                        MovingTileScript.TargetPosition = LastEmptySpacePosition;
                        TileIndex = FindIndex(MovingTileScript);
                        Tiles[EmptyTileSpaceIndex] = Tiles[TileIndex];
                        Tiles[TileIndex] = null;
                        EmptyTileSpaceIndex = TileIndex;
                    }
                }
            }
            int CorrectTiles = 0;
            foreach (var tileitem in Tiles)
            {
                if (tileitem != null)
                {
                    if (tileitem.CorrectPlace)
                    {
                        CorrectTiles++;
                    }
                    else if (!tileitem.CorrectPlace)
                    {
                        break;
                    }
                }
            }
            if (CorrectTiles == Tiles.Length - 1)
            {
                Debug.Log("You Win");
                win = true;
            }
        }
        else if (win && !colourswap)
        {
            if (counter < timer)
            {
                counter += (1 * Time.deltaTime);
            }
            else if (counter >= timer)
            {
                EmptySpriteRenderer.enabled = true;
                foreach (var Tileobject in Tiles)
                {
                    if (Tileobject != null) 
                    { 
                    Tileobject.togglecolour();
                    }
                }
                colourswap = true;
                JumpscareSound.Play();
                tint.SetActive(true);
            }
        }
        else if (win && colourswap)
        {
            if (TileParent.localScale.x < TargetScale.x)
            {
                TileParent.localScale = TileParent.localScale + new Vector3(7f*Time.deltaTime, 7f * Time.deltaTime, 0f);
            }
            else
            {
                if (!done)
                {
                    TileParent.localScale = new Vector3(1, 1, 1);
                    TargetScale = TileParent.localScale;
                    interactibleobject.layer = LayerMask.NameToLayer("Tiles");
                    tint.SetActive(false);
                    flashlight.losebatterylife(0.25f);
                    done = true;
                    StartCoroutine(changelayerspawnitem());
                }
            }
        }
    }
    IEnumerator changelayerspawnitem()
    {
        yield return new WaitForSeconds(.1f);
        Instantiate(drop, TileParent.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.1f);
        interactibleobject.layer = LayerMask.NameToLayer("Interactible");
    }

    private void shuffle()
    {
        if(EmptyTileSpaceIndex!=14)
        {
            var TileOn14LastPos = Tiles[14].TargetPosition;
            Tiles[14].TargetPosition = TileOn14LastPos;
            EmptyTileSpace.position = TileOn14LastPos;
            Tiles[EmptyTileSpaceIndex] = Tiles[14];
            Tiles[14] = null;
            EmptyTileSpaceIndex = 14;
        }
        do
        {
            for (int i = 0; i <= 13; i++)
            {
                var lastpos = Tiles[i].TargetPosition;
                int randomindex = Random.Range(0, 13);
                Tiles[i].TargetPosition = Tiles[randomindex].TargetPosition;
                Tiles[randomindex].TargetPosition = lastpos;

                var tile = Tiles[i];
                Tiles[i] = Tiles[randomindex];
                Tiles[randomindex] = tile;
            }
            Debug.Log("Shuffled Puzzle");
        } while (IsSolvable());
    }

    //gets the index of the Tilescript in the array
    private int FindIndex(TileScript Ts)
    {
        for(int i = 0;i < Tiles.Length;i++)
        {
            if (Tiles[i]!= null)
            {
                if (Tiles[i] == Ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    //checks the inversion value of the puzzle is even to make sure it is solveable
    private bool IsSolvable()
    {
        int[] puzzleArray = new int[Tiles.Length];
        int k = 0;

        // Flatten the puzzle into a 1D array
        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i] != null)
            {
                puzzleArray[k++] = Tiles[i].TileNumber;
            }
        }

        // Count number of inversions
        int inversionCount = 0;
        for (int i = 0; i < puzzleArray.Length - 1; i++)
        {
            for (int j = i + 1; j < puzzleArray.Length; j++)
            {
                if (puzzleArray[i] > puzzleArray[j] && puzzleArray[i] != 0 && puzzleArray[j] != 0)
                {
                    inversionCount++;
                }
            }
        }

        // Check if the inversion count is even
        return inversionCount % 2 == 0;
    }
}
