using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.Match;

public class AutoMatchMaker : MonoBehaviour {
    
	public void StartAutoMatch()
    {
        NetworkManager.singleton.StartMatchMaker();
        NetworkManager.singleton.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        if (success)
        {
            if (matchList.Count <= 0)
            {
                CreateRandomMatch();
                return;
            }
            else
            {
                MatchInfoSnapshot matchInfoSnapshot = matchList[matchList.Count-1];
                JoinMatch(matchInfoSnapshot);
            }

            foreach(MatchInfoSnapshot matchInfoSnapshot in matchList)
            {
                Debug.Log(matchInfoSnapshot.name);
            }
        }
        else
        {
            Debug.Log(extendedInfo);
        }
    }

    public void CreateRandomMatch()
    {
        Guid uniqueId = Guid.NewGuid();
        string matchName = "";// uniqueId.ToString();
        uint matchSize = 2;
        bool matchAdvertise = true;
        string matchPassword = "";
        string publicClientAddress = "";
        string privateClientAddress = "";
        int eloScoreForMatch = 0;
        int requestDomain = 0;

        NetworkManager.singleton.matchMaker.CreateMatch
            (matchName, 
            matchSize, 
            matchAdvertise,
            matchPassword, 
            publicClientAddress, 
            privateClientAddress, 
            eloScoreForMatch, 
            requestDomain,
            OnRandomMatchCreated);
    }

    private void OnRandomMatchCreated(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            NetworkServer.Listen(matchInfo, 7777);
            NetworkManager.singleton.StartHost(matchInfo);
        }
        else
        {
            Debug.Log(extendedInfo);
        }
    }

    private void JoinMatch(MatchInfoSnapshot matchInfoSnapshot)
    {
        string matchPassword = "";
        string publicClientAddress = "";
        string privateClientAddress = "";
        int eloScoreForClient = 0;
        int requestDomain = 0;
        NetworkManager.singleton.matchMaker.JoinMatch
            (matchInfoSnapshot.networkId,
            matchPassword,
            publicClientAddress,
            privateClientAddress,
            eloScoreForClient,
            requestDomain,
            OnMatchJoined);
    }

    private void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            NetworkManager.singleton.StartClient(matchInfo);
        }
        else
        {
            Debug.Log(extendedInfo);
        }
    }

}
