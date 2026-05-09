using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    [Header("Objective")]
    public string levelName = "Sector 1";
    [TextArea(2, 4)]
    public string levelObjective = "Defeat enemy waves and collect power-ups to survive.";
    public int objectiveKills = 12;
    public int maxHull = 5;
    public string nextSceneName = "";
    public float threatPerEnemyDefeated = 0.08f;
    public float threatPerSecond = 0.012f;
    public float maxThreatBonus = 1.75f;

    [Header("HUD")]
    public Text scoreText;
    public Text highScoreText;
    public Text hullText;
    public Text objectiveText;
    public Text timerText;
    public Text powerText;
    public Text levelText;
    public Text difficultyText;
    public Text hintText;
    public Text messageText;
    public Image hullFillImage;
    public Image objectiveFillImage;
    public Image dangerFlashImage;

    [Header("Overlay")]
    public GameObject overlayRoot;
    public Text overlayTitleText;
    public Text overlayBodyText;
    public Text overlayPrimaryButtonText;
    public Button overlayPrimaryButton;
    public Button overlayMenuButton;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip powerUpClip;
    public AudioClip playerHitClip;
    public AudioClip victoryClip;
    public AudioClip gameOverClip;
    public AudioClip clickClip;

    private int score;
    private int highScore;
    private int hull;
    private int enemiesDefeated;
    private float runTimer;
    private float rapidFireTimer;
    private float shieldTimer;
    private float scoreBoostTimer;
    private float messageTimer;
    private float dangerFlashTimer;
    private bool paused;
    private bool ended;

    public bool CanPlay
    {
        get { return !paused && !ended; }
    }

    public float FireRateMultiplier
    {
        get { return rapidFireTimer > 0f ? 0.45f : 1f; }
    }

    public bool ShieldActive
    {
        get { return shieldTimer > 0f; }
    }

    public int ScoreMultiplier
    {
        get { return scoreBoostTimer > 0f ? 2 : 1; }
    }

    public int EnemiesDefeated
    {
        get { return enemiesDefeated; }
    }

    public int ObjectiveKills
    {
        get { return objectiveKills; }
    }

    public float DifficultyMultiplier
    {
        get { return 1f + Mathf.Min(maxThreatBonus, enemiesDefeated * threatPerEnemyDefeated + runTimer * threatPerSecond); }
    }

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
        highScore = PlayerPrefs.GetInt("space_shooter_training_highscore", 0);
        hull = maxHull;

        if (overlayRoot != null)
        {
            overlayRoot.SetActive(false);
        }

        UpdateHud();
        ShowMessage(levelName + ": " + levelObjective, 7f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !ended)
        {
            TogglePause();
        }

        if (!ended && !paused)
        {
            runTimer += Time.deltaTime;
            rapidFireTimer = Mathf.Max(0f, rapidFireTimer - Time.deltaTime);
            shieldTimer = Mathf.Max(0f, shieldTimer - Time.deltaTime);
            scoreBoostTimer = Mathf.Max(0f, scoreBoostTimer - Time.deltaTime);
            dangerFlashTimer = Mathf.Max(0f, dangerFlashTimer - Time.deltaTime);
        }

        if (messageTimer > 0f)
        {
            messageTimer -= Time.unscaledDeltaTime;
            if (messageTimer <= 0f && messageText != null)
            {
                messageText.text = string.Empty;
            }
        }

        UpdateHud();
    }

    public void AddScore(int amount)
    {
        if (ended)
        {
            return;
        }

        score += amount * ScoreMultiplier;
        SaveHighScoreIfNeeded();
    }

    public void RegisterEnemyDefeated(int baseScore, Vector3 worldPosition)
    {
        enemiesDefeated += 1;
        AddScore(baseScore);
        ShowMessage("Enemy down +" + (baseScore * ScoreMultiplier), 1.25f);
        CameraShake.Shake(0.08f, 0.08f);

        if (enemiesDefeated >= objectiveKills)
        {
            WinGame();
        }
    }

    public void RegisterHazardDestroyed(int baseScore)
    {
        AddScore(baseScore);
        ShowMessage("Asteroid cleared +" + (baseScore * ScoreMultiplier), 1.1f);
        CameraShake.Shake(0.08f, 0.05f);
    }

    public void DamagePlayer(int damage)
    {
        if (ended)
        {
            return;
        }

        if (ShieldActive)
        {
            shieldTimer = Mathf.Max(1f, shieldTimer - 1.2f);
            ShowMessage("Shield absorbed the hit", 1.2f);
            return;
        }

        hull = Mathf.Max(0, hull - damage);
        dangerFlashTimer = 0.45f;
        PlayClip(playerHitClip);
        CameraShake.Shake(0.18f, 0.2f);
        ShowMessage("Hull damaged", 1.2f);

        if (hull <= 0)
        {
            LoseGame();
        }
    }

    public void ApplyPowerUp(PowerUpKind kind)
    {
        PlayClip(powerUpClip);

        switch (kind)
        {
            case PowerUpKind.RapidFire:
                rapidFireTimer = 7f;
                ShowMessage("Rapid fire online", 2f);
                break;
            case PowerUpKind.Repair:
                hull = Mathf.Min(maxHull, hull + 2);
                ShowMessage("Hull repaired", 2f);
                break;
            case PowerUpKind.Shield:
                shieldTimer = 6f;
                ShowMessage("Shield active", 2f);
                break;
            case PowerUpKind.ScoreBoost:
                scoreBoostTimer = 8f;
                ShowMessage("Score multiplier x2", 2f);
                break;
        }

        UpdateHud();
    }

    public void Announce(string text, float duration)
    {
        ShowMessage(text, duration);
    }

    public void TogglePause()
    {
        if (paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (ended)
        {
            return;
        }

        paused = true;
        Time.timeScale = 0f;
        ConfigureOverlay(
            "Paused",
            levelName + "\n\n" + levelObjective + "\n\nWASD / Arrow Keys move. Mouse aims. Mouse 1 or Space fires. Esc resumes. Watch the hull bar, objective bar, and active power-up timer.",
            "Resume",
            ResumeGame);
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1f;
        if (overlayRoot != null)
        {
            overlayRoot.SetActive(false);
        }
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            RetryLevel();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    public void PlayClick()
    {
        PlayClip(clickClip);
    }

    private void WinGame()
    {
        ended = true;
        SaveHighScoreIfNeeded();
        PlayClip(victoryClip);
        Time.timeScale = 0f;
        string primaryLabel = string.IsNullOrEmpty(nextSceneName) ? "Play Again" : "Next Sector";
        UnityEngine.Events.UnityAction primaryAction = string.IsNullOrEmpty(nextSceneName) ? RetryLevel : LoadNextLevel;
        ConfigureOverlay(
            "Victory",
            "You cleared " + levelName + " in " + FormatTime(runTimer) + " with " + score + " points. Strong use of power-ups and target priority keeps the run under control.",
            primaryLabel,
            primaryAction);
    }

    private void LoseGame()
    {
        ended = true;
        SaveHighScoreIfNeeded();
        PlayClip(gameOverClip);
        Time.timeScale = 0f;
        ConfigureOverlay(
            "Game Over",
            "Your ship was destroyed after defeating " + enemiesDefeated + " enemies in " + levelName + ". Try collecting shields and repairs before the spawn rate climbs.",
            "Retry",
            RetryLevel);
    }

    private void ConfigureOverlay(string title, string body, string primaryLabel, UnityEngine.Events.UnityAction primaryAction)
    {
        if (overlayRoot != null)
        {
            overlayRoot.SetActive(true);
        }
        if (overlayTitleText != null)
        {
            overlayTitleText.text = title;
        }
        if (overlayBodyText != null)
        {
            overlayBodyText.text = body;
        }
        if (overlayPrimaryButtonText != null)
        {
            overlayPrimaryButtonText.text = primaryLabel;
        }
        if (overlayPrimaryButton != null)
        {
            overlayPrimaryButton.onClick.RemoveAllListeners();
            overlayPrimaryButton.onClick.AddListener(PlayClick);
            overlayPrimaryButton.onClick.AddListener(primaryAction);
        }
        if (overlayMenuButton != null)
        {
            overlayMenuButton.onClick.RemoveAllListeners();
            overlayMenuButton.onClick.AddListener(PlayClick);
            overlayMenuButton.onClick.AddListener(LoadMainMenu);
        }
    }

    private void SaveHighScoreIfNeeded()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("space_shooter_training_highscore", highScore);
        }
    }

    private void ShowMessage(string text, float duration)
    {
        if (messageText != null)
        {
            messageText.text = text;
            messageTimer = duration;
        }
    }

    private void UpdateHud()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        if (highScoreText != null)
        {
            highScoreText.text = "High: " + highScore;
        }
        if (hullText != null)
        {
            hullText.text = ShieldActive ? "Hull: " + hull + "/" + maxHull + "  Shield" : "Hull: " + hull + "/" + maxHull;
        }
        if (objectiveText != null)
        {
            objectiveText.text = "Objective: " + enemiesDefeated + "/" + objectiveKills + " enemies";
        }
        if (levelText != null)
        {
            levelText.text = levelName;
        }
        if (difficultyText != null)
        {
            difficultyText.text = "Threat: " + DifficultyMultiplier.ToString("0.0") + "x";
        }
        if (timerText != null)
        {
            timerText.text = "Time: " + FormatTime(runTimer);
        }
        if (powerText != null)
        {
            powerText.text = GetPowerText();
        }
        if (hintText != null)
        {
            hintText.text = "WASD / Arrows: move   Mouse: aim   Left Click / Space: shoot   Collect colored power-ups";
        }
        if (hullFillImage != null)
        {
            hullFillImage.fillAmount = maxHull <= 0 ? 0f : (float)hull / maxHull;
            hullFillImage.color = ShieldActive ? new Color(1f, 0.84f, 0.25f, 1f) : Color.Lerp(new Color(1f, 0.2f, 0.22f, 1f), new Color(0.25f, 1f, 0.45f, 1f), hullFillImage.fillAmount);
        }
        if (objectiveFillImage != null)
        {
            objectiveFillImage.fillAmount = objectiveKills <= 0 ? 0f : Mathf.Clamp01((float)enemiesDefeated / objectiveKills);
        }
        if (dangerFlashImage != null)
        {
            float lowHullPulse = hull <= 2 && !ended ? (Mathf.Sin(Time.unscaledTime * 8f) + 1f) * 0.07f : 0f;
            float hitFlash = dangerFlashTimer > 0f ? dangerFlashTimer : 0f;
            Color flashColor = dangerFlashImage.color;
            flashColor.a = Mathf.Clamp01(lowHullPulse + hitFlash);
            dangerFlashImage.color = flashColor;
        }
    }

    private string GetPowerText()
    {
        string active = string.Empty;
        if (rapidFireTimer > 0f)
        {
            active += "Rapid " + Mathf.CeilToInt(rapidFireTimer) + "s  ";
        }
        if (shieldTimer > 0f)
        {
            active += "Shield " + Mathf.CeilToInt(shieldTimer) + "s  ";
        }
        if (scoreBoostTimer > 0f)
        {
            active += "x2 " + Mathf.CeilToInt(scoreBoostTimer) + "s";
        }

        return string.IsNullOrEmpty(active) ? "Power: none" : "Power: " + active.Trim();
    }

    private string FormatTime(float seconds)
    {
        int wholeSeconds = Mathf.FloorToInt(seconds);
        int minutes = wholeSeconds / 60;
        int remainingSeconds = wholeSeconds % 60;
        return minutes.ToString("00") + ":" + remainingSeconds.ToString("00");
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
