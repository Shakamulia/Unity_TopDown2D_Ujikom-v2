using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public List<Mission> allMissions;
    public List<Mission> activeMissions;

    public void GiveMission(int missionID)
    {
        Mission mission = allMissions.Find(m => m.MissionID == missionID);
        if (mission != null && !activeMissions.Contains(mission))
        {
            activeMissions.Add(mission);
            Debug.Log("Mission given: " + mission.title);
        }
        else
        {
            Debug.Log("Mission already active or not found.");
        }
    }

    public void CompleteMission(int missionID)
    {
        Mission mission = activeMissions.Find(m => m.MissionID == missionID);
        if (mission != null)
        {
            mission.isCompleted = true;
            Debug.Log("Mission completed: " + mission.title);
        }
    }
}
