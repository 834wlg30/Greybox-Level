using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region GMSingleton
    static GameManager gm;
    public static GameManager GM { get { return gm; } }

    void GameManagerInScene()
    {
        if (gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    #endregion

    public TMP_Text deathText = null;
    public GameObject player;
    public Health plrHP;
    public Image infoPanel;
    private static int ded = 2;
    private static int win = 3;
    public bool playerDetected = false;
    public int infoTime = 0;

    private void Awake()
    {
        GameManagerInScene();
        plrHP = player.GetComponent<Health>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (plrHP.HP >= 100f) { gameOver();  }
    }

    private void FixedUpdate()
    {
        if (playerDetected) { plrHP.HP += 1f; }
        else if (plrHP.HP > 0) { plrHP.HP -= 0.5f; }

        if (infoTime < 500)
        {
            infoTime++;
        }
        else
        {
            infoPanel.gameObject.SetActive(false);
            infoPanel = null;
        }
    }

    public void gameOver()
    {
        SceneManager.LoadScene(ded);
    }

    public void victory()
    {
        SceneManager.LoadScene(win);
    }
}
