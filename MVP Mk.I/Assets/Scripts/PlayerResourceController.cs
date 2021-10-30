using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script controls the player's resources. These resources are :
//-Lives : the amount of times a player can still get hit before he loses a credit or is given a game over. Mind you, I haven't yet decided if there will be credits this late into development ...
//-Bombs : the amount of times a player can launch a "bomb", a powerful attack that can quickly destroy enemies and clears all bullets within a radius of the position where the player threw the bomb.
//-MG Ammo : the amount of ammunition currently stored in the machinegun's magazine. Since it is supposed to be a Gurttrommel 34, this amount may never exceed 50.
//-Magazines : the amount of magazines the player still has in stock. So long as they have spare magazines they can reload, but mind you that any ammo left in the old magazine will be lost.
//-Max Ammunition : The maximum amount of ammunition the player is allowed to carry at any given moment. Any excess of ammunition collected in the field is turned into extra score.
//-Reserve Ammo : how much ammo the player actually has outside of their current magazine. The player can only reload with full magazines, thus they must have at least 50 reserve ammo.
//-Magazine Size : the ammount of ammo held by magazine, not meant to be changed during gameplay. It defaults to 50.
//-Score : how many points the player has gotten during his current run. Updated every frame in this script when the player collects an excess of ammunition.
//-Extra Score Increment : how many points an extra bullet is worth. Not meant to be changed during gameplay (Unless I decide to put in an extra scoring feature. To avoid feature creep, I'll avoid that for now.)
//Note about the ammo types : the related fields have been serialized so this script can be used by other characters and for other guns. It just so happens to default to the main character's stats, with the gun
//being an MG-34 and the magazines being standard 50 round drum magazines. Change this as you see fit when making more characters, just make sure you fully understand this script before doing so.

//Tips on using this script :
//Here be the land of getters and setters. For the most part, this script's job is to hold the player's resources.  It is to be used as an interface between the player character's other controllers :
//pass it around as a reference in any script that needs to modify the player's resources in any way. Actions that this controller can do are :
//-Increment or decrease lives and bombs by 1.
//-Add ammunition to the player's reserves.
//-Add score to the player's score.
//-Properly calculate the amount of magazines the player has based on reserve size and the size of a magazine.
//-Turn extra collected ammunition into score.
public class PlayerResourceController : MonoBehaviour
{
    [SerializeField] private int playerLives = 4;
    [SerializeField] private int playerBombs = 4;
    [SerializeField] private int MG_Ammo = 50;
    [SerializeField] private int numberOfMagazines = 4;
    [SerializeField] private int maxAmmunition = 200;
    [SerializeField] private int reserveAmmo = 200;
    [SerializeField] private int magazineSize = 50;
    [SerializeField] private int playerScore = 0;
    [SerializeField] private int scorePerExtra = 50;

    //Getters, setters, increasers and decreasers for player lives.
    public void incrementPlayerLives()
    {
        playerLives++;
    }
    public void decreasePlayerLives()
    {
        playerLives--;
    }
    public int getPlayerLives()
    {
        return playerLives;
    }
    public void setPlayerLives(int amountToSet)
    {
        playerLives = amountToSet;
    }
    /******************************************************************************************************************************************************************************************************************/
    //Getters, setters, increasers and decreasers for player bombs.
    public void incrementPlayerBombs()
    {
        playerBombs++;
    }
    public void decreasePlayerBombs()
    {
        playerBombs--;
    }
    public int getPlayerBombs()
    {
        return playerBombs;
    }
    public void setPlayerBombs(int amountToSet)
    {
        playerBombs = amountToSet;
    }
    /******************************************************************************************************************************************************************************************************************/
    //Getters, setters and add functions for ammo related things.
    //MG Ammo :
    public int getMG_Ammo()
    {
        return MG_Ammo;
    }
    public void decreaseMG_Ammo()
    {
        MG_Ammo--;
    }
    //Reserve ammo :
    public void addReserveAmmo (int amountToAdd)
    {
        reserveAmmo = reserveAmmo + amountToAdd;
    }
    public int getReserveAmmo()
    {
        return reserveAmmo;
    }
    public void setReserveAmmo(int amountToSet)
    {
        reserveAmmo = amountToSet;
    }
    //Spare magazines :
    //The amount of magazines only has a getter. Any modifications to this amount must be done indirectly through the Reserve Ammo.
    public int getNumberOfMagazines()
    {
        return numberOfMagazines;
    }
    /******************************************************************************************************************************************************************************************************************/
    //Getter, setter and add function for score.
    public void addScore (int amountToAdd) 
    {
        playerScore = playerScore + amountToAdd;
    }
    public int getScore()
    {
        return playerScore;
    }
    public void setScore(int amountToSet)
    {
        playerScore = amountToSet;
    }
    /******************************************************************************************************************************************************************************************************************/
    //Other methods for this script.
    //This method removes ammo from the reserves and transfers it to the player's magazine. You must check if the player's current magazine stock is larger than 0 before using this method. Do it by using an if clause
    //and the numberOfMagazines getter on the object referencing this script.
    public void reloadGun()
    {
        MG_Ammo = magazineSize;
        reserveAmmo = reserveAmmo - magazineSize;
    }
    private void calculateScore()
    {
        int gainedScore = (reserveAmmo - maxAmmunition) * scorePerExtra;
        playerScore = playerScore + gainedScore;
        reserveAmmo = maxAmmunition;
    }
    void Update()
    {
        numberOfMagazines = reserveAmmo / magazineSize;
        if (reserveAmmo > maxAmmunition)
            calculateScore();
    }
}