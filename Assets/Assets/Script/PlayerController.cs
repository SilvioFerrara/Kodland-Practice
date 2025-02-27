using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;     // Prefab del proiettile
    [SerializeField] private Transform rifleStart; // Punto di partenza del proiettile
    [SerializeField] private Text HpText;          // Testo UI per visualizzare la salute

    [SerializeField] private GameObject GameOver;  // Pannello di Game Over
    [SerializeField] private GameObject Victory;   // Pannello di Vittoria

    public float health = 0;  // Salute del giocatore;

    void Start()
    {
        // Rimosso il codice errato che distruggeva il player all'avvio
        //Destroy(this);    // FIXED
        //ChangeHealth(0);  // FIXED

        // Impostata la salute iniziale correttamente
        ChangeHealth(50); // FIXED
    }

    //Aggiunge o riduce la salute in base al valore hp.
    public void ChangeHealth(int hp)
    {
        //Salute massima → Non può superare 100.
        health += hp;
        if (health > 100)
        {
            health = 100;
        }
        //Se la salute scende a 0 o meno → Chiama Lost() (il giocatore perde).
        else if (health <= 0)
        {
            Lost();
        }
        HpText.text = health.ToString();//Aggiorna il testo della UI (HpText).

    }

    public void Win()
    {
        Victory.SetActive(true);   // Mostra il pannello di vittoria
        Destroy(GetComponent<PlayerLook>());  // Disabilita il controllo della visuale
        Cursor.lockState = CursorLockMode.None; // Sblocca il cursore del mouse
    }

    public void Lost()
    {
        GameOver.SetActive(true);  // Mostra la UI di Game Over
        Destroy(GetComponent<PlayerLook>());  // Disabilita il controllo della visuale
        Cursor.lockState = CursorLockMode.None; // Sblocca il cursore
    }

    void Update()
    {
        //LMB (Tasto sinistro del mouse) → Spara un proiettile
        if (Input.GetMouseButtonDown(0))
        {
            GameObject buf = Instantiate(bullet);   //Crea un nuovo proiettile (Instantiate(bullet)).
            buf.transform.position = rifleStart.position;   //Lo posiziona nel punto rifleStart.
            buf.GetComponent<Bullet>().setDirection(transform.forward); //Imposta la direzione del proiettile in avanti.
            buf.transform.rotation = transform.rotation;    //Lo ruota per essere allineato alla visuale del player.
        }

        //RMB(Tasto destro del mouse) → Elimina nemici vicini
        if (Input.GetMouseButtonDown(1))
        {
            //Controlla tutti gli oggetti entro 2 unità dal player.
            Collider[] tar = Physics.OverlapSphere(transform.position, 4);  //FIXED //Raggio aumentato da 2 a 5
            foreach (var item in tar)
            {
                if (item.tag == "Enemy")
                {
                    Destroy(item.gameObject);   //Se un oggetto ha il tag "Enemy", lo distrugge.
                }
            }
        }

        //Controlla gli oggetti entro un raggio di 3 unità e verifica i tag:
        Collider[] targets = Physics.OverlapSphere(transform.position, 3); 
        foreach (var item in targets)
        {
            //"Heal" → Aumenta la salute di 50 e distrugge l'oggetto.
            if (item.tag == "Heal")
            {
                ChangeHealth(50);
                Destroy(item.gameObject);
            }
            //"Finish" → Chiama Win(), segnalando la vittoria.
            if (item.tag == "Finish")
            {
                Win();
            }
            //"Enemy" → Se il nemico è troppo vicino, chiama Lost(), il player perde.
            if (item.tag == "Enemy")
            {
                Lost();
            }
        }
    }
}
