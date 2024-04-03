using System;
public class ScoreManager
{
    private static ScoreManager m_Instance;

    public static ScoreManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new ScoreManager();
            }

            return m_Instance;
        }
    }

    public event Action OnAddScore;

    public int Score { get; set; }
    public void AddScore()
    {
        Score++;
        if (OnAddScore != null)
        {
            OnAddScore();
        }
    }
}
