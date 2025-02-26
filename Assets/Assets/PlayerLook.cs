/*
Questo script blocca il cursore, legge l'input del mouse per ruotare la visuale del giocatore, limita la rotazione verticale e aggiorna le rotazioni delle braccia e del corpo del giocatore. 
Questo permette al giocatore di guardarsi intorno usando il mouse in modo naturale e fluido.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float mouseSense;  //mouseSense: Sensibilità del movimento del mouse.
    [SerializeField] Transform player, playerArms;  //player: Riferimento al Transform del giocatore. playerArms: Riferimento al Transform delle braccia del giocatore.

    float xAxisClamp = 0;   //xAxisClamp: Variabile per limitare la rotazione sull'asse X.
   
    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;   //Cursor.lockState: Blocca il cursore al centro dello schermo.

        //Lettura dell'Input del Mouse: rotateX e rotateY: Leggono l'input del mouse e lo moltiplicano per la sensibilità.
        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;
        
        xAxisClamp -= rotateX;  //Clamping dell'Asse X: Aggiorna xAxisClamp con la rotazione sull'asse X per controllare l'angolo di visuale verticale.

        Vector3 rotPlayerArms = playerArms.rotation.eulerAngles;//rotPlayerArms: Ottenimento degli angoli di rotazione attuali delle braccia del giocatore.
        Vector3 rotPlayer = player.rotation.eulerAngles;//rotPlayer: Ottenimento degli angoli di rotazione attuali del corpo del giocatore.

        rotPlayerArms.x -= rotateY;//rotPlayerArms.x: Aggiorna la rotazione sull'asse X delle braccia del giocatore basato sull'input del mouse verticale.
        rotPlayerArms.z = 0;//rotPlayerArms.z: Fissa la rotazione sull'asse Z a 0 per evitare rotazioni indesiderate.
        rotPlayer.y += rotateX;//rotPlayer.y: Aggiorna la rotazione sull'asse Y del corpo del giocatore basato sull'input del mouse orizzontale.

        // Limita xAxisClamp tra -90 e 90 gradi per evitare che il giocatore possa guardare troppo in alto o troppo in basso.
        // Regola rotPlayerArms.x per mantenere la rotazione limitata quando xAxisClamp supera i limiti.
        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            rotPlayerArms.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            rotPlayerArms.x = 270;
        }

        //Assegna le nuove rotazioni calcolate alle braccia del giocatore e al corpo del giocatore utilizzando Quaternion.Euler per convertire gli angoli di rotazione in quaternioni, che rappresentano le rotazioni in Unity.
        playerArms.rotation = Quaternion.Euler(rotPlayerArms);
        player.rotation = Quaternion.Euler(rotPlayer);

    }

}
