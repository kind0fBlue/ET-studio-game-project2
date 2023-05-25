using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private static UI instance;

    // Get instance
    public static UI getInstance(){
        return instance;
    }

    private void Awake(){
        instance = this;
    }

    
    // Weapon Icon
    public Image weaponIcon;
    
    // Bullet number
    public Text bullet_num;

    // HP value
    public Text HP_value;

    // Score value
    public Text score_value;

    public int score = 0;

    // Update weapon icon and bullet number
    public void updateWeaponUI(Sprite icon, int bulletNum){
        weaponIcon.sprite = icon;
        bullet_num.text = bulletNum.ToString();
    }

    // Update HP value
    public void updateHpUI(int hp_num){
        HP_value.text = hp_num.ToString();
    }

    // Update score value
    public void updateScoreUI(){
        score++;
        score_value.text = score.ToString();
    }

    public int getScore() {
        return score;
    }
}
