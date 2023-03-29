using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleJogadorDoubleJump : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
	public int jumpsLeft = 2;
	public float jumpSize;

	void Start() {
		// inicializa a variável rb com o Rigidbody do componente
		rb = GetComponent<Rigidbody>();
	}

	// Função executada quando se atualiaza os parâmetros
	// físicos de um objeto
	void FixedUpdate() {
		// pega os inputs horizontais
		float moveHorizontal = Input.GetAxis("Horizontal");
		// pega os inputs verticais
		float moveVertical = Input.GetAxis("Vertical");

        float jump = 0.0f;
        if (Input.GetKey(KeyCode.Space) && jumpsLeft > 0) {
            jump = jumpSize;
			jumpsLeft -= 1;
        }
        
		// Vector 3 é uma estrutura de dados da Unity que faz uma matriz
		Vector3 movimento = new Vector3 (moveHorizontal, jump, moveVertical);
		// Adiciona um movimento ao objeto com o rigidbody um objeto
		rb.AddForce(movimento * speed);
	}

	// Entra nesse método toda vez que o objeto colide
	void OnCollisionEnter(Collision col) {
		jumpsLeft = 2;
	}
}
