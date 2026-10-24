using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prime31
{
	public class GPGManager : AbstractManager
	{
		public static event Action<string> authenticationSucceededEvent;

		public static event Action<string> authenticationFailedEvent;

		public static event Action userSignedOutEvent;

		public static event Action<string> reloadDataForKeyFailedEvent;

		public static event Action<string> reloadDataForKeySucceededEvent;

		public static event Action licenseCheckFailedEvent;

		public static event Action<string> profileImageLoadedAtPathEvent;

		public static event Action<string> finishedSharingEvent;

		public static event Action<GPGPlayerInfo, string> loadPlayerCompletedEvent;

		public static event Action<string, string> unlockAchievementFailedEvent;

		public static event Action<string, bool> unlockAchievementSucceededEvent;

		public static event Action<string, string> incrementAchievementFailedEvent;

		public static event Action<string, bool> incrementAchievementSucceededEvent;

		public static event Action<string, string> revealAchievementFailedEvent;

		public static event Action<string> revealAchievementSucceededEvent;

		public static event Action<string, string> submitScoreFailedEvent;

		public static event Action<string, Dictionary<string, object>> submitScoreSucceededEvent;

		public static event Action<string, string> loadScoresFailedEvent;

		public static event Action<List<GPGScore>> loadScoresSucceededEvent;

		public static event Action<GPGScore> loadCurrentPlayerLeaderboardScoreSucceededEvent;

		public static event Action<string, string> loadCurrentPlayerLeaderboardScoreFailedEvent;

		public static event Action<List<GPGEvent>> allEventsLoadedEvent;

		public static event Action<GPGQuest> questListLauncherAcceptedQuestEvent;

		public static event Action<GPGQuestMilestone> questClaimedRewardsForQuestMilestoneEvent;

		public static event Action<GPGQuest> questCompletedEvent;

		public static event Action<List<GPGQuest>> allQuestsLoadedEvent;

		public static event Action<GPGSnapshotMetadata> snapshotListUserSelectedSnapshotEvent;

		public static event Action snapshotListUserRequestedNewSnapshotEvent;

		public static event Action snapshotListCanceledEvent;

		public static event Action saveSnapshotSucceededEvent;

		public static event Action<string> saveSnapshotFailedEvent;

		public static event Action<GPGSnapshot> loadSnapshotSucceededEvent;

		public static event Action<string> loadSnapshotFailedEvent;

		static GPGManager()
		{
			AbstractManager.initialize(typeof(GPGManager));
		}

		private void fireEventWithIdentifierAndError(Action<string, string> theEvent, string json)
		{
			if (theEvent != null)
			{
				Dictionary<string, object> dictionary = json.dictionaryFromJson();
				if (dictionary != null && dictionary.ContainsKey("identifier") && dictionary.ContainsKey("error"))
				{
					theEvent(dictionary["identifier"].ToString(), dictionary["error"].ToString());
				}
				else
				{
					Debug.LogError("json could not be deserialized to an identifier and an error: " + json);
				}
			}
		}

		private void fireEventWithIdentifierAndBool(Action<string, bool> theEvent, string param)
		{
			if (theEvent != null)
			{
				string[] array = param.Split(',');
				if (array.Length == 2)
				{
					theEvent(array[0], array[1] == "1");
				}
				else
				{
					Debug.LogError("param could not be deserialized to an identifier and an error: " + param);
				}
			}
		}

		private void userSignedOut(string empty)
		{
			GPGManager.userSignedOutEvent.fire();
		}

		private void reloadDataForKeyFailed(string error)
		{
			GPGManager.reloadDataForKeyFailedEvent.fire(error);
		}

		private void reloadDataForKeySucceeded(string param)
		{
			GPGManager.reloadDataForKeySucceededEvent.fire(param);
		}

		private void licenseCheckFailed(string param)
		{
			GPGManager.licenseCheckFailedEvent.fire();
		}

		private void profileImageLoadedAtPath(string path)
		{
			GPGManager.profileImageLoadedAtPathEvent.fire(path);
		}

		private void finishedSharing(string errorOrNull)
		{
			GPGManager.finishedSharingEvent.fire(errorOrNull);
		}

		private void loadPlayerCompleted(string playerOrError)
		{
			if (GPGManager.loadPlayerCompletedEvent != null)
			{
				if (playerOrError.StartsWith("{"))
				{
					GPGManager.loadPlayerCompletedEvent(Json.decode<GPGPlayerInfo>(playerOrError), null);
				}
				else
				{
					GPGManager.loadPlayerCompletedEvent(null, playerOrError);
				}
			}
		}

		private void unlockAchievementFailed(string json)
		{
			fireEventWithIdentifierAndError(GPGManager.unlockAchievementFailedEvent, json);
		}

		private void unlockAchievementSucceeded(string param)
		{
			fireEventWithIdentifierAndBool(GPGManager.unlockAchievementSucceededEvent, param);
		}

		private void incrementAchievementFailed(string json)
		{
			fireEventWithIdentifierAndError(GPGManager.incrementAchievementFailedEvent, json);
		}

		private void incrementAchievementSucceeded(string param)
		{
			string[] array = param.Split(',');
			if (array.Length == 2)
			{
				GPGManager.incrementAchievementSucceededEvent.fire(array[0], array[1] == "1");
			}
		}

		private void revealAchievementFailed(string json)
		{
			fireEventWithIdentifierAndError(GPGManager.revealAchievementFailedEvent, json);
		}

		private void revealAchievementSucceeded(string achievementId)
		{
			GPGManager.revealAchievementSucceededEvent.fire(achievementId);
		}

		private void submitScoreFailed(string json)
		{
			fireEventWithIdentifierAndError(GPGManager.submitScoreFailedEvent, json);
		}

		private void submitScoreSucceeded(string json)
		{
			if (GPGManager.submitScoreSucceededEvent != null)
			{
				Dictionary<string, object> dictionary = json.dictionaryFromJson();
				string arg = "Unknown";
				if (dictionary.ContainsKey("leaderboardId"))
				{
					arg = dictionary["leaderboardId"].ToString();
				}
				GPGManager.submitScoreSucceededEvent(arg, dictionary);
			}
		}

		private void loadScoresFailed(string json)
		{
			fireEventWithIdentifierAndError(GPGManager.loadScoresFailedEvent, json);
		}

		private void loadScoresSucceeded(string json)
		{
			if (GPGManager.loadScoresSucceededEvent != null)
			{
				GPGManager.loadScoresSucceededEvent(Json.decode<List<GPGScore>>(json));
			}
		}

		private void loadCurrentPlayerLeaderboardScoreSucceeded(string json)
		{
			if (GPGManager.loadCurrentPlayerLeaderboardScoreSucceededEvent != null)
			{
				GPGManager.loadCurrentPlayerLeaderboardScoreSucceededEvent(Json.decode<GPGScore>(json));
			}
		}

		private void loadCurrentPlayerLeaderboardScoreFailed(string json)
		{
			fireEventWithIdentifierAndError(GPGManager.loadCurrentPlayerLeaderboardScoreFailedEvent, json);
		}

		private void authenticationSucceeded(string param)
		{
			GPGManager.authenticationSucceededEvent.fire(param);
		}

		private void authenticationFailed(string error)
		{
			GPGManager.authenticationFailedEvent.fire(error);
		}

		private void allEventsLoaded(string json)
		{
			if (GPGManager.allEventsLoadedEvent != null)
			{
				GPGManager.allEventsLoadedEvent(Json.decode<List<GPGEvent>>(json));
			}
		}

		private void questListLauncherClaimedRewardsForQuestMilestone(string json)
		{
			if (GPGManager.questClaimedRewardsForQuestMilestoneEvent != null)
			{
				GPGManager.questClaimedRewardsForQuestMilestoneEvent(Json.decode<GPGQuestMilestone>(json));
			}
		}

		private void questCompleted(string json)
		{
			if (GPGManager.questCompletedEvent != null)
			{
				GPGManager.questCompletedEvent(Json.decode<GPGQuest>(json));
			}
		}

		private void questListLauncherAcceptedQuest(string json)
		{
			if (GPGManager.questListLauncherAcceptedQuestEvent != null)
			{
				GPGManager.questListLauncherAcceptedQuestEvent(Json.decode<GPGQuest>(json));
			}
		}

		private void allQuestsLoaded(string json)
		{
			if (GPGManager.allQuestsLoadedEvent != null)
			{
				GPGManager.allQuestsLoadedEvent(Json.decode<List<GPGQuest>>(json));
			}
		}

		private void snapshotListUserSelectedSnapshot(string json)
		{
			if (GPGManager.snapshotListUserSelectedSnapshotEvent != null)
			{
				GPGManager.snapshotListUserSelectedSnapshotEvent(Json.decode<GPGSnapshotMetadata>(json));
			}
		}

		private void snapshotListUserRequestedNewSnapshot(string empty)
		{
			GPGManager.snapshotListUserRequestedNewSnapshotEvent.fire();
		}

		private void snapshotListCanceled(string empty)
		{
			GPGManager.snapshotListCanceledEvent.fire();
		}

		private void saveSnapshotSucceeded(string empty)
		{
			GPGManager.saveSnapshotSucceededEvent.fire();
		}

		private void saveSnapshotFailed(string error)
		{
			GPGManager.saveSnapshotFailedEvent.fire(error);
		}

		private void loadSnapshotSucceeded(string json)
		{
			if (GPGManager.loadSnapshotSucceededEvent != null)
			{
				GPGManager.loadSnapshotSucceededEvent(Json.decode<GPGSnapshot>(json));
			}
		}

		private void loadSnapshotFailed(string error)
		{
			GPGManager.loadSnapshotFailedEvent.fire(error);
		}
	}
}
