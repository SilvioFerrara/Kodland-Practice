using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3.0f; // Velocità di movimento del nemico
    public float stopDistance = 1.5f; // Distanza minima dal player prima di fermarsi
    private Transform player; // Riferimento al Transform del player
    private CharacterController controller; // Componente CharacterController

    void Start()
    {
        // Trova il player tramite il tag "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Nessun oggetto con il tag 'Player' trovato nella scena!");
        }

        // Ottiene il componente CharacterController
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("CharacterController mancante su " + gameObject.name);
        }
    }

    void Update()
    {
        if (player != null && controller != null)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Calcola la direzione verso il player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Evita movimenti verticali indesiderati

        // Se il nemico è più lontano della stopDistance, si muove verso il player
        if (Vector3.Distance(transform.position, player.position) > stopDistance)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }

        // Ruota il nemico verso il player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
