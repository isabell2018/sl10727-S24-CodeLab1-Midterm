using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI levelText;
    
    public class StringIntPair
    {
        public string stringValue;
        public int intValue;

        public StringIntPair(string stringValue, int intValue)
        {
            this.stringValue = stringValue;
            this.intValue = intValue;
        }
    }
    private string playerName = "Anoymous";
    private List<StringIntPair> highScores;
    public string highScoresString = "";
    public TMP_InputField playerNameInputField;
    
    
    public int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            if (isHighScore(score)&&level==6)
            {
                int highScoreSlot = 0;
                for (int i = 0; i < HighScores.Count; i++)
                {
                    if (score > highScores[i].intValue)
                    {
                        highScoreSlot = i;
                        break;
                    }

                }

                StringIntPair newPair =
                    new StringIntPair(playerName, score);
                highScores.Insert(highScoreSlot, newPair);
                highScores = highScores.GetRange(0, 5);

                string scoreBoardText = "";
                foreach (var highScore in highScores)
                {
                    scoreBoardText += highScore.stringValue + "," + highScore.intValue + "\n";
                }

                highScoresString = scoreBoardText;
                File.WriteAllText(FILE_FULL_PATH, highScoresString);
            }
        }
    }
        
    const string DATA_DIR = "/Data/";
    const string DATA_HS_FILE = "highscore.txt";
    string FILE_FULL_PATH;
    
    public List<StringIntPair> HighScores
    {
        get
        {
            if (highScores == null)
            {
                highScores = new List<StringIntPair>();
                highScoresString = File.ReadAllText(FILE_FULL_PATH);
                highScoresString = highScoresString.Trim();//returns only unique value
                string[] highScoreArray = highScoresString.Split("\n");
                //add scores to array
                foreach (string scoreEntry in highScoreArray)
                {
                    string[] parts = scoreEntry.Split(",");
                    if (parts.Length == 2)
                    {
                        string stringValue = parts[0];
                        int intValue = 0;
                        if (int.TryParse(parts[1], out intValue))
                        {
                            highScores.Add(new StringIntPair(stringValue, intValue));
                        }
                    }
                }
            }
            return highScores;
        }

        set
        {
            //If no directory named string DATA_DIR's value exists, create one
            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }
        }
    }
    
    public int level = 0;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Level++;
        //name data file path after path+dir+name
        FILE_FULL_PATH = Application.dataPath + DATA_DIR + DATA_HS_FILE;

    }

    private int check = 0;
    // Update is called once per frame
    void Update()
    {
        playerName = playerNameInputField.text;
        Score+=0;
        
        if (level < 6)
        {
            levelText.text = "Level: " + Level + "\nScore: " + Score;
        }
        else
        {
            levelText.text = "GameOver! \nYour Score: " + Score + "\nHigh Scores: \n" + highScoresString;
            if(check==0){level++;}
            check = 1;
        }


    }
    public bool isHighScore(int score)
    {
        for (int i = 0; i < HighScores.Count; i++)
        {
            if (highScores[i].intValue < score)
            {
                return true;
            }
        }

        return false;
    }
}
