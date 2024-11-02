using System;
using System.Collections.Generic;
using System.Linq;


[Serializable]
public class ClearTimeRecord
{
    public string playerName;
    public string clearTime;
    public ClearTimeRecord(string playerName, TimeSpan clearTime)
    {
        this.playerName = playerName;
        this.clearTime = clearTime.ToString();
    }
}

[Serializable]
public class ClearSpeedRanking
{ 
    public List<ClearTimeRecord> ranking;
    public ClearSpeedRanking()
    {
        ranking = new List<ClearTimeRecord>();
    }
    public void Add(ClearTimeRecord newRecord)
    {
        ranking.Add(newRecord);
        ranking = ranking
            .OrderBy(record => TimeSpan.Parse(record.clearTime))
            .Take(3)
            .ToList();
    }
}