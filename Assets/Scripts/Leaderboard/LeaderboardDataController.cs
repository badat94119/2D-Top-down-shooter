using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LeaderboardDataController : MonoBehaviour
{
    [SerializeField]
    private int _numberOfLeaderboardEntries;
    
    private string _path;

    public IEnumerable<LeaderboardEntry> LeaderboardEntries { get; private set; }

    public static LeaderboardDataController Instance { get; private set; }

    public bool ScoreQualifiesForLeaderboard(int score)
    {
        if (LeaderboardEntries.Count() < _numberOfLeaderboardEntries || LeaderboardEntries.Last().Score < score)
        {
            return true;
        }

        return false;
    }

    public void AddLeaderboardEntry(LeaderboardEntry leaderboardEntry)
    {
        List<LeaderboardEntry> newLeaderboardEntries = LeaderboardEntries.ToList();
        newLeaderboardEntries.Add(leaderboardEntry);
        LeaderboardEntries = newLeaderboardEntries
            .OrderByDescending(x => x.Score)
            .Take(_numberOfLeaderboardEntries)
            .ToList();

        SaveLeaderboardEntries();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        _path = Path.Combine(Application.persistentDataPath, "Leaderboard.dat");
        LoadLeaderboardEntries();
    }

    private void LoadLeaderboardEntries()
    {
        if (File.Exists(_path))
        {
            IFormatter formatter = new BinaryFormatter();
            using Stream stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read);
            LeaderboardEntries = (IEnumerable<LeaderboardEntry>)formatter.Deserialize(stream);
        }
        else
        {
            LeaderboardEntries = new List<LeaderboardEntry>();
        }
    }

    private void SaveLeaderboardEntries()
    {
        IFormatter formatter = new BinaryFormatter();
        using Stream stream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, LeaderboardEntries);
    }
}
