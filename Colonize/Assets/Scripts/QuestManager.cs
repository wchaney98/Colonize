using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class QuestManager : SingletonBehavior<QuestManager>
{
    //Dictionary<string, string> quests = new Dictionary<string, string>()
    //{
    //    { "High Roller", "Gather 500 total Life points" },
    //    { "Taste of their own Medicine", "Leech 250 Life from Viruses" },
    //    { "Daily Fix", "Play 3 rounds of the game" },
    //    { "Jack of all Trades", "Produce 1 of each Node type" },
    //    { "Build a Wall", "Stop 10 Viruses with Prefect Nodes" }
    //};

    public List<Quest> CurrentQuests { get; private set; }
    public delegate void QuestCompletion(Quest q);
    public event QuestCompletion QuestCompletionListener;

    private Type lastCompletedQuest;
    private GameObject achievementFlarePrefab;

    private Dictionary<int, System.Type> _questTable = new Dictionary<int, Type>()
    {
        { 0, typeof(HighRoller) },
        { 1, typeof(TasteOfTheirOwnMedicine) },
        { 2, typeof(DailyFix) },
        { 3, typeof(MostWanted) },
        { 4, typeof(BuildAWall) }
    };

    protected override void Init()
    {
        CurrentQuests = new List<Quest>();
        QuestCompletionListener += OnQuestCompleteEvent;
        achievementFlarePrefab = Resources.Load<GameObject>("Prefabs/AchievementFlare");
        AddRandomQuest();
        AddRandomQuest();
        AddRandomQuest();
    }

    private void OnQuestCompleteEvent(Quest q)
    {
        // UI stuff
        GameObject flare = Instantiate(achievementFlarePrefab);
        flare.GetComponent<AchievementFlare>().Init("Quest Completed:\n" + q.Title);

        if (!CurrentQuests.Any(x => x.GetType() == typeof(HighRoller)))
        {
            Persistence.Instance.LifeGathered = 0;
        }
        if (!CurrentQuests.Any(x => x.GetType() == typeof(TasteOfTheirOwnMedicine)))
        {
            Persistence.Instance.LifeLeeched = 0;
        }
        if (!CurrentQuests.Any(x => x.GetType() == typeof(DailyFix)))
        {
            Persistence.Instance.RoundsPlayed = 0;
        }
        if (!CurrentQuests.Any(x => x.GetType() == typeof(MostWanted)))
        {
            Persistence.Instance.NodeSurvivedHundredSeconds = false;
        }
        if (!CurrentQuests.Any(x => x.GetType() == typeof(BuildAWall)))
        {
            Persistence.Instance.VirusesStopped = 0;
        }

        RemoveQuest(q.GetType());
        AddRandomQuest();
        lastCompletedQuest = q.GetType();
        Debug.Log("Quest completed: " + q.Title);
    }

    private void AddRandomQuest()
    {
        Quest newQuest = null;
        int roll = UnityEngine.Random.Range(0, 5);
        while (QuestAlreadyActive(_questTable[roll]) || _questTable[roll] == lastCompletedQuest)
        {
            roll = UnityEngine.Random.Range(0, 5);
        }

        newQuest = (Quest)Activator.CreateInstance(_questTable[roll]);
        CurrentQuests.Add(newQuest);
    }

    private bool RemoveQuest(Type questType)
    {
        foreach (Quest q in CurrentQuests)
        {
            if(q.GetType() == questType)
            {
                CurrentQuests.Remove(q);
                return true;
            }
        }
        return false;
    }

    private bool QuestAlreadyActive(Type questType)
    {
        foreach (Quest q in CurrentQuests)
        {
            if (q.GetType() == questType)
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        CurrentQuests.ForEach(x =>
        {
            if (x.CheckCompletion())
            {
                // broadcast quest complete
                QuestCompletionListener.Invoke(x);
            }
        });
    }

    public abstract class Quest
    {
        public string Title = "Unset";
        public string Description = "Unset";
        public int Reward = 10;
        public abstract string StatusString { get; }

        public abstract bool CheckCompletion();
    }

    class HighRoller : Quest
    {
        public override string StatusString
        {
            get
            {
                return Persistence.Instance.LifeGathered + "/500";
            }
        }

        public HighRoller()
        {
            Title = "High Roller";
            Description = "Gather 500 total Life points";
        }

        public override bool CheckCompletion()
        {
            // check persisitence for vlaues
            if (Persistence.Instance.LifeGathered >= 500)
            {
                Persistence.Instance.LifeGathered = 0;
                return true;
            }
            return false;
        }
    }

    class TasteOfTheirOwnMedicine : Quest
    {
        public override string StatusString
        {
            get
            {
                return Persistence.Instance.LifeLeeched + "/100";
            }
        }

        public TasteOfTheirOwnMedicine()
        {
            Title = "Taste of their own Medicine";
            Description = "Leech 250 Life from Viruses";
        }

        public override bool CheckCompletion()
        {
            if (Persistence.Instance.LifeLeeched >= 500)
            {
                Persistence.Instance.LifeLeeched = 0;
                return true;
            }
            return false;
        }
    }

    class DailyFix : Quest
    {
        public override string StatusString
        {
            get
            {
                return Persistence.Instance.RoundsPlayed + "/5";
            }
        }

        public DailyFix()
        {
            Title = "Daily Fix";
            Description = "Play 3 rounds of the game";
        }

        public override bool CheckCompletion()
        {
            if (Persistence.Instance.RoundsPlayed >= 5)
            {
                Persistence.Instance.RoundsPlayed = 0;
                return true;
            }
            return false;
        }
    }

    class MostWanted : Quest
    {
        public override string StatusString
        {
            get
            {
                return "0/1";
            }
        }

        public MostWanted()
        {
            Title = "Most Wanted";
            Description = "Keep 1 Node alive for 100 seconds in 1 game";
        }

        public override bool CheckCompletion()
        {
            if (Persistence.Instance.NodeSurvivedHundredSeconds)
            {
                Persistence.Instance.NodeSurvivedHundredSeconds = false;
                return true;
            }
            return false;
        }
    }

    class BuildAWall : Quest
    {
        public override string StatusString
        {
            get
            {
                return Persistence.Instance.VirusesStopped + "/10";
            }
        }

        public BuildAWall()
        {
            Title = "Build a Wall";
            Description = "Stop 10 Viruses with Prefect Nodes";
        }

        public override bool CheckCompletion()
        {
            if (Persistence.Instance.VirusesStopped >= 10)
            {
                Persistence.Instance.VirusesStopped = 0;
                return true;
            }
            return false;
        }
    }

}
