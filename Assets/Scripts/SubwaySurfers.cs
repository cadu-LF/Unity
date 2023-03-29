using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubwaySurfers : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
	private bool jumping = false;
	public float jumpSize;
	private int points;
	public TextMeshProUGUI pointsText;
    private int position;

	private AudioSource coinSound;
	private AudioSource jumpSound;
	private AudioSource specialSound;

	void Start() {
		// inicializa a variável rb com o Rigidbody do componente
		rb = GetComponent<Rigidbody>();
		points = 0;

		coinSound = GetComponents<AudioSource>()[1];
		jumpSound = GetComponents<AudioSource>()[2];
		specialSound = GetComponents<AudioSource>()[3];
        position = 2;
	}

	// Função executada quando se atualiaza os parâmetros
	// físicos de um objeto
	void FixedUpdate() {
		// pega os inputs horizontais
		// float moveHorizontal = Input.GetAxis("Horizontal");
		// pega os inputs verticais
		// float moveVertical = Input.GetAxis("Vertical");

        float jump = 0.0f;
        if (Input.GetKey(KeyCode.Space) && jumping == false) {
            jump = jumpSize;
			jumpSound.Play();
			jumping = true;
		    Vector3 movimento = new Vector3 (0, jump, 0);
		    rb.AddForce(movimento * speed);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && position > 1) {
            position--;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && position < 3) {
            position++;
        }

        float x;
        if (position == 1) {
            x = -250.58F;
        } else if (position == 2) {
            x = -245.87F;
        } else {
            x = -241.24F;
        }
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 5.5F);
		// Vector 3 é uma estrutura de dados da Unity que faz uma matriz
		// Vector3 movimento = new Vector3 (moveHorizontal, jump, moveVertical);
		// Adiciona um movimento ao objeto com o rigidbody um objeto
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
		}
	}

	public void acabou() {
		Application.Quit();
	}
}
