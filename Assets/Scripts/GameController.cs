using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	private bool gameOver;
	private bool restart;
	private int score;
	public GUIText titleText;
	public GUIText musicText;
	public GUIText controlsText;
	public GUIText warningText;

	public int waveThreshold;
	public int spawnThreshold;
	public float difficultyLimit;
	private float timeElapsed;

	void Start(){

		titleText.text = "Asteroid Traverser";
		musicText.text = "Music by Sam Hulick and Jack Wall";
		controlsText.text = "Click to fire\n'F' for special attack";
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		warningText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine( SpawnWaves ());

	}

	void Update(){

		if(restart){
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel(Application.loadedLevel);
			}
		}

	}

	IEnumerator SpawnWaves() {

		yield return new WaitForSeconds(startWait);
		titleText.text = "";
		musicText.text = "";
		controlsText.text = "";

		while(true){

			for (int i = 0 ; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), 0, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				//spawn speeds up here
				if(spawnWait > difficultyLimit && score > spawnThreshold){
					spawnWait = spawnWait / 2;
					spawnThreshold = spawnThreshold * 2;
					warningText.text = "The Asteroid Field Intensifies!";
					timeElapsed = Time.time;
				}
			
				yield return new WaitForSeconds(spawnWait);

				if(timeElapsed + 2 < Time.time){
				    warningText.text = "";
				}
			}
			//wait between waves shortens here
			if(score > waveThreshold){
				waveWait = waveWait / 2;
				waveThreshold = waveThreshold * 2;
			}
			yield return new WaitForSeconds(waveWait);

			if(gameOver){
				restartText.text = "Press R to Restart Mission";
				restart = true;
				break;
			}
		}

	}

	public void AddScore(int newScoreValue){

		score += newScoreValue;
		UpdateScore ();

	}

	void UpdateScore(){
		
		scoreText.text = "Score: " + score;
		
	}

	public void GameOver(){

		gameOverText.text = "Mission Failed";
		gameOver = true;
	}

}
