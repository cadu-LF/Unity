using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControleJogador : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
	private bool jumping = false;
	public float jumpSize;
	private int points;
	public TextMeshProUGUI pointsText;

	private AudioSource defaultMusic;
	private AudioSource coinSound;
	private AudioSource jumpSound;
	private AudioSource specialSound;
	private AudioSource springSound;
	private AudioSource windSound;
	private AudioSource secondaryMusic;

	void Start() {
		// inicializa a variável rb com o Rigidbody do componente
		rb = GetComponent<Rigidbody>();
		points = 0;

		defaultMusic = GetComponents<AudioSource>()[0];
		coinSound = GetComponents<AudioSource>()[1];
		jumpSound = GetComponents<AudioSource>()[2];
		specialSound = GetComponents<AudioSource>()[3];
		springSound = GetComponents<AudioSource>()[4];
		windSound = GetComponents<AudioSource>()[5];
		secondaryMusic = GetComponents<AudioSource>()[6];
	}

	// Função executada quando se atualiaza os parâmetros
	// físicos de um objeto
	void FixedUpdate() {
		// pega os inputs horizontais
		float moveHorizontal = Input.GetAxis("Horizontal");
		// pega os inputs verticais
		float moveVertical = Input.GetAxis("Vertical");

        float jump = 0.0f;
        if (Input.GetKey(KeyCode.Space) && jumping == false) {
            jump = jumpSize;
			jumpSound.Play();
			jumping = true;
        }
        
		// Vector 3 é uma estrutura de dados da Unity que faz uma matriz
		Vector3 movimento = new Vector3 (moveHorizontal, jump, moveVertical);
		// Adiciona um movimento ao objeto com o rigidbody um objeto
		rb.AddForce(movimento * speed);
	}

	// Entra nesse método toda vez que o objeto colide
	void OnCollisionEnter(Collision col) {
		jumping = false;
	}

	// vai ser executado quando o objeto encosta em algum objeto que dispara um gatilho
	void OnTriggerEnter(Collider other) {
		// pega a tag do objeto que colidiu
		if (other.gameObject.CompareTag("coin")) {
			// esconde o objeto
			other.gameObject.SetActive(false);
			points++;
			pointsText.text = points + " pontos";
			coinSound.Play();
		} else if (other.gameObject.CompareTag("cogumelo")) {
			other.gameObject.SetActive(false);
			gameObject.transform.localScale = new Vector3(2, 2, 2);
			specialSound.Play();
		} else if (other.gameObject.CompareTag("Bomb")) {
			other.gameObject.SetActive(false);
			// bombSound.Play();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		} else if (other.gameObject.CompareTag("Boost")) {
			other.gameObject.SetActive(false);
			windSound.Play();
			speed *= 2;
		} else if (other.gameObject.CompareTag("Spring")) {
			other.gameObject.SetActive(false);
			springSound.Play();
			jumping = true;
			Vector3 movement = new Vector3 (0, jumpSize, 0);
			rb.AddForce(movement * speed);
		} else if (other.gameObject.CompareTag("Note")) {
			other.gameObject.SetActive(false);
			defaultMusic.Stop();
			secondaryMusic.Play();
		}
	}

	public void acabou() {
		Application.Quit();
	}
}
