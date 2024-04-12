using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ST_PuzzleDisplay : MonoBehaviour 
{
	public ImageSlider imageSlider;
	// this puzzle texture.
	public Texture PuzzleImage;
	public Texture PuzzleImage2;
	public Texture PuzzleImage3;
	public Conversation[] conversations;

	// the width and height of the puzzle in tiles.
	public int Height = 3;
	public int Width  = 3;

	// tracking completion of puzzle.
	public int PuzzleComplete = 0;

	// additional scaling value.
	public Vector3 PuzzleScale = new Vector3(1.0f, 1.0f, 1.0f);

	// additional positioning offset.
	public Vector3 PuzzlePosition = new Vector3(0.0f, 0.0f, 0.0f);

	// seperation value between puzzle tiles.
	public float SeperationBetweenTiles = 0.5f;

	// the tile display object.
	public GameObject Tile;

	// the shader used to render the puzzle.
	public Shader PuzzleShader;

	// array of the spawned tiles.
	private GameObject[,] TileDisplayArray;
	private List<Vector3>  DisplayPositions = new List<Vector3>();

	// position and scale values.
	private Vector3 Scale;
	private Vector3 Position;

	// has the puzzle been completed?
	public bool Complete = false;

	// Use this for initialization
	void Start () 
	{
		// create the games puzzle tiles from the provided image.
		CreatePuzzleTiles();
		DialogueManagerM.StartConversation(conversations[PuzzleComplete]);

		// mix up the puzzle.
		StartCoroutine(JugglePuzzle());

	}
	
	// Update is called once per frame
	void Update () 
	{
		// move the puzzle to the position set in the inspector.
		this.transform.localPosition = PuzzlePosition;

		// set the scale of the entire puzzle object as set in the inspector.
		this.transform.localScale = PuzzleScale;
	}

	public Vector3 GetTargetLocation(ST_PuzzleTile thisTile)
	{
		// check if we can move this tile and get the position we can move to.
		ST_PuzzleTile MoveTo = CheckIfWeCanMove((int)thisTile.GridLocation.x, (int)thisTile.GridLocation.y, thisTile);

		if(MoveTo != thisTile)
		{
			// get the target position for this new tile.
			Vector3 TargetPos = MoveTo.TargetPosition;
			Vector2 GridLocation = thisTile.GridLocation;
			thisTile.GridLocation = MoveTo.GridLocation;

			// move the empty tile into this tiles current position.
			MoveTo.LaunchPositionCoroutine(thisTile.TargetPosition);
			MoveTo.GridLocation = GridLocation;

			// return the new target position.
			return TargetPos;
		}

		// else return the tiles actual position (no movement).
		return thisTile.TargetPosition;
	}

	private ST_PuzzleTile CheckMoveLeft(int Xpos, int Ypos, ST_PuzzleTile thisTile)
	{
		// move left 
		if((Xpos - 1)  >= 0)
		{
			// we can move left, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos - 1, Ypos, thisTile);
		}
		
		return thisTile;
	}
	
	private ST_PuzzleTile CheckMoveRight(int Xpos, int Ypos, ST_PuzzleTile thisTile)
	{
		// move right 
		if((Xpos + 1)  < Width)
		{
			// we can move right, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos + 1, Ypos , thisTile);
		}
		
		return thisTile;
	}
	
	private ST_PuzzleTile CheckMoveDown(int Xpos, int Ypos, ST_PuzzleTile thisTile)
	{
		// move down 
		if((Ypos - 1)  >= 0)
		{
			// we can move down, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos, Ypos  - 1, thisTile);
		}
		
		return thisTile;
	}
	
	private ST_PuzzleTile CheckMoveUp(int Xpos, int Ypos, ST_PuzzleTile thisTile)
	{
		// move up 
		if((Ypos + 1)  < Height)
		{
			// we can move up, is the space currently being used?
			return GetTileAtThisGridLocation(Xpos, Ypos  + 1, thisTile);
		}
		
		return thisTile;
	}
	
	private ST_PuzzleTile CheckIfWeCanMove(int Xpos, int Ypos, ST_PuzzleTile thisTile)
	{
		// check each movement direction
		if(CheckMoveLeft(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveLeft(Xpos, Ypos, thisTile);
		}
		
		if(CheckMoveRight(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveRight(Xpos, Ypos, thisTile);
		}
		
		if(CheckMoveDown(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveDown(Xpos, Ypos, thisTile);
		}
		
		if(CheckMoveUp(Xpos, Ypos, thisTile) != thisTile)
		{
			return CheckMoveUp(Xpos, Ypos, thisTile);
		}

		return thisTile;
	}

	private ST_PuzzleTile GetTileAtThisGridLocation(int x, int y, ST_PuzzleTile thisTile)
	{
		for(int j = Height - 1; j >= 0; j--)
		{
			for(int i = 0; i < Width; i++)
			{
				// check if this tile has the correct grid display location.
				if((TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>().GridLocation.x == x)&&
				   (TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>().GridLocation.y == y))
				{
					if(TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>().Active == false)
					{
						// return this tile active property. 
						return TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>();
					}
				}
			}
		}

		return thisTile;
	}

	private IEnumerator JugglePuzzle()
	{
		yield return new WaitForSeconds(1.0f);

		// hide a puzzle tile (one is always missing to allow the puzzle movement).
		TileDisplayArray[0,2].GetComponent<ST_PuzzleTile>().Active = false;

		yield return new WaitForSeconds(1.0f);

		for(int k = 0; k < 20; k++)
		{
			// use random to position each puzzle section in the array delete the number once the space is filled.
			for(int j = 0; j < Height; j++)
			{
				for(int i = 0; i < Width; i++)
				{		
					// attempt to execute a move for this tile.
					TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>().ExecuteAdditionalMove();

					yield return new WaitForSeconds(0.02f);
				}
			}
		}

		// continually check for the correct answer.
		StartCoroutine(CheckForComplete());

		yield return null;
	}

	public IEnumerator CheckForComplete()
	{
		while(Complete == false)
		{
			// iterate over all the tiles and check if they are in the correct position.
			Complete = true;
			for(int j = Height - 1; j >= 0; j--)
			{
				for(int i = 0; i < Width; i++)
				{
					// check if this tile has the correct grid display location.
					if(TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>().CorrectLocation == false)  
					{
						Complete = false;
					}
				}
			}

			yield return null;
		}
				
		// if we are still complete then all the tiles are correct.
		if(Complete)
		{


			Debug.Log("Puzzle Complete!");

			// upgrade the image.
			imageSlider.UpgradeImage();

			// reset the puzzle.
			Complete = false;


			if(PuzzleComplete == 0)
			{
				PuzzleImage = PuzzleImage2;

			}
			else if(PuzzleComplete == 1)
			{
				PuzzleImage = PuzzleImage3;

			}

	


			// clear the display positions.
			DisplayPositions.Clear();

			// destroy the old puzzle tiles.
			for(int j = Height - 1; j >= 0; j--)
			{
				for(int i = 0; i < Width; i++)
				{
					Destroy(TileDisplayArray[i,j]);
				}
			}

			// create the new puzzle tiles.



			yield return new WaitForSeconds(2f);
			if(PuzzleComplete == 2)
			{
				DialogueManagerM.StartConversation(conversations[PuzzleComplete+1]);
				StartCoroutine(LoadSceneAfterDialogue());
				yield break;
			}


			// reset tile display array.
			TileDisplayArray = null;



			PuzzleComplete++;
			CreatePuzzleTiles();
			DialogueManagerM.StartConversation(conversations[PuzzleComplete]);
			StartCoroutine(JugglePuzzle());
			
			

		}

		yield return null;
	}

	private IEnumerator LoadSceneAfterDialogue()
	{
		// Wait until the dialogue is not active
		while (!DialogueManagerM.IsConversationFinished())
		{
			yield return null;
		}

		// Load the scene
		SceneManager.LoadScene("memory1");
	}

	private Vector2 ConvertIndexToGrid(int index)
	{
		int WidthIndex = index;
		int HeightIndex = 0;

		// take the index value and return the grid array location X,Y.
		for(int i = 0; i < Height; i++)
		{
			if(WidthIndex < Width)
			{
				return new Vector2(WidthIndex, HeightIndex);
			}
			else
			{
				WidthIndex -= Width;
				HeightIndex++;
			}
		}

		return new Vector2(WidthIndex, HeightIndex);
	}

	private void CreatePuzzleTiles()
	{
		// using the width and height variables create an array.
		TileDisplayArray = new GameObject[Width,Height];


		// set the scale and position values for this puzzle.
		Scale = new Vector3(1.0f/Width, 1.0f, 1.0f/Height);

		Debug.Log("Scale: " + Scale);


		Tile.transform.localScale = Scale;

		// used to count the number of tiles and assign each tile a correct value.
		int TileValue = 0;

		// spawn the tiles into an array.
		for(int j = Height - 1; j >= 0; j--)
		{
			for(int i = 0; i < Width; i++)
			{
				// calculate the position of this tile all centred around Vector3(0.0f, 0.0f, 0.0f).
				Position = new Vector3(((Scale.x * (i + 0.5f))-(Scale.x * (Width/2.0f))) * (10.0f + SeperationBetweenTiles), 
				                       0.0f, 
				                      ((Scale.z * (j + 0.5f))-(Scale.z * (Height/2.0f))) * (10.0f + SeperationBetweenTiles));


				// set this location on the display grid.
				DisplayPositions.Add(Position);

				// spawn the object into play.
				TileDisplayArray[i,j] = Instantiate(Tile, new Vector3(0.0f, 0.0f, 0.0f) , Quaternion.Euler(90.0f, -180.0f, 0.0f)) as GameObject;
				TileDisplayArray[i,j].gameObject.transform.parent = this.transform;
				TileDisplayArray[i,j].gameObject.transform.localScale= Scale;
				Debug.Log("TileDisplayArray[i,j]: " + TileDisplayArray[i,j]);

				// set and increment the display number counter.
				ST_PuzzleTile thisTile = TileDisplayArray[i,j].GetComponent<ST_PuzzleTile>();
				thisTile.ArrayLocation = new Vector2(i,j);
				thisTile.GridLocation = new Vector2(i,j);
				thisTile.LaunchPositionCoroutine(Position);
				TileValue++;

				// create a new material using the defined shader.
				Material thisTileMaterial = new Material(PuzzleShader);

				// apply the puzzle image to it.
				thisTileMaterial.mainTexture = PuzzleImage;
					
				// set the offset and tile values for this material.
				thisTileMaterial.mainTextureOffset = new Vector2(1.0f/Width * i, 1.0f/Height * j);
				thisTileMaterial.mainTextureScale  = new Vector2(1.0f/Width, 1.0f/Height);
					
				// assign the new material to this tile for display.
				TileDisplayArray[i,j].GetComponent<Renderer>().material = thisTileMaterial;
			}

			
		}


	}
}
